using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class UserCartsArchive
    {
        private static UserCartsArchive instance;
        private LinkedList<UserCart> carts;

        private UserCartsArchive()
        {
            carts = new LinkedList<UserCart>();
        }
        public static UserCartsArchive getInstance()
        {
            if (instance == null)
                instance = new UserCartsArchive();
            return instance;
        }

        public static void restartInstance()
        {
            instance = new UserCartsArchive();
        }
        public Boolean updateUserCarts(String userName, int saleId, int amount)
        {
            foreach (UserCart cart in carts)
            {
                if(cart.getUserName().Equals(userName) && cart.getSaleId() == saleId)
                {
                    cart.setAmount(cart.getAmount() + amount);
                    return true;
                }
            }

            UserCart toAdd = new UserCart(userName, saleId, amount);
            carts.AddLast(toAdd);
            return true;
        }
        public Boolean updateUserCarts(String userName, int saleId, int amount,double offer)
        {
            foreach (UserCart cart in carts)
            {
                if (cart.getUserName().Equals(userName) && cart.getSaleId() == saleId)
                {
                    cart.setOffer(offer);
                    return true;
                }
            }

            UserCart toAdd = new UserCart(userName, saleId, amount);
            toAdd.setOffer(offer);
            carts.AddLast(toAdd);
            return true;
        }

        public Boolean editUserCarts(String userName, int saleId, int amount)
        {
            foreach (UserCart cart in carts)
            {
                if (cart.getUserName().Equals(userName) && cart.getSaleId() == saleId)
                {
                    cart.setAmount(amount);
                    return true;
                }
            }
            return false ;
        }       


        public UserCart getUserCart(String userName, int saleId)
        {
            foreach (UserCart cart in carts)
            {
                if (cart.getUserName().Equals(userName) && cart.getSaleId() == saleId)
                {
                    return cart;
                }
            }
            return null;
        }

        public LinkedList<UserCart> getUserShoppingCart(String userName)
        {
            LinkedList<UserCart> ans = new LinkedList<UserCart>();
            foreach(UserCart cart in carts)
            {
                if (cart.getUserName().Equals(userName))
                {
                    ans.AddLast(cart);
                }
            }
            return ans;
        }

        public Boolean removeUserCart(String userName, int saleId)
        {
            foreach(UserCart c in carts)
            {
                if(c.getUserName().Equals(userName) && c.getSaleId() == saleId)
                {
                    carts.Remove(c);
                    return true;
                }
            }
            return false;
        }

    }
}
