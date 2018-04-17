using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests
{
    [TestClass]
    public class RegisterUserTest
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
        public void SimpleRegister()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            us.register(session,"zahi", "123456");
            us.login(session, "zahi", "123456");
            Assert.AreEqual(session.getUserName(),"zahi");
            Assert.AreEqual(session.getPassword(), "123456");
            Assert.IsTrue(session.getState() is LogedIn);
        }

        [TestMethod]
        public void registerUserAlreadyExist()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            session.register("zahi", "123456");
            Assert.AreEqual(session.getUserName(), "guest");
            Assert.AreEqual(session.getPassword(), "guest");
            Assert.IsTrue(session.getState() is Guest);
            Assert.IsFalse(us.register(session, "zahi", "123456"));
            Assert.IsFalse(us.register(session, "zahi", "12345678"));

        }

        [TestMethod]
        public void registerUserWithEmptyUsername()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            Assert.IsFalse(session.register("", "12345678"));
        }

        [TestMethod]
        public void registerUserWithEmptyPassword()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            Assert.IsFalse(session.register("zahi", ""));
        }

        [TestMethod]
        public void registerUserWithspacesUsername()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            Assert.IsFalse(session.register("zahi abow", "123456"));
        }


        [TestMethod]
        public void registerUserWithNullPassword()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            Assert.IsFalse(session.register("zahi", null));
        }

        [TestMethod]
        public void registerUserWithNullUsername()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            Assert.IsFalse(session.register(null, "abow"));
        }
    }
}
