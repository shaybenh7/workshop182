using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class Admin : UserState
    {
        public override Boolean removeUser(User session, string userDeleted)
        {
            return UserArchive.getInstance().removeUser(userDeleted);
        }


        public override Boolean isLogedIn()
        {
            return true;
        }

        public override LinkedList<Purchase> viewStoreHistory(Store store, User session)
        {
            return BuyHistoryArchive.getInstance().viewHistoryByStoreId(store.getStoreId());
        }

        public override LinkedList<Purchase> viewUserHistory(User userToGetHistory)
        {
            return BuyHistoryArchive.getInstance().viewHistoryByUserName(userToGetHistory.getUserName());
        }
    }
}
