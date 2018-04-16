using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public abstract class StoreRole
    {
        public virtual Boolean addProduct(User session, ProductInStore pis)
        {
            return false;
        }

        public virtual ProductInStore addProductInStore(User session, Store s, Product p, double price, int amount)
        {

            //if(check if session is owner or manager with the right permission)
            Product p2 = ProductArchive.getInstance().getProductByName(p.getProductName());
            if (p2 == null)
            {
                p2 = ProductArchive.getInstance().addProduct(new Product(p.getProductName(), ProductArchive.getInstance().getNextProductId()));
            }
            if (price >= 0 && amount >= 0)
            {
                return ProductArchive.getInstance().addProductInStore(new ProductInStore(ProductArchive.getInstance().getNextProductId(), p2, price, amount, s));
            }
            return null;
        }

        public virtual Boolean removeProductFromStore(User session, Store s, ProductInStore p)
        {
           
            return ProductArchive.getInstance().removeProductInStore(p.getProductInStoreId(), s.getStoreId());

        }
        public virtual Boolean addStoreManager(User session, Store s, User newManager)
        {
            StoreRole m = new StoreManager(session);
            return storeArchive.getInstance().addStoreRole(m, s.getStoreId(),newManager.getUserName());
        }
        public virtual Boolean removeStoreManager(User session, Store s, User oldManager)
        {
            return storeArchive.getInstance().removeStoreRole(s.getStoreId(), oldManager.getUserName());
        }

        public virtual Boolean addStoreOwner(User session, Store s, User newOwner)
        {
            StoreRole owner = new StoreOwner();
            return storeArchive.getInstance().addStoreRole(owner, s.getStoreId(), newOwner.getUserName());
        }

        public virtual Boolean addManagerPermission(User session, String permission, Store s, User manager)
        {
            StoreRole sR = storeArchive.getInstance().getStoreRole(s.getStoreId(), manager.getUserName());
            return correlate(session, permission, sR, true);

        }

        public virtual Boolean removeManagerPermission(User session, String permission, Store s, User manager)
        {
            StoreRole sR = storeArchive.getInstance().getStoreRole(s.getStoreId(), manager.getUserName());
            return correlate(session, permission, sR, false);
        }

        public Boolean correlate(User session, String permission, StoreRole sR, Boolean allow)
        {
            switch (permission)
            {
                case "addProductInStore":
                    sR.getPremissions(session).addProductInStore(allow);
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

                default:
                    return false;
            }
        }


        public virtual StorePremissions getPremissions(User session)
        {
            return null;
        }
    }
}
