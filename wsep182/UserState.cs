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
        public virtual User register(String username, String password)
        {
            throw new Exception("already logged in");
        }

        public virtual Boolean createStore(String storeName, User session)
        {
            throw new Exception("Need to be logged in as regular user");
        }

        public virtual Boolean removeUser(User session, string userDeleted)
        {
            throw new Exception("Need to be logged in as admin");
        }

    }
}
