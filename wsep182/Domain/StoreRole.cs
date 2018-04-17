using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public abstract class StoreRole
    {
        private User user;
        private Store store;

        public StoreRole(User u, Store s){
            user = u;
            store = s;
        }

        public static StoreRole getStoreRole(Store store, User user)
        {
            if (store == null || user == null)
                return null;
            return storeArchive.getInstance().getStoreRole(store, user);
        }


        public virtual ProductInStore addProductInStore(User session, Store s, String productName, double price, int amount)
        {
            if (productName == null || session == null || s == null || amount <= 0 || price <= 0
                || productName == ""
                || productName[productName.Length - 1] == ' ')
                return null;
            //if(check if session is owner or manager with the right permission)
            Product p2 = ProductArchive.getInstance().getProductByName(productName);
            if (p2 == null)
            {
                
                p2 = Product.addProduct(productName);
            }
            if (price >= 0 && amount >= 0)
            {
                return ProductArchive.getInstance().addProductInStore(p2, s, amount, price);
            }
            return null;
        }

        public virtual Boolean editProductInStore(User session, ProductInStore p, int quantity, double price)
        {
            if (session!=null && p!=null && price >= 0 && quantity >= 0)
            {
                p.Price = price;
                p.Quantity = quantity;
                return ProductArchive.getInstance().updateProductInStore(p);
            }
            return false;
        }

        public virtual Boolean removeProductFromStore(User session, Store s, ProductInStore p)
        {
            if (session == null || s == null || p == null)
                return false;
            return ProductArchive.getInstance().removeProductInStore(p.getProductInStoreId(), s.getStoreId());

        }
        public virtual Boolean addStoreManager(User session, Store s, User newManager)
        {
            if (session == null || s == null || newManager == null)
                return false;
            StoreRole sr = storeArchive.getInstance().getStoreRole(s, newManager);
            if (sr != null && (sr is StoreOwner || sr is StoreManager))
                return false;
            if (sr != null && (sr is Customer))
                storeArchive.getInstance().removeStoreRole(s.getStoreId(), newManager.getUserName());
            StoreRole m = new StoreManager(newManager, s);
            return storeArchive.getInstance().addStoreRole(m, s.getStoreId(),newManager.getUserName());
        }
        public virtual Boolean removeStoreManager(User session, Store s, User oldManager)
        {
            if (session == null || s == null || oldManager == null)
                return false;
            return storeArchive.getInstance().removeStoreRole(s.getStoreId(), oldManager.getUserName());
        }

        public virtual Boolean addStoreOwner(User session, Store s, User newOwner)
        {
            if(newOwner == null || s == null || session == null)
            {
                return false ;
            }
            StoreRole sr = storeArchive.getInstance().getStoreRole(s, newOwner);
            if (sr != null && (sr is StoreOwner))
                return false;
            if (sr != null && (sr is StoreManager))
                removeStoreManager(session, s, newOwner);
            if (sr != null && (sr is Customer))
                storeArchive.getInstance().removeStoreRole(s.getStoreId(), newOwner.getUserName());
            StoreRole owner = new StoreOwner(newOwner,s);
            return storeArchive.getInstance().addStoreRole(owner, s.getStoreId(), newOwner.getUserName());
        }
        public virtual Boolean removeStoreOwner(User session, Store s, User ownerToDelete)
        {
            if (s.getStoreCreator().getUserName().Equals(ownerToDelete.getUserName()))
                return false;
            return storeArchive.getInstance().removeStoreRole(s.getStoreId(), ownerToDelete.getUserName());
        }
        public virtual Boolean addManagerPermission(User session, String permission, Store s, User manager)
        {
            if (session == null || permission == null || manager == null || s == null)
                return false;
            StoreRole sR = storeArchive.getInstance().getStoreRole(s, manager);
            return correlate(manager, permission, sR, true);

        }
        public virtual int addSaleToStore(User session, int productInStoreId, int typeOfSale, int amount,String dueDate)
        {
            if (ProductArchive.getInstance().getProductInStore(productInStoreId) == null || typeOfSale > 3 || typeOfSale < 1 || amount < 0 || dueDate == null)
                return -1;
            if(typeOfSale == 2)
            {
                //will be implemented next version
                return -1;
            }
            return SalesArchive.getInstance().addSale(productInStoreId, typeOfSale, amount, dueDate).SaleId;
        }

        public virtual Boolean removeSaleFromStore(User session, Store s, int saleId)
        {
            if (session == null || s == null || SalesArchive.getInstance().getSale(saleId) == null)
                return false;
            return SalesArchive.getInstance().removeSale(saleId);
        }

        public virtual Boolean editSale(User session,Store s,int saleId,int amount,String dueDate)
        {
            if (session == null || s == null || SalesArchive.getInstance().getSale(saleId) == null || amount <= 0 || dueDate == null)
                return false;
            return SalesArchive.getInstance().editSale(saleId, amount, dueDate);
        }

        public virtual Boolean addDiscount(User session, ProductInStore p, int percentage, String dueDate)
        {
            if (session == null || p == null || percentage < 0 || dueDate == null)
                return false;
            return DiscountsArchive.getInstance().addNewDiscount(p.getProductInStoreId(), percentage, dueDate);
        }

        public virtual Boolean addNewCoupon(User session, String couponId, ProductInStore p, int percentage, String dueDate)
        {
            if (session == null || p == null || percentage < 0 || dueDate == null)
                return false;
            return CouponsArchive.getInstance().addNewCoupon(couponId, p.getProductInStoreId(), percentage, dueDate);
        }

        public virtual Boolean removeDiscount(User session, ProductInStore p)
        {
            if (p == null || session == null)
                return false;
            return DiscountsArchive.getInstance().removeDiscount(p.getProductInStoreId());
        }

        public virtual Boolean removeCoupon(User session, String couponId)
        {
            if (session == null || couponId==null)
                return false;
            return CouponsArchive.getInstance().removeCoupon(couponId);
        }

        public virtual Boolean removeManagerPermission(User session, String permission, Store s, User manager)
        {
            if (permission == null || manager == null || session == null || s == null)
                return false;
            StoreRole sR = storeArchive.getInstance().getStoreRole(s, manager);
            return correlate(manager, permission, sR, false);
        }
        public virtual LinkedList<Purchase> viewPurchasesHistory(User session,Store s)
        {
            return BuyHistoryArchive.getInstance().viewHistoryByStoreId(s.getStoreId());
        }

        public Boolean correlate(User session, String permission, StoreRole sR, Boolean allow)
        {
            switch (permission)
            {
                case "addProductInStore":
                    sR.getPremissions(session).addProductInStore(allow);
                    return true;

                case "editProductInStore":
                    sR.getPremissions(session).editProductInStore(allow);
                    return true;

                case "removeProductFromStore":
                    sR.getPremissions(session).removeProductFromStore(allow);
                    return true;

                case "addStoreManager":
                    sR.getPremissions(session).addStoreManager(allow);
                    return true;
                case "removeStoreManager":
                    sR.getPremissions(session).removeStoreManager(allow);
                    return true;
                case "addManagerPermission":
                    sR.getPremissions(session).addManagerPermission(allow);
                    return true;
                case "removeManagerPermission":
                    sR.getPremissions(session).removeManagerPermission(allow);
                    return true;
                case "viewPurchasesHistory":
                    sR.getPremissions(session).viewPurchasesHistory(allow);
                    return true;
                case "removeSaleFromStore":
                    sR.getPremissions(session).removeSaleFromStore(allow);
                    return true;
                case "editSale":
                    sR.getPremissions(session).editSale(allow);
                    return true;
                case "addSaleToStore":
                    sR.getPremissions(session).addSaleToStore(allow);
                    return true;
                case "addDiscount":
                    sR.getPremissions(session).addDiscount(allow);
                    return true;
                case "addNewCoupon":
                    sR.getPremissions(session).addNewCoupon(allow);
                    return true;
                case "removeDiscount":
                    sR.getPremissions(session).removeDiscount(allow);
                    return true;
                case "removeCoupon":
                    sR.getPremissions(session).removeCoupon(allow);
                    return true;
                    


                case "getPremissions":
                    return true;
                case "addStoreOwner":
                    return false;
                case "removeStoreOwner":
                    return false;

                default:
                    return false;
            }
        }


        public virtual StorePremissions getPremissions(User session)
        {
            return null;
        }

        public User getUser()
        {
            return user;
        }
        public Store getStore()
        {
            return store;
        }
        public void setUser(User u)
        {
            user = u;
        }
        public void setStore(Store s)
        {
            store = s;
        }

    }
}
