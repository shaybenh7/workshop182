using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;

namespace UnitTests
{
    [TestClass]
    public class StoreArchiveTests
    {
        storeArchive sa;
        private User zahi;
        [TestInitialize]
        public void init()
        {
            storeArchive.restartInstance();
            sa = storeArchive.getInstance();
        }

        [TestMethod]
        public void addNewStore()
        {
            sa.addStore("vadim and sons", new User("checker", "123456"));
            Assert.AreEqual(1, sa.getAllStore().Count);
        }
        [TestMethod]
        public void updateStore()
        {
            Store s = sa.addStore("vadim and sons", new User("checker", "123456"));
            s.setStoreName("Susu and sons");
            sa.updateStore(s);
            Assert.AreEqual(1, sa.getAllStore().Count);
            Assert.AreEqual("Susu and sons",sa.getAllStore().First.Value.getStoreName());
        }
        [TestMethod]
        public void addStoreRole()
        {
            Store s = sa.addStore("vadim and sons", new User("checker", "123456"));
            User temp = new User("Vadim", "Vadim");
            Assert.IsTrue(sa.addStoreRole(new StoreManager(temp, s), s.getStoreId(), "Vadim"));
            Assert.IsFalse(sa.addStoreRole(new StoreManager(temp, s), s.getStoreId(), "Vadim"));

        }
        [TestMethod]
        public void getStore()
        {
            Store s = sa.addStore("vadim and sons", new User("checker", "123456"));
            Assert.IsNotNull(sa.getStore(s.getStoreId()));

        }
        [TestMethod]
        public void getStoreRole()
        {
            Store s = sa.addStore("vadim and sons", new User("checker", "123456"));
            User temp = new User("Vadim", "Vadim");
            Assert.IsTrue(sa.addStoreRole(new StoreManager(temp, s), s.getStoreId(), "Vadim"));
            Assert.IsTrue(sa.getStoreRole(s, temp) is StoreManager);
        }
        [TestMethod]
        public void removeStoreRole()
        {
            Store s = sa.addStore("vadim and sons", new User("checker", "123456"));
            User temp = new User("Vadim", "Vadim");
            Assert.IsTrue(sa.addStoreRole(new StoreManager(temp, s), s.getStoreId(), "Vadim"));
            Assert.IsTrue(sa.removeStoreRole(s.getStoreId(), temp.getUserName()));
            Assert.IsTrue(sa.getStoreRole(s, temp) is Customer);
        }
        [TestMethod]
        public void getAllOwners()
        {
            User itamar = new User("checker", "123456");
            Store s = sa.addStore("vadim and sons", itamar);
            User temp = new User("Vadim", "Vadim");
            Assert.IsTrue(sa.addStoreRole(new StoreOwner(temp, s), s.getStoreId(), "Vadim"));
            Assert.AreEqual(1, sa.getAllOwners(s.getStoreId()).Count);

        }
        [TestMethod]
        public void getAllManagers()
        {
            User itamar = new User("checker", "123456");
            Store s = sa.addStore("vadim and sons", itamar);
            User temp = new User("Vadim", "Vadim");
            Assert.IsTrue(sa.addStoreRole(new StoreManager(temp, s), s.getStoreId(), "Vadim"));
            Assert.AreEqual(1, sa.getAllManagers(s.getStoreId()).Count);

        }

        [TestMethod]
        public void getAllStoreRolesOfAUser()
        {
            User itamar = new User("checker", "123456");
            Store s = sa.addStore("vadim and sons", itamar);
            User temp = new User("Vadim", "Vadim");
            Assert.IsTrue(sa.addStoreRole(new StoreManager(temp, s), s.getStoreId(), "Vadim"));
            Store s2 = sa.addStore("vadim and sons2", itamar);
            Assert.IsTrue(sa.addStoreRole(new StoreManager(temp, s2), s.getStoreId(), "Vadim"));
            Assert.AreEqual(2, sa.getAllStoreRolesOfAUser(temp.getUserName()).Count);
        }
        [TestMethod]
        public void getAllStore()
        {
            User itamar = new User("checker", "123456");
            Store s = sa.addStore("vadim and sons", itamar);
            Store s2 = sa.addStore("vadim and sons2", itamar);
            Assert.AreEqual(2, sa.getAllStore().Count);
        }



    }
}
