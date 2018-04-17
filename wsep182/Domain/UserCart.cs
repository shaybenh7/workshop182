using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class UserCart
    {
        private String userName;
        private int saleId;
        private int amount;
        private double offer;
        private Boolean couponActivated;

        public UserCart(String userName, int saleId, int amount)
        {
            this.userName = userName;
            this.saleId = saleId;
            this.amount = amount;
            this.offer = 0;
            couponActivated = false;
        }

        public String getUserName()
        {
            return userName;
        }
        public int getSaleId()
        {
            return saleId;
        }
        public int getAmount()
        {
            return amount;
        }
        public void setUserName(String newUserName)
        {
            this.userName = newUserName;
        }
        public void setSaleId(int newSaleId)
        {
            this.saleId = newSaleId;
        }
        public void setAmount(int newAmount)
        {
            this.amount = newAmount;
        }
        public double getOffer()
        {
            return offer;
        }
        public void setOffer(double offer)
        {
            this.offer = offer;
        }
        public Boolean activateCoupon(String couponId)
        {
            Sale sale = SalesArchive.getInstance().getSale(saleId);
            Coupon coupon = CouponsArchive.getInstance().getCoupon(couponId, sale.ProductInStoreId);
            if (coupon != null)
            {
                couponActivated = true;
                return true;
            }
            return false;
        }

        public double updateAndReturnFinalPrice(String couponId)
        {
            Sale sale = SalesArchive.getInstance().getSale(saleId);
            Coupon coupon = CouponsArchive.getInstance().getCoupon(couponId, sale.ProductInStoreId);
            Discount discount = DiscountsArchive.getInstance().getDiscount(sale.ProductInStoreId);
            double finalPrice = -1;
            ProductInStore product = ProductArchive.getInstance().getProductInStore(sale.ProductInStoreId);
            if (product != null)
            {
                finalPrice = product.getPrice() * amount;
                if (discount != null)
                {
                    finalPrice = discount.getPriceAfterDiscount(product.getPrice(), amount);
                }
                if (coupon != null && couponActivated)
                {
                    finalPrice = coupon.getPriceAfterCouponDiscount(product.getPrice());
                }
            }
            return finalPrice;
        }

    }
}
