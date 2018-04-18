using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.StoreTests
{
    [TestClass]
    public class AddStoreManagerTests
    {
        private userServices us;
        private storeServices ss;
        private User zahi;

        [TestInitialize]
        public void init()
        {
            ProductArchive.restartInstance();
            SalesArchive.restartInstance();
            storeArchive.restartInstance();
            UserArchive.restartInstance();
            UserCartsArchive.restartInstance();
            BuyHistoryArchive.restartInstance();
            CouponsArchive.restartInstance();
            DiscountsArchive.restartInstance();
            RaffleSalesArchive.restartInstance();
            StorePremissionsArchive.restartInstance();
            us = userServices.getInstance();
            ss = storeServices.getInstance();
            zahi = us.startSession();
            us.register(zahi, "zahi", "123456");
            us.login(zahi, "zahi", "123456");
        }

        [TestMethod]
        public void SimpleAddStoreManager()
        {
            User aviad = us.startSession();
            us.register(aviad, "aviad", "123456");
            us.login(aviad, "aviad", "123456");

            Store store = ss.createStore("abowim", zahi);
            LinkedList<StoreManager> managers = store.getManagers();
            Assert.AreEqual(managers.Count, 0);

            ss.addStoreManager(store, "aviad", zahi);
            managers = store.getManagers();
            Assert.AreEqual(managers.Count, 1);
            StoreManager SM = managers.First.Value;
            Assert.AreEqual(SM.getUser().getUserName(), aviad.getUserName());
            Assert.AreEqual(SM.getUser().getPassword(), aviad.getPassword());
            Assert.AreEqual(SM.getStore(), store);

            StorePremissions SP= SM.getPremissions(aviad);
            Dictionary<string, Boolean> Dict = SP.getPrivileges();
            foreach (KeyValuePair<string, Boolean> entry in Dict)
            {
                Assert.IsFalse(entry.Value);
            }
        }

        [TestMethod]
        public void OwnerTryAddHimselfToBeStoreManager()
        {
            Store store = ss.createStore("abowim", zahi);
            ss.addStoreManager(store, "zahi", zahi);
            LinkedList<StoreManager>  managers = store.getManagers();
            Assert.AreEqual(managers.Count, 0);
        }

        [TestMethod]
        public void OwnerTryAddOtherOwnerToBeStoreManager()
        {
            User aviad = us.startSession();
            us.register(aviad, "aviad", "123456");
            us.login(aviad, "aviad", "123456");
            Store store = ss.createStore("abowim", zahi);
            ss.addStoreOwner(store, "aviad", zahi);
            ss.addStoreManager(store, "aviad", zahi);
            LinkedList<StoreManager>  managers = store.getManagers();
            Assert.AreEqual(managers.Count, 0);
        }

        [TestMethod]
        public void AddStoreManagerWhoIsNull()
        {
            
            Store store = ss.createStore("abowim", zahi);
            ss.addStoreManager(store, null, zahi);
            LinkedList<StoreManager> managers = store.getManagers();
            Assert.AreEqual(managers.Count, 0);
        }

        [TestMethod]
        public void NullTryToAddStoreManager()
        {

            User aviad = us.startSession();
            us.register(aviad, "aviad", "123456");
            us.login(aviad, "aviad", "123456");
            Store store = ss.createStore("abowim", zahi);
            ss.addStoreManager(store, "aviad", null);
            LinkedList<StoreManager> managers = store.getManagers();
            Assert.AreEqual(managers.Count, 0);
        }

        [TestMethod]
        public void TryToAddStoreManagerToNullStore()
        {
            User aviad = us.startSession();
            us.register(aviad, "aviad", "123456");
            us.login(aviad, "aviad", "123456");
            storeServices ss = storeServices.getInstance();
            Store store = ss.createStore("abowim", zahi);
            ss.addStoreManager(null, "aviad", zahi);
            LinkedList<StoreManager> managers = store.getManagers();
            Assert.AreEqual(managers.Count, 0);
        }

        [TestMethod]
        public void TryToAddStoreManagerToNoneExistingStore()
        {
            User aviad = us.startSession();
            us.register(aviad, "aviad", "123456");
            us.login(aviad, "aviad", "123456");
            Store store = new Store(1,"abowim", zahi);
            ss.addStoreManager(store, "aviad", zahi);
            LinkedList<StoreManager> managers = store.getManagers();
            Assert.AreEqual(managers.Count, 0);
        }

        [TestMethod]
        public void NoneExistingUserTryAddManagerToStore()
        {
            User shay = us.startSession();
            us.register(shay, "shay", "123456");
            us.login(shay, "shay", "123456");
            User aviad = new User("aviad", "123456");
            Store store = new Store(1, "abowim", zahi);
            ss.addStoreManager(store, "shay", aviad);
            LinkedList<StoreManager> managers = store.getManagers();
            Assert.AreEqual(managers.Count, 0);
        }

        [TestMethod]
        public void GuestTryAddManagerToStore()
        {
            User shay = us.startSession();
            us.register(shay, "shay", "123456");
            Store store = new Store(1, "abowim", zahi);
            ss.addStoreManager(store, "shay", zahi);
            LinkedList<StoreManager> managers = store.getManagers();
            Assert.AreEqual(managers.Count, 0);
        }

        [TestMethod]
        public void AddStoreManagerWhoAllreadyManager()
        {
            User aviad = us.startSession();
            us.register(aviad, "aviad", "123456");
            us.login(aviad, "aviad", "123456");
            Store store = ss.createStore("abowim", zahi);
            LinkedList<StoreManager> managers = store.getManagers();
            Assert.AreEqual(managers.Count, 0);
            ss.addStoreManager(store, "aviad", zahi);
            ss.addStoreManager(store, "aviad", zahi);
            managers = store.getManagers();
            Assert.AreEqual(managers.Count, 1);
            StoreManager SM = managers.First.Value;
            Assert.AreEqual(SM.getUser().getUserName(), aviad.getUserName());
            Assert.AreEqual(SM.getUser().getPassword(), aviad.getPassword());
            Assert.AreEqual(SM.getStore(), store);
            StorePremissions SP = SM.getPremissions(aviad);
            Dictionary<string, Boolean> Dict = SP.getPrivileges();
            foreach (KeyValuePair<string, Boolean> entry in Dict)
            {
                Assert.IsFalse(entry.Value);
            }
        }

    }
}
