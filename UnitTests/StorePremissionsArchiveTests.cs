using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using System.Collections.Generic;
using wsep182.services;

namespace UnitTests
{
    [TestClass]
    public class StorePremissionsArchiveTests
    {
        User partislav;
        User manager1;
        User manager2;
        Store s;
        Store s2;
        StoreRole ownerRole;
        StoreRole ownerRole2;

        private userServices us;
        private storeServices ss;
        StorePremissionsArchive premissions;

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

            partislav = us.startSession();
            us.register(partislav, "partislav", "123456");
            us.login(partislav, "partislav", "123456");

            manager1 = us.startSession();
            us.register(manager1, "manager1", "123456");
            us.login(manager1, "manager1", "123456");

            manager2 = us.startSession();
            us.register(manager2, "manager2", "123456");
            us.login(manager2, "manager2", "123456");

            s = ss.createStore("makolet", partislav);
            s2 = ss.createStore("makolet", partislav);

            ownerRole = StoreRole.getStoreRole(s, partislav);
            ownerRole2 = StoreRole.getStoreRole(s, partislav);

            ownerRole.addStoreManager(partislav, s, "manager1");
            ownerRole.addStoreManager(partislav, s, "manager2");
            ownerRole.addStoreManager(partislav, s2, "manager1");
            ownerRole.addStoreManager(partislav, s2, "manager2");

        }

        
        [TestMethod]
        public void addANewPremission()
        {
            StorePremissionsArchive.getInstance().addManagerPermission(s.getStoreId(), "manager1", true);
            Assert.IsTrue(StorePremissionsArchive.getInstance().getAllPremissions(s.getStoreId(),manager1.getUserName()).getPrivileges().Count == 1);
            Assert.IsTrue(StorePremissionsArchive.getInstance().checkPrivilege(s.getStoreId(), manager1.getUserName(), "addManagerPermission"));
        }

        [TestMethod]
        public void addAnExistingPremission()
        {
            StorePremissionsArchive.getInstance().addManagerPermission(s.getStoreId(), "manager1", true);
            StorePremissionsArchive.getInstance().addManagerPermission(s.getStoreId(), "manager1", true);
            Assert.IsTrue(StorePremissionsArchive.getInstance().getAllPremissions(s.getStoreId(), manager1.getUserName()).getPrivileges().Count == 1);
            Assert.IsTrue(StorePremissionsArchive.getInstance().checkPrivilege(s.getStoreId(), manager1.getUserName(), "addManagerPermission"));
        }

        [TestMethod]
        public void reapprovePremission()
        {
            StorePremissionsArchive.getInstance().addManagerPermission(s.getStoreId(), "manager1", true);
            StorePremissionsArchive.getInstance().addManagerPermission(s.getStoreId(), "manager1", false);
            Assert.IsTrue(StorePremissionsArchive.getInstance().getAllPremissions(s.getStoreId(), manager1.getUserName()).getPrivileges().Count == 0);
            Assert.IsFalse(StorePremissionsArchive.getInstance().checkPrivilege(s.getStoreId(), manager1.getUserName(), "addManagerPermission"));
        }


        [TestMethod]
        public void DualUserAddPremissions()
        {
            StorePremissionsArchive.getInstance().addManagerPermission(s.getStoreId(), "manager1", true);
            StorePremissionsArchive.getInstance().addDiscount(s.getStoreId(), "manager2", true);
            Assert.IsTrue(StorePremissionsArchive.getInstance().getAllPremissions(s.getStoreId(), manager1.getUserName()).getPrivileges().Count == 1);
            Assert.IsTrue(StorePremissionsArchive.getInstance().getAllPremissions(s.getStoreId(), manager2.getUserName()).getPrivileges().Count == 1);
            Assert.IsTrue(StorePremissionsArchive.getInstance().checkPrivilege(s.getStoreId(), manager1.getUserName(), "addManagerPermission"));
            Assert.IsFalse(StorePremissionsArchive.getInstance().checkPrivilege(s.getStoreId(), manager2.getUserName(), "addManagerPermission"));
            Assert.IsTrue(StorePremissionsArchive.getInstance().checkPrivilege(s.getStoreId(), manager2.getUserName(), "addDiscount"));
            Assert.IsFalse(StorePremissionsArchive.getInstance().checkPrivilege(s.getStoreId(), manager1.getUserName(), "addDiscount"));
        }

        [TestMethod]
        public void DualStoreAddPremissions()
        {
            StorePremissionsArchive.getInstance().addManagerPermission(s.getStoreId(), "manager1", true);
            StorePremissionsArchive.getInstance().addManagerPermission(s2.getStoreId(), "manager1", true);
            Assert.IsTrue(StorePremissionsArchive.getInstance().getAllPremissions(s.getStoreId(), manager1.getUserName()).getPrivileges().Count == 1);
            Assert.IsTrue(StorePremissionsArchive.getInstance().getAllPremissions(s2.getStoreId(), manager1.getUserName()).getPrivileges().Count == 1);
            Assert.IsTrue(StorePremissionsArchive.getInstance().checkPrivilege(s.getStoreId(), manager1.getUserName(), "addManagerPermission"));
            Assert.IsTrue(StorePremissionsArchive.getInstance().checkPrivilege(s2.getStoreId(), manager1.getUserName(), "addManagerPermission"));
            StorePremissionsArchive.getInstance().addManagerPermission(s2.getStoreId(), "manager1", false);
            Assert.IsTrue(StorePremissionsArchive.getInstance().checkPrivilege(s.getStoreId(), manager1.getUserName(), "addManagerPermission"));
            Assert.IsFalse(StorePremissionsArchive.getInstance().checkPrivilege(s2.getStoreId(), manager1.getUserName(), "addManagerPermission"));
        }

        [TestMethod]
        public void getAllPremissionsWithNoPremissions()
        {
            Assert.IsTrue(StorePremissionsArchive.getInstance().getAllPremissions(s.getStoreId(), manager1.getUserName()).getPrivileges().Count == 0);
        }

    }
}
