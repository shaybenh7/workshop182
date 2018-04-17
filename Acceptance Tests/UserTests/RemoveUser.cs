using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.UserTests
{
    [TestClass]
    public class RemoveUser
    {
        private userServices us;
        private storeServices ss;
        private User zahi, itamar, niv, admin, admin1;
        private Store store;

        [TestInitialize]
        public void init()
        {
            ProductArchive.restartInstance();
            SalesArchive.restartInstance();
            storeArchive.restartInstance();
            UserArchive.restartInstance();
            UserCartsArchive.restartInstance();
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
            itamar.login("itamar", "123456");
            store = itamar.createStore("Maria&Netta Inc.");

            niv = us.startSession();
            us.register(niv, "niv", "123456");
            

            ss.addStoreManager(store, niv, itamar);
            


        }

        [TestMethod]
        public void SimpleRemoveUser()
        {
            us.login(admin, "admin", "123456");
            Assert.IsTrue(admin.removeUser("zahi"));
            Assert.IsFalse(us.login(zahi, "zahi", "123456"));
        }
        [TestMethod]
        public void RemoveUserAdminNotLogin()
        {
            Assert.IsFalse(admin1.removeUser("zahi"));
            Assert.IsTrue(us.login(zahi, "zahi", "123456"));
        }
        [TestMethod]
        public void AdminRemoveHimself()
        {
            Assert.IsFalse(admin.removeUser("admin"));
            admin.logOut();
            Assert.IsTrue(admin.login("admin", "123456"));
        }
        [TestMethod]
        public void UserRemoveHimself()
        {
            us.login(zahi, "zahi", "123456");
            Assert.IsFalse(zahi.removeUser("zahi"));
            zahi.logOut();
            Assert.IsTrue(admin.login("zahi", "123456"));
        }
        [TestMethod]
        public void UserRemoveUser()
        {
            us.login(zahi, "zahi", "123456");
            User shay = us.startSession();
            us.register(shay, "shay", "123456");
            Assert.IsFalse(zahi.removeUser("shay"));
            Assert.IsTrue(shay.login("shay", "123456"));
        }
        [TestMethod]
        public void AdminRemoveAdmin()
        {
            Assert.IsTrue(admin.removeUser("admin1"));
            Assert.IsFalse(admin1.login("admin1", "123456"));
        }
        [TestMethod]
        public void AdminRemoveAdminThatTryToRemoveUser()
        {
            us.login(admin1, "admin1", "123456");
            Assert.IsTrue(admin.removeUser("admin1"));
            Assert.IsFalse(admin1.removeUser("zahi"));
            Assert.IsTrue(zahi.login("zahi", "123456"));
            User setion = us.startSession();
            Assert.IsFalse(setion.login("admin1", "123456"));
        }
        [TestMethod]
        public void RemoveUserTwice()
        {
            Assert.IsTrue(admin.removeUser("zahi"));
            Assert.IsFalse(admin.removeUser("zahi"));
            Assert.IsFalse(zahi.login("zahi", "123456"));
        }
        [TestMethod]
        public void RemoveUserThatNotExist()
        {
            Assert.IsFalse(admin.removeUser("shay"));
        }
        [TestMethod]
        public void RemoveManneger()
        {
            Assert.IsTrue(admin.removeUser("niv"));
            Assert.IsFalse(niv.login("niv", "123456"));
            Assert.AreEqual(store.getManagers().Count, 0);
        }
        [TestMethod]
        public void RemoveCreatoreOwner()
        {
            Assert.IsFalse(admin.removeUser("itamar"));
            Assert.IsTrue(itamar.login("itamar", "123456"));
            Assert.AreEqual(store.getOwners().Count, 1);
        }
        [TestMethod]
        public void RemoveNotCreatoreOwner()
        {
            ss.addStoreOwner(store, zahi, itamar);
            Assert.IsTrue(admin.removeUser("zahi"));
            Assert.IsFalse(zahi.login("zahi", "123456"));
            Assert.AreEqual(store.getOwners().Count, 1);
        }
        [TestMethod]
        public void RemoveCreatoreOwnerWithAnotherOwner()
        {
            ss.addStoreOwner(store, zahi, itamar);
            Assert.IsFalse(admin.removeUser("itamar"));
            Assert.IsTrue(itamar.login("itamar", "123456"));
            Assert.AreEqual(store.getOwners().Count, 2);
        }
        [TestMethod]
        public void RemoveCreatoreOwnerWithAnotherManneger()
        {
            Assert.IsFalse(admin.removeUser("itamar"));
            Assert.IsTrue(itamar.login("itamar", "123456"));
            Assert.AreEqual(store.getOwners().Count, 2);
        }
        [TestMethod]
        public void RemoveMannegerInFewStores()
        {
            Store store2 = admin.createStore("admin store");
            ss.addStoreManager(store2, niv, admin);
            Assert.IsTrue(admin.removeUser("niv"));
            Assert.IsFalse(niv.login("niv", "123456"));
            Assert.AreEqual(store.getManagers().Count, 0);
            Assert.AreEqual(store2.getManagers().Count, 0);
        }

    }
}
