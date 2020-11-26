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
    public partial class Food : UserControl
    {
        public Food()
        {
            InitializeComponent();
        }

        private void Food_Load(object sender, EventArgs e)
        {

        }

        public void setImage(Image img)
        {
            pictureBox1.Image = img;
        }

        public void setName(string name)
        {
            FoodName.Text = name;
        }

        public void setPrice(int price)
        {
            FoodPrice.Text = "가격 : "+price+"원";
        }
    }
}
