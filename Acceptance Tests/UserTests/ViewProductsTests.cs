using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.UserTests
{
    [TestClass]
    public class ViewProductsTests
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
        public void SimpleViewProductWithOneProduct()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            us.register(session, "zahi", "123456");
            us.login(session, "zahi", "123456");
            storeServices ss = storeServices.getInstance();
            Store s = ss.createStore("abowim", session);
            ProductInStore pis=ss.addProductInStore("cola", 3.2, 10, session, s);
            LinkedList<ProductInStore> pisList = us.viewProductsInStore(s);
            LinkedList<ProductInStore> piStorsList = us.viewProductsInStores();
            Assert.IsTrue(pisList.Contains(pis));
            Assert.AreEqual(pisList.Count, 1);
            Assert.IsTrue(piStorsList.Contains(pis));
            Assert.AreEqual(piStorsList.Count, 1);
        }


        [TestMethod]
        public void ViewProductWhenThereIsNoProducts()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            us.register(session, "zahi", "123456");
            us.login(session, "zahi", "123456");
            storeServices ss = storeServices.getInstance();
            Store s = ss.createStore("abowim", session);
            Product p = new Product("cola");
            LinkedList<ProductInStore> pisList = us.viewProductsInStore(s);
            LinkedList<ProductInStore> piStorsList = us.viewProductsInStores();
            Assert.AreEqual(pisList.Count, 0);
            Assert.AreEqual(piStorsList.Count, 0);

        }

        [TestMethod]
        public void SimpleViewProductWithTwoProducts()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            us.register(session, "zahi", "123456");
            us.login(session, "zahi", "123456");
            storeServices ss = storeServices.getInstance();
            Store s = ss.createStore("abowim", session);
            ProductInStore pis = ss.addProductInStore("cola", 3.2, 10, session, s);
            ProductInStore pis2 = ss.addProductInStore("sprite", 3.2, 10, session, s);
            LinkedList<ProductInStore> pisList = us.viewProductsInStore(s);
            LinkedList<ProductInStore> piStorsList = us.viewProductsInStores();
            Assert.IsTrue(pisList.Contains(pis));
            Assert.IsTrue(pisList.Contains(pis2));
            Assert.AreEqual(pisList.Count, 2);
            Assert.IsTrue(piStorsList.Contains(pis));
            Assert.IsTrue(piStorsList.Contains(pis2));
            Assert.AreEqual(piStorsList.Count, 2);
        }

        [TestMethod]
        public void ViewProductInMultipleStores()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            us.register(session, "zahi", "123456");
            us.login(session, "zahi", "123456");
            User aviad = us.startSession();
            us.register(aviad, "aviad", "123456");
            us.login(aviad, "aviad", "123456");
            storeServices ss = storeServices.getInstance();
            Store s = ss.createStore("abowim", session);
            Store s2 = ss.createStore("bro's", aviad);
            ProductInStore pis = ss.addProductInStore("cola", 3.2, 10, session, s);
            ProductInStore pis2 = ss.addProductInStore("sprite", 3.2, 10, session, s);
            ProductInStore pis3 = ss.addProductInStore("milk", 3.2, 10, aviad, s2);
            LinkedList<ProductInStore> pisList = us.viewProductsInStore(s);
            LinkedList<ProductInStore> pisList2 = us.viewProductsInStore(s2);
            LinkedList<ProductInStore> piStorsList = us.viewProductsInStores();

            Assert.IsTrue(pisList.Contains(pis));
            Assert.IsTrue(pisList.Contains(pis2));
            Assert.IsFalse(pisList.Contains(pis3));
            Assert.IsFalse(pisList2.Contains(pis));
            Assert.IsFalse(pisList2.Contains(pis2));
            Assert.IsTrue(pisList2.Contains(pis3));

            Assert.AreEqual(pisList.Count, 2);
            Assert.AreEqual(pisList2.Count, 1);
            Assert.AreEqual(piStorsList.Count, 3);
        }

    }
}
