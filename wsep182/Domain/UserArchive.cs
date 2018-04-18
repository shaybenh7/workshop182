﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            users.AddLast(newUser);
            return true;
        }

        public Boolean updateUser(User newUser)
        {
            foreach (User u in users)
            {
                if (u.getUserName().Equals(newUser.getUserName()))
                {
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
