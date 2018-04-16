using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.StoreTests
{
    [TestClass]
    public class RemoveProductFromStoreTests
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
            us = userServices.getInstance();
            ss = storeServices.getInstance();
            zahi = us.startSession();
            us.register(zahi, "zahi", "123456");
        }

        [TestMethod]
        public void SimpleRemoveProduct()
        {
            us.login(zahi, "zahi", "123456");
            Store s = ss.createStore("abowim", zahi);
            Product p = new Product("cola");
            ProductInStore pis = ss.addProductInStore(p, 3.2, 10, zahi, s);
            bool result=ss.removeProductFromStore(s,pis, zahi);
            Assert.IsTrue(result);
            LinkedList<ProductInStore> LPIS=us.viewProductsInStores();
            Assert.AreEqual(LPIS.Count, 0);
        }

        [TestMethod]
        public void RemoveProductThatNotExist()
        {
            us.login(zahi, "zahi", "123456");
            Store s = ss.createStore("abowim", zahi);
            Product p = new Product("cola");
            ss.addProductInStore(p, 3.2, 10, zahi, s);
            ProductInStore pis = new ProductInStore(2, p, 4, 3, s);
            bool result = ss.removeProductFromStore(s,pis, zahi);
            Assert.IsFalse(result);
            LinkedList<ProductInStore> LPIS = us.viewProductsInStores();
            Assert.AreEqual(LPIS.Count, 1);
        }

        [TestMethod]
        public void AdminTryToRemoveProduct()
        {
            us.login(zahi, "zahi", "123456");
            Store s = ss.createStore("abowim", zahi);
            Product p = new Product("cola");
            ProductInStore pis = ss.addProductInStore(p, 3.2, 10, zahi, s);
            User admin = us.startSession();
            us.login(admin, "admin", "admin");
            bool result = ss.removeProductFromStore(s,pis, admin);
            Assert.IsFalse(result);
            LinkedList<ProductInStore> LPIS = us.viewProductsInStores();
            Assert.AreEqual(LPIS.Count, 1);
            Assert.IsTrue(LPIS.Contains(pis));
        }

        [TestMethod]
        public void GuestTryToRemoveProduct()
        {
            us.login(zahi, "zahi", "123456");
            Store s = ss.createStore("abowim", zahi);
            Product p = new Product("cola");
            ProductInStore pis = ss.addProductInStore(p, 3.2, 10, zahi, s);
            zahi.logOut();
            bool result = ss.removeProductFromStore(s,pis, zahi);
            Assert.IsFalse(result);
            LinkedList<ProductInStore> LPIS = us.viewProductsInStores();
            Assert.AreEqual(LPIS.Count, 1);
            Assert.IsTrue(LPIS.Contains(pis));
        }

        [TestMethod]
        public void CostumerTryToRemoveProduct()
        {
            us.login(zahi, "zahi", "123456");
            User aviad = us.startSession();
            us.register(aviad, "aviad", "123456");
            us.login(aviad, "aviad", "123456");
            Store s = ss.createStore("abowim", zahi);
            Product p = new Product("cola");
            ProductInStore pis = ss.addProductInStore(p, 3.2, 10, zahi, s);
            bool result = ss.removeProductFromStore(s, pis, aviad);
            Assert.IsFalse(result);
            LinkedList<ProductInStore> LPIS = us.viewProductsInStores();
            Assert.AreEqual(LPIS.Count, 1);
            Assert.IsTrue(LPIS.Contains(pis));
        }

        [TestMethod]
        public void OwnerOfOtherStoreTryToRemoveProduct()
        {
            us.login(zahi, "zahi", "123456");
            User aviad = us.startSession();
            us.register(aviad, "aviad", "123456");
            us.login(aviad, "aviad", "123456");
            Store s = ss.createStore("abowim", zahi);
            Store s2 = ss.createStore("Brohim", aviad);
            Product p = new Product("cola");
            ProductInStore pis = ss.addProductInStore(p, 3.2, 10, zahi, s);
            bool result = ss.removeProductFromStore(s, pis, aviad);
            Assert.IsFalse(result);
            LinkedList<ProductInStore> LPIS = us.viewProductsInStores();
            Assert.AreEqual(LPIS.Count, 1);
            Assert.IsTrue(LPIS.Contains(pis));
        }

        [TestMethod]
        public void nullTryToRemoveProduct()
        {
            us.login(zahi, "zahi", "123456");
            Store s = ss.createStore("abowim", zahi);
            Product p = new Product("cola");
            ProductInStore pis = ss.addProductInStore(p, 3.2, 10, zahi, s);
            bool result = ss.removeProductFromStore(s, pis, null);
            Assert.IsFalse(result);
            LinkedList<ProductInStore> LPIS = us.viewProductsInStores();
            Assert.AreEqual(LPIS.Count, 1);
            Assert.IsTrue(LPIS.Contains(pis));
        }

        [TestMethod]
        public void RemoveNullProduct()
        {
            us.login(zahi, "zahi", "123456");
            Store s = ss.createStore("abowim", zahi);
            Product p = new Product("cola");
            ProductInStore pis = ss.addProductInStore(p, 3.2, 10, zahi, s);
            bool result = ss.removeProductFromStore(s, null, zahi);
            Assert.IsFalse(result);
            LinkedList<ProductInStore> LPIS = us.viewProductsInStores();
            Assert.AreEqual(LPIS.Count, 1);
            Assert.IsTrue(LPIS.Contains(pis));
        }

        [TestMethod]
        public void RemoveNullProductFromNullStore()
        {
            us.login(zahi, "zahi", "123456");
            Store s = ss.createStore("abowim", zahi);
            Product p = new Product("cola");
            ProductInStore pis = ss.addProductInStore(p, 3.2, 10, zahi, s);
            bool result = ss.removeProductFromStore(null, pis, zahi);
            Assert.IsFalse(result);
            LinkedList<ProductInStore> LPIS = us.viewProductsInStores();
            Assert.AreEqual(LPIS.Count, 1);
            Assert.IsTrue(LPIS.Contains(pis));
        }


        [TestMethod]
        public void RemoveRemovedProductFromStore()
        {
            us.login(zahi, "zahi", "123456");
            Store s = ss.createStore("abowim", zahi);
            Product p = new Product("cola");
            Product p2 = new Product("sprite");
            ProductInStore pis = ss.addProductInStore(p, 3.2, 10, zahi, s);
            ProductInStore pis2 = ss.addProductInStore(p2, 3.2, 10, zahi, s);
            ss.removeProductFromStore(s, pis, zahi);
            bool result = ss.removeProductFromStore(s, pis, zahi);
            Assert.IsFalse(result);
            LinkedList<ProductInStore> LPIS = us.viewProductsInStores();
            Assert.AreEqual(LPIS.Count, 1);
            Assert.IsTrue(LPIS.Contains(pis2));
        }

    }
}
