using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wsep182.Domain;

namespace wsep182.services
{
    public class storeServices
    {
        private static storeServices instance = null;
        private storeServices() { }
        public static storeServices getInstance()
        {
            if (instance == null)
            {
                instance = new storeServices();
            }
            return instance;
        }
        //req 2.2- returns new store if user is logged and storeName is not empty, else returns null
        public Store createStore(String storeName, User session)
        {
            if (storeName == null || session == null)
                return null;
            return session.getState().createStore(storeName, session);
        }



        public LinkedList<Product> getAllProducts(User session)
        {
            return Product.getProducts();
        }
        //req 3.1
        public ProductInStore addProductInStore(Product p, Double price, int amount, User session, Store s)
        {
            if (p == null || session == null || s == null || amount == 0)
                return null;
            if (!session.getState().isLogedIn())
                return null;
            StoreRole sR = storeArchive.getInstance().getStoreRole(s.getStoreId(), session.getUserName());
            if (sR == null)
                return null;
            return sR.addProductInStore(session, s, p, price, amount);
        }
        //req 3.2
        public Boolean removeProductFromStore(Store s, ProductInStore p, User session)
        {
            if (p == null || session == null || s == null)
                return false;
            StoreRole sR = storeArchive.getInstance().getStoreRole(s.getStoreId(), session.getUserName());
            if (sR == null)
                return false;
            return sR.removeProductFromStore(session, s, p);
        }
        public Boolean addStoreManager(Store s, User newManager, User session)
        {
            if (newManager == null || session == null || s == null)
                return false;
            StoreRole sR = storeArchive.getInstance().getStoreRole(s.getStoreId(), session.getUserName());
            if (sR == null)
                return false;
            return sR.addStoreManager(session, s, newManager);
        }
        public Boolean removeStoreManager(Store s, User oldManager, User session)
        {
            if (oldManager == null || session == null || s == null)
                return false;
            StoreRole sR = storeArchive.getInstance().getStoreRole(s.getStoreId(), session.getUserName());
            if (sR == null)
                return false;
            return sR.removeStoreManager(session, s, oldManager);
        }

        public Product addProdut(String productName, User session)
        {
            if (!(session.getState() is Admin))
                return null;
            if (!session.getState().isLogedIn())
                return null;
            return Product.addProduct(productName);
        }

        public Boolean addStoreOwner(Store s, User newOwner, User session)
        {
            if (newOwner == null || session == null || s == null)
                return false;
            StoreRole sR = storeArchive.getInstance().getStoreRole(s.getStoreId(), session.getUserName());
            if (sR == null)
                return false;
            return sR.addStoreOwner(session, s, newOwner);
        }

        public Boolean addManagerPermission(String permission, Store s, User manager, User session)
        {
            if (permission == null || manager == null || session == null || s == null)
                return false;
            StoreRole sR = storeArchive.getInstance().getStoreRole(s.getStoreId(), session.getUserName());
            if (sR == null)
                return false;
            return sR.addManagerPermission(session, permission, s, manager);

        }

        public Boolean removeManagerPermission(String permission, Store s, User manager, User session)
        {
            if (permission == null || manager == null || session == null || s == null)
                return false;
            StoreRole sR = storeArchive.getInstance().getStoreRole(s.getStoreId(), session.getUserName());
            if (sR == null)
                return false;
            return sR.removeManagerPermission(session, permission, s, manager);
        }

        public Boolean addBuyPolicy(ProductInStore p, User session)
        {
            if (session == null || p == null)
                return false;
            //Not implemented in this version
            return false;
        }
        public Boolean removeBuyPolicy(ProductInStore p, User session)
        {
            if (session == null || p == null)
                return false;
            //Not implemented in this version
            return false;
        }
        public Boolean addHiddenDiscount(ProductInStore p, User session)
        {
            if (session == null || p == null)
                return false;
            return false;
        }
        public Boolean addRevealedDiscount(ProductInStore p, User session)
        {
            if (session == null || p == null)
                return false;
            return false;
        }
        public Boolean removeDiscount(ProductInStore p, User session)
        {
            if (session == null || p == null)
                return false;
            return false;
        }
        //TO FIX - CALL THE FUNCTION IN STORE ROLE
        public Boolean viewStoreHistory(User session)
        {
            if (session == null)
                return false;
            return false;
        }

        public LinkedList<StoreOwner> getOwners(Store s)
        {
            if (s == null)
                return null;
            return s.getOwners();
        }

        public LinkedList<StoreManager> getManagers(Store s)
        {
            if (s == null)
                return null;
            return s.getManagers();
        }

        public LinkedList<Sale> viewSalesByStore(Store store)
        {
            if (store == null)
                return null;
            return store.getAllSales();
        }

    }
}