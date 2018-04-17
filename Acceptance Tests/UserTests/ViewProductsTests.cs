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
            Assert.IsTrue(pisList.Contains(pis));
            Assert.AreEqual(pisList.Count, 1);
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
            Assert.AreEqual(pisList.Count, 0);
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
            Assert.IsTrue(pisList.Contains(pis));
            Assert.IsTrue(pisList.Contains(pis2));
            Assert.AreEqual(pisList.Count, 2);
        }

    }
}
