using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    class ShippingSystem
    {
        private static ShippingSystem instance;

        private ShippingSystem()
        {}
        public static ShippingSystem getInstance()
        {
            if (instance == null)
                instance = new ShippingSystem();
            return instance;
        }
        public Boolean sendShippingRequest(params Object[] args)
        {
            return false;
        }
    }
}
