using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wsep182.Domain;

namespace wsep182.services
{
    public class sellServices
    {
        //req 1.5 a
        public Boolean addProductToCart(User user, Sale sale, int amount)
        {
            return user.addToCart(sale.SaleId, amount);
        }
        //req 1.5 b
        public Boolean addRaffleProductToCart(User user, Sale sale, double offer)
        {
            return user.addToCartRaffle(sale, offer);
        }

        // req 1.6.1
        public ShoppingCart viewCart(User user)
        {
            return user.getShoppingCart();
        }
        // req 1.6.2
        public Boolean editCart(User user, Sale sale, int newAmount)
        {
            return user.editCart(sale.SaleId, newAmount);
        }

        //req 1.7.1 for all the user cart
        public Boolean buyProducts(User session, String creditCard, String couponId)
        {
            return session.buyProducts(creditCard, couponId);
        }

        public LinkedList<Sale> viewSalesByProductInStoreId(ProductInStore productInStore)
        {
            return User.viewSalesByProductInStoreId(productInStore);
        }

    }
}
