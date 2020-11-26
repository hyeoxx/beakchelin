using beakchelin.shop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace bs_Server.Shops
{
    class Shop
    {
        private int Id;
        private String Name;
        private String Number;
        private String old_Address;
        private String new_Address;
        private int Category;
        private string Image;
        private int star;

        private List<ShopItem> Items = new List<ShopItem>();
        private List<ReviewC> Reviews = new List<ReviewC>();

        public Shop(int id_, String name_, String number_, String old_a, String new_a, int category_, string image_, int star_)
        {
            Id = id_;
            Name = name_;
            Number = number_;
            old_Address = old_a;
            new_Address = new_a;
            Category = category_;
            Image = image_;
            star = star_;
        }

        public int getId() => Id;
        public String getName() => Name;
        public String getOld() => old_Address;
        public String getNew() => new_Address;
        public String getNumber() => Number;
        public int getCategory() => Category;
        public int getStar() => star;
        public string getImage() => Image;
        public List<ShopItem> getItems() => Items;
        public List<ReviewC> getReviews() => Reviews;

        public bool addReview(ReviewC a)
        {
            try
            {
                Reviews.Add(a);
                return true;
            } catch (Exception e)
            {
                Console.WriteLine("오류가 발생했습니다.\n{0}", e.StackTrace);
                return false;
            }
        }


        public bool addItem(ShopItem item)
        {
            try
            {
                Items.Add(item);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("오류가 발생했습니다.\n{0}", e.StackTrace);
                return false;
            }
        }
    }
}
