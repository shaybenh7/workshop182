using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class User
    {
        private UserState state;
        private String userName;
        private String password;
        private ShoppingCart shoppingCart;
        public User(string userName, string password)
        {
            this.password = password;
            this.userName = userName;
            state = new LogedIn();
            shoppingCart = new ShoppingCart();
        }
        
        public String getUserName()
        {
            return userName;
        }
        public String getPassword()
        {
            return password;
        }
        public UserState getState()
        {
            return state;
        }
        void setState(UserState s)
        {
            state = s;
        }
        
        public User logOut()
        {
            state = new Guest();
            userName = "guest";
            password = "guest";
            return this;
        }

        public ShoppingCart getShoppingCart()
        {
            return shoppingCart;
        }
        public Boolean login(String username, String password)
        {
            User user = state.login(username, password);
            if (state.login(username, password) != null)
            {
                if (username == "admin")
                    state = new Admin();
                else
                    state = user.state;
                this.userName = username;
                this.password = password;
                return true;
            }
            return false;
        }



        public Boolean register(String username, String password)
        {
            return state.register(username,password)!=null;
        }

        public Store createStore(String storeName)
        {
            return state.createStore(storeName, this);
        }

        public Boolean removeUser(User userMakingDeletion, String userName)
        {
            return userMakingDeletion.state.removeUser(this,userName);
        }


        public Boolean addToCart(int saleId, int amount)
        {
            return shoppingCart.addToCart(this, saleId, amount);
        }

        public Boolean editCart(int saleId, int amount)
        {
            return shoppingCart.editCart(this, saleId, amount);
        }
        public Boolean buyProducts(String creditCard, String couponId)
        {
            return shoppingCart.buyProducts(this, creditCard,couponId);
        }

        public Boolean addToCartRaffle(Sale sale, double offer)
        {
            return shoppingCart.addToCartRaffle(this, sale, offer);
        }

    }
}