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
            return saleId++;
        }

        public static SalesArchive getInstance()
        {
            if (instance == null)
                instance = new SalesArchive();
            return instance;
        }


        public int addSale(int productInStoreId, int typeOfSale, int amount, String dueDate)
        {
            int saleId = getNextSaleId();
            Sale toAdd = new Sale(saleId, productInStoreId, typeOfSale, amount, dueDate);
            sales.AddLast(toAdd);
            return saleId;
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

        public LinkedList<Sale> getAllSales()
        {
            return sales;
        }

    }
}