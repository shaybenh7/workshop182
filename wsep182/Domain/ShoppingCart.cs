﻿using System;
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
            Sale saleExist = SalesArchive.getInstance().getSale(saleId);
            if (saleExist == null || !checkValidDate(saleExist))
            {
                return false;
            }
            
            if (saleExist.TypeOfSale != 1)
                return false;
            int amountInStore = ProductArchive.getInstance().getProductInStore(saleExist.ProductInStoreId).getAmount();
            if (amount > amountInStore || amount <= 0)
                return false;

            int amountForSale = SalesArchive.getInstance().getSale(saleId).Amount;
            if (amount > amountForSale || amount <= 0)
                return false;

            if (!(session.getState() is Guest))
                UserCartsArchive.getInstance().updateUserCarts(session.getUserName(), saleId, amount);

            UserCart toAdd = new UserCart(session.getUserName(), saleId, amount);
            foreach (UserCart c in products)
            {
                if(c.getUserName().Equals(toAdd.getUserName()) && c.getSaleId() == toAdd.getSaleId()){
                    if(c.getAmount() + amount <= amountInStore)
                    {
                        c.setAmount(c.getAmount() + amount);
                        return true;
                    }
                    return false;
                }
            }

            products.AddLast(toAdd);
            return true;
        }

        public Boolean addToCartRaffle(User session, Sale sale, double offer)
        {
            if (sale == null || sale.TypeOfSale != 3 || !checkValidDate(sale))
                return false;

            UserCart isExist = UserCartsArchive.getInstance().getUserCart(session.getUserName(), sale.SaleId);
            if (isExist != null)
            {
                return false;
            }
                
            foreach(UserCart uc in session.getShoppingCart())
            {
                if (uc.getSaleId() == sale.SaleId)
                    return false;
            }

            if (!(session.getState() is Guest))
            {
                UserCartsArchive.getInstance().updateUserCarts(session.getUserName(), sale.SaleId, 1,offer);
            }
            else
            {
                return false;
            }
            double remainingSum = getRemainingSumForOffers(sale.SaleId);
            if(offer > remainingSum || offer <= 0)
            {
                return false;
            }

            //UserCart toAdd = UserCartsArchive.getInstance().getUserCart(session.getUserName(), sale.SaleId);
            UserCart toAdd = new UserCart(session.getUserName(), sale.SaleId, 1);
            toAdd.setOffer(offer);
            session.getShoppingCart().AddLast(toAdd);
            return true;
        }

        public Boolean editCart(User session, int saleId, int newAmount)
        {
            Sale sale = SalesArchive.getInstance().getSale(saleId);
            if (sale == null)
                return false;

            if (sale.TypeOfSale == 3)
                return false;

            ProductInStore p = ProductArchive.getInstance().getProductInStore(sale.ProductInStoreId);
            if (newAmount > p.getAmount() || newAmount > sale.Amount ||newAmount <= 0)
                return false;
            

            if (!(session.getState() is Guest))
                UserCartsArchive.getInstance().editUserCarts(session.getUserName(), saleId, newAmount);

            foreach (UserCart product in products)
            {
                if (product.getUserName().Equals(session.getUserName()) && saleId == product.getSaleId())
                {
                    product.setAmount(newAmount);
                    return true;
                }
            }
            return false;
        }

        public Boolean removeFromCart(User session, Sale sale)
        {
            Sale isExist = SalesArchive.getInstance().getSale(sale.SaleId);
            if (isExist == null)
            {
                return false;
            }
            if (!(session.getState() is Guest))
            {
                if (!UserCartsArchive.getInstance().removeUserCart(session.getUserName(), sale.SaleId))
                    return false;
            }

            foreach(UserCart c in products)
            {
                if (c.getUserName().Equals(session.getUserName()) && c.getSaleId() == sale.SaleId)
                {
                    products.Remove(c);
                    return true;
                }
            }
            return false;
        }

        public Boolean buyProducts(User session, String creditCard, String couponId)
        {
            LinkedList<UserCart> toDelete = new LinkedList<UserCart>();
            Boolean allBought = true;
            if (creditCard == null || creditCard.Equals(""))
                return false;
            foreach (UserCart product in products)
            {
                if (couponId != null && couponId != "")
                {
                    product.activateCoupon(couponId);
                }
                Sale sale = SalesArchive.getInstance().getSale(product.getSaleId());
                if (sale.TypeOfSale == 1 && checkValidAmount(sale, product) && checkValidDate(sale)) //regular buy
                {
                    if (PaymentSystem.getInstance().payForProduct(creditCard, session, product))
                    {
                        ShippingSystem.getInstance().sendShippingRequest();
                        ProductInStore p = ProductArchive.getInstance().getProductInStore(sale.ProductInStoreId);
                        int productId = p.getProduct().getProductId();
                        int storeId = p.getStore().getStoreId();
                        String userName = session.getUserName();
                        double price = product.updateAndReturnFinalPrice(couponId);
                        DateTime currentDate = DateTime.Today;
                        String date = currentDate.ToString();
                        int amount = product.getAmount();
                        int typeOfSale = sale.TypeOfSale;
                        BuyHistoryArchive.getInstance().addBuyHistory(productId, storeId, userName, price, date, amount,
                            typeOfSale);
                        //products.Remove(product);
                        toDelete.AddLast(product);
                        SalesArchive.getInstance().setNewAmountForSale(product.getSaleId(), sale.Amount - product.getAmount());
                    }
                    else
                    {
                        allBought = false;
                    }
                }
                else if (sale.TypeOfSale == 2) // auction buy
                {}
                else if (sale.TypeOfSale == 3 && checkValidDate(sale)) // raffle buy
                {
                    double offer = product.getOffer();
                    double remainingSum = getRemainingSumForOffers(sale.SaleId);
                    if (offer > remainingSum)
                    {
                        allBought = false;
                    }
                    else
                    {
                        if (RaffleSalesArchive.getInstance().addRaffleSale(sale.SaleId, session.getUserName(), offer, sale.DueDate))
                        {
                            PaymentSystem.getInstance().payForProduct(creditCard, session, product);
                            ProductInStore p = ProductArchive.getInstance().getProductInStore(sale.ProductInStoreId);
                            int productId = p.getProduct().getProductId();
                            int storeId = p.getStore().getStoreId();
                            String userName = session.getUserName();
                            DateTime currentDate = DateTime.Today;
                            String date = currentDate.ToString();
                            int amount = product.getAmount();
                            int typeOfSale = sale.TypeOfSale;
                            BuyHistoryArchive.getInstance().addBuyHistory(productId, storeId, userName, offer, date, amount,
                                typeOfSale);
                            //products.Remove(product);
                            toDelete.AddLast(product);
                        }
                        else
                        {
                            allBought = false;
                        }
                    }
                }
            }
            foreach(UserCart uc in toDelete)
            {
                products.Remove(uc);
            }
            return allBought;
        }

        private Boolean checkValidAmount(Sale sale, UserCart cart)
        {
            if (cart.getAmount() <= sale.Amount)
                return true;
            return false;
        }

        private Boolean checkValidDate(Sale sale)
        {
            DateTime now = DateTime.Now;
            DateTime dueDateTime;
            try
            {
                dueDateTime = DateTime.Parse(sale.DueDate);
            }
            catch (System.FormatException e)
            {
                return false;
            }
            if (DateTime.Compare(dueDateTime, DateTime.Now) < 0)
                return false;
            return true;
        }

        private double getRemainingSumForOffers(int saleId)
        {
            Sale currSale = SalesArchive.getInstance().getSale(saleId);
            double totalPrice = ProductArchive.getInstance().getProductInStore(currSale.ProductInStoreId).getPrice();
            LinkedList<RaffleSale> sales = RaffleSalesArchive.getInstance().getAllRaffleSalesBySaleId(saleId);
            if (sales.Count() == 0)
                return totalPrice;
            else
            {
                foreach (RaffleSale sale in sales)
                {
                    totalPrice -= sale.Offer;
                }
                return totalPrice;
            }
        }


    }
}
