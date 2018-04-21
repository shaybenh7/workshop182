using System;
using wsep182.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class DicountArchiveUnitTests
    {
        DiscountsArchive discountsArchive;

        [TestInitialize]
        public void init()
        {
            DiscountsArchive.restartInstance();
            discountsArchive = DiscountsArchive.getInstance();
            discountsArchive.addNewDiscount(1, 10, DateTime.Now.AddDays(10).ToString());
            discountsArchive.addNewDiscount(2, 20, DateTime.Now.AddDays(15).ToString());
        }

        [TestMethod]
        public void addNewDiscount()
        {
            
            int beforeInsertion = discountsArchive.getAllDiscounts().Count;
            Boolean check = discountsArchive.addNewDiscount(3, 30, DateTime.Now.AddDays(10).ToString());
            int afterInsertion = discountsArchive.getAllDiscounts().Count;
            Assert.IsTrue(check);
            Assert.AreEqual(beforeInsertion + 1, afterInsertion);
        }
        [TestMethod]
        public void addExistingDiscount()
        {

            int beforeInsertion = discountsArchive.getAllDiscounts().Count;
            Boolean check = discountsArchive.addNewDiscount(1, 30, DateTime.Now.AddDays(10).ToString());
            int afterInsertion = discountsArchive.getAllDiscounts().Count;
            Assert.IsFalse(check);
            Assert.AreEqual(beforeInsertion , afterInsertion);
        }

        [TestMethod]
        public void removeExistingDiscount()
        {
            int beforeDeletion = discountsArchive.getAllDiscounts().Count;
            Boolean check = discountsArchive.removeDiscount(1);
            int afterDeletion = discountsArchive.getAllDiscounts().Count;
            Assert.IsTrue(check);
            Assert.AreEqual(beforeDeletion, afterDeletion+1);
        }

        [TestMethod]
        public void removeNonExistingDiscount()
        {
            int beforeDeletion = discountsArchive.getAllDiscounts().Count;
            Boolean check = discountsArchive.removeDiscount(3);
            int afterDeletion = discountsArchive.getAllDiscounts().Count;
            Assert.IsFalse(check);
            Assert.AreEqual(beforeDeletion, afterDeletion);
        }
        [TestMethod]
        public void editExistingDiscount()
        {  
            Boolean check = discountsArchive.editDiscount(1,50, DateTime.Now.AddMonths(10).ToString());    
            Assert.IsTrue(check);
            Discount d = discountsArchive.getDiscount(1);
            Assert.AreEqual(d.Percentage, 50);
            Assert.AreEqual(d.DueDate, DateTime.Now.AddMonths(10).ToString());
        }
        [TestMethod]
        public void editNonExistingDiscount()
        {
            Boolean check = discountsArchive.editDiscount(3, 50, DateTime.Now.AddMonths(10).ToString());
            Assert.IsFalse(check);
        }
        [TestMethod]
        public void getExistingDiscount()
        {
            Discount check = discountsArchive.getDiscount(1);
            Assert.IsTrue(check!=null);
        }
        [TestMethod]
        public void getNonExistingDiscount()
        {
            Discount check = discountsArchive.getDiscount(3);
            Assert.IsTrue(check == null);
        }



    }
}
