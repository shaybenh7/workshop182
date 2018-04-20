using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.SellTests
{
    [TestClass]
    public class addProductToCartTest
    {

        private userServices us;
        private storeServices ss;
        private sellServices sellS;
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
        public void simpleAddProductToCart()
        {
            us.login(zahi, "zahi", "123456");
            LinkedList<Sale> saleList = ss.viewSalesByStore(store);
            Assert.IsTrue(sellS.addProductToCart(zahi, saleList.First.Value, 1));
        }
        [TestMethod]
        public void AddProductToCartAmountToBig()
        {
            us.login(zahi, "zahi", "123456");
            LinkedList<Sale> saleList = ss.viewSalesByStore(store);
            Assert.IsFalse(sellS.addProductToCart(zahi, saleList.First.Value, 8));
            Assert.IsFalse(sellS.addProductToCart(zahi, saleList.First.Value, 12));
        }
        [TestMethod]
        public void AddProductToCartBuyMax()
        {
            us.login(zahi, "zahi", "123456");
            LinkedList<Sale> saleList = ss.viewSalesByStore(store);
            Assert.IsFalse(sellS.addProductToCart(zahi, saleList.First.Value, 8));
            Assert.IsTrue(sellS.addProductToCart(zahi, saleList.First.Value, 1));
            Assert.IsTrue(sellS.addProductToCart(niv, saleList.First.Value, 4));
        }
        [TestMethod]
        public void AddProductToCartNull()
        {
            us.login(zahi, "zahi", "123456");
            LinkedList<Sale> saleList = ss.viewSalesByStore(store);
            Assert.IsNull(sellS.addProductToCart(null, saleList.First.Value, 1));
            Assert.IsNull(sellS.addProductToCart(zahi, null, 1));
        }
        [TestMethod]
        public void AddProductToCartZero()
        {
            us.login(zahi, "zahi", "123456");
            LinkedList<Sale> saleList = ss.viewSalesByStore(store);
            Assert.IsFalse(sellS.addProductToCart(zahi, saleList.First.Value, 0));
        }
        [TestMethod]
        public void AddProductToCartNegative()
        {
            us.login(zahi, "zahi", "123456");
            LinkedList<Sale> saleList = ss.viewSalesByStore(store);
            Assert.IsFalse(sellS.addProductToCart(zahi, saleList.First.Value, -1));
        }
        [TestMethod]
        public void AddProductToCartNormalSell()
        {
            us.login(zahi, "zahi", "123456");
            int saleId=ss.addSaleToStore(itamar, store, sprite.getProductInStoreId(), 3, 1, "20/5/2018");
            LinkedList<Sale> saleList = ss.viewSalesByStore(store);
            foreach(Sale sale in saleList)
            {
                if(sale.SaleId==saleId)
                    Assert.IsFalse(sellS.addProductToCart(zahi, sale, 1));//raffle product
                else
                    Assert.IsTrue(sellS.addProductToCart(zahi, sale, 1));
            }
            
        }
    }
}
