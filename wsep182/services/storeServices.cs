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
            if (session == null || !session.getState().isLogedIn())
                return null;
            return session.createStore(storeName);
        }



        public LinkedList<Product> getAllProducts(User session)
        {
            return Product.getProducts();
        }
        //req 3.1
        public ProductInStore addProductInStore(Product p, Double price, int amount, User session, Store s)
        {
            if (session == null || !session.getState().isLogedIn())
                return null;
            StoreRole sR = StoreRole.getStoreRole(s, session);
            if (sR == null)
                return null;
            return sR.addProductInStore(session, s, p, price, amount);
        }

        public virtual Boolean editProductInStore(User session, Store s,ProductInStore p, int quantity, double price)
        {
            if (session == null || !session.getState().isLogedIn())
                return false;
            StoreRole sR = StoreRole.getStoreRole(s, session);
            if (sR == null)
                return false;
            return sR.editProductInStore(session, p, quantity, price);
        }


        public int addSaleToStore(User session, Store s, int productInStoreId, int typeOfSale, int amount, String dueDate)
        {
            if (session == null || !session.getState().isLogedIn())
                return -1;
            StoreRole sR = StoreRole.getStoreRole(s, session);
            if (sR == null)
                return -1;
            return sR.addSaleToStore(session, productInStoreId, typeOfSale, amount, dueDate);
        }

        public Boolean removeSaleFromStore(User session, Store s, int saleId)
        {
            if (session == null || !session.getState().isLogedIn())
                return false;
            StoreRole sR = StoreRole.getStoreRole(s, session);
            if (sR == null)
                return false;
            return sR.removeSaleFromStore(session, s, saleId);
        }
        public Boolean editSale(User session, Store s, int saleId, int amount, String dueDate)
        {
            if (session == null || !session.getState().isLogedIn())
                return false;
            StoreRole sR = StoreRole.getStoreRole(s, session);
            if (sR == null)
                return false;
            return sR.editSale(session, s, saleId,amount,dueDate);
        }

        //req 3.2
        public Boolean removeProductFromStore(Store s, ProductInStore p, User session)
        {
            if (session == null || !session.getState().isLogedIn())
                return false;
            StoreRole sR = StoreRole.getStoreRole(s, session);
            if (sR == null)
                return false;
            return sR.removeProductFromStore(session, s, p);
        }
        public Boolean addStoreManager(Store s, User newManager, User session)
        {
            if (session == null || !session.getState().isLogedIn())
                return false;
            StoreRole sR = StoreRole.getStoreRole(s, session);
            if (sR == null)
                return false;
            return sR.addStoreManager(session, s, newManager);
        }
        public Boolean removeStoreManager(Store s, User oldManager, User session)
        {
            if (session == null || !session.getState().isLogedIn())
                return false;
            StoreRole sR = StoreRole.getStoreRole(s, session);
            if (sR == null)
                return false;
            return sR.removeStoreManager(session, s, oldManager);
        }

        public Product addProduct(String productName, User session)
        {

            return null;
        }

        public Boolean addStoreOwner(Store s, User newOwner, User session)
        {
            if (session == null || !session.getState().isLogedIn())
                return false;
            StoreRole sR = StoreRole.getStoreRole(s, session);
            if (sR == null)
                return false;
            return sR.addStoreOwner(session, s, newOwner);
        }

        public Boolean addManagerPermission(String permission, Store s, User manager, User session)
        {
            if (session == null || !session.getState().isLogedIn())
                return false;
            StoreRole sR = StoreRole.getStoreRole(s, session);
            if (sR == null)
                return false;
            return sR.addManagerPermission(session, permission, s, manager);

        }

        public Boolean removeManagerPermission(String permission, Store s, User manager, User session)
        {
            if (session == null || !session.getState().isLogedIn())
                return false;
            StoreRole sR = StoreRole.getStoreRole(s, session);
            if (sR == null)
                return false;
            return sR.removeManagerPermission(session, permission, s, manager);
        }

        public Boolean addBuyPolicy(ProductInStore p, User session)
        {
            //Not implemented in this version
            return false;
        }

        public Boolean removeBuyPolicy(ProductInStore p, User session)
        {
            //Not implemented in this version
            return false;
        }

        public Boolean addCouponDiscount(User session,Store s, String couponId, ProductInStore p, int percentage, String dueDate)
        {
            if (session == null || !session.getState().isLogedIn())
                return false;
            StoreRole sR = StoreRole.getStoreRole(s, session);
            if (sR == null)
                return false;
            return sR.addNewCoupon(session, couponId, p, percentage, dueDate);
        }
        public Boolean addDiscount(ProductInStore p, int percentage ,String dueDate,User session ,Store s)
        {
            if (session == null || !session.getState().isLogedIn())
                return false;
            StoreRole sR = StoreRole.getStoreRole(s, session);
            if (sR == null)
                return false;
            return sR.addDiscount(session,p, percentage, dueDate);
            
        }
        public Boolean removeDiscount(ProductInStore p, Store s, User session)
        {
            if (session == null || !session.getState().isLogedIn())
                return false;
            StoreRole sR = StoreRole.getStoreRole(s, session);
            if (sR == null)
                return false;
            return sR.removeDiscount(session, p);
        }

        public Boolean removeCoupon(Store s, User session, String couponId)
        {
            if (session == null || !session.getState().isLogedIn())
                return false;
            StoreRole sR = StoreRole.getStoreRole(s, session);
            if (sR == null)
                return false;
            return sR.removeCoupon(session, couponId);
        }

        public LinkedList<Purchase> viewStoreHistory(User session, Store store)
        {
            if (session == null || store==null)
                return null;
            return session.viewStoreHistory(store);
        }

        public LinkedList<Purchase> viewUserHistory(User session, User userToGetHistory)
        {
            if (session == null || userToGetHistory == null)
                return null;
            return session.viewUserHistory(userToGetHistory);
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