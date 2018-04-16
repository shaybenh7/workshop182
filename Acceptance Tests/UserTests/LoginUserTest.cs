using System;
using wsep182.Domain;
using wsep182.services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Acceptance_Tests
{
    [TestClass]
    public class LoginUserTest
    {

        [TestInitialize]
        public void init()
        {
            ProductArchive.restartInstance();
            SalesArchive.restartInstance();
            storeArchive.restartInstance();
            UserArchive.restartInstance();
            UserCartsArchive.restartInstance();
        }

        [TestMethod]
        public void simpleLogin()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            us.register(session, "zahi", "123456");
            us.login(session, "zahi", "123456");
            Assert.AreEqual(session.getUserName(), "zahi");
            Assert.AreEqual(session.getPassword(), "123456");
            Assert.IsTrue(session.getState() is LogedIn);

        }

        [TestMethod]
        public void LoginToNoneExisitingUser()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            Assert.IsFalse(us.login(session,"zahi", "123456"));
        }

        [TestMethod]
        public void LoginWithBadUserName()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            us.register(session,"zahi", "123456");
            Assert.IsFalse(us.login(session, "gabi", "123456"));
            Assert.IsFalse(us.login(session, "Zahi", "123456"));
            Assert.IsFalse(us.login(session, "zahi ", "123456"));
            Assert.IsFalse(us.login(session, "zaHi", "123456"));
            Assert.IsFalse(us.login(session, "zahi1", "123456"));
            Assert.IsFalse(us.login(session, "zahI", "123456"));
            Assert.IsFalse(us.login(session, " zahi", "123456"));
        }

        [TestMethod]
        public void LoginWithBadPassword()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            us.register(session, "zahi", "abow");
            Assert.IsFalse(us.login(session, "zahi", "Abow"));
            Assert.IsFalse(us.login(session, "zahi", "aboW"));
            Assert.IsFalse(us.login(session, "zahi", "aBow"));
            Assert.IsFalse(us.login(session, "zahi", "abow1"));
            Assert.IsFalse(us.login(session, "zahi", "1abow"));
            Assert.IsFalse(us.login(session, "zahi", " abow"));
            Assert.IsFalse(us.login(session, "zahi", "abow "));
        }

        [TestMethod]
        public void LoginWithEmptyPasswordOrUsername()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            Assert.IsFalse(us.login(session,"", "abow"));
            Assert.IsFalse(us.login(session, "zahi", ""));
        }

        [TestMethod]
        public void LoginWhenThereIsNoUsers()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            Assert.IsFalse(us.login(session, "zahi", "abow"));
        }

        [TestMethod]
        public void LoginWithUserWithSpaces()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            Assert.IsFalse(us.login(session, "zahi abow", "abow"));
            Assert.IsFalse(us.login(session, "zahi", "zahi abow"));
        }

        [TestMethod]
        public void LoginUserWithNullPassword()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            Assert.IsFalse(us.login(session, "zahi", null));
        }

        [TestMethod]
        public void LoginUserWithNullUsername()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            Assert.IsFalse(us.login(session, null, "abow"));
        }
    }
}
