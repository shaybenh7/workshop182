using System;
using System.Collections.Generic;
using System.Security.Cryptography;


namespace wsep182.Domain
{
    public class UserArchive
    {
        private static UserArchive instance;
        private LinkedList<User> users;
        private UserArchive()
        {
            users = new LinkedList<User>();
        }
        public static UserArchive getInstance()
        {
            if (instance == null)
                instance = new UserArchive();
            return instance;
        }

        public static void restartInstance()
        {
            instance = new UserArchive();
        }
        public Boolean addUser(User newUser)
        {
            foreach (User u in users)
                if (u.getUserName().Equals(newUser.getUserName()))
                    return false;
            newUser.setPassword(encrypt(newUser.getUserName() + newUser.getPassword()));
            users.AddLast(newUser);
            return true;
        }

        private String encrypt(String password)
        {
            byte[] pwd;
            using (SHA512 shaM = new SHA512Managed())
            {
                pwd = System.Text.Encoding.UTF8.GetBytes(password);
                pwd = shaM.ComputeHash(pwd);
            }
            return System.Text.Encoding.UTF8.GetString(pwd);
        }

        public Boolean updateUser(User newUser)
        {
            foreach (User u in users)
            {
                if (u.getUserName().Equals(newUser.getUserName()))
                {
                    newUser.setPassword(encrypt(newUser.getUserName() + newUser.getPassword()));
                    users.Remove(u);
                    users.AddLast(newUser);
                    return true;
                }
            }
            return false;
        }
        public User getUser(string userName)
        {
            foreach (User u in users)
                if (u.getUserName().Equals(userName))
                    return u;
            return null;
        }

        public Boolean removeUser(string userName)
        {
            foreach (User u in users)
                if (u.getUserName().Equals(userName))
                {
                    LinkedList<Store> allStores = storeArchive.getInstance().getAllStore();
                    foreach(Store s in allStores)
                    {
                        if(s.getStoreCreator().getUserName().Equals(u.getUserName()) && s.getIsActive() == 1)
                            return false;
                    }
                    //users.Remove(u);
                    u.setIsActive(false);
                    return true;
                }
            return false;
        }

    }
}
