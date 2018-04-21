using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsep182.Domain
{
    public class Store
    {
        private int storeId;
        private int isActive; //delete next version
        String name;
        User storeCreator;
        public Store(int id, String name, User storeCreator)
        {
            storeId = id;
            this.isActive = 1;
            this.name = name;
            this.storeCreator = storeCreator;
        }
        public int getStoreId()
        {
            return storeId;
        }
        public User getStoreCreator()
        {
            return storeCreator;
        }
        public String getStoreName()
        {
            return name;
        }
        public void setStoreName(String name)
        {
            this.name = name;
        }
        public int getIsActive()
        {
            return isActive;
        }
        public LinkedList<ProductInStore> getProductsInStore()
        {
            return ProductArchive.getInstance().getAllProductsInStore(storeId);
        }

        public static Store createStore(String name,User session)
        {
            return storeArchive.getInstance().addStore(name,session);
        }

        public LinkedList<StoreOwner> getOwners()
        {
            return storeArchive.getInstance().getAllOwners(storeId);
        }

        public static LinkedList<Store> viewStores()
        {
            return storeArchive.getInstance().getAllStore();
        }

        public LinkedList<StoreManager> getManagers()
        {
            return storeArchive.getInstance().getAllManagers(storeId);
        }

		public LinkedList<Sale> getAllSales()
        {
            LinkedList<ProductInStore> products = getProductsInStore();
            LinkedList<Sale> ans = new LinkedList<Sale>();
            foreach (Sale sale in SalesArchive.getInstance().getAllSales())
            {
                foreach(ProductInStore product in products)
                {
                    if (sale.ProductInStoreId.Equals(product.getProductInStoreId()))
                        ans.AddLast(sale);
                }
            }
            return ans;
        }
        public void setIsActive(int active)
        {
            isActive = active;
        }
        public Boolean closeStore()
        {
            //WILL BE IMPLEMENTED NEXT VERSION
            return false;
        }
    }
}