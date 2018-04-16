using System;
using System.Collections.Generic;

namespace wsep182.Domain
{
    public class storeArchive
    {
        private static storeArchive instance;
        private LinkedList<Store> stores;
        private static int storeIndex = 0;

        private LinkedList<Tuple<String,int>> managers;
        private LinkedList<Tuple<String, int>> owners;
        private Dictionary<int,Dictionary<String,StoreRole>> archive;

        private storeArchive()
        {
            stores = new LinkedList<Store>();
            managers = new LinkedList<Tuple<String, int>>();
            owners = new LinkedList<Tuple<String, int>>();
        }
        public static storeArchive getInstance()
        {
            if (instance == null)
                instance = new storeArchive();
            return instance;
        }
        public static void restartInstance()
        {
            instance = new storeArchive();
        }
        public int getNextStoreId()
        {
            return ++storeArchive.storeIndex;
        }
        public Store addStore(String storeName, User storeOwner)
        {
            Store newStore;
            lock (this)
            {
                newStore = new Store(getNextStoreId(), storeName, storeOwner);
                storeIndex++;
            }
            foreach (Store s in stores)
                if (s.getStoreId() == newStore.getStoreId())
                    return null;
            stores.AddLast(newStore);
            return newStore;
        }

        public Boolean updateStore(Store newStore)
        {
            foreach (Store s in stores)
                if (s.getStoreId() == newStore.getStoreId())
                {
                    stores.Remove(s);
                    stores.AddLast(newStore);
                    return true;
                }
            return false;
        }

        public Store getStore(int storeId)
        {
            foreach (Store s in stores)
                if (s.getStoreId() == storeId)
                    return s;
            return null;
        }

        public Boolean removeStore(int storeId)
        {
            foreach (Store s in stores)
                if (s.getStoreId() == storeId)
                {
                    stores.Remove(s);
                    return true;
                }
            return false;
        }
        public Boolean addStoreRole(StoreRole newPremissions, int storeId, string userName)
        {
            if (archive[storeId][userName] != null)
                return false;
            archive[storeId][userName] = newPremissions;
            return true;
        }

        public Boolean updateStoreRole(StoreRole newPremissions, int storeId, string userName)
        {
            if (archive[storeId][userName] != null)
            {
                archive[storeId][userName] = newPremissions;
                return true;
            }
            return false;
        }

        public StoreRole getStoreRole(int storeId, string userName)
        {
            return archive[storeId][userName];
        }

        public Boolean removeStoreRole(int storeId, string userName)
        {
            if (archive[storeId][userName] != null)
            {
                archive[storeId][userName] = null;
                return true;
            }
            return false;
        }

        public LinkedList<String> getAllOwners(int storeId)
        {
            LinkedList<String> res = new LinkedList<String>();
            foreach (String userName in archive[storeId].Keys)
            {
                if (archive[storeId][userName] is StoreOwner)
                    res.AddLast(userName);
            }
            return res;
        }

        public LinkedList<String> getAllManagers(int storeId)
        {
            LinkedList<String> res = new LinkedList<String>();
            foreach (String userName in archive[storeId].Keys)
            {
                if (archive[storeId][userName] is StoreManager)
                    res.AddLast(userName);
            }
            return res;
        }
    }
}
