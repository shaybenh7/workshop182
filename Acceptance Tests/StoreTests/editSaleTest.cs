using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using wsep182.Domain;
using wsep182.services;
namespace Acceptance_Tests.StoreTests
{
    [TestClass]
    public class editSaleTest
    {

        private userServices us;
        private storeServices ss;
        private User zahi, itamar, niv, admin, admin1; //admin,itamar logedin
        private Store store;//itamar owner , niv manneger
        ProductInStore cola, sprite;
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
            saleId = ss.addSaleToStore(itamar, store, cola.getProductInStoreId(), 1, 1, "20.5.2018");
            raffleSale=ss.addSaleToStore(itamar, store, cola.getProductInStoreId(), 3, 1, "20.5.2018");

        }

        [TestMethod]
        public void SimpleEditSale()
        {
            ss.editSale(itamar, store, saleId, 10, "15.2.2019");
            Assert.AreEqual(10, SalesArchive.getInstance().getSale(saleId).Amount);
            Assert.AreEqual("15.2.2019", SalesArchive.getInstance().getSale(saleId).DueDate);
        }
        [TestMethod]
        public void SimpleEditRaffleSale()
        {
            ss.editSale(itamar, store, raffleSale, 10, "15.2.2019");
            Assert.AreEqual(10, SalesArchive.getInstance().getSale(raffleSale).Amount);
            Assert.AreEqual("15.2.2019", SalesArchive.getInstance().getSale(raffleSale).DueDate);
        }
        [TestMethod]
        public void EditSaleNegativeAmount()
        {
            Assert.IsFalse(ss.editSale(itamar, store, saleId, -1, "20/5/2018"));
        }
        [TestMethod]
        public void EditSaleZeroAmount()
        {
            Assert.IsTrue(ss.editSale(itamar, store, saleId, 0, "20/5/2018"));
        }

        [TestMethod]
        public void EditSaleBiggerThenOneAmount()
        {
            Assert.IsFalse(ss.editSale(itamar, store, saleId, 25, "15.2.2019"));
        }
        [TestMethod]
        public void EditSaleWithOwnerOfAnotherStore()
        {
            Assert.IsFalse(ss.editSale(admin, store, saleId, 1, "20/5/2018"));
        }
        [TestMethod]
        public void EditSaleWithNullParameters()
        {
            Assert.IsFalse(ss.editSale(null, store, saleId, 1, "20/5/2018"));
            Assert.IsFalse(ss.editSale(itamar, null, saleId, 1, "20/5/2018"));
            Assert.IsFalse(ss.editSale(itamar, store, saleId, 1, null));
        }
        [TestMethod]
        public void EditSaleWithDoesExistsSaleId()
        {
            Assert.IsFalse(ss.editSale(itamar, store, 9, 1, "20/5/2018"));
        }
        [TestMethod]
        public void AddSaleWithDateNotGood()
        {
            Assert.IsFalse(ss.editSale(itamar, store, saleId, 1, "HEY"));
        }
    }
    }