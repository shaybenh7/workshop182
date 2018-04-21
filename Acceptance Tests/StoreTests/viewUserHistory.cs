using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.StoreTests
{

    [TestClass]
    public class viewUserHistory
    {
        private userServices us;
        private storeServices ss;
        private sellServices ses;
        private User zahi;
        private User admin;

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
            admin = us.startSession();
            us.register(admin, "admin", "123456");
            us.login(admin, "admin", "123456");
        }



        //User is creating a store, adding products
        //another user is buying the products from him
        [TestMethod]
        public void simpleViewUserHistory()
        {
            User aviad = us.startSession();
            Assert.IsNotNull(aviad);
            Store store = ss.createStore("abowim", zahi);
            Assert.IsNotNull(store);
            Assert.IsTrue(us.register(aviad, "aviad", "123456"));
            Assert.IsTrue(us.login(aviad, "aviad", "123456"));
            ProductInStore pis = ss.addProductInStore("cola", 3.2, 10, zahi, store);
            Assert.IsNotNull(pis);
            int saleId = ss.addSaleToStore(zahi, store, pis.getProductInStoreId(), 1, 8, DateTime.Now.AddDays(10).ToString());
            LinkedList<Sale> sales = ses.viewSalesByProductInStoreId(pis);
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            Assert.IsTrue(ses.addProductToCart(aviad, sale, 2));
            LinkedList<UserCart> sc = ses.viewCart(aviad);
            Assert.IsTrue(sc.Count == 1);
            Assert.IsTrue(sc.First.Value.getSaleId() == saleId);
            Assert.IsTrue(ses.buyProducts(aviad, "1234", ""));
            LinkedList<Purchase> historyList = ss.viewUserHistory(admin, "aviad");
            Assert.IsTrue(historyList.Count == 1);
            Assert.IsTrue(historyList.First.Value.ProductId == pis.getProduct().getProductId());
            Assert.IsTrue(historyList.First.Value.Amount == 2);
        }


        [TestMethod]
        public void emptyViewUserHistory()
        {
            User aviad = us.startSession();
            Assert.IsTrue(us.register(aviad, "aviad", "123456"));
            Assert.IsTrue(us.login(aviad, "aviad", "123456"));
            LinkedList<Purchase> historyList = ss.viewUserHistory(admin, "aviad");
            Assert.IsTrue(historyList.Count == 0);
        }

        //with a guest user
        [TestMethod]
        public void viewHistoryOf2Users()
        {
            User aviad = us.startSession();
            Assert.IsNotNull(aviad);
            Store store = ss.createStore("abowim", zahi);
            Assert.IsNotNull(store);
            Assert.IsTrue(us.register(aviad, "aviad", "123456"));
            Assert.IsTrue(us.login(aviad, "aviad", "123456"));
            ProductInStore pis = ss.addProductInStore("cola", 3.2, 10, zahi, store);
            Assert.IsNotNull(pis);
            int saleId = ss.addSaleToStore(zahi, store, pis.getProductInStoreId(), 1, 8, DateTime.Now.AddDays(10).ToString());
            LinkedList<Sale> sales = ses.viewSalesByProductInStoreId(pis);
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            Assert.IsTrue(ses.addProductToCart(aviad, sale, 2));
            LinkedList<UserCart> sc = ses.viewCart(aviad);
            Assert.IsTrue(sc.Count == 1);
            Assert.IsTrue(sc.First.Value.getSaleId() == saleId);
            Assert.IsTrue(ses.buyProducts(aviad, "1234", ""));
            LinkedList<Purchase> historyList = ss.viewUserHistory(admin, "aviad");
            Assert.IsTrue(historyList.Count == 1);
            Assert.IsTrue(historyList.First.Value.ProductId == pis.getProduct().getProductId());
            Assert.IsTrue(historyList.First.Value.Amount == 2);


            User vadim = us.startSession();
            Assert.IsNotNull(vadim);
            Assert.IsTrue(us.register(vadim, "vadim", "123456"));
            Assert.IsTrue(us.login(vadim, "vadim", "123456"));
            Store store2 = ss.createStore("abowim2", zahi);
            Assert.IsNotNull(store2);
            ProductInStore pis2 = ss.addProductInStore("cola2", 3.2, 10, zahi, store2);
            Assert.IsNotNull(pis2);
            int saleId2 = ss.addSaleToStore(zahi, store2, pis2.getProductInStoreId(), 1, 8, DateTime.Now.AddDays(10).ToString());
            LinkedList<Sale> sales2 = ses.viewSalesByProductInStoreId(pis2);
            Assert.IsTrue(sales2.Count == 1);
            Sale sale2 = sales2.First.Value;
            Assert.IsTrue(ses.addProductToCart(vadim, sale2, 2));
            LinkedList<UserCart> sc2 = ses.viewCart(vadim);
            Assert.IsTrue(sc2.Count == 1);
            Assert.IsTrue(sc2.First.Value.getSaleId() == saleId2);
            Assert.IsTrue(ses.buyProducts(vadim, "1234", ""));
            LinkedList<Purchase> historyList2 = ss.viewUserHistory(admin, "vadim");
            Assert.IsTrue(historyList2.Count == 1);
            Assert.IsTrue(historyList2.First.Value.ProductId == pis2.getProduct().getProductId());
            Assert.IsTrue(historyList2.First.Value.Amount == 2);
        }

        [TestMethod]
        public void viewUserHistoryOf2Sales()
        {
            User aviad = us.startSession();
            Assert.IsNotNull(aviad);
            Store store = ss.createStore("abowim", zahi);
            Assert.IsNotNull(store);
            Assert.IsTrue(us.register(aviad, "aviad", "123456"));
            Assert.IsTrue(us.login(aviad, "aviad", "123456"));
            ProductInStore pis = ss.addProductInStore("cola", 3.2, 10, zahi, store);
            Assert.IsNotNull(pis);
            int saleId = ss.addSaleToStore(zahi, store, pis.getProductInStoreId(), 1, 8, DateTime.Now.AddDays(10).ToString());
            LinkedList<Sale> sales = ses.viewSalesByProductInStoreId(pis);
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            Assert.IsTrue(ses.addProductToCart(aviad, sale, 2));
            LinkedList<UserCart> sc = ses.viewCart(aviad);
            Assert.IsTrue(sc.Count == 1);
            Assert.IsTrue(sc.First.Value.getSaleId() == saleId);
            Assert.IsTrue(ses.buyProducts(aviad, "1234", ""));
            LinkedList<Purchase> historyList = ss.viewUserHistory(admin, "aviad");
            Assert.IsTrue(historyList.Count == 1);
            Assert.IsTrue(historyList.First.Value.ProductId == pis.getProduct().getProductId());
            Assert.IsTrue(historyList.First.Value.Amount == 2);


            Store store2 = ss.createStore("abowim2", zahi);
            Assert.IsNotNull(store2);
            ProductInStore pis2 = ss.addProductInStore("cola2", 3.2, 10, zahi, store2);
            Assert.IsNotNull(pis2);
            int saleId2 = ss.addSaleToStore(zahi, store2, pis2.getProductInStoreId(), 1, 8, DateTime.Now.AddDays(10).ToString());
            LinkedList<Sale> sales2 = ses.viewSalesByProductInStoreId(pis2);
            Assert.IsTrue(sales2.Count == 1);
            Sale sale2 = sales2.First.Value;
            Assert.IsTrue(ses.addProductToCart(aviad, sale2, 2));
            LinkedList<UserCart> sc2 = ses.viewCart(aviad);
            Assert.IsTrue(sc2.Count == 1);
            Assert.IsTrue(sc2.First.Value.getSaleId() == saleId2);
            Assert.IsTrue(ses.buyProducts(aviad, "1234", ""));
            LinkedList<Purchase> historyList2 = ss.viewUserHistory(admin, "aviad");
            Assert.IsTrue(historyList2.Count == 2);
        }



        [TestMethod]
        public void viewUserHistoryOfAFailedTransaction()
        {
            User aviad = us.startSession();
            Assert.IsNotNull(aviad);
            Store store = ss.createStore("abowim", zahi);
            Assert.IsNotNull(store);
            Assert.IsTrue(us.register(aviad, "aviad", "123456"));
            Assert.IsTrue(us.login(aviad, "aviad", "123456"));
            ProductInStore pis = ss.addProductInStore("cola", 3.2, 10, zahi, store);
            Assert.IsNotNull(pis);
            int saleId = ss.addSaleToStore(zahi, store, pis.getProductInStoreId(), 1, 8, DateTime.Now.AddDays(10).ToString());
            LinkedList<Sale> sales = ses.viewSalesByProductInStoreId(pis);
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            ses.addProductToCart(aviad, sale, 100);
            Assert.IsFalse(ses.buyProducts(aviad, "1234", ""));
            LinkedList<Purchase> historyList = ss.viewUserHistory(admin, "aviad");
            Assert.IsTrue(historyList.Count == 0);

        }


    }
}
