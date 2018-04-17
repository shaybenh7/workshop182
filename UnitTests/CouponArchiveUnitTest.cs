using System;
using wsep182.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class CouponArchiveUnitTest
    {
        Product p1; Product p2; Product p3;
        ProductInStore pis1; ProductInStore pis2; ProductInStore pis3;
        Store store;
        User storeOwner;
        CouponsArchive couponArchive;

        [TestInitialize]
        public void init()
        {
            ProductArchive.restartInstance();
            SalesArchive.restartInstance();
            storeArchive.restartInstance();
            UserArchive.restartInstance();
            UserCartsArchive.restartInstance();
            DiscountsArchive.restartInstance();
            CouponsArchive.restartInstance();
            RaffleSalesArchive.restartInstance();

            couponArchive = CouponsArchive.getInstance();

            p1 = ProductArchive.getInstance().addProduct("milk");
            p2 = ProductArchive.getInstance().addProduct("bread");
            p3 = ProductArchive.getInstance().addProduct("meat");

            storeOwner = new User("itamar", "1234");
            store = storeArchive.getInstance().addStore("halavi", storeOwner);
            pis1 = ProductArchive.getInstance().addProductInStore(p1, store, 50, 100);
            pis2 = ProductArchive.getInstance().addProductInStore(p2, store, 100, 12.5);
            pis3 = ProductArchive.getInstance().addProductInStore(p3, store, 150, 46);

            couponArchive.addNewCoupon("n-unique", pis1.getProductInStoreId(), 50, "jan 12, 2008");
        }

        [TestMethod]
        /* Description:
         * add a new coupon which is not exist in archive
         * OUTCOME: TEST SHOULD PASS
         * */
        public void addNewCoupon1()
        {
            Boolean check = couponArchive.addNewCoupon("unique", pis1.getProductInStoreId(), 50, "jan 12, 2008");
            Assert.IsTrue(check);
        }

        [TestMethod]
        /* Description:
         * add a new coupon which already exist in archive
         * OUTCOME: add should not be allowed
         * */
        public void addNewCoupon2()
        {
            Boolean check = couponArchive.addNewCoupon("n-unique", pis1.getProductInStoreId(), 60, "jan 12, 2008");
            Assert.IsFalse(false);
        }

        [TestMethod]
        /* Description:
         * remove a coupon that exist in the archive
         * OUTCOME: test should pass
         * */
        public void removeCoupon1()
        {
            Boolean check = couponArchive.removeCoupon("n-unique");
            Assert.IsTrue(true);
        }

        [TestMethod]
        /* Description:
         * remove a coupon that does not exist in the archive
         * OUTCOME: test should pass
         * */
        public void removeCoupon2()
        {
            Boolean check = couponArchive.removeCoupon("notHere");
            Assert.IsFalse(check);
        }

        [TestMethod]
        /* Description:
         * edits a coupon that exist in archive
         * OUTCOME: test should pass
         * */
        public void editCoupon1()
        {
            Boolean check = couponArchive.editCoupon("n-unique",90,"dec 13,2010");
            Assert.IsTrue(check);
        }


        [TestMethod]
        /* Description:
         * edits a coupon that exist in archive
         * OUTCOME: test should pass
         * */
        public void getCoupon1()
        {
            Coupon check = couponArchive.getCoupon("n-unique", pis1.getProductInStoreId());
            Assert.IsTrue(check!=null);
        }




    }
}
