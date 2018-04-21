using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.SellTests
{
    [TestClass]
    public class viewCartTest
    {

        private userServices us;
        private storeServices ss;
        private sellServices sellS;
        private User zahi, itamar, niv, admin, admin1; //admin,itamar , niv logedin
        private Store store, store2;//itamar owner , niv manneger
        ProductInStore cola, sprite, chicken, cow;
        int saleId1, saleId2;
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
            sellS = sellServices.getInstance();

            admin = us.startSession();
            us.register(admin, "admin", "123456");
            us.login(admin, "admin", "123456");

            admin1 = us.startSession();
            us.register(admin1, "admin1", "123456");

            zahi = us.startSession();
            us.register(zahi, "zahi", "123456");
            us.login(zahi, "zahi", "123456");
            store2 = ss.createStore("Darkness Inc.", zahi);

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
            chicken = ss.addProductInStore("chicken", 50, 20, zahi, store2);
            cow = ss.addProductInStore("cow", 80, 40, zahi, store2);
            saleId1 = ss.addSaleToStore(itamar, store, cola.getProductInStoreId(), 1, 5, "20/5/2018");
            saleId2 = ss.addSaleToStore(zahi, store2, chicken.getProductInStoreId(), 1, 15, "20/7/2019");
        }

        [TestMethod]
        public void simpleViewCart()
        {
            LinkedList<Sale> saleList = ss.viewSalesByStore(store);
            sellS.addProductToCart(niv, saleList.First.Value, 1);
            LinkedList<UserCart> sc = sellS.viewCart(niv);
            Assert.AreEqual(sc.Count, 1);
            UserCart uc = sc.First.Value;
            Assert.AreEqual(uc.getUserName(), "niv");
            Assert.AreEqual(uc.getAmount(), 1);
        }
        [TestMethod]
        public void multipleViewCart()
        {

            LinkedList<Sale> saleList = ss.viewSalesByStore(store);
            LinkedList<Sale> saleList2 = ss.viewSalesByStore(store2);
            sellS.addProductToCart(niv, saleList.First.Value, 1);
            sellS.addProductToCart(niv, saleList2.First.Value, 5);
            LinkedList<UserCart> sc = sellS.viewCart(niv);
            Assert.AreEqual(sc.Count, 2);
            UserCart uc1 = sc.First.Value;
            UserCart uc2 = sc.Last.Value;
            Assert.AreEqual(uc1.getUserName(), "niv");
            Assert.AreEqual(uc1.getAmount(), 1);
            Assert.AreEqual(uc2.getUserName(), "niv");
            Assert.AreEqual(uc2.getAmount(), 5);
            Assert.AreEqual(uc1.getSaleId(), saleId1);
            Assert.AreEqual(uc2.getSaleId(), saleId2);
        }

        [TestMethod]
        public void viewEmptyCart()
        {
            LinkedList<UserCart> empty = sellS.viewCart(niv);
            Assert.AreEqual(empty.Count, 0);
        }


    }
}
