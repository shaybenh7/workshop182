using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.StoreTests
{
    [TestClass]
    public class removeStoreManger
    {

        private userServices us;
        private storeServices ss;
        private User zahi, itamar, niv, admin, admin1; //admin,itamar logedin
        private Store store;//itamar owner , niv manneger

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
            admin = us.startSession();
            us.register(admin, "admin", "123456");
            us.login(admin, "admin", "123456");

            admin1 = us.startSession();
            us.register(admin1, "admin1", "123456");

            zahi = us.startSession();
            us.register(zahi, "zahi", "123456");

            itamar = us.startSession();
            us.register(itamar, "itamar", "123456");
            us.login(itamar,"itamar", "123456");
            store = itamar.createStore("Maria&Netta Inc.");

            niv = us.startSession();
            us.register(niv, "niv", "123456");
            us.login(niv, "niv", "123456");

            ss.addStoreManager(store, "niv", itamar);

        }

        [TestMethod]
        public void simpleRemoveManger()
        {
           Assert.IsTrue(ss.removeStoreManager(store, "niv", itamar));
            Assert.AreEqual(store.getManagers().Count , 0);
        }
        [TestMethod]
        public void RemoveMangerByAdmin()
        {
            Assert.IsFalse(ss.removeStoreManager(store, "niv", admin));
            Assert.AreEqual(store.getManagers().Count, 1);
        }
        [TestMethod]
        public void RemoveMangerByUser()
        {
            zahi.login("zahi", "123456");
            Assert.IsFalse(ss.removeStoreManager(store, "niv", zahi));
            Assert.AreEqual(store.getManagers().Count, 1);
        }
        [TestMethod]
        public void RemoveMangerByNotExistUser()
        {
            Assert.IsFalse(ss.removeStoreManager(store, "niv", zahi));
            Assert.AreEqual(store.getManagers().Count, 1);
        }
        [TestMethod]
        public void RemoveMangerByManegerWithPremition()
        {
            Assert.IsTrue(ss.addManagerPermission("removeStoreManager", store, "niv", itamar));
            us.login(zahi, "zahi", "123456");
            ss.addStoreManager(store, "zahi", itamar);
            Assert.IsTrue(ss.removeStoreManager(store, "zahi", niv));
            Assert.AreEqual(store.getManagers().Count, 1);
        }

        [TestMethod]
        public void RemoveMangerByHimselfWithPremition()
        {
            ss.addManagerPermission("removeStoreManager", store, "niv", itamar);
            Assert.IsFalse(ss.removeStoreManager(store, "niv", niv));
            Assert.AreEqual(store.getManagers().Count, 1);
        }
        [TestMethod]
        public void RemoveMangerByManegerThatRemoved()
        {
            ss.addManagerPermission("removeStoreManager", store, "niv", itamar);
            us.login(zahi, "zahi", "123456");
            ss.addStoreManager(store, "zahi", itamar);
            ss.addStoreManager(store, "admin", itamar);
            ss.addManagerPermission("removeStoreManager", store, "zahi", itamar);
            ss.addManagerPermission("removeStoreManager", store, "admin", itamar);
            Assert.IsTrue(ss.removeStoreManager(store, "zahi", niv));
            Assert.IsFalse(ss.removeStoreManager(store, "niv", zahi));
            Assert.IsTrue(ss.removeStoreManager(store, "admin", niv));
            Assert.AreEqual(store.getManagers().Count, 1);
        }
    }
}
