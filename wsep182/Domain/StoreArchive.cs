using System;
using System.Collections.Generic;

namespace wsep182.Domain
{
    public class storeArchive
    {
        private static storeArchive instance;
        private LinkedList<Store> stores;
        private static int storeIndex = 0;

        private Dictionary<int,Dictionary<String,StoreRole>> archive;

        private storeArchive()
        {
            stores = new LinkedList<Store>();
            archive = new Dictionary<int, Dictionary<string, StoreRole>>();
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
            if (!archive.ContainsKey(storeId))
                archive.Add(storeId,new Dictionary<string, StoreRole>());
            if (archive[storeId].ContainsKey(userName))
                return false;
            archive[storeId].Add(userName,newPremissions);
            return true;
        }

        public Boolean updateStoreRole(StoreRole newPremissions, int storeId, string userName)
        {
            if (!archive.ContainsKey(storeId))
                return false;
            if (archive[storeId].ContainsKey(userName))
            {
                archive[storeId].Remove(userName);
                archive[storeId].Add(userName, newPremissions);
                return true;
            }
            return false;
        }

        public StoreRole getStoreRole(Store store, User user)
        {
            if (!archive.ContainsKey(store.getStoreId()) || !archive.ContainsKey(store.getStoreId()) || !archive[store.getStoreId()].ContainsKey(user.getUserName()))
                addStoreRole(new Customer(user, store),store.getStoreId(),user.getUserName());
            return archive[store.getStoreId()][user.getUserName()];
        }

        public Boolean removeStoreRole(int storeId, string userName)
        {
            if (!archive.ContainsKey(storeId))
                return false;
            if (archive[storeId].ContainsKey(userName))
            {
                archive[storeId].Remove(userName);
                return true;
            }
            return false;
        }

        public LinkedList<StoreOwner> getAllOwners(int storeId)
        {
            LinkedList<StoreOwner> res = new LinkedList<StoreOwner>();
            foreach (String userName in archive[storeId].Keys)
            {
                if (archive[storeId][userName] is StoreOwner)
                    res.AddLast((StoreOwner)archive[storeId][userName]);
            }
            return res;
        }

        public LinkedList<StoreManager> getAllManagers(int storeId)
        {
            LinkedList<StoreManager> res = new LinkedList<StoreManager>();
            foreach (String userName in archive[storeId].Keys)
            {
                if (archive[storeId][userName] is StoreManager)
                    res.AddLast((StoreManager)archive[storeId][userName]);
            }
            return res;
        }

        public LinkedList<StoreRole> getAllStoreRolesOfAUser(String username)
        {
            LinkedList<StoreRole> res = new LinkedList<StoreRole>();
            foreach (int key in archive.Keys){
                foreach (String key2 in archive[key].Keys)
                {
                    if (key2 == username)
                        res.AddLast(archive[key][key2]);
                }
            }
            return res;
        }

        public LinkedList<Store> getAllStore()
        {
            return stores;
        }
    }
}
