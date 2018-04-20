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
/*
        private userServices us;
        private storeServices ss;
        private sellServices sellS;
        private User zahi, itamar, niv, admin, admin1; //admin,itamar , niv logedin
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
            sellS = sellServices.getInstance();

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
            ss.addSaleToStore(itamar, store, cola.getProductInStoreId(), 1, 5, "20/5/2018");
        }

        [TestMethod]
        public void simpleViewCart()
        {
            LinkedList<Sale> saleList = ss.viewSalesByStore(store);
            sellS.addProductToCart(niv, saleList.First.Value, 1);
            LinkedList<UserCart> sc = sellS.viewCart(niv).getShoppingCartProducts(niv);
        }
        */
    }
}
