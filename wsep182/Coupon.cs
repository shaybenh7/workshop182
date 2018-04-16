using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    class Coupon
    {
        private String couponId;
        private int productInStoreId;
        private int percentage;
        private String dueDate;

        public Coupon(String couponId, int productInStoreId, int percentage, String dueDate)
        {
            this.couponId = couponId;
            this.productInStoreId = productInStoreId;
            this.percentage = percentage;
            this.dueDate = dueDate;
        }

        public string CouponId { get => couponId; set => couponId = value; }
        public int ProductInStoreId { get => productInStoreId; set => productInStoreId = value; }
        public int Percentage { get => percentage; set => percentage = value; }
        public string DueDate { get => dueDate; set => dueDate = value; }

        public double getPriceAfterCouponDiscount(double price)
        {
            return price * (percentage / 100);
        }

    }
}
