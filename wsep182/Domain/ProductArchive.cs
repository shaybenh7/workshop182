using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class ProductArchive
    {
        private static ProductArchive instance;
        private LinkedList<Product> products;
        private LinkedList<ProductInStore> productsInStores;
        private static int productInStoreId = 0;
        private static int productId = 0;
        private ProductArchive()
        {
            products = new LinkedList<Product>();
            productsInStores = new LinkedList<ProductInStore>();
        }
        public static ProductArchive getInstance()
        {
            if (instance == null)
                instance = new ProductArchive();
            return instance;
        }

        public static void restartInstance()
        {
            instance = new ProductArchive();
        }

        public int getNextProductId()
        {
            return ++productId;
        }
        public int getNextProductInStoreId()
        {
            return ++productInStoreId;
        }
        public Product addProduct(String productName)
        {
            foreach (Product p in products)
                if (p.getProductName() == productName)
                    return null;
            Product newProduct = new Product(productName, getNextProductId());
            products.AddLast(newProduct);
            return newProduct;
        }

        public Boolean updateProduct(Product newProduct)
        {
            foreach (Product p in products)
                if (p.getProductId() == newProduct.getProductId())
                {
                    products.Remove(p);
                    products.AddLast(newProduct);
                    return true;
                }
            return false;
        }

        public Product getProduct(int productId)
        {
            foreach (Product p in products)
                if (p.getProductId() == productId)
                    return p;
            return null;
        }

        public LinkedList<Product> getAllProducts()
        {
            return products;
        }

        public Product getProductByName(String name)
        {
            foreach (Product p in products)
                if (p.getProductName() == name)
                    return p;
            return null;
        }
        public Boolean removeProduct(int productId)
        {
            foreach (Product p in products)
                if (p.getProductId() == productId)
                {
                    products.Remove(p);
                    return true;
                }
            return false;
        }

        /*
         *         Product product;
        Store store;
        int quantity; //will be removed in the future
        double price;
        int isActive;
        int productInStoreId;
         */


        public ProductInStore addProductInStore(Product product, Store store, int quantity, double price)
        {
            ProductInStore newProduct;
            lock (this)
            {
                newProduct = new ProductInStore(getNextProductInStoreId(), product, price, quantity, store);
                productInStoreId++;
            }
            
            foreach (ProductInStore p in productsInStores)
                if (p.getProduct().getProductId() == newProduct.getProduct().getProductId() && p.getStore().getStoreId() == newProduct.getStore().getStoreId())
                    return null;
            productsInStores.AddLast(newProduct);
            return newProduct;
        }



        public Boolean updateProductInStore(ProductInStore newProduct)
        {
            foreach (ProductInStore p in productsInStores)
                if (p.getProduct().getProductId() == newProduct.getProduct().getProductId() && p.getStore().getStoreId() == newProduct.getStore().getStoreId())
                {
                    productsInStores.Remove(p);
                    productsInStores.AddLast(newProduct);
                    return true;
                }
            return false;
        }


        public ProductInStore getProductInStore(int productInStoreId)
        {
            foreach (ProductInStore p in productsInStores)
                if (p.getProductInStoreId() == productInStoreId)
                    return p;
            return null;
        }
        public LinkedList<ProductInStore> getProductsInStore(int storeId)
        {
            LinkedList<ProductInStore> ret = new LinkedList<ProductInStore>();
            foreach (ProductInStore p in productsInStores)
                if (p.getStore().getStoreId() == storeId)
                    ret.AddLast(p);
            return ret;
        }


        public ProductInStore getProductInStore(int productId, int storeId)
        {
            foreach (ProductInStore p in productsInStores)
                if (p.getProduct().getProductId() == productId && p.getStore().getStoreId() == storeId)
                    return p;
            return null;
        }

        public LinkedList<ProductInStore> getAllProductsInStore(int storeId)
        {
            LinkedList<ProductInStore> res = new LinkedList<ProductInStore>();
            foreach (ProductInStore p in productsInStores)
                if (p.getStore().getStoreId() == storeId)
                    if (p.getIsActive() == 1)
                        res.AddLast(p);
            return res;
        }

        public Boolean removeProductInStore(int productId, int storeId)
        {
            foreach (ProductInStore p in productsInStores)
                if (p.getProduct().getProductId() == productId && p.getStore().getStoreId() == storeId)
                {
                    if (p.getIsActive() == 0)
                        return false;
                    if (SalesArchive.getInstance().getSalesByProductInStoreId(p.getProductInStoreId()) != null)
                        return false;
                    p.IsActive = 0;
                    return true;
                }
            return false;
        }

        public int getProductInStoreQuantity(int productId, int storeId)
        {
            foreach (ProductInStore p in productsInStores)
                if (p.getProduct().getProductId() == productId && p.getStore().getStoreId() == storeId)
                {
                    return (p.getIsActive() == 1) ? p.getAmount() : 0;
                }
            return 0;
        }

        //gets minus amount in case of buying a product
        public Boolean decreaseIncreaseQuantity(int productId, int storeId, int amount)
        {
            foreach (ProductInStore p in productsInStores)
                if (p.getProduct().getProductId() == productId && p.getStore().getStoreId() == storeId)
                {
                    lock (p)
                    {
                        if (p.getAmount() > amount)
                        {
                            p.increaseDecreaseQuantity(amount);
                            return true;
                        }
                        else return false;
                    }
                }
            return false;
        }

        public LinkedList<ProductInStore> getAllProductsInStores()
        {
            LinkedList<ProductInStore> res = new LinkedList<ProductInStore>();
            foreach (ProductInStore p in productsInStores)
                if (p.getIsActive() == 1)
                    res.AddLast(p);
            return res;
        }
    }
}
