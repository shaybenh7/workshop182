using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.StoreTests
{
    [TestClass]
    public class removeSaleFromStoreTests
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
        public void simpleRemoveSale()
        {
            Assert.IsTrue(ss.removeSaleFromStore(zahi, store, colaSale.SaleId));
            Assert.AreEqual(ss.viewSalesByStore(store).Count,0);
        }

        [TestMethod]
        public void RemoveSaleWithNullSession()
        {
            Assert.IsFalse(ss.removeSaleFromStore(null, store, colaSale.SaleId));
            Assert.AreEqual(ss.viewSalesByStore(store).Count, 1);
        }

        [TestMethod]
        public void RemoveSaleWithNullStore()
        {
            Assert.IsFalse(ss.removeSaleFromStore(zahi, null, colaSale.SaleId));
            Assert.AreEqual(ss.viewSalesByStore(store).Count, 1);
        }

        [TestMethod]
        public void RemoveSaleWithNoneExistingSaleId()
        {
            Assert.IsFalse(ss.removeSaleFromStore(zahi, store, 1000));
            Assert.AreEqual(ss.viewSalesByStore(store).Count, 1);
        }
    }
}
