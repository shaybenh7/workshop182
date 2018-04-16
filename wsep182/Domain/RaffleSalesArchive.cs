using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class RaffleSalesArchive
    {
        private LinkedList<RaffleSale> raffleSales;
        private static RaffleSalesArchive instance;

        private RaffleSalesArchive()
        {
            raffleSales = new LinkedList<RaffleSale>();
        }

        public static RaffleSalesArchive getInstance()
        {
            if (instance == null)
                instance = new RaffleSalesArchive();
            return instance;
        }

        public Boolean addRaffleSale(int saleId, String userName, double offer, String dueDate)
        {
            RaffleSale toAdd = new RaffleSale(saleId, userName, offer, dueDate);
            raffleSales.AddLast(toAdd);
            return true;
        }


    }
}
