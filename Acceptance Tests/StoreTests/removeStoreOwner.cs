using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.StoreTests
{
    [TestClass]
    public class removeStoreOwner
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
            zahi.login("zahi", "123456");

            itamar = us.startSession();
            us.register(itamar, "itamar", "123456");
            itamar.login("itamar", "123456");
            store = itamar.createStore("Maria&Netta Inc.");

            niv = us.startSession();
            us.register(niv, "niv", "123456");
            niv.login("niv", "123456");

            ss.addStoreManager(store, niv, itamar);

        }

        [TestMethod]
        public void SimpleRemoveStoreOwner()
        {
            ss.addStoreOwner(store, zahi, itamar);
            Assert.IsTrue(ss.removeStoreOwner(store, zahi, itamar));
            Assert.AreEqual(ss.getOwners(store).Count, 1);  
        }
        [TestMethod]
        public void OwnerRemoveHimself()
        {
            ss.addStoreOwner(store, zahi, itamar);
            Assert.IsFalse(ss.removeStoreOwner(store, zahi, zahi));
            Assert.IsFalse(ss.removeStoreOwner(store, itamar, itamar));
            Assert.AreEqual(ss.getOwners(store).Count, 2);
        }
        [TestMethod]
        public void OwnerRemoveHimselfWhenThierIsOneOwner()
        {
            Assert.IsFalse(ss.removeStoreOwner(store, itamar, itamar));
            Assert.AreEqual(ss.getOwners(store).Count, 1);
        }
        [TestMethod]
        public void MannegerRemoveOwner()
        {
            ss.addStoreOwner(store, zahi, itamar);
            Assert.IsFalse(ss.removeStoreOwner(store, zahi, niv));
            Assert.AreEqual(ss.getOwners(store).Count, 2);
        }
        [TestMethod]
        public void UserRemoveOwner()
        {
            User aviad = us.startSession();
            aviad.register("aviad", "123456");
            us.login(aviad, "aviad", "123456");
            ss.addStoreOwner(store, zahi, itamar);
            Assert.IsFalse(ss.removeStoreOwner(store, zahi, aviad));
            Assert.AreEqual(ss.getOwners(store).Count, 2);
        }
        [TestMethod]
        public void GusetRemoveOwner()
        {
            ss.addStoreOwner(store, zahi, itamar);
            Assert.IsFalse(ss.removeStoreOwner(store, zahi, niv));
            Assert.AreEqual(ss.getOwners(store).Count, 2);
        }
        [TestMethod]
        public void RemoveOwnerThatNotOwner()
        {
            ss.addStoreOwner(store, zahi, itamar);
            Assert.IsFalse(ss.removeStoreOwner(store, niv, itamar));
            Assert.AreEqual(ss.getOwners(store).Count, 2);
        }
        [TestMethod]
        public void RemoveOwnerThatNotExist()
        {
            User aviad = new User("aviad", "123456");
            Assert.IsFalse(ss.removeStoreOwner(store, aviad, itamar));
            Assert.AreEqual(ss.getOwners(store).Count, 1);
        }
        [TestMethod]
        public void RemoveOwnerStoreNotExist()
        {
            Store store2 = new Store(2, "abow", zahi);
            Assert.IsFalse(ss.removeStoreOwner(store2, niv, zahi));
        }


    }
}
