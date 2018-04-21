using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace UnitTests
{
    [TestClass]
    public class GuestUnitTests
    {

        private Guest g;

        [TestInitialize]
        public void init()
        {
            UserArchive.restartInstance();
            g = new Guest();
        }

        [TestMethod]
        public void simpleRegister()
        {
            Assert.IsTrue(g.register("zahi", "123456") is LogedIn);
        }

        [TestMethod]
        public void RegisterWithNullUserName()
        {
            Assert.IsNull(g.register(null, "123456"));
        }


        [TestMethod]
        public void RegisterWithEmptyUserName()
        {
            Assert.IsNull(g.register("", "123456"));
        }

        [TestMethod]
        public void RegisterWithNullPassword()
        {
            Assert.IsNull(g.register("zahi", null));
        }

        [TestMethod]
        public void RegisterWithEmptyPassword()
        {
            Assert.IsNull(g.register("zahi", ""));
        }

        [TestMethod]
        public void Simplelogin()
        {
            userServices us = userServices.getInstance();
            User session = us.startSession();
            us.register(session, "zahi", "123456");
            User u = g.login("zahi", "123456");
            Assert.AreEqual(u.getUserName(),"zahi");
        }

    }
}
