using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    class BuyHistoryArchive
    {

        private LinkedList<BuyHistory> buysHistory;
        private static BuyHistoryArchive instance;
        private static int buyId;

        private BuyHistoryArchive()
        {
            buysHistory = new LinkedList<BuyHistory>();
            buyId = 0;
        }

        public static BuyHistoryArchive getInstance()
        {
            if (instance == null)
                instance = new BuyHistoryArchive();
            return instance;
        }

        public int getNextBuyId()
        {
            return buyId++;
        }

        public Boolean addBuyHistory(int productId, int storeId , String userName, double price,
        String date, int amount, int typeOfSale)
        {
            int buyId = getNextBuyId();
            BuyHistory toAdd = new BuyHistory(buyId, productId, storeId, userName, price, date, amount, typeOfSale);
            buysHistory.AddLast(toAdd);
            return true;
        }

        public LinkedList<BuyHistory> viewHistory()
        {
            return buysHistory;
        }
        
        public LinkedList<BuyHistory> viewHistoryByStoreId(int storeId)
        {
            LinkedList<BuyHistory> ans = new LinkedList<BuyHistory>();
            foreach(BuyHistory buy in buysHistory)
            {
                if (buy.StoreId == storeId)
                {
                    ans.AddLast(buy);
                }
            }
            return ans;
        }
        public LinkedList<BuyHistory> viewHistoryByUserName(String userName)
        {
            LinkedList<BuyHistory> ans = new LinkedList<BuyHistory>();
            foreach (BuyHistory buy in buysHistory)
            {
                if (buy.UserName.Equals(userName))
                {
                    ans.AddLast(buy);
                }
            }
            return ans;
        }





    }
}
