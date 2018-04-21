using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;


namespace Acceptance_Tests.StoreTests
{
    [TestClass]
    public class addCouponDiscountTests
    {

        private userServices us;
        private storeServices ss;
        private CouponsArchive ca;
        private User zahi;
        private Store store;//itamar owner , niv manneger
        private ProductInStore cola;
        private Sale colaSale;

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
            ca = CouponsArchive.getInstance();

            zahi = us.startSession();
            us.register(zahi, "zahi", "123456");
            us.login(zahi, "zahi", "123456");

            store = ss.createStore("Abowim", zahi);

            cola = ss.addProductInStore("cola", 10, 100, zahi, store);

            ss.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 2, "20/5/2018");

            LinkedList<Sale> SL = ss.viewSalesByStore(store);
            foreach (Sale sale in SL)
            {
                if (sale.ProductInStoreId == cola.getProductInStoreId())
                {
                    colaSale = sale;
                }
            }
        }


        [TestMethod]
        public void SimpleAddCoupon()
        {
            Assert.IsTrue(ss.addCouponDiscount(zahi, store, "copun", cola, 10, "20/6/2018"));
            Coupon c = ca.getCoupon("copun", cola.getProductInStoreId());
            Assert.AreEqual(c.DueDate, "20/6/2018");
            Assert.AreEqual(c.CouponId, "copun");
            Assert.AreEqual(c.Percentage, 10);
        }

        [TestMethod]
        public void AddCouponWithNullId()
        {
            Assert.IsFalse(ss.addCouponDiscount(zahi, store, null, cola, 10, "20/6/2018"));
            Assert.IsNull(ca.getCoupon("copun", cola.getProductInStoreId()));
        }

        [TestMethod]
        public void AddCouponWithNullproduct()
        {
            Assert.IsFalse(ss.addCouponDiscount(zahi, store, "copun", null, 10, "20/6/2018"));
            Assert.IsNull(ca.getCoupon("copun", cola.getProductInStoreId()));
        }

        [TestMethod]
        public void AddCouponWithzeroPrecent()
        {
            Assert.IsFalse(ss.addCouponDiscount(zahi, store, "copun", cola, 0, "20/6/2018"));
            Assert.IsNull(ca.getCoupon("copun", cola.getProductInStoreId()));
        }

        [TestMethod]
        public void AddCouponWithzNegPrecent()
        {
            Assert.IsFalse(ss.addCouponDiscount(zahi, store, "copun", cola, -1, "20/6/2018"));
            Assert.IsNull(ca.getCoupon("copun", cola.getProductInStoreId()));
        }

        [TestMethod]
        public void AddCouponWithzNullDueDate()
        {
            Assert.IsFalse(ss.addCouponDiscount(zahi, store, "copun", cola, 10, null));
            Assert.IsNull(ca.getCoupon("copun", cola.getProductInStoreId()));
        }

        [TestMethod]
        public void AddCouponWithzDueDateAllreadyPassed()
        {
            Assert.IsFalse(ss.addCouponDiscount(zahi, store, "copun", cola, 10, DateTime.Now.AddDays(-1).ToString()));
            Assert.IsNull(ca.getCoupon("copun", cola.getProductInStoreId()));
        }
    }
}
