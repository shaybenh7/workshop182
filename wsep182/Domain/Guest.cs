using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class Guest : UserState
    {
        public override User register(String username, String password)
        {
            if (username == null || password == null)
                return null;
            if (username.Equals("") || password.Equals(""))
                return null;
            if (username.Contains(" "))
                return null;
            User u = new User(username, password);
            if (UserArchive.getInstance().addUser(u))
            {
                return u;
            }
            return null;
        }

        public override User login(String username, String password)
        {
            User u = UserArchive.getInstance().getUser(username);
            if (u != null)
            {
                if (u.getPassword() == password)
                    return u;
            }
            return null;
        }
        public override Boolean isLogedIn()
        {
            return false;
        }
    }
}
