using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beakchelin.shop
{
    class ReviewC
    {
        private string nickName;
        private string comment;
        private int score;
        private int shopid;

        public ReviewC(string nick_, string com_, int score_, int id_)
        {
            nickName = nick_;
            comment = com_;
            score = score_;
            shopid = id_;
        }


        public void setNickName(string name)
        {
            nickName = name;
        }
        public void setComment(string text)
        {
            comment = text;
        }
        public void setScore(int a)
        {
            score = a;
        }
        public void setShopId(int id)
        {
            shopid = id;
        }

        public string getNickName() => nickName;
        public string getComment() => comment;
        public int getScore() => score;
        public int getShopId() => shopid;

    }
}
