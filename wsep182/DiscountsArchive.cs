using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    class DiscountsArchive
    {
        private LinkedList<Discount> discounts;
        private static DiscountsArchive instance;

        private DiscountsArchive()
        {
            discounts = new LinkedList<Discount>();
        }
        public static DiscountsArchive getInstance()
        {
            if (instance == null)
                instance = new DiscountsArchive();
            return instance;
        }

        public Boolean addNewDiscount(int productInStoreId, int percentage, String dueDate)
        {
            Discount toAdd = new Discount(productInStoreId, percentage, dueDate);
            discounts.AddLast(toAdd);
            return true;
        }
        public Boolean removeDiscount(int productInStoreId)
        {
            foreach(Discount discount in discounts)
            {
                if(discount.ProductInStoreId == productInStoreId)
                {
                    discounts.Remove(discount);
                    return true;
                }
            }
            return false;
        }
        public Boolean editDiscount(int productInStoreId, int newPercentage, String newDueDate)
        {
            foreach (Discount discount in discounts)
            {
                if (discount.ProductInStoreId == productInStoreId)
                {
                    discount.Percentage = newPercentage;
                    discount.DueDate = newDueDate;
                    return true;
                }
            }
            return false;
        }

        public LinkedList<Discount> getAllDiscounts()
        {
            return discounts;
        }

        public Discount getDiscount(int productInStoreId)
        {
            foreach(Discount discount in discounts)
            {
                if(discount.ProductInStoreId == productInStoreId)
                {
                    return discount;
                }
            }
            return null;
        }
    }
}
