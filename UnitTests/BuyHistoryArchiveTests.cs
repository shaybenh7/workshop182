using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;

namespace UnitTests
{
    [TestClass]
    public class BuyHistoryArchiveTests
    {
         private BuyHistoryArchive bha;

        [TestInitialize]
        public void init()
        {
            BuyHistoryArchive.restartInstance();
            bha = BuyHistoryArchive.getInstance();
        }

        [TestMethod]
        public void SimpleAddToHistory()
        {
            Assert.IsTrue(bha.addBuyHistory(1,2,"zahi",10,"20/10/18", 3 ,1));
            LinkedList<Purchase> lp = bha.viewHistory();
            Assert.AreEqual(lp.Count,1);
            Assert.AreEqual(lp.First.Value.ProductId, 1);
            Assert.AreEqual(lp.First.Value.StoreId, 2);
            Assert.AreEqual(lp.First.Value.UserName, 3);
            Assert.AreEqual(lp.First.Value.TypeOfSale, 1);
            Assert.AreEqual(lp.First.Value.Price, 10);
            Assert.AreEqual(lp.First.Value.Date, "20/10/18");
            Assert.AreEqual(lp.First.Value.Amount, "20/10/18");
        }


        [TestMethod]
        public void SimpleViewHistory()
        {
            LinkedList<Purchase> lp = bha.viewHistory();
            Assert.AreEqual(lp.Count, 0);
        }

        [TestMethod]
        public void SimpleViewHistoryByStoreId()
        {
            Assert.IsTrue(bha.addBuyHistory(1, 2, "zahi", 10, "20/10/18", 3, 1));
            LinkedList<Purchase> lp = bha.viewHistoryByStoreId(2);
            Assert.AreEqual(lp.Count, 1);
            Assert.AreEqual(lp.First.Value.ProductId, 1);
            Assert.AreEqual(lp.First.Value.StoreId, 2);
            Assert.AreEqual(lp.First.Value.UserName, 3);
            Assert.AreEqual(lp.First.Value.TypeOfSale, 1);
            Assert.AreEqual(lp.First.Value.Price, 10);
            Assert.AreEqual(lp.First.Value.Date, "20/10/18");
            Assert.AreEqual(lp.First.Value.Amount, "20/10/18");
        }

        [TestMethod]
        public void SimpleViewHistoryByUserName()
        {
            Assert.IsTrue(bha.addBuyHistory(1, 2, "zahi", 10, "20/10/18", 3, 1));
            LinkedList<Purchase> lp = bha.viewHistoryByUserName("zahi");
            Assert.AreEqual(lp.Count, 1);
            Assert.AreEqual(lp.First.Value.ProductId, 1);
            Assert.AreEqual(lp.First.Value.StoreId, 2);
            Assert.AreEqual(lp.First.Value.UserName, 3);
            Assert.AreEqual(lp.First.Value.TypeOfSale, 1);
            Assert.AreEqual(lp.First.Value.Price, 10);
            Assert.AreEqual(lp.First.Value.Date, "20/10/18");
            Assert.AreEqual(lp.First.Value.Amount, "20/10/18");
        }
    }
}
