.using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;
namespace Acceptance_Tests.StoreTests
{
    [TestClass]
    public class editSaleTests
    {

        private userServices us;
        private storeServices ss;
        private User zahi; 
        private Store store;//itamar owner , niv manneger
        ProductInStore cola;
        Sale colaSale;

        [TestInitialize]
        public void init()
        {
            ProductArchive.restartInstance();
            SalesArchive.restartInstance();
            storeArchive.restartInstance();
            UserArchive.restartInstance();
            UserCartsArchive.restartInstance();
            BuyHistoryArchive.restartInstance();
            CouponsArchive.restartInstance();
            DiscountsArchive.restartInstance();
            RaffleSalesArchive.restartInstance();
            StorePremissionsArchive.restartInstance();

            us = userServices.getInstance();
            ss = storeServices.getInstance();
           
            zahi = us.startSession();
            us.register(zahi, "zahi", "123456");
            us.login(zahi, "zahi", "123456");

            store = ss.createStore("Abowim", zahi);

            cola = ss.addProductInStore("cola", 3.2, 10, zahi, store);

            ss.addSaleToStore(zahi, store, cola.getProductInStoreId(), 1, 2, "20/5/2018");

            LinkedList<Sale> SL = ss.viewSalesByStore(store);
            foreach(Sale sale in SL)
            {
                if(sale.ProductInStoreId == cola.getProductInStoreId())
                {
                    colaSale = sale;
                }
            }
        }

        [TestMethod]
        public void simpleEditSale()
        {
            Assert.IsTrue(ss.editSale(zahi, store, colaSale.SaleId, 5, "20/6/2018"));
            Assert.AreEqual(colaSale.Amount, 5);
            Assert.AreEqual(colaSale.DueDate, "20/6/2018");
        }

        [TestMethod]
        public void EditSaleToNegAmount()
        {
            Assert.IsFalse(ss.editSale(zahi, store, colaSale.SaleId,-1, "20/6/2018"));
            Assert.AreEqual(colaSale.Amount, 2);
            Assert.AreEqual(colaSale.DueDate, "20/5/2018");
        }

        [TestMethod]
        public void EditSaleToAmountBiggerThanTheAmountOfProduct()
        {
            Assert.IsFalse(ss.editSale(zahi, store, colaSale.SaleId, 11, "20/6/2018"));
            Assert.AreEqual(colaSale.Amount, 2);
            Assert.AreEqual(colaSale.DueDate, "20/5/2018");
        }

        [TestMethod]
        public void EditSaleToDueDateInThePastInYears()
        {
            Assert.IsFalse(ss.editSale(zahi, store, colaSale.SaleId, 11, "20/6/2017"));
            Assert.AreEqual(colaSale.Amount, 2);
            Assert.AreEqual(colaSale.DueDate, "20/5/2018");
        }

        [TestMethod]
        public void EditSaleToDueDateInThePastInMonth()
        {
            Assert.IsFalse(ss.editSale(zahi, store, colaSale.SaleId, 11, "20/3/2018"));
            Assert.AreEqual(colaSale.Amount, 2);
            Assert.AreEqual(colaSale.DueDate, "20/5/2018");
        }

        [TestMethod]
        public void EditSaleToDueDateInThePastInDays()
        {
            Assert.IsFalse(ss.editSale(zahi, store, colaSale.SaleId, 11, "20/4/2018"));
            Assert.AreEqual(colaSale.Amount, 2);
            Assert.AreEqual(colaSale.DueDate, "20/5/2018");
        }

        [TestMethod]
        public void EditSaleToDueDateNull()
        {
            Assert.IsFalse(ss.editSale(zahi, store, colaSale.SaleId, 11, null));
            Assert.AreEqual(colaSale.Amount, 2);
            Assert.AreEqual(colaSale.DueDate, "20/5/2018");
        }
    }
}
