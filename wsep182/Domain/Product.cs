using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class Product
    {
        private int productId;
        private String name;
        public Product(String name)
        {
            this.name = name;
            this.productId = -1;
        }
        public Product(String name, int id)
        {
            this.name = name;
            this.productId = id;
        }
        public int getProductId()
        {
            return productId;
        }
        public String getProductName()
        {
            return this.name;
        }

        public static Product addProduct(Product p)
        {
            return ProductArchive.getInstance().addProduct(p);
        }

        public static LinkedList<Product> getProducts()
        {
            return ProductArchive.getInstance().getAllProducts();
        }


    }
}
