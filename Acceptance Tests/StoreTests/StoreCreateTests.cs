using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.StoreTests
{


    [TestClass]
    public class StoreCreateTests
    {
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
        }

        [TestMethod]
        public void SimpleStoreCreate()
        {
            userServices us = userServices.getInstance();
            User zahi = us.startSession();
            us.register(zahi, "zahi", "123456");
            us.login(zahi, "zahi", "123456");
            storeServices ss = storeServices.getInstance();
            Store store = ss.createStore("abowim", zahi);
            Assert.AreEqual(store.getStoreName(), "abowim");
            LinkedList<StoreOwner> owners = store.getOwners();
            Assert.AreEqual(owners.Count, 1);
            Assert.IsTrue(owners.Last.Value.getUser()==zahi);
        }

        [TestMethod]
        public void StoreCreateWithEmptyName()
        {
            userServices us = userServices.getInstance();
            User zahi = us.startSession();
            us.register(zahi, "zahi", "123456");
            us.login(zahi, "zahi", "123456");
            storeServices ss = storeServices.getInstance();
            Store store = ss.createStore("", zahi);
            Assert.IsNull(store);
        }

        [TestMethod]
        public void StoreCreateWithNoneExistUser()
        {
            User zahi = new User("zahi","123456");
            storeServices ss = storeServices.getInstance();
            Store store = ss.createStore("abowim", zahi);
            Assert.IsNull(store);
        }

        [TestMethod]
        public void StoreCreateWithNotLoggedInUser()
        {
            userServices us = userServices.getInstance();
            User zahi = us.startSession();
            us.register(zahi, "zahi", "123456");
            storeServices ss = storeServices.getInstance();
            Store store = ss.createStore("aboim", zahi);
            Assert.IsNull(store);
        }

        [TestMethod]
        public void StoreCreateWithOnlySpaces()
        {
            userServices us = userServices.getInstance();
            User zahi = us.startSession();
            us.register(zahi, "zahi", "123456");
            us.login(zahi, "zahi", "123456");
            storeServices ss = storeServices.getInstance();
            Store store = ss.createStore("          ", zahi);
            Assert.IsNull(store);
        }


        [TestMethod]
        public void StoreCreateWithWhiteSpaces()
        {
            userServices us = userServices.getInstance();
            storeServices ss = storeServices.getInstance();
            User zahi = us.startSession();
            us.register(zahi, "zahi", "123456");
            us.login(zahi, "zahi", "123456");
            Store store = ss.createStore("abowim bro", zahi);
            Assert.AreEqual(store.getStoreName(), "abowim bro");
            LinkedList<StoreOwner> owners = store.getOwners();
            Assert.AreEqual(owners.Count, 1);
            Assert.IsTrue(owners.First.Value.getUser()==zahi);
        }

        [TestMethod]
        public void StoreCreateWithNullUser()
        {
            storeServices ss = storeServices.getInstance();
            Store store = ss.createStore("aboim", null);
            Assert.IsNull(store);
        }

        [TestMethod]
        public void StoreCreateWithNullStoreName()
        {
            userServices us = userServices.getInstance();
            User zahi = us.startSession();
            us.register(zahi, "zahi", "123456");
            us.login(zahi, "zahi", "123456");
            storeServices ss = storeServices.getInstance();
            Store store = ss.createStore(null, zahi);
            Assert.IsNull(store);
        }


        [TestMethod]
        public void StoreCreateWithNameOfExistingStore()
        {
            userServices us = userServices.getInstance();
            User zahi = us.startSession();
            us.register(zahi, "zahi", "123456");
            us.login(zahi, "zahi", "123456");
            storeServices ss = storeServices.getInstance();
            Store store = ss.createStore("abowim", zahi);
            Store store2 = ss.createStore("abowim", zahi);
            Assert.AreEqual(store2.getStoreName(), "abowim");
            LinkedList<StoreOwner> owners = store2.getOwners();
            Assert.AreEqual(owners.Count, 1);
            Assert.IsTrue(owners.First.Value.getUser()==zahi);
        }

    }
}
