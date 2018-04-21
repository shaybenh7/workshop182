using System;
using wsep182.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class UserCartArchiveUnitTest
    {
        private UserCartsArchive userCartsArchive;

        [TestInitialize]
        public void init()
        {
            UserCartsArchive.restartInstance();
            userCartsArchive = UserCartsArchive.getInstance();
            userCartsArchive.updateUserCarts("itamar", 1, 1);
            userCartsArchive.updateUserCarts("itamar", 2, 1);
            userCartsArchive.updateUserCarts("niv", 3, 3, 50);
            userCartsArchive.updateUserCarts("niv", 3, 1);
        }

        [TestMethod]
        public void addNonExistingUserCart()
        {
            Boolean check = userCartsArchive.updateUserCarts("zahi", 1, 12);
            Assert.IsTrue(check);
        }
        [TestMethod]
        public void addExistingUserCartWithSameUserNameAndSaleId()
        {
            Boolean check = userCartsArchive.updateUserCarts("itamar", 1, 14);
            Assert.IsTrue(check);
            UserCart uc = userCartsArchive.getUserCart("itamar", 1);
            Assert.AreEqual(uc.getAmount(), 15);
        }

        [TestMethod]
        public void addNonExistingUserCartRaffle()
        {
            Boolean check = userCartsArchive.updateUserCarts("itamar", 4, 12,50);
            Assert.IsTrue(check);
            UserCart uc = userCartsArchive.getUserCart("itamar", 4);
            Assert.AreEqual(uc.getOffer(), 50);
        }
        [TestMethod]
        public void placeNewBidOnUserCartRaffleSale()
        {
            Boolean check = userCartsArchive.updateUserCarts("itamar", 4, 12, 50);
            Assert.IsTrue(check);
            UserCart uc = userCartsArchive.getUserCart("itamar", 4);
            Assert.AreEqual(uc.getOffer(), 50);
            check = userCartsArchive.updateUserCarts("itamar", 4, 12, 100);
            uc = userCartsArchive.getUserCart("itamar", 4);
            Assert.AreEqual(uc.getOffer(), 100);
        }

        [TestMethod]
        public void editExistingUserCart()
        {
            Boolean check = userCartsArchive.editUserCarts("itamar", 1, 20);
            Assert.IsTrue(check);
            UserCart uc = userCartsArchive.getUserCart("itamar", 1);
            Assert.AreEqual(uc.getAmount(), 20);
        }
        [TestMethod]
        public void editNonExistingUserCart()
        {
            Boolean check = userCartsArchive.editUserCarts("non-existing", 1, 20);
            Assert.IsFalse(check);
            check = userCartsArchive.editUserCarts("itamar", 13, 20);
            Assert.IsFalse(check);
        }

        [TestMethod]
        public void getExistingUserCart()
        {
            UserCart check = userCartsArchive.getUserCart("itamar", 1);
            Assert.IsTrue(check != null);
        }
        [TestMethod]
        public void getNonExistingUserCart()
        {
            UserCart check = userCartsArchive.getUserCart("aviad", 1);
            Assert.IsTrue(check == null);
        }
        public void getExistingShoppingCart()
        {
            LinkedList<UserCart> check = userCartsArchive.getUserShoppingCart("itamar");
            Assert.IsTrue(check.Count > 0);
        }
        [TestMethod]
        public void getNonExistingShoppingCart()
        {
            LinkedList<UserCart> check = userCartsArchive.getUserShoppingCart("aviad");
            Assert.IsTrue(check.Count == 0);
        }

        [TestMethod]
        public void removeExistingUserCart()
        {
            int beforeDeletion = userCartsArchive.getUserShoppingCart("itamar").Count;
            Boolean check = userCartsArchive.removeUserCart("itamar", 1);
            int afterDeletion = userCartsArchive.getUserShoppingCart("itamar").Count;
            Assert.IsTrue(check);
            Assert.AreEqual(beforeDeletion, afterDeletion + 1);
        }
        [TestMethod]
        public void removeNonExistingUserCart()
        {
            int beforeDeletion = userCartsArchive.getUserShoppingCart("itamar").Count;
            Boolean check = userCartsArchive.removeUserCart("itamar", 13);
            int afterDeletion = userCartsArchive.getUserShoppingCart("itamar").Count;
            Assert.IsFalse(check);
            Assert.AreEqual(beforeDeletion, afterDeletion);
        }

    }
}
