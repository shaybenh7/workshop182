using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class DiscountsArchive
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
        public static void restartInstance()
        {
            instance = new DiscountsArchive();
        }

        public Boolean addNewDiscount(int productInStoreId, int percentage, String dueDate)
        {
            DateTime dueDateTime;
            try
            {
                dueDateTime = DateTime.Parse(dueDate);
            }
            catch (System.FormatException e)
            {
                return false;
            }
            if (DateTime.Compare(dueDateTime, DateTime.Now) < 0)
                return false;
            foreach (Discount d in discounts)
            {
                if (d.ProductInStoreId == productInStoreId)
                {
                    return false;
                }
            }
            Discount toAdd = new Discount(productInStoreId, percentage, dueDate);
            discounts.AddLast(toAdd);
            return true;
        }
        public Boolean removeDiscount(int productInStoreId)
        {
            foreach (Discount discount in discounts)
            {
                if (discount.ProductInStoreId == productInStoreId)
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
            foreach (Discount discount in discounts)
            {
                if (discount.ProductInStoreId == productInStoreId)
                {
                    return discount;
                }
            }
            return null;
        }
    }
}
