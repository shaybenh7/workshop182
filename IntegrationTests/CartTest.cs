using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;

namespace IntegrationTests
{
    [TestClass]
    public class CartTest
    {
        private User zahi;  // owner of store
        private User aviad; //manager of store
        private User shay;
        private User itamar; // not a real user
        private User niv; // guest
        private Store store;
        StoreRole zahiOwner;
        StoreRole aviadManeger;
        ProductInStore cola;

        [TestInitialize]
        public void init()
        {
            ProductArchive.restartInstance();
            SalesArchive.restartInstance();
            storeArchive.restartInstance();
            UserArchive.restartInstance();
            UserCartsArchive.restartInstance();
            StorePremissionsArchive.restartInstance();
            BuyHistoryArchive.restartInstance();
            CouponsArchive.restartInstance();
            DiscountsArchive.restartInstance();
            RaffleSalesArchive.restartInstance();
            StorePremissionsArchive.restartInstance();
            zahi = new User("zahi", "123456");
            zahi.register("zahi", "123456");
            zahi.login("zahi", "123456");
            aviad = new User("aviad", "123456");
            aviad.register("aviad", "123456");
            aviad.login("aviad", "123456");
            shay = new User("shay", "123456");
            shay.register("shay", "123456");
            shay.login("shay", "123456");
            itamar = new User("itamar", "123456");
            niv = new User("niv", "123456");
            niv.register("niv", "123456");
            store = zahi.createStore("abowim");
            zahiOwner = new StoreOwner(zahi, store);
            aviadManeger = new StoreManager(aviad, store);
            zahiOwner.addStoreManager(zahi, store, "aviad");
            niv.logOut();
            cola = zahiOwner.addProductInStore(zahi, store, "cola", 3.2, 10);
        }
        [TestMethod]
        public void AddProductToCart()
        {
            int saleId=zahiOwner.addSaleToStore(zahi, cola.getProductInStoreId(), 1, 5, "20/5/2018");
            LinkedList<Sale> saleList = store.getAllSales();
            Assert.IsTrue(aviad.addToCart(saleList.First.Value.SaleId, 1));
            Assert.AreEqual(aviad.getShoppingCart().First.Value.getSaleId(),saleId);
        }
        [TestMethod]
        public void AddRaffleProductToCart()
        {
            int saleId = zahiOwner.addSaleToStore(zahi, cola.getProductInStoreId(), 3, 1, "20/5/2018");
            LinkedList<Sale> saleList = store.getAllSales(); ;
            Assert.IsTrue(aviad.addToCartRaffle(saleList.First.Value, 1));
        }
        [TestMethod]
        public void EditAmount()
        {
            int saleId = zahiOwner.addSaleToStore(zahi, cola.getProductInStoreId(), 1, 5, "20/5/2018");
            LinkedList<Sale> saleList = store.getAllSales();
            aviad.addToCart(saleList.First.Value.SaleId, 1);
            Boolean check = aviad.editCart(saleList.First.Value.SaleId, 4);
            Assert.IsTrue(check);
            LinkedList<UserCart> aviadCart = aviad.getShoppingCart();
            UserCart uc = aviadCart.First.Value;
            Assert.AreEqual(uc.getAmount(), 4);
        }
        [TestMethod]
        public void viewSaleByProductInStoreId()
        {
            int saleId = zahiOwner.addSaleToStore(zahi, cola.getProductInStoreId(), 1, 5, "20/5/2018");
            Assert.AreEqual(1, User.viewSalesByProductInStoreId(cola).Count);
        }


        //User is creating a store, adding products
        //another user is buying the products from him
        [TestMethod]
        public void Transcation()
        {
            int saleId = zahiOwner.addSaleToStore(zahi, cola.getProductInStoreId(), 1, 5, "20/5/2018");
            LinkedList<Sale> sales = User.viewSalesByProductInStoreId(cola);
            Assert.IsTrue(sales.Count == 1);
            Sale sale = sales.First.Value;
            Assert.IsTrue(aviad.addToCart(sale.SaleId, 1));
            LinkedList<UserCart> sc = aviad.getShoppingCart();
            Assert.IsTrue(sc.Count == 1);
            Assert.IsTrue(sc.First.Value.getSaleId() == saleId);
            Assert.IsTrue(aviad.buyProducts("1234", ""));
        }
    }
}
