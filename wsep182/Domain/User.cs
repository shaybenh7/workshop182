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
        private Boolean isActive;
        public User(string userName, string password)
        {
            this.password = password;
            this.userName = userName;
            isActive = true;
            state = new Guest();
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
            User user = UserArchive.getInstance().getUser(userName);
            if (user == null)
                return new Guest();
            return user.state;
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

        public LinkedList<UserCart> getShoppingCart()
        {
            return shoppingCart.getShoppingCartProducts(this);
        }
        public Boolean login(String username, String password)
        {
            User user = state.login(username, password);
            if (user != null)
            {
                if (!user.getIsActive())
                    return false;
                if (username == "admin" || username == "admin1")
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
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)
                || username.Equals("") || password.Equals("") || username.Contains(" "))
                return false;
            User u = new User(username, password);
            u.setState(state.register(username, password));
            return UserArchive.getInstance().addUser(u);
        }

        public Store createStore(String storeName)
        {
            if (storeName == null || storeName.Length==0 || String.IsNullOrWhiteSpace(storeName))
                return null;
            return this.state.createStore(storeName, this);
        }

        public Boolean removeUser(String userName)
        {
            return this.state.removeUser(this,userName);
        }



        public Boolean addToCart(int saleId, int amount)
        {
            if (amount <= 0)
                return false;
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
            if (sale == null || offer <= 0)
                return false;
            return shoppingCart.addToCartRaffle(this, sale, offer);
        }

        public static LinkedList<Sale> viewSalesByProductInStoreId(ProductInStore product)
        {
            if (product == null)
                return null;
            return SalesArchive.getInstance().getSalesByProductInStoreId(product.getProductInStoreId());
        }

        public LinkedList<Purchase> viewStoreHistory(Store store)
        {
            return state.viewStoreHistory(store,this);
        }

        public LinkedList<Purchase> viewUserHistory(String userNameToGetHistory)
        {
            if (userNameToGetHistory == null)
                return null;
            User userToGetHistory = UserArchive.getInstance().getUser(userNameToGetHistory);
            return state.viewUserHistory(userToGetHistory);
        }

        internal Boolean getIsActive()
        {
            User user = UserArchive.getInstance().getUser(userName);
            if (user == null)
                return false;
            return user.isActive;
        }

        internal void setIsActive(Boolean state)
        {
            isActive = state;
            this.state = new Guest();
        }
        public Boolean removeFromCart(Sale sale)
        {
            return shoppingCart.removeFromCart(this, sale);
        }
    }
}