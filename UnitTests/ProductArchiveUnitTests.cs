using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wsep182.Domain;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class ProductArchiveUnitTests
    {
        ProductArchive productArchive;
        Product p1;
        [TestInitialize]
        public void init()
        {
            ProductArchive.restartInstance();
            productArchive = ProductArchive.getInstance();
            p1 = productArchive.addProduct("bread");
        }

        [TestMethod]
        public void addANewProduct()
        {
            int beforeInsertion = productArchive.getAllProducts().Count;
            Product check = productArchive.addProduct("milk");
            int afterInsertion = productArchive.getAllProducts().Count;
            Assert.IsTrue(check != null);
            Assert.AreEqual(beforeInsertion + 1, afterInsertion);
            Product bread = productArchive.getProduct(1);
            Assert.AreEqual(check.getProductId(), bread.getProductId()+1);
        }
        [TestMethod]
        public void addExistingProduct()
        {
            int beforeInsertion = productArchive.getAllProducts().Count;
            Product check = productArchive.addProduct("bread");
            int afterInsertion = productArchive.getAllProducts().Count;
            Assert.IsTrue(check == null);
            Assert.AreEqual(beforeInsertion , afterInsertion);
        }

        [TestMethod]
        public void editExistingProduct()
        {
            Product bread = productArchive.getProduct(1);
            Assert.IsTrue(bread != null);
            bread.setProductName("bread-2018");
            Boolean check = productArchive.updateProduct(bread);
            Assert.IsTrue(check);
            Product newBread = productArchive.getProduct(1);
            Assert.AreEqual(newBread.getProductName(), "bread-2018");
        }
        [TestMethod]
        public void editNonExistingProduct()
        {
            Product bread = productArchive.getProduct(3);
            Assert.IsTrue(bread == null);
            Boolean check = productArchive.updateProduct(bread);
            Assert.IsFalse(check);
        }

        [TestMethod]
        public void getExistingProduct()
        {
            Product bread = productArchive.getProduct(1);
            Assert.IsTrue(bread != null);
        }
        [TestMethod]
        public void getNonExistingProduct()
        {
            Product bread = productArchive.getProduct(13);
            Assert.IsTrue(bread == null);
        }
        [TestMethod]
        public void getExistingProductByName()
        {
            Product bread = productArchive.getProductByName("bread");
            Assert.IsTrue(bread != null);
        }
        [TestMethod]
        public void getNonExistingProductByName()
        {
            Product bread = productArchive.getProductByName("PickleRick");
            Assert.IsTrue(bread == null);
        }
        [TestMethod]
        public void removeExistingProduct()
        {
            int before = productArchive.getAllProducts().Count;
            Boolean deleted = productArchive.removeProduct(1);
            int after = productArchive.getAllProducts().Count;
            Assert.IsTrue(deleted);
            Assert.AreEqual(productArchive.getAllProducts().Count, 0);
            Assert.AreEqual(before, after +1);
        }
        [TestMethod]
        public void removeNonExistingProduct()
        {
            int before = productArchive.getAllProducts().Count;
            Boolean deleted = productArchive.removeProduct(13);
            int after = productArchive.getAllProducts().Count;
            Assert.IsFalse(deleted);
            Assert.AreEqual(productArchive.getAllProducts().Count, 1);
            Assert.AreEqual(before, after);
        }

        [TestMethod]
        public void addNewProductInStore()
        {
            Store store = new Store(1, "halavi", null);
            Product milk = productArchive.addProduct("milk");
            ProductInStore milkInStore = productArchive.addProductInStore(milk, store, 50,50);
            Assert.IsTrue(milkInStore != null);
            Assert.AreEqual(milkInStore.getPrice(), 50);
            Assert.AreEqual(milkInStore.getProduct(), milk);
            Assert.AreEqual(milkInStore.getAmount(), 50);
            Assert.AreEqual(milkInStore.getStore(), store);
        }
        [TestMethod]
        public void addExistingProductInStore()
        {
            Store store = new Store(1, "halavi", null);
            Product milk = productArchive.addProduct("milk");
            ProductInStore breadInStore = productArchive.addProductInStore(milk, store, 50, 50);
            ProductInStore anotherBread = productArchive.addProductInStore(milk, store, 60, 60);
            Assert.AreEqual(anotherBread, null);
        }

        [TestMethod]
        public void editExistingProductInStore()
        {
            Store store = new Store(1, "halavi", null);
            Product milk = productArchive.addProduct("milk");
            ProductInStore breadInStore = productArchive.addProductInStore(milk, store, 50, 50);
            // at this point bread is in store - already been tested
            breadInStore.Quantity = 200;
            breadInStore.Price = 3000;
            int id = breadInStore.getProductInStoreId();
            Boolean check = productArchive.updateProductInStore(breadInStore);
            Assert.IsTrue(check);
            ProductInStore b = productArchive.getProductInStore(id);
            Assert.AreEqual(b.getPrice(), 3000);
            Assert.AreEqual(b.getAmount(), 200);
        }

        [TestMethod]
        public void editNonExistingProductInStore()
        {
            ProductInStore check1 = null;
            Boolean check1Ans = productArchive.updateProductInStore(check1);
            Assert.IsFalse(check1Ans);

            Store store = new Store(1, "halavi", null);
            Product milk = productArchive.addProduct("milk");
            ProductInStore breadInStore = productArchive.addProductInStore(milk, store, 50, 50);

            ProductInStore check2 = new ProductInStore(2, null, 13,8 , null);
            Boolean check2Ans = productArchive.updateProductInStore(check2);
            Assert.IsFalse(check2Ans);
        }

        [TestMethod]
        public void getExistingProductInStore()
        {
            Store store = new Store(1, "halavi", null);
            Product milk = productArchive.addProduct("milk");
            ProductInStore breadInStore = productArchive.addProductInStore(milk, store, 50, 50);

            ProductInStore check = productArchive.getProductInStore(breadInStore.getProductInStoreId());
            Assert.IsTrue(check != null);
        }
        [TestMethod]
        public void getNonExistingProductInStore()
        {
            ProductInStore check = productArchive.getProductInStore(17);
            Assert.IsTrue(check == null);
        }

        [TestMethod]
        public void getProductsInStoreByStoreId()
        {
            
            Store store = new Store(1, "halavi", null);
            LinkedList<ProductInStore> pis = productArchive.getProductsInStore(1);
            Assert.AreEqual(pis.Count, 0);
            Product milk = productArchive.addProduct("milk");
            ProductInStore milkInStore = productArchive.addProductInStore(milk, store, 50, 50);
            Product meat = productArchive.addProduct("meat");
            ProductInStore meatInStore = productArchive.addProductInStore(meat, store, 20, 30);

            pis = productArchive.getProductsInStore(1);
            Assert.AreEqual(pis.Count, 2);
        }

        [TestMethod]
        public void removeProductInStore()
        {
            Store store = new Store(1, "halavi", null);
            Product milk = productArchive.addProduct("milk");
            ProductInStore milkInStore = productArchive.addProductInStore(milk, store, 50, 50);
            Boolean check = productArchive.removeProductInStore(milkInStore.getProductInStoreId(), store.getStoreId());
            Assert.IsTrue(check);
            Assert.IsTrue(store.getProductsInStore().Count == 0);
        }

        [TestMethod]
        public void removeNonExistentProductInStore()
        {
            Store store = new Store(1, "halavi", null);
            Boolean check = productArchive.removeProductInStore(13, store.getStoreId());
            Assert.IsFalse(check);
            Assert.IsTrue(store.getProductsInStore().Count == 0);
        }

        [TestMethod]
        public void getProductInStoreQuantity()
        {
            Store store = new Store(1, "halavi", null);
            Product milk = productArchive.addProduct("milk");
            ProductInStore milkInStore = productArchive.addProductInStore(milk, store, 50, 50);
            int quantity = ProductArchive.getInstance().getProductInStoreQuantity(milkInStore.getProductInStoreId());
            Assert.AreEqual(quantity,milkInStore.getAmount());
        }

        [TestMethod]
        public void getAllProductsInStores()
        {
            Store store = new Store(1, "halavi", null);
            Store store2 = new Store(2, "tiv-taam", null);
            Store store3 = new Store(3, "super-li", null);
            Store store4 = new Store(4, "soda-stream", null);
            Product milk = productArchive.addProduct("milk");
            Product soda = productArchive.addProduct("soda");
            Product water = productArchive.addProduct("water");
            Product pc = productArchive.addProduct("pc");
            ProductInStore milkInStore = productArchive.addProductInStore(milk, store, 50, 50);
            ProductInStore sodaInStore = productArchive.addProductInStore(soda, store2, 50, 50);
            ProductInStore waterInStore = productArchive.addProductInStore(water, store3, 50, 50);
            ProductInStore pcInStore = productArchive.addProductInStore(pc, store3, 50, 50);

            LinkedList<ProductInStore> ans = ProductArchive.getInstance().getAllProductsInStores();
            Assert.AreEqual(ans.Count, 4);
        }



    }
}
