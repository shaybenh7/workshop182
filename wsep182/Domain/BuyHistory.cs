using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class BuyHistory
    {
        private int buyId;
        private int productId;
        private int storeId;
        private String userName;
        private double price;
        private String date;
        private int amount;
        private int typeOfSale;

        public BuyHistory(int buyId, int productId, int storeId, String userName, double price
            , String date, int amount , int typeOfSale)
        {
            this.buyId = buyId;
            this.productId = productId;
            this.storeId = storeId;
            this.userName = userName;
            this.price = price;
            this.date = date;
            this.amount = amount;
            this.typeOfSale = typeOfSale;
        }

        public int BuyId { get => buyId; set => buyId = value; }
        public int ProductId { get => productId; set => productId = value; }
        public int StoreId { get => storeId; set => storeId = value; }
        public string UserName { get => userName; set => userName = value; }
        public double Price { get => price; set => price = value; }
        public string Date { get => date; set => date = value; }
        public int Amount { get => amount; set => amount = value; }
        public int TypeOfSale { get => typeOfSale; set => typeOfSale = value; }
    }
}
