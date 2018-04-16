using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class LogedIn : UserState
    {
        public override Store createStore(String storeName, User session)
        {
            Store newStore = storeArchive.getInstance().addStore(storeName,session);
            storeArchive.getInstance().addStoreRole(new StoreOwner(session,newStore), newStore.getStoreId(), session.getUserName());
            return newStore;
        }

        public override Boolean isLogedIn()
        {
            return true;
        }
    }
}
