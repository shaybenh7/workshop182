using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public abstract class UserState
    {
        public virtual User login(String username, String password)
        {
            return null;
        }

        public virtual LinkedList<Purchase> viewUserHistory(User userToGetHistory)
        {
            return null;
        }

        public abstract Boolean isLogedIn();

        public abstract LinkedList<Purchase> viewStoreHistory(Store store, User session);

        public virtual UserState register(String username, String password)
        {
            return null;
        }

        public virtual Store createStore(String storeName, User session)
        {
            return null;
        }


        public virtual Boolean removeUser(User session, string userDeleted)
        {
            return false;
        }


    }
}
