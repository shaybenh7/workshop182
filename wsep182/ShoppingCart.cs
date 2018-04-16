using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class ShoppingCart
    {
        LinkedList<UserCart> products;
        public ShoppingCart()
        {
            products = new LinkedList<UserCart>();
        }

        public LinkedList<UserCart> getShoppingCartProducts(User session)
        {
            if (session.getState() is Guest)
                return products;
            else
                return UserCartsArchive.getInstance().getUserShoppingCart(session.getUserName());
        }

        public Boolean addToCart(User session, int saleId, int amount)
        {
            if (!(session.getState() is Guest))
                UserCartsArchive.getInstance().updateUserCarts(session.getUserName(), saleId, amount);

            UserCart toAdd = UserCartsArchive.getInstance().getUserCart(session.getUserName(), saleId);
            products.AddLast(toAdd);
            return true;
        }

        public Boolean editCart(User session, int saleId, int newAmount)
        {
            if (!(session.getState() is Guest))
                UserCartsArchive.getInstance().editUserCarts(session.getUserName(), saleId, newAmount);

            foreach(UserCart product in products)
            {
                if(product.getUserName().Equals(session.getUserName()) && saleId == product.getSaleId())
                {
                    product.setAmount(newAmount);
                    return true;
                }
            }
            return false;
        }

        public Boolean buyProducts(User session, String creditCard, String couponId)
        {
            foreach(UserCart product in products)
            {
                if(couponId != null && couponId != "")
                {
                    product.activateCoupon(couponId);
                }
                Sale sale = SalesArchive.getInstance().getSale(product.getSaleId());
                if (PaymentSystem.getInstance().payForProduct(creditCard, session, product))
                {
                    if (sale.TypeOfSale == 1)
                    {
                        ShippingSystem.getInstance().sendShippingRequest();
                        ProductInStore p = ProductArchive.getInstance().getProductInStore(sale.ProductInStoreId);
                        int productId = p.getProduct().getProductId();
                        int storeId = p.getStore().getStoreId();
                        String userName = session.getUserName();
                        double price = product.updateAndReturnFinalPrice(couponId);
                        String date = "TO IMPLEMENT";
                        int amount = product.getAmount();
                        int typeOfSale = sale.TypeOfSale;
                        BuyHistoryArchive.getInstance().addBuyHistory(productId, storeId, userName, price, date, amount,
                           typeOfSale);
                    }
                    products.Remove(product);
                }
            }
            return false;
        }

    }
}
