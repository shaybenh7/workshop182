using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using wsep182.services;

namespace Acceptance_Tests.StoreTests
{

    /*
      "addProductInStore":
       "removeProductFromStore":
       "addStoreManager":
       "removeStoreManager":
       "addManagerPermission":
       "removeManagerPermission":
    */

    [TestClass]
    public class addManagerPermissionTests
    {
        private userServices us;
        private storeServices ss;
        private User zahi;  // owner of store
        private User aviad; //manager of store
        private User shay;
        private User itamar; // not a real user
        private User niv; // guest
        private Store store;

        [TestInitialize]
        public void init()
        {
            ProductArchive.restartInstance();
            SalesArchive.restartInstance();
            storeArchive.restartInstance();
            UserArchive.restartInstance();
            UserCartsArchive.restartInstance();
            us = userServices.getInstance();
            zahi = us.startSession();
            us.register(zahi, "zahi", "123456");
            us.login(zahi, "zahi", "123456");
            aviad = us.startSession();
            us.register(aviad, "aviad", "123456");
            us.login(aviad, "aviad", "123456");
            shay = us.startSession();
            us.register(shay, "shay", "123456");
            us.login(shay, "shay", "123456");
            itamar = new User("itamar", "123456");
            niv = us.startSession();
            us.register(niv, "niv", "123456");
            ss = storeServices.getInstance();
            store = ss.createStore("abowim", zahi);
            ss.addStoreManager(store, aviad, zahi);
            niv.logOut();
        }


        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
