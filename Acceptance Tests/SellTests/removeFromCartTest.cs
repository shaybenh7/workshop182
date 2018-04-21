using System;
using wsep182.Domain;
using wsep182.services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Acceptance_Tests.SellTests
{
    [TestClass]
    public class removeFromCartTest
    {

        private userServices us;
        private storeServices ss;
        private sellServices sellS;
        private User zahi, itamar, niv, admin, admin1; //admin,itamar , niv logedin
        private Store store, store2; //itamar owner , niv manneger
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
        public void removeExistingFromCart()
        {
            LinkedList<Sale> saleList = ss.viewSalesByStore(store);
            sellS.addProductToCart(niv, saleList.First.Value, 2);

            LinkedList<UserCart> uc = sellS.viewCart(niv);
            int beforeDeletion = uc.Count;
            Boolean check = sellS.removeFromCart(niv, saleList.First.Value);
            Assert.IsTrue(check);
            uc = sellS.viewCart(niv);
            int afterDeletion = uc.Count;
            Assert.AreEqual(beforeDeletion, afterDeletion + 1);
        }

        [TestMethod]
        public void removeNonExistingFromCart()
        {
            LinkedList<Sale> saleList = ss.viewSalesByStore(store);
            sellS.addProductToCart(niv, saleList.First.Value, 2);
            LinkedList<UserCart> uc = sellS.viewCart(niv);
            int beforeDeletion = uc.Count;
            Sale nada = new Sale(4, 4, 1, 2, "");
            Boolean check = sellS.removeFromCart(niv, nada);
            Assert.IsFalse(check);
            uc = sellS.viewCart(niv);
            int afterDeletion = uc.Count;
            Assert.AreEqual(beforeDeletion, afterDeletion);
        }

        [TestMethod]
        public void badUserInput1()
        {
            LinkedList<Sale> saleList = ss.viewSalesByStore(store);
            sellS.addProductToCart(niv, saleList.First.Value, 2);
            LinkedList<UserCart> uc = sellS.viewCart(niv);
            int beforeDeletion = uc.Count;
            Boolean check = sellS.removeFromCart(null, saleList.First.Value);
            Assert.IsFalse(check);
            uc = sellS.viewCart(niv);
            int afterDeletion = uc.Count;
            Assert.AreEqual(beforeDeletion, afterDeletion);
        }
        [TestMethod]
        public void badUserInput2()
        {
            LinkedList<Sale> saleList = ss.viewSalesByStore(store);
            sellS.addProductToCart(niv, saleList.First.Value, 2);
            LinkedList<UserCart> uc = sellS.viewCart(niv);
            int beforeDeletion = uc.Count;
            Boolean check = sellS.removeFromCart(zahi, saleList.First.Value);
            Assert.IsFalse(check);
            uc = sellS.viewCart(niv);
            int afterDeletion = uc.Count;
            Assert.AreEqual(beforeDeletion, afterDeletion);
        }
        [TestMethod]
        public void badSaleInput1()
        {
            LinkedList<Sale> saleList = ss.viewSalesByStore(store);
            sellS.addProductToCart(niv, saleList.First.Value, 2);
            LinkedList<UserCart> uc = sellS.viewCart(niv);
            int beforeDeletion = uc.Count;
            Boolean check = sellS.removeFromCart(niv, null);
            Assert.IsFalse(check);
            uc = sellS.viewCart(niv);
            int afterDeletion = uc.Count;
            Assert.AreEqual(beforeDeletion, afterDeletion);
        }

    }
}
