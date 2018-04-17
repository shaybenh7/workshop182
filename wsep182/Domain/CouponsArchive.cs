using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class CouponsArchive
    {
        private LinkedList<Coupon> coupons;
        private static CouponsArchive instance;

        private CouponsArchive()
        {
            coupons = new LinkedList<Coupon>();
        }
        public static CouponsArchive getInstance()
        {
            if (instance == null)
                instance = new CouponsArchive();
            return instance;
        }
        public static void restartInstance()
        {
            instance = new CouponsArchive();
        }

        public Boolean addNewCoupon(String couponId, int productInStoreId, int percentage, String dueDate)
        {
            Coupon toAdd = new Coupon(couponId, productInStoreId, percentage, dueDate);
            foreach (Coupon coupon in coupons)
            {
                if (coupon.CouponId.Equals(couponId) && coupon.ProductInStoreId == productInStoreId)
                    return false;
            }
            coupons.AddLast(toAdd);
            return true;
        }

        public Boolean removeCouponForSpecificProduct(String couponId, int productInStoreId)
        {
            foreach (Coupon coupon in coupons)
            {
                if (coupon.CouponId.Equals(couponId) && coupon.ProductInStoreId == productInStoreId)
                {
                    coupons.Remove(coupon);
                    return true;
                }
            }
            return false;
        }
        public Boolean removeCoupon(String couponId)
        {
            foreach (Coupon coupon in coupons)
            {
                if (coupon.CouponId.Equals(couponId))
                {
                    coupons.Remove(coupon);
                }
            }
            return true;
        }
        public Boolean editCoupon(String couponId, int newPercentage, String newDueDate)
        {
            foreach (Coupon coupon in coupons)
            {
                if (coupon.CouponId.Equals(couponId))
                {
                    coupon.Percentage = newPercentage;
                    coupon.DueDate = newDueDate;
                }
            }
            return true;
        }

        public Coupon getCoupon(String couponId, int productInStoreId)
        {
            foreach (Coupon coupon in coupons)
            {
                if (coupon.CouponId.Equals(couponId) && coupon.ProductInStoreId == productInStoreId)
                {
                    return coupon;
                }
            }
            return null;
        }

    }
}
