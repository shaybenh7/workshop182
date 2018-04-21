using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class StorePremissionsArchive
    {
        private static StorePremissionsArchive instance;
        Dictionary<int, StorePremissions> privilegesOfaStore;
        private StorePremissionsArchive()
        {
            privilegesOfaStore = new Dictionary<int, StorePremissions>();
        }
        public static StorePremissionsArchive getInstance()
        {
            if (instance == null)
                instance = new StorePremissionsArchive();
            return instance;
        }

        public Premissions getAllPremissions(int storeId, string username)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            return privilegesOfaStore[storeId].getPrivileges(username);
        }

        public static void restartInstance()
        {
            instance = new StorePremissionsArchive();
        }


        public void addProductInStore(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            privilegesOfaStore[storeId].addProductInStore(username, allow);
        }

        public void editProductInStore(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            privilegesOfaStore[storeId].editProductInStore(username, allow);
        }

        public void removeProductFromStore(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            privilegesOfaStore[storeId].removeProductFromStore(username, allow);
        }

        public void addStoreManager(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            privilegesOfaStore[storeId].addStoreManager(username, allow);
        }

        public void addDiscount(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            privilegesOfaStore[storeId].addDiscount(username, allow);
        }

        public void addNewCoupon(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            privilegesOfaStore[storeId].addNewCoupon(username, allow);
        }

        public void removeDiscount(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            privilegesOfaStore[storeId].removeDiscount(username, allow);
        }

        public void removeCoupon(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            privilegesOfaStore[storeId].removeCoupon(username, allow);
        }

        public void removeStoreManager(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            privilegesOfaStore[storeId].removeStoreManager(username, allow);
        }

        public void addManagerPermission(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            privilegesOfaStore[storeId].addManagerPermission(username, allow);
        }

        public void removeManagerPermission(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            privilegesOfaStore[storeId].removeManagerPermission(username, allow);
        }

        public void addSaleToStore(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            privilegesOfaStore[storeId].addSaleToStore(username, allow);
        }

        public void removeSaleFromStore(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            privilegesOfaStore[storeId].removeSaleFromStore(username, allow);
        }

        public void editSale(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            privilegesOfaStore[storeId].editSale(username, allow);
        }

        public void viewPurchasesHistory(int storeId, string username, Boolean allow)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            privilegesOfaStore[storeId].viewPurchasesHistory(username, allow);
        }

        public Boolean checkPrivilege(int storeId, string username, string privilege)
        {
            if (!privilegesOfaStore.ContainsKey(storeId))
                privilegesOfaStore.Add(storeId, new StorePremissions());
            return privilegesOfaStore[storeId].checkPrivilege(username, privilege);
        }
        

        
        //public Boolean addNewCoupon(string username, string privilege)
        //{
        //   return privilegesOfaUser[username].checkPrivilege(privilege);
        //}
        
    }
}
