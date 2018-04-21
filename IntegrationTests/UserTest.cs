using System;
using wsep182.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace IntegrationTests
{
    [TestClass]
    public class UserTest
    {
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
        }

        [TestMethod]
        public void LoginAndRegister()
        {
            User aviad=new User("aviad","123456");
            Assert.IsTrue(aviad.getState() is Guest);
            aviad.register("aviad", "123456");
            aviad.login("aviad", "123456");
            Assert.AreEqual(aviad.getUserName(), "aviad");
            Assert.AreEqual(aviad.getPassword(), "123456");
            Assert.IsTrue(aviad.getState() is LogedIn);
        }
        [TestMethod]
        public void createStoreAndOwnerManneger()
        {
            User aviad = new User("aviad", "123456");
            aviad.register("aviad", "123456");
            User zahi = new User("zahi", "123456");
            zahi.register("zahi", "123456");
            User niv = new User("niv", "123456");
            niv.register("niv", "123456");
            aviad.login("aviad", "123456");
            Store s=aviad.createStore("bro burger");
            Assert.AreEqual(s.getStoreName(), "bro burger");
            Assert.AreEqual(s.getOwners().Count, 1);
            StoreRole sr = new StoreOwner(aviad, s);
            sr.addStoreOwner(aviad, s, "zahi");
            Assert.AreEqual(s.getOwners().Count, 2);
            sr.addStoreManager(aviad, s, "niv");
            Assert.AreEqual(s.getManagers().Count, 1);
        }
        [TestMethod]
        public void UserAddProductAndViewIt()
        {
            User aviad = new User("aviad", "123456");
            aviad.register("aviad", "123456");
            aviad.login("aviad", "123456");
            Store s = aviad.createStore("bro burger");
            StoreRole sr = new StoreOwner(aviad, s);
            ProductInStore pis=sr.addProductInStore(aviad,s, "cola", 3.2, 10);
            LinkedList<ProductInStore> pisList = s.getProductsInStore();
            Assert.IsTrue(pisList.Contains(pis));
            Assert.AreEqual(pisList.Count, 1);
        }
    }
}
