using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wsep182.Domain;

namespace wsep182.services
{
    public class userServices
    {
        private static userServices instance = null;

        private userServices() {

        }
        public static userServices getInstance()
        {
            if (instance == null)
            {
                instance = new userServices();
            }
            return instance;
        }

        public User startSession()
        {
            User user = new User("guest", "guest");
            return user;
        }

        // req 1.2- returns null if failed, else returns the user
        public Boolean register(User session, String username, String password)
        {
            return session.register(username, password);
        }
        //req 2.1- returns null if user doesnt exists or password is wrong
        public Boolean login(User session, String userName, String password)
        {
            return session.login(userName, password);
        }
        //req 1.3
        public LinkedList<ProductInStore> viewProductsInStore(Store s)
        {
            return s.getProductsInStore();
        }

        //req 1.3
        public LinkedList<ProductInStore> viewProductsInStores()
        {
            return ProductInStore.getAllProductsInAllStores();
       }
        //req 1.3 - TO BE ADDED, WAITING FOR NIV THE RUSSIAN IMPLEMENTATION
        public LinkedList<Store> viewStores()
        {
            return Store.viewStores();
        }
        // req 5.2
        public Boolean removeUser(User userMakingDeletion, User userDeleted)
        {
            return userMakingDeletion.removeUser(userMakingDeletion, userDeleted.getUserName());
        }


    }
}