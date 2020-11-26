using System;
using System.Collections.Generic;
using System.Text;

namespace bs_Server.Shops
{
    class ShopItem
    {
        private int Shop;
        private string Name;
        private string Image;
        private int Price;
        private int Category;

        public ShopItem(int shop_, String name_, String image_, int price_, int category_)
        {
            Shop = shop_;
            Name = name_;
            Image = image_;
            Price = price_;
            Category = category_;

        }

        public int getShopId() => Shop;
        public string getName() => Name;
        public string getImage() => Image;
        public int getPrice() => Price;
        public int getCategory() => Category;
    }
}
