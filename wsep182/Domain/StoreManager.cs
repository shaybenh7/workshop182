using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class StoreManager : StoreRole
    {
        StorePremissionsArchive premissions;
        public StoreManager(User u, Store s) : base(u, s)
        {
            StorePremissionsArchive.getInstance().initManagerPrivileges(u.getUserName());
            premissions = StorePremissionsArchive.getInstance();
        }

        public override ProductInStore addProductInStore(User session, Store s, Product p, double price, int amount)
        {
            if (premissions.checkPrivilege(session.getUserName(),"addProductInStore"))
                return base.addProductInStore(session, s, p, price, amount);
            return null;
        }

        public override Boolean editProductInStore(User session, ProductInStore p, int quantity, double price)
        {
            if (premissions.checkPrivilege(session.getUserName(), "editProductInStore"))
                return base.editProductInStore(session, p, quantity, price);
            return false;
        }

        public override Boolean addStoreOwner(User session, Store s, User newOwner)
        {
            return false;
        }

        public override Boolean removeStoreOwner(User session, Store s, User ownerToDelete)
        {
            return false;
        }

        public override Boolean removeProductFromStore(User session, Store s, ProductInStore p)
        {
            if (premissions.checkPrivilege(session.getUserName(),"removeProductFromStore"))
                return base.removeProductFromStore(session, s, p);
            return false;
        }

        public override Boolean addStoreManager(User session, Store s, User newManager)
        {
            if (premissions.checkPrivilege(session.getUserName(), "addStoreManager"))
                return base.addStoreManager(session, s, newManager);
            return false;
        }
        public override Boolean removeStoreManager(User session, Store s, User oldManager)
        {
            if (premissions.checkPrivilege(session.getUserName(), "removeStoreManager"))
                return base.removeStoreManager(session, s, oldManager);
            return false;
            
        }

        public override StorePremissions getPremissions(User session)
        {
            return premissions.getAllPremissions(session.getUserName());
        }

        public override Boolean addManagerPermission(User session, String permission, Store s, User manager)
        {
            if (premissions.checkPrivilege(session.getUserName(), "addManagerPermission"))
                return base.addManagerPermission(session, permission, s, manager);
            return false;
        }

        public override Boolean removeManagerPermission(User session, String permission, Store s, User manager)
        {
            if (premissions.checkPrivilege(session.getUserName(), "removeManagerPermission"))
                return base.removeManagerPermission(session, permission, s, manager);
            return false;
        }

        public override Boolean addSaleToStore(User session, int productInStoreId, int typeOfSale, int amount, String dueDate)
        {
            if (premissions.checkPrivilege(session.getUserName(), "addSaleToStore"))
                return base.addSaleToStore(session, productInStoreId, typeOfSale, amount, dueDate);
            return false;
        }

        public override LinkedList<BuyHistory> viewPurchasesHistory(User session, Store s)
        {
            if (premissions.checkPrivilege(session.getUserName(), "viewPurchasesHistory"))
                return base.viewPurchasesHistory(session, s);
            return null;
        }


    }
}
