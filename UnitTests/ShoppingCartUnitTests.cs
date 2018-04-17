using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;

namespace UnitTests
{
    [TestClass]
    public class ShoppingCartUnitTests
    {
        ShoppingCart cart;
        Product p1; Product p2; Product p3;
        ProductInStore pis1; ProductInStore pis2; ProductInStore pis3;
        User zahi,aviad;
        Store store;
        Sale sale1,sale2,sale3;
        ProductArchive pA;
        SalesArchive saleA;
        storeArchive storeA;
        UserArchive userA;
        
        UserCartsArchive userCartA;
        DiscountsArchive discountA;
        CouponsArchive couponA;
        RaffleSalesArchive raffleA;

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
            pA = ProductArchive.getInstance();
            saleA = SalesArchive.getInstance();
            storeA = storeArchive.getInstance();
            userA = UserArchive.getInstance();
            userCartA = UserCartsArchive.getInstance();
            discountA = DiscountsArchive.getInstance();
            couponA = CouponsArchive.getInstance();
            raffleA = RaffleSalesArchive.getInstance();

            p1 = pA.addProduct("Milk");
            p2 = pA.addProduct("Bread");
            p3 = pA.addProduct("T.V");

            zahi = new User("zahi", "123456");
            aviad = new User("aviad", "123456");
            aviad.register("aviad", "123456");
            zahi.register("zahi", "123456");

            store = storeA.addStore("zahi inc", zahi);

            pis1 = ProductArchive.getInstance().addProductInStore(p1, store,20, 10);
            pis2 = ProductArchive.getInstance().addProductInStore(p2, store, 30, 15);
            pis3 = ProductArchive.getInstance().addProductInStore(p3, store, 40, 50);

            sale1 = saleA.addSale(pis1.getProductInStoreId(), 1, 10, "1/5/2020");
            sale2 = saleA.addSale(pis2.getProductInStoreId(), 1, 10, "1/5/2020");
            sale3 = saleA.addSale(pis3.getProductInStoreId(), 3, 1, "1/5/2020");

            cart = new ShoppingCart();
        }

        //addToCart Tests

        [TestMethod]
        /**Description:
         * Function add to cart a new productInStore, which exist in the Store and sales archives
         * Outcome: TEST SOULD PASS!
         */
        public void simpleAddToCart()
        {
            Assert.IsTrue(cart.addToCart(aviad, sale1.SaleId, 1));
            Assert.IsTrue(cart.addToCart(aviad, sale2.SaleId, 1));
        }
        [TestMethod]
        /**Description:
         * add to cart raffle iteam
         * Outcome: TEST SOULD FAIL!
         */
        public void addToCartWhithRaffleProduct()
        {
            Assert.IsFalse(cart.addToCart(aviad, sale3.SaleId, 1));
        }

        [TestMethod]
        /**Description:
        * Function add to cart a product that not exsist
        * Outcome:  TEST SOULD RETURN FALSE!
        */
        public void addProductNotExsistToCart()
        {
            Assert.IsFalse(cart.addToCart(aviad, 4, 1));
        }
        [TestMethod]
        /**Description:
         * add product with amount to big
         * Outcome: 
         */
        public void addToCartAmountToBig()
        {
            Assert.IsFalse(cart.addToCart(aviad, 1, 100));
        }
        [TestMethod]
        /**Description:
         * add product with amount to zero
         * Outcome: 
         */
        public void addToCartAmountZero()
        {
            Assert.IsFalse(cart.addToCart(aviad, sale1.SaleId, 0));
        }
        [TestMethod]
        /**Description:
         * add product To not log in
         * Outcome: TEST SOULD PASS!
         */
        public void addToCartUserLogin()
        {
            aviad.login("aviad", "123456");
            Assert.IsTrue(cart.addToCart(aviad, 1, 1));
        }
        [TestMethod]
        /**Description:
         * add product To owner in store
         * Outcome: TEST SOULD PASS!
         */
        public void addToCartUserToOwner()
        {
            zahi.login("zahi", "123456");
            Assert.IsTrue(cart.addToCart(zahi, 1, 1));
        }
        [TestMethod]
        /**Description:
         * negative amount
         * Outcome: TEST SOULD PASS!
         */
        public void addToCartNegativeAmount()
        {
            aviad.login("aviad", "123456");
            Assert.IsFalse(cart.addToCart(aviad, 1, -1));
        }
        [TestMethod]
        /**Description:
         * same product add twis
         * Outcome: TEST SOULD PASS!
         */
        public void addToCartSameProduct()
        {
            aviad.login("aviad", "123456");
            Assert.IsTrue(cart.addToCart(aviad, 1, 1));
            Assert.IsTrue(cart.addToCart(aviad, 1, 1));
        }
        [TestMethod]
        /**Description:
         * same product add to diffrent users
         * Outcome: TEST SOULD PASS!
         */
        public void addToCartSameProductByDiffrentUsers()
        {
            User shay = new User("shay", "123456");
            userA.addUser(shay);
            aviad.login("aviad", "123456");
            Assert.IsTrue(cart.addToCart(aviad, 1, 1));
            Assert.IsTrue(cart.addToCart(shay, 1, 1));
        }


        //addRaffleToCart Tests

        [TestMethod]
        /**Description:
         * Function add to cart a new productInStore, which exist in the Store and sales archives
         * Outcome: TEST SOULD PASS!
         */
        public void simpleAddRaffleToCart()
        {
            aviad.login("aviad", "123456");
            Assert.IsTrue(cart.addToCartRaffle(aviad, sale3, 1));
        }
        [TestMethod]
        /**Description:
         *  add a normal product to raffle cart
         * Outcome: TEST SOULD PASS!
         */
        public void AddRaffleToCartNoramalBuyProduct()
        {
            aviad.login("aviad", "123456");
            Assert.IsFalse(cart.addToCartRaffle(aviad, sale2, 1));
        }
        [TestMethod]
        /**Description:
         *  gust add ruffle product
         * Outcome:  SOULD FAIL!
         */
        public void gustAddRaffleToCart()
        {
            Assert.IsFalse(cart.addToCartRaffle(aviad, sale3, 1));
        }
        [TestMethod]
        /**Description:
         *  zero price
         * Outcome:  SOULD FAIL!
         */
        public void addRaffleToCartZeroPrice()
        {
            aviad.login("aviad", "123456");
            Assert.IsFalse(cart.addToCartRaffle(aviad, sale3, 0));
        }
        [TestMethod]
        /**Description:
         *   price bigger then the product
         * Outcome:  SOULD FAIL!
         */
        public void addRaffleToCartBigPrice()
        {
            aviad.login("aviad", "123456");
            Assert.IsFalse(cart.addToCartRaffle(aviad, sale3, 200));
        }
        [TestMethod]
        /**Description:
         *   price bigger then the product
         * Outcome:  SOULD PASS!
         */
        public void addRaffleToCartExactPrice()
        {
            aviad.login("aviad", "123456");
            Assert.IsTrue(cart.addToCartRaffle(aviad, sale3, 50));
        }
        [TestMethod]
        /**Description:
         *   price bigger then the product
         * Outcome:  SOULD PASS!
         */
        public void simpleRaffleToCartAddTwoBids()
        {
            User shay = new User("shay", "123456");
            shay.register("shay", "123456");
            shay.login("shay", "123456");
            aviad.login("aviad", "123456");
            Assert.IsTrue(cart.addToCartRaffle(aviad, sale3, 10));
            Assert.IsTrue(cart.addToCartRaffle(shay, sale3, 10));
        }
        [TestMethod]
        /**Description:
         *   owner place bid
         * Outcome:  SOULD PASS!
         */
        public void RaffleToCartAddAsOwner()
        {
            zahi.login("zahi", "123456");
            Assert.IsTrue(cart.addToCartRaffle(zahi, sale3, 10));
        }
        [TestMethod]
        /**Description:
         *   owner place bid
         * Outcome:  SOULD FAIL!
         */
        public void RaffleToCartTotalToBig()
        {
            User shay = new User("shay", "123456");
            userA.addUser(shay);
            shay.login("shay", "123456");
            aviad.login("aviad", "123456");
            Assert.IsTrue(cart.addToCartRaffle(aviad, sale3, 10));
            Assert.IsFalse(cart.addToCartRaffle(shay, sale3, 50));
        }
        [TestMethod]
        /**Description:
         *   Exact Total
         * Outcome:  SOULD FAIL!
         */
        public void RaffleToCartExactTotal()
        {
            User shay = new User("shay", "123456");
            shay.register("shay", "123456");
            shay.login("shay", "123456");
            aviad.login("aviad", "123456");
            Assert.IsTrue(cart.addToCartRaffle(aviad, sale3, 10));
            Assert.IsTrue(cart.addToCartRaffle(shay, sale3, 50));

        }
        [TestMethod]
        /**Description:
         *   negative Bid
         * Outcome:  SOULD FAIL!
         */
        public void RaffleToCartNegativeBid()
        {          
            aviad.login("aviad", "123456");
            Assert.IsFalse(cart.addToCartRaffle(aviad, sale3, -1));
        }

        [TestMethod]
        /**Description:
         * same product bids twis
         * Outcome: TEST SOULD PASS!
         */
        public void RaffleToCartSameProduct()
        {
            aviad.login("aviad", "123456");
            Assert.IsTrue(cart.addToCart(aviad, 1, 1));
            Assert.IsTrue(cart.addToCart(aviad, 1, 1));
        }

        //getProductsInCart Tests

        [TestMethod]
        /**Description:
         * return all produacts in shoppingCart
         * Outcome: TEST SOULD PASS!
         */
        public void SimpleGetProdcts()
        {
            aviad.login("aviad", "123456");
            cart.addToCart(aviad, 1, 1);
            LinkedList<UserCart> shopingCart  = cart.getShoppingCartProducts(aviad);

            Assert.AreEqual(shopingCart.Count, 1);
            Assert.AreEqual(shopingCart.First.Value.getAmount(), 1);
            Assert.AreEqual(shopingCart.First.Value.getSaleId(), 1);
        }
        [TestMethod]
        /**Description:
         * return all produacts in shoppingCart of 2 gust
         * Outcome: TEST SOULD PASS!
         */
        public void getProdctsOfTwoGustCarts()
        {
            User shay = new User("shay", "123456");
            shay.register("shay", "123456");
            aviad.addToCart(sale1.SaleId, 1);
            shay.addToCart(sale2.SaleId, 2);
            LinkedList<UserCart> shopingCartAviad = cart.getShoppingCartProducts(aviad);
            LinkedList<UserCart> shopingCartShay = cart.getShoppingCartProducts(shay);

            Assert.AreEqual(shopingCartAviad.Count, 1);
            Assert.AreEqual(shopingCartAviad.First.Value.getAmount(), 1);
            Assert.AreEqual(shopingCartAviad.First.Value.getSaleId(), 1);
            Assert.AreEqual(shopingCartShay.Count, 1);
            Assert.AreEqual(shopingCartShay.First.Value.getAmount(), 2);
            Assert.AreEqual(shopingCartShay.First.Value.getSaleId(), 2);
        }
        [TestMethod]
        /**Description:
         * return all produacts in shoppingCart of 2 login
         * Outcome: TEST SOULD PASS!
         */
        public void getProdctsOfTwoLoginCarts()
        {
            User shay = new User("shay", "123456");
            shay.register("shay", "123456");
            shay.login("shay", "123456");
            aviad.login("aviad", "123456");
            aviad.addToCart(sale1.SaleId, 1);
            shay.addToCart(sale2.SaleId, 2);
            LinkedList<UserCart> shopingCartAviad = cart.getShoppingCartProducts(aviad);
            LinkedList<UserCart> shopingCartShay = cart.getShoppingCartProducts(shay);

            Assert.AreEqual(shopingCartAviad.Count, 1);
            Assert.AreEqual(shopingCartAviad.First.Value.getAmount(), 1);
            Assert.AreEqual(shopingCartAviad.First.Value.getSaleId(), 1);
            Assert.AreEqual(shopingCartShay.Count, 1);
            Assert.AreEqual(shopingCartShay.First.Value.getAmount(), 2);
            Assert.AreEqual(shopingCartShay.First.Value.getSaleId(), 2);
        }
        [TestMethod]
        /**Description:
         * add the 2 products
         * Outcome: TEST SOULD PASS!
         */
        public void getProdctsAddTwoProducts()
        {
            aviad.login("aviad", "123456");
            cart.addToCart(aviad, sale1.SaleId, 1);
            cart.addToCart(aviad, sale2.SaleId, 2);
            LinkedList<UserCart> shopingCart = cart.getShoppingCartProducts(aviad);

            Assert.AreEqual(shopingCart.Count, 2);
            foreach(UserCart uc in shopingCart){
                if (uc.getSaleId() != 1 && uc.getSaleId() != 2)
                    Assert.Fail();
                else
                {
                    if(uc.getSaleId() == 1)
                        Assert.AreEqual(uc.getAmount(), 1);
                    if (uc.getSaleId() == 2)
                        Assert.AreEqual(uc.getAmount(), 2);
                }
            }
        }
        [TestMethod]
        /**Description:
         * add the same product
         * Outcome: TEST SOULD PASS!
         */
        public void getProdctsAddSameProduct()
        {
            aviad.login("aviad", "123456");
            cart.addToCart(aviad, 1, 1);
            cart.addToCart(aviad, 1, 1);
            LinkedList<UserCart> shopingCart = cart.getShoppingCartProducts(aviad);

            Assert.AreEqual(shopingCart.Count, 1);
            Assert.AreEqual(shopingCart.First.Value.getAmount(), 2);
            Assert.AreEqual(shopingCart.First.Value.getSaleId(), 1);
        }
        [TestMethod]
        /**Description:
         * return all produacts in shoppingCart
         * Outcome: TEST SOULD PASS!
         */
        public void SimpleGetProdctsRaffle()
        {
            aviad.login("aviad", "123456");
            cart.addToCartRaffle(aviad, sale3, 1);
            LinkedList<UserCart> shopingCart = cart.getShoppingCartProducts(aviad);
            
            Assert.AreEqual(shopingCart.Count, 1);
            Assert.AreEqual(shopingCart.First.Value.getAmount(), 1);
            Assert.AreEqual(shopingCart.First.Value.getSaleId(), sale3.SaleId);
            Assert.AreEqual(shopingCart.First.Value.getOffer(), 1);
        }
        [TestMethod]
        /**Description:
         * add the same product
         * Outcome: TEST SOULD PASS!
         */
        public void getProdctsAddSameProductRaffle()
        {
            aviad.login("aviad", "123456");
            cart.addToCartRaffle(aviad, sale3, 1);
            cart.addToCartRaffle(aviad, sale3, 1);
            LinkedList<UserCart> shopingCart = cart.getShoppingCartProducts(aviad);
            Assert.AreEqual(shopingCart.Count, 1);
            Assert.AreEqual(shopingCart.First.Value.getAmount(), 1);
            Assert.AreEqual(shopingCart.First.Value.getSaleId(), sale3.SaleId);
            Assert.AreEqual(shopingCart.First.Value.getOffer(), 1);
        }

        //getProductsInCart Tests

        [TestMethod]
        /**Description:
         * Edit Cart
         * Outcome: TEST SOULD PASS!
         */
        public void simpleEditCart()
        {
            aviad.login("aviad", "123456");
            cart.addToCart(aviad, sale1.SaleId, 1);
            Assert.IsTrue(cart.editCart(aviad, sale1.SaleId, 2));
            LinkedList<UserCart> shopingCart = cart.getShoppingCartProducts(aviad);
            int c = shopingCart.Count;
            Assert.AreEqual(shopingCart.Count, 1);
            Assert.AreEqual(shopingCart.First.Value.getAmount(), 2);
            Assert.AreEqual(shopingCart.First.Value.getSaleId(), 1);
        }
        [TestMethod]
        /**Description:
         * Edit raffle product
         * Outcome: TEST SOULD PASS!
         */
        public void editCartRaffle()
        {
            aviad.login("aviad", "123456");
            cart.addToCartRaffle(aviad, sale3, 1);
            Assert.IsFalse(cart.editCart(aviad, sale3.SaleId, 2));
        }
        [TestMethod]
        /**Description:
         * negative amount
         * Outcome: TEST SOULD PASS!
         */
        public void editCartNegative()
        { 
            aviad.login("aviad", "123456");
            cart.addToCart(aviad, 1, 1);
            Assert.IsFalse(cart.editCart(aviad, 1, -2));
            LinkedList<UserCart> shopingCart = cart.getShoppingCartProducts(aviad);
            Assert.AreEqual(shopingCart.Count, 1);
            Assert.AreEqual(shopingCart.First.Value.getAmount(), 1);
            Assert.AreEqual(shopingCart.First.Value.getSaleId(), 1);
        }
        [TestMethod]
        /**Description:
         * zero amount
         * Outcome: TEST SOULD PASS!
         */
        public void editCartZero()
        {
            aviad.login("aviad", "123456");
            cart.addToCart(aviad, sale1.SaleId, 1);
            Assert.IsFalse(cart.editCart(aviad, sale1.SaleId, 0));
            LinkedList<UserCart> shopingCart = cart.getShoppingCartProducts(aviad);
            Assert.AreEqual(shopingCart.Count, 1);
            Assert.AreEqual(shopingCart.First.Value.getAmount(), 1);
            Assert.AreEqual(shopingCart.First.Value.getSaleId(), 1);
        }
        [TestMethod]
        /**Description:
         * big amount
         * Outcome: TEST SOULD PASS!
         */
        public void editCartBigAmount()
        {
            aviad.login("aviad", "123456");
            cart.addToCart(aviad, sale1.SaleId, 1);
            Assert.IsFalse(cart.editCart(aviad, sale1.SaleId, 100));
            LinkedList<UserCart> shopingCart = cart.getShoppingCartProducts(aviad);
            Assert.AreEqual(shopingCart.Count, 1);
            Assert.AreEqual(shopingCart.First.Value.getAmount(), 1);
            Assert.AreEqual(shopingCart.First.Value.getSaleId(), 1);
        }
        [TestMethod]
        /**Description:
         * big amount
         * Outcome: TEST SOULD PASS!
         */
        public void editCartNotExistProduct()
        {
            aviad.login("aviad", "123456");
            Assert.IsFalse(cart.editCart(aviad, sale1.SaleId, 1));
            LinkedList<UserCart> shopingCart = cart.getShoppingCartProducts(aviad);
            Assert.AreEqual(shopingCart.Count, 0);
            cart.addToCart(aviad, sale1.SaleId, 1);
            Assert.IsFalse(cart.editCart(aviad, sale2.SaleId, 1));
            shopingCart = cart.getShoppingCartProducts(aviad);
            Assert.AreEqual(shopingCart.Count, 1);

        }



    }
}
