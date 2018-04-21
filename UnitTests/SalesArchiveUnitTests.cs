using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class SalesArchiveUnitTests
    {
        SalesArchive sa;

        [TestInitialize]
        public void init()
        {
            SalesArchive.restartInstance();
            sa = SalesArchive.getInstance();
        }

        [TestMethod]
        public void simpleAddSaleTest()
        {
            Sale s =sa.addSale(1,1,10,"20/5/2018");
            Sale s2 = sa.getSale(s.SaleId);
            Assert.AreEqual(s.ProductInStoreId, s2.ProductInStoreId);
            Assert.AreEqual(s.SaleId, s2.SaleId);
            Assert.AreEqual(s.TypeOfSale, s2.TypeOfSale);
        }

        [TestMethod]
        public void removeSale()
        {
            Sale s = sa.addSale(1, 1, 10, "20/5/2018");
            sa.removeSale(s.SaleId);
            Sale s2 = sa.getSale(s.SaleId);
            Assert.IsNull(s2);
        }

        [TestMethod]
        public void getSaleNotExist()
        {
            Sale s2 = sa.getSale(1);
            Assert.IsNull(s2);
        }
    }
}
