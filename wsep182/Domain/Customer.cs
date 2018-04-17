using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class Customer : StoreRole
    {
        public Customer(User u, Store s) : base(u, s)
        {

        }

        public LinkedList<Purchase> viewStoreHistory(Store store, User session)
        {
            return null;
        }

        public override Product addProduct(User session, String productName)
        {
            return null;
        }

        public override Boolean addDiscount(User session, ProductInStore p, int percentage, String dueDate)
        {
            return false;
        }

        public override Boolean addNewCoupon(User session, String couponId, ProductInStore p, int percentage, String dueDate)
        {
            return false;
        }

        public override Boolean editProductInStore(User session, ProductInStore p, int quantity, double price)
        {
            return false;
        }

        public override Boolean removeProductFromStore(User session, Store s, ProductInStore p)
        {
            return false;
        }

        public override Boolean addStoreManager(User session, Store s, User newManager)
        {
            return false;
        }

        public override Boolean removeStoreManager(User session, Store s, User oldManager)
        {
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

        public override Boolean addManagerPermission(User session, String permission, Store s, User manager)
        {
            return false;

        }

        public override int addSaleToStore(User session, int productInStoreId, int typeOfSale, int amount, String dueDate)
        {
            return -1;
        }

        public override Boolean removeManagerPermission(User session, String permission, Store s, User manager)
        {
            return false;
        }

        public override Boolean removeSaleFromStore(User session, Store s, int saleId)
        {
            return false;
        }

        public override Boolean editSale(User session, Store s, int saleId, int amount, String dueDate)
        {
            return false;
        }

        public override LinkedList<Purchase> viewPurchasesHistory(User session, Store s)
        {
            return null;
        }

        public override StorePremissions getPremissions(User session)
        {
            return null;
        }
    }
}
