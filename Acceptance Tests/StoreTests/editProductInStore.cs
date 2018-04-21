using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.StoreTests
{
    [TestClass]
    public class editProductInStore
    {
        private userServices us;
        private storeServices ss;
        private User zahi;
        private Store store;
        private ProductInStore cola;

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
            store = ss.createStore("abowim", zahi);
            cola = ss.addProductInStore("cola", 3.2, 10, zahi, store);

        }
        [TestMethod]
        public void simpleEditProductInStore()
        {
            Assert.IsTrue(ss.editProductInStore(zahi, store, cola, 100, 5.2));
            Assert.AreEqual(cola.getAmount(),100);
            Assert.AreEqual(cola.getPrice(), 5.2);
        }

        [TestMethod]
        public void EditProductInStoreWithNullSession()
        {
            Assert.IsFalse(ss.editProductInStore(null, store, cola, 100, 5.2));
            Assert.AreEqual(cola.getAmount(), 10);
            Assert.AreEqual(cola.getPrice(), 3.2);
        }

        [TestMethod]
        public void EditProductInStoreWithNullStore()
        {
            Assert.IsFalse(ss.editProductInStore(zahi, null, cola, 100, 5.2));
            Assert.AreEqual(cola.getAmount(), 10);
            Assert.AreEqual(cola.getPrice(), 3.2);
        }

        [TestMethod]
        public void EditProductInStoreWithNullProductInStore()
        {
            Assert.IsFalse(ss.editProductInStore(zahi, store, null, 100, 5.2));
            Assert.AreEqual(cola.getAmount(), 10);
            Assert.AreEqual(cola.getPrice(), 3.2);
        }

        [TestMethod]
        public void EditProductInStoreWithNegativeAmount()
        {
            Assert.IsFalse(ss.editProductInStore(zahi, store, cola, -1, 5.2));
            Assert.AreEqual(cola.getAmount(), 10);
            Assert.AreEqual(cola.getPrice(), 3.2);
        }

        [TestMethod]
        public void EditProductInStoreWithZeroAmount()
        {
            Assert.IsTrue(ss.editProductInStore(zahi, store, cola, 0, 5.2));
            Assert.AreEqual(cola.getAmount(), 0);
            Assert.AreEqual(cola.getPrice(), 5.2);
        }

        [TestMethod]
        public void EditProductInStoreWithNegativePrice()
        {
            Assert.IsFalse(ss.editProductInStore(zahi, store, cola, 100, -4));
            Assert.AreEqual(cola.getAmount(), 10);
            Assert.AreEqual(cola.getPrice(), 3.2);
        }

    }
}
