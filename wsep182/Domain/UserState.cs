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
            throw new Exception("already logged in");
        }

        public abstract Boolean isLogedIn();

        public abstract LinkedList<Purchase> viewStoreHistory(Store store, User session);

        public virtual UserState register(String username, String password)
        {
            throw new Exception("already logged in");
        }

        public virtual Store createStore(String storeName, User session)
        {
            throw new Exception("Need to be logged in as regular user");
        }

        public virtual Boolean removeUser(User session, string userDeleted)
        {
            throw new Exception("Need to be logged in as admin");
        }

    }
}
