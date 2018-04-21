using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        UserArchive ua;

        [TestInitialize]
        public void init()
        {
            UserArchive.restartInstance();
            ua = UserArchive.getInstance();
        }

        [TestMethod]
        public void simpleAddUser()
        {
            User u = new User("zahi", "abow");
            ua.addUser(u);
            User u2=ua.getUser("zahi");
            Assert.AreEqual(u.getUserName(),u2.getUserName());
            Assert.AreEqual(u.getPassword(), u2.getPassword());
        }

        [TestMethod]
        public void getUserWhichBNotExist()
        {
            User u2 = ua.getUser("zahi");
            Assert.IsNull(u2);
        }

        [TestMethod]
        public void simpleRemoveUser()
        {
            User u = new User("zahi", "abow");
            ua.addUser(u);
            ua.removeUser("zahi");
            User u2 = ua.getUser("zahi");
            Assert.IsFalse(u2.getIsActive());
        }

        [TestMethod]
        public void RemoveUserWhichNotExist()
        {
            User u = new User("zahi", "abow");
            ua.addUser(u);
            ua.removeUser("zahi2");
            User u2 = ua.getUser("zahi");
            Assert.AreEqual(u.getUserName(), u2.getUserName());
            Assert.AreEqual(u.getPassword(), u2.getPassword());
        }
    }
}
