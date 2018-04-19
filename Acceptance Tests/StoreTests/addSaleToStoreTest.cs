using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.SaleTests
{
    [TestClass]
    public class addSaleToStoreTest
    {

        private userServices us;
        private storeServices ss;
        private User zahi, itamar, niv, admin, admin1; //admin,itamar logedin
        private Store store;//itamar owner , niv manneger
        ProductInStore cola, sprite;

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
            sprite = ss.addProductInStore("sprite", 5.3, 20, itamar, store);

        }

        [TestMethod]
        public void SimpleAddSale()
        {
           Assert.IsTrue(ss.addSaleToStore(itamar, store, cola.getProductInStoreId(), 1, 1, "20/5/2018")>-1);
        }
        [TestMethod]
        public void SimpleAddRaffleSale()
        {
            Assert.IsTrue(ss.addSaleToStore(itamar, store, cola.getProductInStoreId(), 3, 1, "20/5/2018") > -1);
        }
        [TestMethod]
        public void AddTypeNotSupportedSale()
        {
            Assert.IsFalse(ss.addSaleToStore(itamar, store, cola.getProductInStoreId(), 2, 1, "20/5/2018") > -1);
        }
        [TestMethod]
        public void AddSaleNegativeAmount()
        {
            Assert.IsFalse(ss.addSaleToStore(itamar, store, cola.getProductInStoreId(), 1, -1, "20/5/2018") > -1);
        }
        [TestMethod]
        public void AddSaleZeroAmount()
        {
            Assert.IsTrue(ss.addSaleToStore(itamar, store, cola.getProductInStoreId(), 1, 0, "20/5/2018") > -1);
        }
        [TestMethod]
        public void AddSaleTwiceSameProduct()
        {
            Assert.IsTrue(ss.addSaleToStore(itamar, store, cola.getProductInStoreId(), 1, 2, "20/5/2018") > -1);
            Assert.IsTrue(ss.addSaleToStore(itamar, store, cola.getProductInStoreId(), 1, 3, "20/5/2018") > -1);
        }
        [TestMethod]
        public void AddSaleBiggerThenOneAmount()
        {
            Assert.IsTrue(ss.addSaleToStore(itamar, store, cola.getProductInStoreId(), 1, 2, "20/5/2018") > -1);
            Assert.IsFalse(ss.addSaleToStore(itamar, store, cola.getProductInStoreId(), 3, 2, "20/5/2018") > -1);
        }
        [TestMethod]
        public void AddSaleWithProductNotInStore()
        {
            Store store2 = ss.createStore("admin store", admin);
            ProductInStore milk = ss.addProductInStore("milk", 3.2, 10, admin, store2);
            Assert.IsFalse(ss.addSaleToStore(itamar, store, milk.getProductInStoreId(), 1, 1, "20/5/2018") > -1);
        }
        [TestMethod]
        public void AddSaleWithOwnerOfAnotherStore()
        {
            Store store2 = ss.createStore("admin store", admin);
            ProductInStore milk = ss.addProductInStore("milk", 3.2, 10, admin, store2);
            Assert.IsFalse(ss.addSaleToStore(admin, store, milk.getProductInStoreId(), 1, 1, "20/5/2018") > -1);
        }
        [TestMethod]
        public void AddSaleWithNullParameters()
        {
            Store store2 = ss.createStore("admin store", admin);
            ProductInStore milk = ss.addProductInStore("milk", 3.2, 10, admin, store2);
            Assert.IsNull(ss.addSaleToStore(null, store2, milk.getProductInStoreId(), 1, 1, "20/5/2018") );
            Assert.IsNull(ss.addSaleToStore(admin, null, milk.getProductInStoreId(), 1, 1, "20/5/2018") );
            Assert.IsNull(ss.addSaleToStore(admin, store2, milk.getProductInStoreId(), 1, 1, null));
        }



    }
}
