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

        public override Product addProduct(User session, String productName)
        {
            return null;
        }
        public override ProductInStore addProductInStore(User session, Store s, Product p, double price, int amount)
        {
            return null;
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
        public override Boolean addSaleToStore(User session, int productInStoreId, int typeOfSale, int amount, String dueDate)
        {
            return false;
        }
        public override Boolean removeManagerPermission(User session, String permission, Store s, User manager)
        {
            return false;
        }
        public override StorePremissions getPremissions(User session)
        {
            return null;
        }
    }
}
