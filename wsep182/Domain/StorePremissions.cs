using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class StorePremissions
    {
        Dictionary<string, Boolean> privileges;

        public Dictionary<string, Boolean> getPrivileges()
        {
            return privileges;
        }
        //        bool addProduct;
        public StorePremissions(LinkedList<String> newPrivileges)
        {
            privileges = new Dictionary<string, bool>();
            foreach (String s in newPrivileges)
                addPrivilege(s);
        }
        public StorePremissions()
        {
            privileges = new Dictionary<string, bool>();
        }
        private void addPrivilege(string privilege)
        {
            if (!privileges.ContainsKey(privilege))
                privileges.Add(privilege, true);
        }

        private void removePrivilege(string privilege)
        {
            if (privileges.ContainsKey(privilege))
                privileges.Remove(privilege);
        }
        public void addProductInStore(Boolean allow)
        {
            if (allow)
                addPrivilege("addProductInStore");
            else removePrivilege("addProductInStore");
        }

        public void removeProductFromStore(Boolean allow)
        {
            if (allow)
                addPrivilege("removeProductFromStore");
            else removePrivilege("removeProductFromStore");
        }

        public void editProductInStore(Boolean allow)
        {
            if (allow)
                addPrivilege("editProductInStore");
            else removePrivilege("editProductInStore");
        }
        public void addStoreManager(Boolean allow)
        {
            if (allow)
                addPrivilege("addStoreManager");
            else removePrivilege("addStoreManager");
        }
        public void viewPurchasesHistory(Boolean allow)
        {
            if (allow)
                addPrivilege("viewPurchasesHistory");
            else removePrivilege("viewPurchasesHistory");
        }
        public void removeStoreManager(Boolean allow)
        {
            if (allow)
                addPrivilege("removeStoreManager");
            else removePrivilege("removeStoreManager");
        }

        public void addManagerPermission(Boolean allow)
        {
            if (allow)
                addPrivilege("addManagerPermission");
            else removePrivilege("addManagerPermission");
        }
        public void removeManagerPermission(Boolean allow)
        {
            if (allow)
                addPrivilege("removeManagerPermission");
            else removePrivilege("removeManagerPermission");
        }

        public void addSaleToStore(Boolean allow)
        {
            if (allow)
                addPrivilege("addSaleToStore");
            else removePrivilege("addSaleToStore");
        }

        public void removeSaleFromStore(Boolean allow)
        {
            if (allow)
                addPrivilege("removeSaleFromStore");
            else removePrivilege("removeSaleFromStore");
        }

        public void editSale(Boolean allow)
        {
            if (allow)
                addPrivilege("editSale");
            else removePrivilege("editSale");
        }

        public void addNewCoupon(Boolean allow)
        {
            if (allow)
                addPrivilege("addNewCoupon");
            else removePrivilege("addNewCoupon");
        }

        public void addDiscount(Boolean allow)
        {
            if (allow)
                addPrivilege("addDiscount");
            else removePrivilege("addDiscount");
        }
        
        public Boolean checkPrivilege(string privilege)
        {
            Boolean res = false;
            try
            {
                privileges.TryGetValue(privilege, out res);
                return res;
            }
            catch (ArgumentNullException)
            {
                return false;
            }
        }

    }
}
