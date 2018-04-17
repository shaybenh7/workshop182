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
            if (user != null)
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
            User u = new User(username, password);
            u.setState(state.register(username, password));
            return UserArchive.getInstance().addUser(u);
        }

        public Store createStore(User session ,String storeName)
        {
            if (storeName == null || session == null)
                return null;
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

        public static LinkedList<Sale> viewSalesByProductInStoreId(ProductInStore product)
        {
            return SalesArchive.getInstance().getSalesByProductInStoreId(product.getProductInStoreId());
        }

        public LinkedList<Purchase> viewStoreHistory(Store store)
        {
            return state.viewStoreHistory(store,this);
        }

        public LinkedList<Purchase> viewUserHistory(User userToGetHistory)
        {
            return state.viewUserHistory(userToGetHistory);
        }

        internal Boolean getIsActive()
        {
            return isActive;
        }

        internal void setIsActive(Boolean state)
        {
            isActive = state;
        }
    }
}