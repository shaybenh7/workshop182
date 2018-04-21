using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class RaffleSalesArchiveTests
    {
        RaffleSalesArchive rsa;
        [TestInitialize]
        public void init()
        {
            RaffleSalesArchive.restartInstance();
            rsa = RaffleSalesArchive.getInstance();
        }


        [TestMethod]
        public void simpleAddRaffleSale()
        {
            Assert.IsTrue(rsa.addRaffleSale(1, "zahi", 10, "20/3/12"));
            LinkedList<RaffleSale> lrs = rsa.getAllRaffleSalesBySaleId(1);
            Assert.AreEqual(lrs.Count, 1);
            Assert.AreEqual(lrs.First.Value.UserName, "zahi");
            Assert.AreEqual(lrs.First.Value.Offer, 10);
            Assert.AreEqual(lrs.First.Value.DueDate, "20/3/12");
        }

        [TestMethod]
        public void getAllRaffleSalesBySaleIdWithWrongStoreId()
        {
            Assert.IsTrue(rsa.addRaffleSale(1, "zahi", 10, "20/3/12"));
            LinkedList<RaffleSale> lrs = rsa.getAllRaffleSalesBySaleId(2);
            Assert.AreEqual(lrs.Count, 0);
        }

        [TestMethod]
        public void getAllRaffleSalesBySaleIdwhenThereIsNotRaffleStore()
        {
            LinkedList<RaffleSale> lrs = rsa.getAllRaffleSalesBySaleId(2);
            Assert.AreEqual(lrs.Count, 0);
        }

        [TestMethod]
        public void SimpleGetAllRaffleSalesByUserName()
        {
            Assert.IsTrue(rsa.addRaffleSale(1, "zahi", 10, "20/3/12"));
            LinkedList<RaffleSale> lrs = rsa.getAllRaffleSalesByUserName("zahi");
            Assert.AreEqual(lrs.Count, 1);
            Assert.AreEqual(lrs.First.Value.UserName, "zahi");
            Assert.AreEqual(lrs.First.Value.Offer, 10);
            Assert.AreEqual(lrs.First.Value.DueDate, "20/3/12");
        }

    }
}
