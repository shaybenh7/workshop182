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
            DateTime dueDateTime;
            try
            {
                dueDateTime = DateTime.Parse(dueDate);
            }
            catch (System.FormatException e)
            {
                return false;
            }
            if (DateTime.Compare(dueDateTime, DateTime.Now) < 0)
                return false;
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
            Boolean found = false;
            LinkedList<int> indexes = new LinkedList<int>();
            for (int i = 0; i < coupons.Count; i++)
            {
                if (coupons.ElementAt(i).CouponId.Equals(couponId))
                {
                    indexes.AddLast(i);
                    found = true;
                }
            }
            if (!found)
                return false;
            for (int i = indexes.Count - 1; i >= 0; i--)
            {
                coupons.Remove(coupons.ElementAt(indexes.ElementAt(i)));
            }
            if (!found)
                return false;

            return true;
        }

        public Boolean editCoupon(String couponId, int newPercentage, String newDueDate)
        {
            Boolean found = false;
            foreach (Coupon coupon in coupons)
            {
                if (coupon.CouponId.Equals(couponId))
                {
                    coupon.Percentage = newPercentage;
                    coupon.DueDate = newDueDate;
                    found = true;
                }
            }
            if (!found)
                return false;
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

        public LinkedList<Coupon> getAllCoupons()
        {
            return coupons;
        }

    }
}
