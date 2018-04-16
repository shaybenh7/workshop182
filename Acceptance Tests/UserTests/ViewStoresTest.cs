using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.UserTests
{
    [TestClass]
    public class ViewStoresTest
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
        public void SimpleViewStore()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            us.register(session, "zahi", "123456");
            us.login(session, "zahi", "123456");
            storeServices ss = storeServices.getInstance();
            Store store = ss.createStore("abowim", session);
            LinkedList<Store> Lstore=us.viewStores();
            Assert.IsTrue(Lstore.Contains(store));
            Assert.AreEqual(Lstore.Count, 1);
        }

        [TestMethod]
        public void ViewStoreWhenThrerIsNoStores()
        {
            userServices us = userServices.getInstance();
            LinkedList<Store> Lstore = us.viewStores();
            Assert.AreEqual(Lstore.Count, 0);
        }
    }
}
