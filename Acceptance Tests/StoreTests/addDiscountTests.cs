using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.StoreTests
{
    [TestClass]
    public class addDiscountTests
    {

        private userServices us;
        private storeServices ss;
        private User zahi;
        private Store store;//itamar owner , niv manneger
        ProductInStore cola;
        Sale colaSale;

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

            store = ss.createStore("Abowim", zahi);

            cola = ss.addProductInStore("cola", 10, 100, zahi, store);

            ss.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 2, "20/5/2018");

            LinkedList<Sale> SL = ss.viewSalesByStore(store);
            foreach (Sale sale in SL)
            {
                if (sale.ProductInStoreId == cola.getProductInStoreId())
                {
                    colaSale = sale;
                }
            }
        }

        [TestMethod]
        public void simpleAddDiscount()
        {
            Assert.IsTrue(ss.addDiscount(cola, 10, "20/6/2018", zahi, store));
            Assert.AreEqual(colaSale.getPriceAfterDiscount(1), 9);
        }

        [TestMethod]
        public void simpleAddDiscountMoreThan100()
        {
            Assert.IsFalse(ss.addDiscount(cola, 101, "20/6/2018", zahi, store));
            Assert.AreEqual(colaSale.getPriceAfterDiscount(1), 10);
        }

        [TestMethod]
        public void simpleAddDiscountWithDueDateAllreadyPassed()
        {
            Assert.IsFalse(ss.addDiscount(cola, 10, "20/6/2016", zahi, store));
            Assert.AreEqual(colaSale.getPriceAfterDiscount(1), 10);
        }

        [TestMethod]
        public void simpleAddDiscountWithDueDateNull()
        {
            Assert.IsFalse(ss.addDiscount(cola, 10, null, zahi, store));
            Assert.AreEqual(colaSale.getPriceAfterDiscount(1), 10);
        }

        [TestMethod]
        public void simpleAddDiscountWithNullSession()
        {
            Assert.IsFalse(ss.addDiscount(cola, 10, "20/6/2016", null, store));
            Assert.AreEqual(colaSale.getPriceAfterDiscount(1), 10);
        }

        [TestMethod]
        public void simpleAddDiscountWithNullStore()
        {
            Assert.IsFalse(ss.addDiscount(cola, 10, "20/6/2016", zahi, null));
            Assert.AreEqual(colaSale.getPriceAfterDiscount(1), 10);
        }

        [TestMethod]
        public void AddDiscountTwice()
        {
            Assert.IsTrue(ss.addDiscount(cola, 10, "20/6/2018", zahi, store));
            Assert.IsFalse(ss.addDiscount(cola, 20, "20/6/2018", zahi, store));
            Assert.AreEqual(colaSale.getPriceAfterDiscount(1), 9);
        }

    }
}
