using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.StoreTests
{
    [TestClass]
    public class addProductInStoreTest
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
        public void SimpleAddProduct()
        {
            Store s = ss.createStore("abowim", zahi);
            ProductInStore pis=ss.addProductInStore("cola", 3.2, 10, zahi, s);
            Assert.AreEqual(pis.getPrice(), 3.2);
            Assert.AreEqual(pis.getAmount(), 10);
            Assert.AreEqual(pis.getStore().getStoreId(), s.getStoreId());
            LinkedList<ProductInStore> pList=s.getProductsInStore();
            Assert.IsTrue(pList.Contains(pis));
            Assert.AreEqual(pList.Count, 1);

        }

        [TestMethod]
        public void AddProductByCostumer()
        {
            User aviad = us.startSession();
            us.register(aviad, "aviad", "123456");
            us.login(aviad, "aviad", "123456");
            Store s = ss.createStore("abowim", zahi);
            ProductInStore pis = ss.addProductInStore("cola", 3.2, 10, aviad, s);
            Assert.IsNull(pis);
            Assert.AreEqual(s.getProductsInStore().Count, 0);
        }


        [TestMethod]
        public void AddProductTwice()
        {
            Store s = ss.createStore("abowim", zahi);
            ProductInStore pis = ss.addProductInStore("cola", 3.2, 10, zahi, s);
            ProductInStore pis2 = ss.addProductInStore("cola", 3.2, 10, zahi, s);
            Assert.AreEqual(pis.getPrice(), 3.2);
            Assert.AreEqual(pis.getAmount(), 10);
            Assert.AreEqual(pis.getStore().getStoreId(), s.getStoreId());
            LinkedList<ProductInStore> pList = s.getProductsInStore();
            Assert.IsTrue(pList.Contains(pis));
            Assert.AreEqual(pList.Count, 1);
            Assert.IsNull(pis2);
        }


        [TestMethod]
        public void AddProductWithNegativeAmount()
        {
            Store s = ss.createStore("abowim", zahi);
            ProductInStore pis = ss.addProductInStore("cola", 3.2, -31, zahi, s);
            Assert.IsNull(pis);
            Assert.AreEqual(s.getProductsInStore().Count, 0);
        }

        [TestMethod]
        public void AddProductWithNegativePrice()
        {
            Store s = ss.createStore("abowim", zahi);
            ProductInStore pis = ss.addProductInStore("cola", -3, 31, zahi, s);
            Assert.IsNull(pis);
            Assert.AreEqual(s.getProductsInStore().Count, 0);
        }

        [TestMethod]
        public void AddProductWithZeroPrice()
        {
            Store s = ss.createStore("abowim", zahi);
            ProductInStore pis = ss.addProductInStore("cola", 0, 31, zahi, s);
            Assert.IsNull(pis);
            Assert.AreEqual(s.getProductsInStore().Count, 0);
        }

        [TestMethod]
        public void AddProductWithZeroAmount()
        {
            Store s = ss.createStore("abowim", zahi);
            ProductInStore pis = ss.addProductInStore("cola", 3.2, 0, zahi, s);
            Assert.AreEqual(s.getProductsInStore().Count, 0);
            Assert.IsNull(pis);
            Assert.AreEqual(s.getProductsInStore().Count, 0);
        }


        [TestMethod]
        public void AddProductWithEmptyName()
        {
            Store s = ss.createStore("abowim", zahi);
            ProductInStore pis = ss.addProductInStore("", 3.2, 31, zahi, s);
            Assert.IsNull(pis);
            Assert.AreEqual(s.getProductsInStore().Count, 0);
        }

        [TestMethod]
        public void AddProductWithOnlySpacesName()
        {
            Store s = ss.createStore("abowim", zahi);
            ProductInStore pis = ss.addProductInStore("     ", 3.2, 31, zahi, s);
            Assert.IsNull(pis);
            Assert.AreEqual(s.getProductsInStore().Count, 0);
        }


        [TestMethod]
        public void AddProductInStoreWithNullProduct()
        {
            Store s = ss.createStore("abowim", zahi);
            ProductInStore pis = ss.addProductInStore(null, 3.2, 31, null, s);
            Assert.IsNull(pis);
            Assert.AreEqual(s.getProductsInStore().Count, 0);
        }

        [TestMethod]
        public void AddProductWithSpacesInName()
        {
            Store s = ss.createStore("abowim", zahi);
            ProductInStore pis = ss.addProductInStore("coca cola", 3.2, 10, zahi, s);
            Assert.AreEqual(pis.getPrice(), 3.2);
            Assert.AreEqual(pis.getAmount(), 10);
            Assert.AreEqual(pis.getStore().getStoreId(), s.getStoreId());
            LinkedList<ProductInStore> pList = s.getProductsInStore();
            Assert.IsTrue(pList.Contains(pis));
            Assert.AreEqual(pList.Count, 1);
        }

        [TestMethod]
        public void AddProductToNoneExistingStore()
        {
            Store s = new Store(3,"coca", zahi);
            ProductInStore pis = ss.addProductInStore("cola", 3.2, 10, zahi, s);
            Assert.IsNull(pis);
            LinkedList<ProductInStore> pList = s.getProductsInStore();
            Assert.IsTrue(pList.Count == 0); //store is not exist in archive
        }

        [TestMethod]
        public void AddProductToStoreByGuest()
        {
            Store s = ss.createStore("abowim", zahi);
            zahi.logOut();
            ProductInStore pis = ss.addProductInStore("cola", 3.2, 10, zahi, s);
            Assert.IsNull(pis);
            LinkedList<ProductInStore> pList = s.getProductsInStore();
            Assert.AreEqual(pList.Count,0); 
        }

    }
}
