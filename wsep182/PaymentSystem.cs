using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    class PaymentSystem
    {
        private static PaymentSystem instance;

        private PaymentSystem()
        {}
        public static PaymentSystem getInstance()
        {
            if (instance == null)
                instance = new PaymentSystem();
            return instance;
        }
        public Boolean payForProduct(params Object[] args)
        {
            return false;
        }

    }
}
