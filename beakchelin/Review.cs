using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace beakchelin
{
    public partial class Review : UserControl
    {
        public Review()
        {
            InitializeComponent();
        }

        private void addReview_Load(object sender, EventArgs e)
        {

        }

        public void setName(string n)
        {
            NickName.Text = n;
        }

        public void setScore(int n)
        {
            string a = "별점 : ";
            for (int i = 0; i < n; i++)
            {
                a += "★";
            }
            a += " (" + n + "점)";

            Score.Text = a;
        }

        public void setComment(string n)
        {
            Comment.Text = n;
        }
    }
}
