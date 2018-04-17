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
        String dueDate;

        public Sale(int saleId, int productInStoreId, int typeOfSale, int amount, String dueDate)
        {
            this.saleId = saleId;
            this.productInStoreId = productInStoreId;
            this.typeOfSale = typeOfSale;
            this.amount = amount;
            this.dueDate = dueDate;
        }

        public int SaleId { get => saleId; set => saleId = value; }
        public int ProductInStoreId { get => productInStoreId; set => productInStoreId = value; }
        public int Amount { get => amount; set => amount = value; }
        public int TypeOfSale { get => typeOfSale; set => typeOfSale = value; }
        public string DueDate { get => dueDate; set => dueDate = value; }

        public double getPriceBeforeDiscount(int amount)
        {
            ProductInStore p = ProductArchive.getInstance().getProductInStore(productInStoreId);
            return p.getPrice() * amount;
        }
        public double getPriceAfterDiscount(int amount)
        {
            ProductInStore p = ProductArchive.getInstance().getProductInStore(productInStoreId);
            Discount d = DiscountsArchive.getInstance().getDiscount(productInStoreId);
            if (d != null)
            {
                DateTime currDate = DateTime.Today;
                DateTime discountDueDate = DateTime.Parse(d.DueDate);
                int result = DateTime.Compare(currDate, discountDueDate);
                if (result < 0)
                {
                    return d.getPriceAfterDiscount(p.getPrice(), amount);
                }
                //otherwise the date is not relvant
                else
                {
                    return getPriceBeforeDiscount(amount);
                }
            }
            else
            {
                return getPriceBeforeDiscount(amount);
            }
        }

    }
}
