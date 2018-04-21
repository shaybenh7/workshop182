using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class SalesArchive
    {
        private LinkedList<Sale> sales;
        private static SalesArchive instance;
        private static int saleId;

        private SalesArchive()
        {
            sales = new LinkedList<Sale>();
            saleId = 0;
        }
        public static void restartInstance()
        {
            instance = new SalesArchive();
        }
        public int getNextSaleId()
        {
            return ++saleId;
        }

        public static SalesArchive getInstance()
        {
            if (instance == null)
                instance = new SalesArchive();
            return instance;
        }

        public Boolean removeSale(int saleId)
        {
            foreach(Sale s in sales)
            {
                if (s.SaleId == saleId)
                {
                    sales.Remove(s);
                    return true;
                }
            }
            return false;
        }

        public Sale addSale(int productInStoreId, int typeOfSale, int amount, String dueDate)
        {
            DateTime dueDateTime;
            try
            {
                dueDateTime = DateTime.Parse(dueDate);
            }
            catch (System.FormatException e)
            {
                return null;
            }
            if (DateTime.Compare(dueDateTime, DateTime.Now) < 0)
                return null;
            foreach (Sale sale in sales)
            {
                if(sale.ProductInStoreId==productInStoreId && sale.TypeOfSale == typeOfSale && sale.DueDate.Equals(dueDate))
                {
                    sale.Amount += amount;
                    return sale;
                }
            }
            int saleId = getNextSaleId();
            Sale toAdd = new Sale(saleId, productInStoreId, typeOfSale, amount, dueDate);
            sales.AddLast(toAdd);
            return toAdd;
        }

        public Sale getSale(int saleId)
        {
            foreach (Sale sale in sales)
            {
                if (sale.SaleId == saleId)
                {
                    return sale;
                }
            }
            return null;
        }
        public Boolean editSale(int saleId, int amount, String dueDate)
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
            foreach (Sale sale in sales)
            {
                if (sale.SaleId == saleId)
                {
                    sale.Amount = amount;
                    sale.DueDate = dueDate;
                    return true;
                }
            }
            return false;
        }
        public Boolean setNewAmountForSale(int saleId, int amount)
        {
            foreach (Sale sale in sales)
            {
                if (sale.SaleId == saleId)
                {
                    sale.Amount = amount;
                    return true;
                }
            }
            return false;
        }

        public LinkedList<Sale> getAllSales()
        {
            return sales;
        }

        public LinkedList<Sale> getSalesByProductInStoreId(int productInStoreId)
        {
            LinkedList<Sale> ans = new LinkedList<Sale>();
            foreach(Sale sale in sales)
            {
                if(sale.ProductInStoreId == productInStoreId)
                {
                    ans.AddLast(sale);
                }
            }

            return ans;
        }

    }
}
