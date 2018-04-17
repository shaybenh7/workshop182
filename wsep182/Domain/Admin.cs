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
            LinkedList<StoreRole> roles = storeArchive.getInstance().getAllStoreRolesOfAUser(session.getUserName());
            if (loneOwner(roles))
                return false;
            removeAllRolesOfAUser(roles);
            return UserArchive.getInstance().removeUser(userDeleted);
        }

        private Boolean loneOwner(LinkedList<StoreRole> roles)
        {
            foreach (StoreRole sr in roles)
            {
                if (sr is StoreOwner)
                {
                    if (storeArchive.getInstance().getAllOwners(sr.getStore().getStoreId()).Count == 1)
                        return true;
                }
            }
            return false;
        }

        private Boolean removeAllRolesOfAUser(LinkedList<StoreRole> roles)
        {
            foreach (StoreRole sr in roles)
            {
                if (sr is StoreOwner)
                {
                    if (storeArchive.getInstance().getAllOwners(sr.getStore().getStoreId()).Count > 1)
                        storeArchive.getInstance().removeStoreRole(sr.getStore().getStoreId(), sr.getUser().getUserName());
                    else throw new Exception("something went seriously wrong"); // in the interval between the call to the safety check to now, something occured
                }
                if (sr is StoreManager)
                {
                    storeArchive.getInstance().removeStoreRole(sr.getStore().getStoreId(), sr.getUser().getUserName());
                }
            }
            return false;
        }

        public override Boolean isLogedIn()
        {
            return true;
        }


    }
}
