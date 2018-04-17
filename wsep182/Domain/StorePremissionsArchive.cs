using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    class StorePremissionsArchive
    {
        private static StorePremissionsArchive instance;
        Dictionary<string, StorePremissions> privilegesOfaUser;
        private StorePremissionsArchive()
        {
            privilegesOfaUser = new Dictionary<string, StorePremissions>();
        }
        public static StorePremissionsArchive getInstance()
        {
            if (instance == null)
                instance = new StorePremissionsArchive();
            return instance;
        }

        public StorePremissions getAllPremissions(string username)
        {
            return privilegesOfaUser[username];
        }

        public static void restartInstance()
        {
            instance = new StorePremissionsArchive();
        }

        public void initManagerPrivileges(string username)
        {
            privilegesOfaUser.Add(username, new StorePremissions());
        }


        public void addProductInStore(string username, Boolean allow)
        {
            privilegesOfaUser[username].addProductInStore(allow);
        }

        public void editProductInStore(string username, Boolean allow)
        {
            privilegesOfaUser[username].editProductInStore(allow);
        }

        public void removeProductFromStore(string username, Boolean allow)
        {
            privilegesOfaUser[username].removeProductFromStore(allow);
        }

        public void addStoreManager(string username, Boolean allow)
        {
            privilegesOfaUser[username].addStoreManager(allow);
        }

        public void addDiscount(string username, Boolean allow)
        {
            privilegesOfaUser[username].addDiscount(allow);
        }

        public void addNewCoupon(string username, Boolean allow)
        {
            privilegesOfaUser[username].addNewCoupon(allow);
        }

        public void removeDiscount(string username, Boolean allow)
        {
            privilegesOfaUser[username].removeDiscount(allow);
        }

        public void removeCoupon(string username, Boolean allow)
        {
            privilegesOfaUser[username].removeCoupon(allow);
        }

        public void removeStoreManager(string username, Boolean allow)
        {
            privilegesOfaUser[username].removeStoreManager(allow);
        }

        public void addManagerPermission(string username, Boolean allow)
        {
            privilegesOfaUser[username].addManagerPermission(allow);
        }

        public void removeManagerPermission(string username, Boolean allow)
        {
            privilegesOfaUser[username].removeManagerPermission(allow);
        }

        public void addSaleToStore(string username, Boolean allow)
        {
            privilegesOfaUser[username].addSaleToStore(allow);
        }

        public void removeSaleFromStore(string username, Boolean allow)
        {
            privilegesOfaUser[username].removeSaleFromStore(allow);
        }

        public void editSale(string username, Boolean allow)
        {
            privilegesOfaUser[username].editSale(allow);
        }

        public void viewPurchasesHistory(string username, Boolean allow)
        {
            privilegesOfaUser[username].viewPurchasesHistory(allow);
        }

        public Boolean checkPrivilege(string username, string privilege)
        {
            return privilegesOfaUser[username].checkPrivilege(privilege);
        }
        

        
        //public Boolean addNewCoupon(string username, string privilege)
        //{
        //   return privilegesOfaUser[username].checkPrivilege(privilege);
        //}
        
    }
}
