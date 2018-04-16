using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    class StorePremissionsArchive
    {
        private static StorePremissionsArchive instance;
        Dictionary<string, StorePremissions> privilegesOfaUser;
        private StorePremissionsArchive()
        {
            privilegesOfaUser = new Dictionary<string, StorePremissions>();
        }
        public static StorePremissionsArchive getInstance()
        {
            if (instance == null)
                instance = new StorePremissionsArchive();
            return instance;
        }

        public StorePremissions getAllPremissions(string username)
        {
            return privilegesOfaUser[username];
        }

        public static void restartInstance()
        {
            instance = new StorePremissionsArchive();
        }

        public void initManagerPrivileges(string username)
        {
            privilegesOfaUser.Add(username, new StorePremissions());
        }


        public void addProductInStore(string username, Boolean allow)
        {
            privilegesOfaUser[username].addProductInStore(allow);
        }
        public void editProductInStore(string username, Boolean allow)
        {
            privilegesOfaUser[username].editProductInStore(allow);
        }
        public void removeProductFromStore(string username, Boolean allow)
        {
            privilegesOfaUser[username].removeProductFromStore(allow);
        }
        public void addStoreManager(string username, Boolean allow)
        {
            privilegesOfaUser[username].addStoreManager(allow);
        }

        public void removeStoreManager(string username, Boolean allow)
        {
            privilegesOfaUser[username].removeStoreManager(allow);
        }

        public void addManagerPermission(string username, Boolean allow)
        {
            privilegesOfaUser[username].addManagerPermission(allow);
        }
        public void removeManagerPermission(string username, Boolean allow)
        {
            privilegesOfaUser[username].removeManagerPermission(allow);
        }
        public Boolean checkPrivilege(string username, string privilege)
        {
            return privilegesOfaUser[username].checkPrivilege(privilege);
        }
    }
}
