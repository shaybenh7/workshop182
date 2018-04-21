using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using wsep182.Domain;
using wsep182.services;
namespace Acceptance_Tests.SellTests
{
    [TestClass]
    public class viewSalesByProductInStoreIdTests
    {
        private userServices us;
        private storeServices ss;
        private sellServices sell;
        private User zahi, itamar, niv, admin, admin1; //admin,itamar logedin
        private Store store;//itamar owner , niv manneger
        ProductInStore cola;
        int saleId;
        int raffleSale;

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
            sell = sellServices.getInstance();

            admin = us.startSession();
            us.register(admin, "admin", "123456");
            us.login(admin, "admin", "123456");

            admin1 = us.startSession();
            us.register(admin1, "admin1", "123456");

            zahi = us.startSession();
            us.register(zahi, "zahi", "123456");

            itamar = us.startSession();
            us.register(itamar, "itamar", "123456");
            us.login(itamar, "itamar", "123456");
            store = ss.createStore("Maria&Netta Inc.", itamar);

            niv = us.startSession();
            us.register(niv, "niv", "123456");
            us.login(niv, "niv", "123456");

            ss.addStoreManager(store, "niv", itamar);

            cola = ss.addProductInStore("cola", 3.2, 10, itamar, store);
            saleId = ss.addSaleToStore(itamar, store, cola.getProductInStoreId(), 1, 1, "20.5.2018");
            raffleSale = ss.addSaleToStore(itamar, store, cola.getProductInStoreId(), 3, 1, "20.5.2018");

        }

        [TestMethod]
        public void viewSaleByProductInStoreIdSimple()
        {
            Assert.AreEqual(2,sell.viewSalesByProductInStoreId(cola).Count);
        }
        [TestMethod]
        public void viewSaleByProductInStoreIdWithDelete()
        {
            ss.removeSaleFromStore(itamar, store, saleId);
            Assert.AreEqual(1, sell.viewSalesByProductInStoreId(cola).Count);
        }
    }
}

