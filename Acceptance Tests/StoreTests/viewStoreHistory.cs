﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.StoreTests
{

    [TestClass]
    public class viewStoreHistory
    {
        private userServices us;
        private storeServices ss;
        private sellServices ses;
        private User zahi;

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
            ses = sellServices.getInstance();
            zahi = us.startSession();
            us.register(zahi, "zahi", "123456");
            us.login(zahi, "zahi", "123456");
        }

        //User is creating a store, adding products
        //another user is buying the products from him
        [TestMethod]
        public void simpleViewHistory()
        {
            User aviad = us.startSession();
            Assert.IsNotNull(aviad);
            Store store = ss.createStore("abowim", zahi);
            Assert.IsNotNull(store);
            Assert.IsTrue(us.register(aviad, "aviad", "123456"));
            Assert.IsTrue(us.login(aviad, "aviad", "123456"));
            ProductInStore pis = ss.addProductInStore("cola", 3.2, 10, zahi, store);
            Assert.IsNotNull(pis);
            int saleId = ss.addSaleToStore(zahi, store, pis.getProductInStoreId(), 1, 1, DateTime.Now.AddDays(10).ToString());
            LinkedList<Sale> sales = ses.viewSalesByProductInStoreId(pis);
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            Assert.IsTrue(ses.addProductToCart(aviad, sale, 2));
            LinkedList<UserCart> sc = ses.viewCart(aviad);
            Assert.IsTrue(sc.Count == 1);
            Assert.IsTrue(sc.First.Value.getSaleId() == saleId);
            Assert.IsTrue(ses.buyProducts(aviad, "1234", ""));
        }

        //with a guest user
        [TestMethod]
        public void TransactionContaining2DifferentStores()
        {
            User aviad = us.startSession();
            User vadim = us.startSession();
            Assert.IsTrue(us.register(vadim, "vadim", "123456"));
            Assert.IsTrue(us.login(vadim, "vadim", "123456"));

            Assert.IsNotNull(aviad);
            Store store = ss.createStore("abowim", zahi);
            Store store2 = ss.createStore("Russian liquor", vadim);
            Assert.IsNotNull(store);
            ProductInStore pis = ss.addProductInStore("cola", 3.2, 10, zahi, store);
            ProductInStore pis2 = ss.addProductInStore("vodka", 3.2, 10, vadim, store2);
            Assert.IsNotNull(pis);
            Assert.IsNotNull(pis2);
            int saleId = ss.addSaleToStore(zahi, store, pis.getProductInStoreId(), 1, 1, DateTime.Now.AddDays(10).ToString());
            int saleId2 = ss.addSaleToStore(vadim, store2, pis2.getProductInStoreId(), 1, 1, DateTime.Now.AddDays(10).ToString());

            LinkedList<Sale> sales = ses.viewSalesByProductInStoreId(pis);
            LinkedList<Sale> sales2 = ses.viewSalesByProductInStoreId(pis2);
            Assert.IsTrue(sales.Count == 1);
            Assert.IsTrue(sales2.Count == 1);

            Sale sale = sales.First.Value;
            Sale sale2 = sales.First.Value;

            Assert.IsTrue(ses.addProductToCart(aviad, sale, 1));
            Assert.IsTrue(ses.addProductToCart(aviad, sale2, 2));
            LinkedList<UserCart> sc = ses.viewCart(aviad);
            Assert.IsTrue(sc.Count == 2);
            Assert.IsTrue(ses.buyProducts(aviad, "1234", ""));
        }

        [TestMethod]
        public void TransactionWith2Sales()
        {
            User aviad = us.startSession();
            Assert.IsNotNull(aviad);
            Store store = ss.createStore("abowim", zahi);
            Assert.IsNotNull(store);
            Assert.IsTrue(us.register(aviad, "aviad", "123456"));
            Assert.IsTrue(us.login(aviad, "aviad", "123456"));
            ProductInStore pis = ss.addProductInStore("cola", 3.2, 10, zahi, store);
            Assert.IsNotNull(pis);
            int saleId = ss.addSaleToStore(zahi, store, pis.getProductInStoreId(), 1, 1, DateTime.Now.AddDays(10).ToString());
            int saleId2 = ss.addSaleToStore(zahi, store, pis.getProductInStoreId(), 1, 3, DateTime.Now.AddDays(10).ToString());
            LinkedList<Sale> sales = ses.viewSalesByProductInStoreId(pis);
            Assert.IsTrue(sales.Count == 2);
            Sale sale = sales.First.Value;
            Sale sale2 = sales.First.Next.Value;
            Assert.IsTrue(ses.addProductToCart(aviad, sale, 2));
            Assert.IsTrue(ses.addProductToCart(aviad, sale2, 1));

            LinkedList<UserCart> sc = ses.viewCart(aviad);
            Assert.IsTrue(sc.Count == 2);
            Assert.IsTrue(ses.buyProducts(aviad, "1234", ""));
        }

    }
}
