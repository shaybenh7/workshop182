using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.StoreTests
{
    [TestClass]
    public class removeDiscountTests
    {
        private userServices us;
        private storeServices ss;
        private User zahi;
        private Store store;
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

            ss.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 2, DateTime.Now.AddDays(5).ToString());

            LinkedList<Sale> SL = ss.viewSalesByStore(store);
            foreach (Sale sale in SL)
            {
                if (sale.ProductInStoreId == cola.getProductInStoreId())
                {
                    colaSale = sale;
                }
            }
            ss.addDiscount(cola, 10, DateTime.Now.AddDays(5).ToString(), zahi, store);
        }

        [TestMethod]
        public void simpleRemoveDiscount()
        {
            Assert.IsTrue(ss.removeDiscount(cola, store, zahi));
            Assert.AreEqual(colaSale.getPriceAfterDiscount(1), 10);
        }

        [TestMethod]
        public void RemoveDiscountWithNullProduct()
        {
            Assert.IsFalse(ss.removeDiscount(null, store, zahi));
            Assert.AreEqual(colaSale.getPriceAfterDiscount(1), 9);
        }

        [TestMethod]
        public void RemoveDiscountWithNullStore()
        {
            Assert.IsFalse(ss.removeDiscount(cola, null, zahi));
            Assert.AreEqual(colaSale.getPriceAfterDiscount(1), 9);
        }

        [TestMethod]
        public void RemoveDiscountWithNullSession()
        {
            Assert.IsFalse(ss.removeDiscount(cola, store, null));
            Assert.AreEqual(colaSale.getPriceAfterDiscount(1), 9);
        }


    }
}
