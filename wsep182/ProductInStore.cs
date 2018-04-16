using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class ProductInStore
    {
        Product product;
        Store store;
        Discount Discount;
        int quantity; //will be removed in the future
        double price;
        int isActive;
        int productInStoreId;
        public ProductInStore(int id, Product p,double price, int amount,Store s)
        {
            this.productInStoreId = id;
            this.product = p;
            this.price = price;
            this.quantity = amount;
            this.store = s;
            this.Discount = null;
            this.isActive = 1;
        }
        public int getProductInStoreId()
        {
            return productInStoreId;
        }
        public Product getProduct()
        {
            return product;
        }
        public Store getStore()
        {
            return store;
        }
        public int getAmount()
        {
            return quantity;
        }
        public double getPrice()
        {
            return price;
        }
        public int getIsActive()
        {
            return isActive;
        }
        public void increaseDecreaseQuantity(int amount)
        {
           this.quantity += amount;
        }


}
}
