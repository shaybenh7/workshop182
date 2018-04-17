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

        // req 1.1
        public User startSession()
        {
            User user = new User("guest", "guest");
            return user;
        }

        // req 1.2
        public Boolean register(User session, String username, String password)
        {
            return session.register(username, password);
        }

        //req 1.3 a
        public LinkedList<ProductInStore> viewProductsInStore(Store s)
        {
            return s.getProductsInStore();
        }

        //req 1.3 b
        public LinkedList<ProductInStore> viewProductsInStores()
        {
            return ProductInStore.getAllProductsInAllStores();
        }

        //req 1.3 c
        public LinkedList<Store> viewStores()
        {
            return Store.viewStores();
        }

        //req 2.1 
        public Boolean login(User session, String userName, String password)
        {
            return session.login(userName, password);
        }


        // req 5.2
        public Boolean removeUser(User userMakingDeletion, User userDeleted)
        {
            return userMakingDeletion.removeUser(userMakingDeletion, userDeleted.getUserName());
        }


    }
}