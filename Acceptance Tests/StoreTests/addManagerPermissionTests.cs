using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.StoreTests
{

    /*
      "addProductInStore":
       "removeProductFromStore":
       "addStoreManager":
       "removeStoreManager":
       "addManagerPermission":
       "removeManagerPermission":
    */

    [TestClass]
    public class addManagerPermissionTests
    {
        private userServices us;
        private storeServices ss;
        private User zahi;  // owner of store
        private User aviad; //manager of store
        private User shay;
        private User itamar; // not a real user
        private User niv; // guest
        private Store store;

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
            us = userServices.getInstance();
            zahi = us.startSession();
            us.register(zahi, "zahi", "123456");
            us.login(zahi, "zahi", "123456");
            aviad = us.startSession();
            us.register(aviad, "aviad", "123456");
            us.login(aviad, "aviad", "123456");
            shay = us.startSession();
            us.register(shay, "shay", "123456");
            us.login(shay, "shay", "123456");
            itamar = new User("itamar", "123456");
            niv = us.startSession();
            us.register(niv, "niv", "123456");
            ss = storeServices.getInstance();
            store = ss.createStore("abowim", zahi);
            ss.addStoreManager(store, "aviad", zahi);
            niv.logOut();
        }


        [TestMethod]
        public void addProductInStore()
        {
            ss.addProductInStore("cola", 10, 4, aviad, store);
            Assert.AreEqual(0, store.getProductsInStore().Count);
            ss.addManagerPermission("addProductInStore", store, "aviad", zahi);
            ss.addProductInStore("cola", 10, 4, aviad, store);
            Assert.AreEqual(1, store.getProductsInStore().Count);
        }

        [TestMethod]
        public void editProductInStoreWithManagerPermission()
        {
            ProductInStore pis=ss.addProductInStore("cola", 10, 4, zahi, store);
            Assert.AreEqual(1, store.getProductsInStore().Count);
            ss.editProductInStore(aviad, store, pis, 13, 4.5);
            Assert.AreEqual(10, pis.getPrice());
            Assert.AreEqual(4, pis.getAmount());
            ss.addManagerPermission("editProductInStore", store, "aviad", zahi);
            ss.editProductInStore(aviad, store, pis, 13, 4.5);
            Assert.AreEqual(4.5, pis.getPrice());
            Assert.AreEqual(13, pis.getAmount());
        }
        [TestMethod]
        public void removeProductFromStoreWithManagerPermission()
        {
            ProductInStore pis = ss.addProductInStore("cola", 10, 4, zahi, store);
            ss.removeProductFromStore(store, pis, aviad);
            Assert.AreEqual(1, store.getProductsInStore().Count);
            ss.addManagerPermission("removeProductFromStore", store, "aviad", zahi);
            ss.removeProductFromStore(store, pis, aviad);
            Assert.AreEqual(0, store.getProductsInStore().Count);
        }
        [TestMethod]
        public void tryoToAddStoreOwnerWithManagerPermission()
        {
            User newOwner;
            newOwner = us.startSession();
            us.register(newOwner, "newOwner", "123456");
            us.login(newOwner, "newOwner", "123456");
            Assert.AreEqual(1, store.getOwners().Count);
            ss.addManagerPermission("addStoreOwner", store, aviad, zahi);
            ss.addStoreOwner(store, newOwner, aviad);
            Assert.AreEqual(1, store.getOwners().Count);
        }
        [TestMethod]
        public void tryoToRemoveStoreOwnerWithManagerPermission()
        {
            User newOwner;
            newOwner = us.startSession();
            us.register(newOwner, "newOwner", "123456");
            us.login(newOwner, "newOwner", "123456");
            ss.addStoreOwner(store, newOwner, zahi);
            Assert.AreEqual(2, store.getOwners().Count);
            ss.addManagerPermission("removeStoreOwner", store, aviad, zahi);
            ss.removeStoreOwner(store, newOwner, aviad);
            Assert.AreEqual(2, store.getOwners().Count);
        }
        [TestMethod]
        public void addStoreManagerWithManagerPermission()
        {
            User newManager;
            newManager = us.startSession();
            us.register(newManager, "newManager", "123456");
            us.login(newManager, "newManager", "123456");
            ss.addStoreManager(store, newManager, aviad);
            Assert.AreEqual(1, store.getManagers().Count);
            ss.addManagerPermission("addStoreManager", store, aviad, zahi);
            ss.addStoreManager(store, newManager, aviad);
            Assert.AreEqual(2, store.getManagers().Count);
        }
        [TestMethod]
        public void removeStoreManagerWithManagerPermission()
        {
            User newManager;
            newManager = us.startSession();
            us.register(newManager, "newManager", "123456");
            us.login(newManager, "newManager", "123456");
            ss.addManagerPermission("addStoreManager", store, aviad, zahi);
            ss.addStoreManager(store, newManager, aviad);
            Assert.AreEqual(2, store.getManagers().Count);
            ss.removeStoreManager(store, newManager, aviad);
            Assert.AreEqual(2, store.getManagers().Count);
            ss.addManagerPermission("removeStoreManager", store, aviad, zahi);
            ss.removeStoreManager(store, newManager, aviad);
            Assert.AreEqual(1, store.getManagers().Count);
        }
        [TestMethod]
        public void addManagerPermissionWithManagerPermission()
        {
            User newManager;
            newManager = us.startSession();
            us.register(newManager, "newManager", "123456");
            us.login(newManager, "newManager", "123456");
            ss.addManagerPermission("addManagerPermission", store, aviad, zahi);
            ss.addStoreManager(store, newManager, zahi);
            ss.addProductInStore("cola", 10, 4, newManager, store);
            Assert.AreEqual(0, store.getProductsInStore().Count);
            ss.addManagerPermission("addProductInStore", store, newManager, aviad);
            ss.addProductInStore("cola", 10, 4, newManager, store);
            Assert.AreEqual(1, store.getProductsInStore().Count);
        }
        [TestMethod]
        public void removeManagerPermissionWithManagerPermission()
        {
            User newManager;
            newManager = us.startSession();
            us.register(newManager, "newManager", "123456");
            us.login(newManager, "newManager", "123456");
            ss.addManagerPermission("addManagerPermission", store, aviad, zahi);
            ss.addManagerPermission("removeManagerPermission", store, aviad, zahi);
            ss.addStoreManager(store, newManager, zahi);
            ss.addManagerPermission("addProductInStore", store, newManager, aviad);
            ss.addProductInStore("cola", 10, 4, newManager, store);
            Assert.AreEqual(1, store.getProductsInStore().Count);
            ss.removeManagerPermission("addProductInStore", store, newManager, aviad);
            ss.addProductInStore("cola2", 10, 4, newManager, store);
            Assert.AreEqual(1, store.getProductsInStore().Count);
        }

        [TestMethod]
        public void addSaleToStoreWithManagerPermission()
        {
            ProductInStore pis = ss.addProductInStore("cola", 10, 4, zahi, store);
            int saleId = ss.addSaleToStore(aviad, store, pis.getProductInStoreId(), 1, 100, "10.1.2019");
            Assert.AreEqual(-1,saleId);
            ss.addManagerPermission("addSaleToStore", store, aviad, zahi);
            saleId = ss.addSaleToStore(aviad, store, pis.getProductInStoreId(), 1, 100, "10.1.2019");
            Assert.AreEqual(saleId, SalesArchive.getInstance().getSalesByProductInStoreId(pis.getProductInStoreId()).First.Value.SaleId);
        }

        [TestMethod]
        public void removeSaleFromStoreWithManagerPermission()
        {
            ProductInStore pis = ss.addProductInStore("cola", 10, 4, zahi, store);
            ss.addManagerPermission("addSaleToStore", store, aviad, zahi);
            int saleId = ss.addSaleToStore(aviad, store, pis.getProductInStoreId(), 1, 100, "10.1.2019");
            Assert.AreEqual(saleId, SalesArchive.getInstance().getSalesByProductInStoreId(pis.getProductInStoreId()).First.Value.SaleId);
            Boolean deleted= ss.removeSaleFromStore(aviad, store,saleId);
            Assert.AreEqual(deleted,false);
            ss.addManagerPermission("removeSaleFromStore", store, aviad, zahi);
            deleted = ss.removeSaleFromStore(aviad, store, saleId);
            Assert.AreEqual(deleted, true);
            Assert.AreEqual(0, SalesArchive.getInstance().getAllSales().Count);
        }

        [TestMethod]
        public void editSaleWithManagerPermission()
        {
            ProductInStore pis = ss.addProductInStore("cola", 100, 100, zahi, store);
            int saleId = ss.addSaleToStore(zahi, store, pis.getProductInStoreId(), 1, 40, "10.1.2019");
            Assert.AreEqual(saleId, SalesArchive.getInstance().getSalesByProductInStoreId(pis.getProductInStoreId()).First.Value.SaleId);
            Boolean edited = ss.editSale(aviad, store, saleId,30, "11.1.2019");
            Assert.AreEqual(40,SalesArchive.getInstance().getSale(saleId).Amount);
            Assert.AreEqual("10.1.2019",SalesArchive.getInstance().getSale(saleId).DueDate);
            Assert.AreEqual(edited, false);
            ss.addManagerPermission("editSale", store, aviad, zahi);
            edited = ss.editSale(aviad, store, saleId, 30, "11.1.2019");
            Assert.AreEqual(30,SalesArchive.getInstance().getSale(saleId).Amount);
            Assert.AreEqual("11.1.2019",SalesArchive.getInstance().getSale(saleId).DueDate);
            Assert.AreEqual(edited, true);
        }

        [TestMethod]
        public void addDiscountWithManagerPermission()
        {
            ProductInStore pis = ss.addProductInStore("cola", 150, 100, zahi, store);
            int saleId = ss.addSaleToStore(zahi, store, pis.getProductInStoreId(), 1, 40, "10.1.2019");
            Boolean added=ss.addDiscount(pis, 11, "20.1.2019", aviad, store);
            Assert.AreEqual(false, added);
            Assert.AreEqual(0, DiscountsArchive.getInstance().getAllDiscounts().Count);
            ss.addManagerPermission("addDiscount", store, aviad, zahi);
            added = ss.addDiscount(pis, 11, "20.1.2019", aviad, store);
            Assert.AreEqual(true, added);
            Assert.AreEqual(1, DiscountsArchive.getInstance().getAllDiscounts().Count);
            Assert.AreEqual(133.5, SalesArchive.getInstance().getSale(saleId).getPriceAfterDiscount(1));
        }

        [TestMethod]
        public void removeDiscountWithManagerPermission()
        {
            ProductInStore pis = ss.addProductInStore("cola", 150, 100, zahi, store);
            int saleId = ss.addSaleToStore(zahi, store, pis.getProductInStoreId(), 1, 40, "10.1.2019");
            ss.addManagerPermission("addDiscount", store, aviad, zahi);
            Boolean added = ss.addDiscount(pis, 11, "20.1.2019", aviad, store);
            Assert.AreEqual(true, added);
            ss.removeDiscount(pis, store, aviad);
            Assert.AreEqual(1, DiscountsArchive.getInstance().getAllDiscounts().Count);
            ss.addManagerPermission("removeDiscount", store, aviad, zahi);
            Boolean removed = ss.removeDiscount(pis, store, aviad);
            Assert.AreEqual(true, removed);
            Assert.AreEqual(0, DiscountsArchive.getInstance().getAllDiscounts().Count);
        }

        [TestMethod]
        public void addCouponWithManagerPermission()
        {
            ProductInStore pis = ss.addProductInStore("cola", 150, 100, zahi, store);
            Boolean added = ss.addCouponDiscount(aviad, store,"coupon" ,pis, 10, "10.1.2019");
            Assert.AreEqual(false, added);
            ss.addManagerPermission("addNewCoupon", store, aviad, zahi);
            added = ss.addCouponDiscount(aviad, store, "coupon", pis, 10, "10.1.2019");
            Assert.AreEqual(true, added);
        }

        [TestMethod]
        public void removeCouponWithManagerPermission()
        {
            ProductInStore pis = ss.addProductInStore("cola", 150, 100, zahi, store);
            ss.addManagerPermission("addNewCoupon", store, aviad, zahi);
            Boolean added = ss.addCouponDiscount(aviad, store, "coupon", pis, 10, "10.1.2019");
            Assert.AreEqual(true, added);
            Assert.AreEqual(false, ss.removeCoupon(store, aviad, "coupon"));
            ss.addManagerPermission("removeCoupon", store, aviad, zahi);
            Assert.AreEqual(true, ss.removeCoupon(store, aviad, "coupon"));

        }

    }
}
