using System;
using wsep182.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class CouponArchiveUnitTest
    {
        CouponsArchive couponArchive;

        [TestInitialize]
        public void init()
        {
            CouponsArchive.restartInstance();

            couponArchive = CouponsArchive.getInstance();
            couponArchive.addNewCoupon("firstCoupon", 1, 50, "jan 12, 2008");
            couponArchive.addNewCoupon("secondCoupon", 2, 30, "jun 9, 2010");
        }

        [TestMethod]
        public void addNewCoupon1()
        {
            int sizeBeforeAdd = couponArchive.getAllCoupons().Count;
            Boolean check = couponArchive.addNewCoupon("check", 3, 7, "dec 9, 1990");
            int sizeAfterAdd = couponArchive.getAllCoupons().Count;
            Assert.IsTrue(check);
            Assert.AreEqual(sizeBeforeAdd + 1, sizeAfterAdd);
        }

        [TestMethod]
        public void addExistingCoupon()
        {
            int sizeBeforeAdd = couponArchive.getAllCoupons().Count;
            Boolean check = couponArchive.addNewCoupon("firstCoupon", 1, 60, "jan 12, 2008");
            int sizeAfterAdd = couponArchive.getAllCoupons().Count;
            Assert.IsFalse(check);
            Assert.AreEqual(sizeBeforeAdd, sizeAfterAdd);
        }

        [TestMethod]
        public void removeCoupon1()
        {
            Boolean check = couponArchive.removeCoupon("firstCoupon");
            Assert.IsTrue(check);
        }

        [TestMethod]
        public void removeNotExistngCoupon()
        {
            Boolean check = couponArchive.removeCoupon("NotHere");
            Assert.IsFalse(check);
        }

        [TestMethod]
        public void editExistingCoupon()
        {
            Boolean check = couponArchive.editCoupon("firstCoupon", 15, "feb 17, 2016");
            Assert.IsTrue(check);
            Assert.AreEqual(couponArchive.getCoupon("firstCoupon", 1).Percentage, 15);
            Assert.AreEqual(couponArchive.getCoupon("firstCoupon", 1).DueDate, "feb 17, 2016");
        }

        [TestMethod]
        public void editNotExistingCoupon()
        {
            Boolean check = couponArchive.editCoupon("NotExistingCoupon", 15, "feb 17, 2016");
            Assert.IsFalse(check);
        }

        [TestMethod]
        public void getCoupons()
        {
            LinkedList<Coupon> check = couponArchive.getAllCoupons();
            Assert.AreEqual(check.Count, 2);
        }

        [TestMethod]
        public void getCoupons2()
        {
            couponArchive.addNewCoupon("thirdCoupon", 2, 13, "may 5, 2020");
            LinkedList<Coupon> check = couponArchive.getAllCoupons();
            Assert.AreEqual(check.Count, 3);
        }

        [TestMethod]
        public void getExistingCoupons()
        {
            Coupon check = couponArchive.getCoupon("firstCoupon", 1);
            Assert.IsTrue(check!=null);
        }

        [TestMethod]
        public void getNonExistingCoupons()
        {
            Coupon check = couponArchive.getCoupon("NotExisting", 1);
            Assert.IsTrue(check == null);
        }

    }
}
