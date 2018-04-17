using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class Discount
    {
        private int productInStoreId;
        private int percentage;
        private String dueDate;

        public Discount(int productInStoreId, int percentage, String dueDate){
            this.productInStoreId = productInStoreId;
            this.percentage = percentage;
            this.dueDate = dueDate;
        }

        public int ProductInStoreId { get => productInStoreId; set => productInStoreId = value; }
        public int Percentage { get => percentage; set => percentage = value; }
        public string DueDate { get => dueDate; set => dueDate = value; }

        public Double getPriceAfterDiscount(double pricePerUnit, int amount)
        {
            return (pricePerUnit * amount) * (percentage / 100);
        }

    }
}
