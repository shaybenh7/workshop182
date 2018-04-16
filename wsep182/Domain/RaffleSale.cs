using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class RaffleSale
    {
        private int saleId;
        private String userName;
        private double offer;
        private String dueDate;

        public RaffleSale(int saleId, string userName, double offer, string dueDate)
        {
            this.saleId = saleId;
            this.userName = userName;
            this.offer = offer;
            this.dueDate = dueDate;
        }

        public int SaleId { get => saleId; set => saleId = value; }
        public string UserName { get => userName; set => userName = value; }
        public double Offer { get => offer; set => offer = value; }
        public string DueDate { get => dueDate; set => dueDate = value; }
    }
}
