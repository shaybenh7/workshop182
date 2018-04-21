using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using wsep182.Domain;
using wsep182.services;
namespace Acceptance_Tests.StoreTests
{
    [TestClass]
    public class removeManagerPermissionTests
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
                //ADD ALL PERMISSIONS
                ss.addManagerPermission("addProductInStore", store, "aviad", zahi);
                ss.addManagerPermission("editProductInStore", store, "aviad", zahi);
                ss.addManagerPermission("removeProductFromStore", store, "aviad", zahi);
                ss.addManagerPermission("addStoreManager", store, aviad.getUserName(), zahi);
                ss.addManagerPermission("removeStoreManager", store, aviad.getUserName(), zahi);
                ss.addManagerPermission("addManagerPermission", store, aviad.getUserName(), zahi);
                ss.addManagerPermission("removeManagerPermission", store, aviad.getUserName(), zahi);
                ss.addManagerPermission("addSaleToStore", store, aviad.getUserName(), zahi);
                ss.addManagerPermission("removeSaleFromStore", store, aviad.getUserName(), zahi);
                ss.addManagerPermission("editSale", store, aviad.getUserName(), zahi);
                ss.addManagerPermission("addDiscount", store, aviad.getUserName(), zahi);
                ss.addManagerPermission("removeDiscount", store, aviad.getUserName(), zahi);
                ss.addManagerPermission("addNewCoupon", store, aviad.getUserName(), zahi);
                ss.addManagerPermission("removeCoupon", store, aviad.getUserName(), zahi);
        }


        [TestMethod]
            public void addProductInStore()
            {
                ss.removeManagerPermission("addProductInStore", store, "aviad", zahi);
                ss.addProductInStore("cola", 10, 4, aviad, store);
                Assert.AreEqual(0, store.getProductsInStore().Count);
            }

            [TestMethod]
            public void editProductInStoreWithManagerPermission()
            {
                ProductInStore pis = ss.addProductInStore("cola", 10, 4, zahi, store);
                Assert.AreEqual(1, store.getProductsInStore().Count);
                ss.removeManagerPermission("editProductInStore", store, "aviad", zahi);
                ss.editProductInStore(aviad, store, pis, 13, 4.5);
                Assert.AreEqual(10, pis.getPrice());
                Assert.AreEqual(4, pis.getAmount());
            }
            [TestMethod]
            public void removeProductFromStoreWithManagerPermission()
            {
                ss.removeManagerPermission("removeProductFromStore", store, "aviad", zahi);
                ProductInStore pis = ss.addProductInStore("cola", 10, 4, zahi, store);
                ss.removeProductFromStore(store, pis, aviad);
                Assert.AreEqual(1, store.getProductsInStore().Count);
            }

            [TestMethod]
            public void addStoreManagerWithManagerPermission()
            {
                User newManager;
                newManager = us.startSession();
                us.register(newManager, "newManager", "123456");
                us.login(newManager, "newManager", "123456");
                ss.removeManagerPermission("addStoreManager", store, aviad.getUserName(), zahi);
                ss.addStoreManager(store, newManager.getUserName(), aviad);
                Assert.AreEqual(1, store.getManagers().Count);
            }
            [TestMethod]
            public void removeStoreManagerWithManagerPermission()
            {
                User newManager;
                newManager = us.startSession();
                us.register(newManager, "newManager", "123456");
                us.login(newManager, "newManager", "123456");
                ss.addStoreManager(store, newManager.getUserName(), aviad);
                Assert.AreEqual(2, store.getManagers().Count);
                ss.removeManagerPermission("removeStoreManager", store, aviad.getUserName(), zahi);
                ss.removeStoreManager(store, newManager.getUserName(), aviad);
                Assert.AreEqual(2, store.getManagers().Count);
            }
            [TestMethod]
            public void addManagerPermissionWithManagerPermission()
            {
                User newManager;
                newManager = us.startSession();
                us.register(newManager, "newManager", "123456");
                us.login(newManager, "newManager", "123456");
                ss.removeManagerPermission("addManagerPermission", store, aviad.getUserName(), zahi);
                ss.addStoreManager(store, newManager.getUserName(), zahi);
                ss.addProductInStore("cola", 10, 4, newManager, store);
                Assert.AreEqual(0, store.getProductsInStore().Count);
                ss.addManagerPermission("addProductInStore", store, newManager.getUserName(), aviad);
                ss.addProductInStore("cola", 10, 4, newManager, store);
                Assert.AreEqual(0, store.getProductsInStore().Count);
            }
            [TestMethod]
            public void removeManagerPermissionWithManagerPermission()
            {
                User newManager;
                newManager = us.startSession();
                us.register(newManager, "newManager", "123456");
                us.login(newManager, "newManager", "123456");
                ss.addStoreManager(store, newManager.getUserName(), zahi);
                ss.addManagerPermission("addProductInStore", store, newManager.getUserName(), aviad);
                ss.addProductInStore("cola", 10, 4, newManager, store);
                Assert.AreEqual(1, store.getProductsInStore().Count);
                ss.removeManagerPermission("removeManagerPermission", store, aviad.getUserName(), zahi);
                ss.removeManagerPermission("addProductInStore", store, newManager.getUserName(), aviad);
                ss.addProductInStore("cola2", 10, 4, newManager, store);
                Assert.AreEqual(2, store.getProductsInStore().Count);
            }

            [TestMethod]
            public void addSaleToStoreWithManagerPermission()
            {
                ProductInStore pis = ss.addProductInStore("cola", 10, 4, zahi, store);
                ss.removeManagerPermission("addSaleToStore", store, aviad.getUserName(), zahi);
                int saleId = ss.addSaleToStore(aviad, store, pis.getProductInStoreId(), 1, 100, "10.1.2019");
                Assert.AreEqual(-1, saleId);
            }

            [TestMethod]
            public void removeSaleFromStoreWithManagerPermission()
            {
                ProductInStore pis = ss.addProductInStore("cola", 10, 4, zahi, store);
                int saleId = ss.addSaleToStore(aviad, store, pis.getProductInStoreId(), 1, 100, "10.1.2019");
                Assert.AreEqual(saleId, SalesArchive.getInstance().getSalesByProductInStoreId(pis.getProductInStoreId()).First.Value.SaleId);
                ss.removeManagerPermission("removeSaleFromStore", store, aviad.getUserName(), zahi);
                Boolean deleted = ss.removeSaleFromStore(aviad, store, saleId);
                Assert.AreEqual(deleted, false);
                
            }

            [TestMethod]
            public void editSaleWithManagerPermission()
            {
                ProductInStore pis = ss.addProductInStore("cola", 100, 100, zahi, store);
                int saleId = ss.addSaleToStore(zahi, store, pis.getProductInStoreId(), 1, 40, "10.1.2019");
                Assert.AreEqual(saleId, SalesArchive.getInstance().getSalesByProductInStoreId(pis.getProductInStoreId()).First.Value.SaleId);
                ss.removeManagerPermission("editSale", store, aviad.getUserName(), zahi);
                Boolean edited = ss.editSale(aviad, store, saleId, 30, "11.1.2019");
                Assert.AreEqual(40, SalesArchive.getInstance().getSale(saleId).Amount);
                Assert.AreEqual("10.1.2019", SalesArchive.getInstance().getSale(saleId).DueDate);
                Assert.AreEqual(edited, false);
            }

            [TestMethod]
            public void addDiscountWithManagerPermission()
            {
                ProductInStore pis = ss.addProductInStore("cola", 150, 100, zahi, store);
                int saleId = ss.addSaleToStore(zahi, store, pis.getProductInStoreId(), 1, 40, "10.1.2019");
                ss.removeManagerPermission("addDiscount", store, aviad.getUserName(), zahi);
                Boolean added = ss.addDiscount(pis, 11, "20.1.2019", aviad, store);
                Assert.AreEqual(false, added);
                Assert.AreEqual(0, DiscountsArchive.getInstance().getAllDiscounts().Count);
                
            }

            [TestMethod]
            public void removeDiscountWithManagerPermission()
            {
                ProductInStore pis = ss.addProductInStore("cola", 150, 100, zahi, store);
                int saleId = ss.addSaleToStore(zahi, store, pis.getProductInStoreId(), 1, 40, "10.1.2019");
                Boolean added = ss.addDiscount(pis, 11, "20.1.2019", aviad, store);
                Assert.AreEqual(true, added);
                ss.removeManagerPermission("removeDiscount", store, aviad.getUserName(), zahi);
                Boolean removed = ss.removeDiscount(pis, store, aviad);
                Assert.AreEqual(1, DiscountsArchive.getInstance().getAllDiscounts().Count);
                Assert.AreEqual(false, removed);
            }

            [TestMethod]
            public void addCouponWithManagerPermission()
            {
                ProductInStore pis = ss.addProductInStore("cola", 150, 100, zahi, store);
                ss.removeManagerPermission("addNewCoupon", store, aviad.getUserName(), zahi);
                Boolean added = ss.addCouponDiscount(aviad, store, "coupon", pis, 10, "10.1.2019");
                Assert.AreEqual(false, added);
            }

            [TestMethod]
            public void removeCouponWithManagerPermission()
            {
                ProductInStore pis = ss.addProductInStore("cola", 150, 100, zahi, store);
                Boolean added = ss.addCouponDiscount(aviad, store, "coupon", pis, 10, "10.1.2019");
                Assert.AreEqual(true, added);
                ss.removeManagerPermission("removeCoupon", store, aviad.getUserName(), zahi);
                Assert.AreEqual(false, ss.removeCoupon(store, aviad, "coupon"));

            }

        }
    }
