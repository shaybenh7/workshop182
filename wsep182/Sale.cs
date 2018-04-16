using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class Sale
    {
        
        int saleId;
        int productInStoreId;
        int typeOfSale;
        int amount;

        public Sale(int saleId, int productInStoreId, int typeOfSale, int amount)
        {
            this.saleId = saleId;
            this.productInStoreId = productInStoreId;
            this.typeOfSale = typeOfSale;
            this.amount = amount;
        }

        public int SaleId { get => saleId; set => saleId = value; }
        public int ProductInStoreId { get => productInStoreId; set => productInStoreId = value; }
        public int Amount { get => amount; set => amount = value; }
        public int TypeOfSale { get => typeOfSale; set => typeOfSale = value; }

        public double getPriceBeforeDiscount()
        {
            ProductInStore p = ProductArchive.getInstance().getProductInStore(productInStoreId);
            return p.getPrice() * amount;
        }
        public double getPriceAfterDiscount()
        {
            ProductInStore p = ProductArchive.getInstance().getProductInStore(productInStoreId);
            Discount d = DiscountsArchive.getInstance().getDiscount(productInStoreId);
            if (d != null)
            {
                //to add check for date validity
                return d.getPriceAfterDiscount(p.getPrice(), amount);
            }
            else
            {
                return getPriceBeforeDiscount();
            }
        }

    }
}
