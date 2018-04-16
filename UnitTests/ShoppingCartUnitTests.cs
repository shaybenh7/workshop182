using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;

namespace UnitTests
{
    [TestClass]
    public class ShoppingCartUnitTests
    {
        ShoppingCart cart;
        Product p1;Product p2;Product p3;
        ProductInStore pis1; ProductInStore pis2; ProductInStore pis3;

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

            p1 = new Product("Milk");
            p2 = new Product("Bread");
            p3 = new Product("T.V");

            cart = new ShoppingCart();
        }

        [TestMethod]
        /**Description:
         * Function add to cart a new productInStore, which exist in the Store and sales archives
         * Outcome: TEST SOULD PASS!
         */
        public void addToCart1()
        {
            
        }
        [TestMethod]
         /**Description:
         * 
         * Outcome: 
         */
        public void addToCart2()
        {
        }
        [TestMethod]
        /**Description:
         * 
         * Outcome: 
         */
        public void addToCart3()
        {
        }
        [TestMethod]
        /**Description:
         * 
         * Outcome: 
         */
        public void getProdcts()
        {

        }


    }
}
