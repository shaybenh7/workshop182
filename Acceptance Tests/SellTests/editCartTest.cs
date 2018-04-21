using System;
using System.Collections.Generic;
using wsep182.services;
using wsep182.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Acceptance_Tests.SellTests
{
    [TestClass]
    public class editCartTest
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
            saleId2 = ss.addSaleToStore(itamar, store, sprite.getProductInStoreId(), 1, 20, "20/7/2019");
        }

        [TestMethod]
        public void simpleEditAmount()
        {
            LinkedList<Sale> saleList = ss.viewSalesByStore(store);
            sellS.addProductToCart(niv, saleList.First.Value, 2);
            Boolean check = sellS.editCart(niv, saleList.First.Value, 4);
            Assert.IsTrue(check);
            LinkedList<UserCart> nivCart = niv.getShoppingCart();
            UserCart uc = nivCart.First.Value;
            Assert.AreEqual(uc.getAmount(), 4);
        }
        [TestMethod]
        public void multipleEditAmount()
        {
            LinkedList<Sale> saleList = ss.viewSalesByStore(store);
            sellS.addProductToCart(niv, saleList.First.Value, 2);
            sellS.addProductToCart(niv, saleList.Last.Value, 5);
            Boolean check1 = sellS.editCart(niv, saleList.First.Value, 4);
            Boolean check2 = sellS.editCart(niv, saleList.Last.Value, 15);
            Assert.IsTrue(check1);
            Assert.IsTrue(check2);
            LinkedList<UserCart> nivCart = niv.getShoppingCart();
            UserCart uc1 = nivCart.First.Value;
            UserCart uc2 = nivCart.Last.Value;
            Assert.AreEqual(uc1.getAmount(), 4);
            Assert.AreEqual(uc2.getAmount(), 15);
        }

        [TestMethod]
        public void editNonExistingProduct()
        {
            Sale non = new Sale(15, 4, 1, 13, "");
            Boolean check1 = sellS.editCart(niv, non, 4);
            Boolean check2 = sellS.editCart(niv, null, 10);
            Boolean check3 = sellS.editCart(null, non, 4);
            Assert.IsFalse(check1);
            Assert.IsFalse(check2);
            Assert.IsFalse(check3);
        }
        [TestMethod]
        public void editInvalidAmount1()
        {
            LinkedList<Sale> saleList = ss.viewSalesByStore(store);
            sellS.addProductToCart(niv, saleList.First.Value, 2);
            Boolean check = sellS.editCart(niv, saleList.First.Value, saleList.First.Value.Amount + 2);
            Assert.IsFalse(check); 
        }
        [TestMethod]
        public void editInvalidAmount2()
        {
            LinkedList<Sale> saleList = ss.viewSalesByStore(store);
            sellS.addProductToCart(niv, saleList.First.Value, 2);
            Boolean check = sellS.editCart(niv, saleList.First.Value, -5);
            Assert.IsFalse(check);
        }
        [TestMethod]
        public void editWrongSaleId()
        {
            LinkedList<Sale> saleList = ss.viewSalesByStore(store);
            sellS.addProductToCart(niv, saleList.First.Value, 2);
            Boolean check = sellS.editCart(niv, saleList.Last.Value, 1);
            Assert.IsFalse(check);
        }




    }
}
