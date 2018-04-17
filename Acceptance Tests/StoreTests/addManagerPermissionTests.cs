using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.StoreTests
{

    /*
      "addProductInStore":
       "removeProductFromStore":
       "addStoreManager":
       "removeStoreManager":
       "addManagerPermission":
       "removeManagerPermission":
    */

    [TestClass]
    public class addManagerPermissionTests
    {
        private userServices us;
        private storeServices ss;
        private User zahi;  // owner of store
        private User aviad; //manager of store
        private User shay;
        private User itamar; // not a real user
        private User niv; // guest
        private Store store;

        [TestInitialize]
        public void init()
        {
            ProductArchive.restartInstance();
            SalesArchive.restartInstance();
            storeArchive.restartInstance();
            UserArchive.restartInstance();
            UserCartsArchive.restartInstance();
            StorePremissionsArchive.restartInstance();

            us = userServices.getInstance();
            zahi = us.startSession();
            us.register(zahi, "zahi", "123456");
            us.login(zahi, "zahi", "123456");
            aviad = us.startSession();
            us.register(aviad, "aviad", "123456");
            us.login(aviad, "aviad", "123456");
            shay = us.startSession();
            us.register(shay, "shay", "123456");
            us.login(shay, "shay", "123456");
            itamar = new User("itamar", "123456");
            niv = us.startSession();
            us.register(niv, "niv", "123456");
            ss = storeServices.getInstance();
            store = ss.createStore("abowim", zahi);
            ss.addStoreManager(store, aviad, zahi);
            niv.logOut();
        }


        [TestMethod]
        public void addProductInStore()
        {
            ss.addProductInStore("cola", 10, 4, aviad, store);
            Assert.AreEqual(0, store.getProductsInStore().Count);
            ss.addManagerPermission("addProductInStore", store, aviad, zahi);
            ss.addProductInStore("cola", 10, 4, aviad, store);
            Assert.AreEqual(1, store.getProductsInStore().Count);
        }

        [TestMethod]
        public void editProductInStoreWithManagerPermission()
        {
            ProductInStore pis=ss.addProductInStore("cola", 10, 4, zahi, store);
            Assert.AreEqual(1, store.getProductsInStore().Count);
            ss.editProductInStore(aviad, store, pis, 13, 4.5);
            Assert.AreEqual(10, pis.getPrice());
            Assert.AreEqual(4, pis.getAmount());
            ss.addManagerPermission("editProductInStore", store, aviad, zahi);
            ss.editProductInStore(aviad, store, pis, 13, 4.5);
            Assert.AreEqual(4.5, pis.getPrice());
            Assert.AreEqual(13, pis.getAmount());
        }
        [TestMethod]
        public void removeProductFromStoreWithManagerPermission()
        {
            ProductInStore pis = ss.addProductInStore("cola", 10, 4, zahi, store);
            ss.removeProductFromStore(store, pis, aviad);
            Assert.AreEqual(1, store.getProductsInStore().Count);
            ss.addManagerPermission("removeProductFromStore", store, aviad, zahi);
            ss.removeProductFromStore(store, pis, aviad);
            Assert.AreEqual(0, store.getProductsInStore().Count);
        }
        

    }
}
