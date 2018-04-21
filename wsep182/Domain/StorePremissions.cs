using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class StorePremissions
    {
        Dictionary<string, Premissions> privileges;

        public Premissions getPrivileges(string username)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            return privileges[username];
        }
        //        bool addProduct;
        public StorePremissions()
        {
            privileges = new Dictionary<string, Premissions>();
        }
        public void addProductInStore(string username, Boolean allow)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username,new Premissions());
            privileges[username].addProductInStore(allow);
        }

        public void removeProductFromStore(string username, Boolean allow)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            privileges[username].removeProductFromStore(allow);
        }

        public void editProductInStore(string username, Boolean allow)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            privileges[username].editProductInStore(allow);
        }
        public void addStoreManager(string username, Boolean allow)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            privileges[username].addStoreManager(allow);
        }
        public void viewPurchasesHistory(string username, Boolean allow)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            privileges[username].viewPurchasesHistory(allow);
        }
        public void removeStoreManager(string username, Boolean allow)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            privileges[username].removeStoreManager(allow);
        }

        public void addManagerPermission(string username, Boolean allow)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            privileges[username].addManagerPermission(allow);
        }
        public void removeManagerPermission(string username, Boolean allow)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            privileges[username].removeManagerPermission(allow);
        }

        public void addSaleToStore(string username, Boolean allow)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            privileges[username].addSaleToStore(allow);
        }

        public void removeSaleFromStore(string username, Boolean allow)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            privileges[username].removeSaleFromStore(allow);
        }

        public void editSale(string username, Boolean allow)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            privileges[username].editSale(allow);
        }

        public void addNewCoupon(string username, Boolean allow)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            privileges[username].addNewCoupon(allow);
        }

        public void addDiscount(string username, Boolean allow)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            privileges[username].addDiscount(allow);
        }

        public void removeDiscount(string username, Boolean allow)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            privileges[username].removeDiscount(allow);
        }

        public void removeCoupon(string username, Boolean allow)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            privileges[username].removeCoupon(allow);
        }

        public Boolean checkPrivilege(string username, string privilege)
        {
            if (!privileges.ContainsKey(username))
                privileges.Add(username, new Premissions());
            return privileges[username].checkPrivilege(privilege);
        }

    }
}
