using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class LogedIn : UserState
    {
        public override Boolean createStore(String storeName, User session)
        {
            return storeArchive.getInstance().addStore(storeName,session)!=null;
        }
    }
}
