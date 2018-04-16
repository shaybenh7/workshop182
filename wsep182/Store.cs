using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class Store
    {
        private int storeId;
        private int isActive; //delete next version
        String name;
        User storeCreator;
        public Store(int id, String name, User storeCreator)
        {
            storeId = id;
            this.isActive = 1;
            this.name = name;
            this.storeCreator = storeCreator;
        }
        public int getStoreId()
        {
            return storeId;
        }
        public User getStoreCreator()
        {
            return storeCreator;
        }
        public String getStoreName()
        {
            return name;
        }
        public LinkedList<ProductInStore> getProductsInStore()
        {
            return ProductArchive.getInstance().getProductsInStore(storeId);
        }

        public static Store createStore(String name,User session)
        {
            if (session.getState() is LogedIn && name !="")
                return storeArchive.getInstance().addStore(name,session);
            return null;
        }

        public LinkedList<User> getOwners()
        {
            LinkedList<User> users = new LinkedList<User>();
            LinkedList<String> userNames = storeArchive.getInstance().getAllOwners(storeId);
            foreach (String userName in userNames)
            {
                users.AddLast(UserArchive.getInstance().getUser(userName));
            }
            return users;
        }


        public LinkedList<User> getManagers(User session, int storeId)
        {
            LinkedList<User> users = new LinkedList<User>();
            LinkedList<String> userNames = storeArchive.getInstance().getAllManagers(storeId);
            foreach (String userName in userNames)
            {
                users.AddLast(UserArchive.getInstance().getUser(userName));
            }
            return users;
        }

    }
}