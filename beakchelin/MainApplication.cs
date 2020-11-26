using beakchelin.shop;
using bs_Server.Shops;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace beakchelin
{
    public partial class MainApplication : Form
    {
        private Socket client;
        private Socket cbSock;
        private int index;

        private byte[] recvBuffer;
        private const int MAXSIZE = 4096;
        private string HOST = "127.0.0.1";
        private int PORT = 10100;

        private List<Shop> shops = new List<Shop>();

        private int page = 0;
        private int currentCategory = 0;
        private int currentS = 0;
        private Shop seldShop;
        private int reviewidx = 0;

        private bool onMenu = true;
        public MainApplication()
        {
            InitializeComponent();
            recvBuffer = new byte[MAXSIZE];
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                client.BeginConnect(HOST, PORT, new AsyncCallback(ConnectCallBack), client);
            } catch (SocketException e)
            {
                MessageBox.Show("서버 연결에 실패했습니다.", "백슐랭", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Random rand = new Random();
            string[] foods = {"치킨이", "피자가", "햄버거가", "한식이", "양식이", "중식이", "일식이", "분식이", "디저트가" };
            int idx = rand.Next(0, foods.Length - 1);

            label10.Text = "오늘은 " + foods[idx] + " 땡긴다!";
            button1.Enabled = false;

        }

        private void ConnectCallBack(IAsyncResult IAR)
        {
            try
            {
                Socket temp = (Socket)IAR.AsyncState;
                IPEndPoint EP = (IPEndPoint)temp.RemoteEndPoint;

                temp.EndConnect(IAR);
                cbSock = temp;
                cbSock.BeginReceive(recvBuffer, 0, recvBuffer.Length, SocketFlags.None,
                                    new AsyncCallback(OnReceiveCallBack), cbSock);
                //MessageBox.Show("서버 연결에 성공했습니다.", "백슐랭", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (SocketException e)
            {
                MessageBox.Show("서버 연결에 실패했습니다.", "백슐랭", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(e);
            }
        }

        private void OnReceiveCallBack(IAsyncResult IAR)
        {
            try
            {
                Socket temp = (Socket)IAR.AsyncState;
                int nReadSize = temp.EndReceive(IAR);
                if (nReadSize != 0)
                {
                    // 여기서 처리
                    String readData = Encoding.Unicode.GetString(recvBuffer, 0, nReadSize);
                    PacketHandler(readData);
                }

                cbSock.BeginReceive(this.recvBuffer, 0, recvBuffer.Length, SocketFlags.None,
                                    new AsyncCallback(OnReceiveCallBack), cbSock);
            } catch (SocketException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void PacketHandler(String recv)
        {
            String[] recvs = recv.Split('|');
            var opcode = recvs[0];
            //MessageBox.Show("[R]" + recv, "ss");
            switch (Int32.Parse(opcode))
            {
                case 0: // getIndex
                    {
                        index = Int32.Parse(recvs[1]) - 1;
                        BeginSend("1|" + index);
                    }
                    break;
                case 1: // getshopdata
                    {
                        int id = Int32.Parse(recvs[1]);
                        string name = recvs[2];
                        string Number = recvs[3];
                        string new_Add = recvs[4];
                        string old_Add = recvs[5];
                        int category = Int32.Parse(recvs[6]);
                        string img = recvs[7];
                        int star = Int32.Parse(recvs[8]);

                        Shop tempShop = new Shop(id, name, Number, new_Add, old_Add, category, img, star);
                        shops.Add(tempShop);
                    }
                    break;
                case 2:
                    {
                        int idx = Int32.Parse(recvs[1]);
                        int id = Int32.Parse(recvs[2]);
                        string name = recvs[3];
                        string image = recvs[4];
                        int price = Int32.Parse(recvs[5]);
                        int category = Int32.Parse(recvs[6]);
                        shops[idx].addItem(new ShopItem(id, name, image, price, category));
                        
                    }
                    break;
                case 3:
                    {
                        int idx = Int32.Parse(recvs[1]);
                        int id = Int32.Parse(recvs[2]);
                        string name = recvs[3];
                        string comment = recvs[4];
                        int score = Int32.Parse(recvs[5]);
                        shops[idx].addReview(new ReviewC(name, comment, score, id));
                    }
                    break;
            }

        }

        public void BeginSend(string msg)
        {
            try
            {
                if (client.Connected)
                {
                    byte[] buffer = Encoding.Unicode.GetBytes(msg);
                    client.BeginSend(buffer, 0, buffer.Length, SocketFlags.None,
                                     new AsyncCallback(SendCallBack), msg);
                }
            } catch (SocketException e)
            {
                // 오류처리
            }
        }

        private void SendCallBack(IAsyncResult IAR)
        {
            string msg = (string)IAR.AsyncState;
        }

        private void MainApplication_Load(object sender, EventArgs e)
        {
        }
        private void shopCtr_ButtonClick(object sender, EventArgs e)
        {
            //handle the event 
            MessageBox.Show("dd");
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void timer_logo_Tick(object sender, EventArgs e)
        {
            timer_logo.Enabled = false;
            //logobox.Visible = false;
            panel1.Visible = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void shop_Click(object sender, EventArgs e)
        {
            MessageBox.Show("곧 완성");
        }

        private void btn_Click(object sender, EventArgs e)
        {
            var a = (PictureBox)sender;
            int cate = Int32.Parse(a.Name.Split('_')[1]);
            currentCategory = cate;

            int count = 1;
            for (int i = 0; i < shops.Count; i++)
            {
                if (shops[i].getCategory() == cate)
                {
                    var tmpctr = this.Controls.Find("ShopName_" + count, true).FirstOrDefault();
                    if (tmpctr != null)
                    {
                        var temp = (Label)tmpctr;
                        temp.Text = shops[i].getName();
                    }
                    var tmpctr2 = this.Controls.Find("shoplogo_" + count, true).FirstOrDefault();
                    if (tmpctr2 != null)
                    {
                        var temp = (PictureBox)tmpctr2;
                        var image = Image.FromFile(Application.StartupPath + "\\logo\\" + shops[i].getImage());
                        temp.Image = image;
                    }
                    var tmpctr3 = this.Controls.Find("shopstar_" + count, true).FirstOrDefault();
                    if (tmpctr3 != null)
                    {
                        var temp = (Label)tmpctr3;
                        string t = "별점 : ";
                        for (int j = 0; j < shops[i].getStar(); j++)
                        {
                            t += "★";
                        }
                        t += " (" + shops[i].getStar() + "점)";
                        temp.Text = t;
                        count++;
                    }
                }
                if (count == 5)
                {
                    currentS = i;
                    break;
                }
            }
            pictureBox1.Image = a.Image;
            panel2.Visible = true;
            panel1.Visible = false;
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            page = 0;
            pagelbl.Text = "1페이지";
            button1.Enabled = false;
            button2.Enabled = true;
            panel2.Visible = false;
            panel1.Visible = true;
            panel5.Visible = true;
            seldShop = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (page == 0)
            {
                return;
            }


            ClearShopList();
            page--;
            if (page == 0)
            {
                button1.Enabled = false;
            }
            if (page < 1)
            {
                button2.Enabled = true;
            }
            pagelbl.Text = (page + 1) + "페이지";

            int count = 1;
            int c = -1 * (0 - (page * 4));
            for (int i = c; i < shops.Count; i++)
            {
                if (shops[i].getCategory() == currentCategory)
                {
                    var tmpctr = this.Controls.Find("ShopName_" + count, true).FirstOrDefault();
                    if (tmpctr != null)
                    {
                        var temp = (Label)tmpctr;
                        temp.Text = shops[i].getName();
                    }
                    var tmpctr2 = this.Controls.Find("shoplogo_" + count, true).FirstOrDefault();
                    if (tmpctr2 != null)
                    {
                        var temp = (PictureBox)tmpctr2;
                        var image = Image.FromFile(Application.StartupPath+ "\\logo\\" + shops[i].getImage());
                        temp.Image = image;
                    }
                    var tmpctr3 = this.Controls.Find("shopstar_" + count, true).FirstOrDefault();
                    if (tmpctr3 != null)
                    {
                        var temp = (Label)tmpctr3;
                        string t = "별점 : ";
                        for (int j = 0; j < shops[i].getStar(); j++)
                        {
                            t += "★";
                        }
                        t += " (" + shops[i].getStar() + "점)";
                        temp.Text = t;
                        count++;
                    }
                }
                if (count == 5)
                {
                    currentS = i;
                    break;
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            page++;
            if (page == 1)
            {
                button2.Enabled = false;
            }
            pagelbl.Text = (page + 1) +"페이지";
            if (page > 0)
            {
                button1.Enabled = true;
            }

            ClearShopList();
            int count = 1;
            int c = (0 + (page * 4)) + 1;
            for (int i = currentS + 1; i < shops.Count; i++)
            {
                if (shops[i].getCategory() == currentCategory)
                {
                    var tmpctr = this.Controls.Find("ShopName_" + count, true).FirstOrDefault();
                    if (tmpctr != null)
                    {
                        var temp = (Label)tmpctr;
                        temp.Text = shops[i].getName();
                    }
                    var tmpctr2 = this.Controls.Find("shoplogo_" + count, true).FirstOrDefault();
                    if (tmpctr2 != null)
                    {
                        var temp = (PictureBox)tmpctr2;
                        var image = Image.FromFile(Application.StartupPath + "\\logo\\" + shops[i].getImage());
                        temp.Image = image;
                    }
                    var tmpctr3 = this.Controls.Find("shopstar_" + count, true).FirstOrDefault();
                    if (tmpctr3 != null)
                    {
                        var temp = (Label)tmpctr3;
                        string t = "별점 : ";
                        for (int j = 0; j < shops[i].getStar(); j++)
                        {
                            t += "★";
                        }
                        t += " (" + shops[i].getStar() + "점)";
                        temp.Text = t;
                        count++;
                    }
                }
                if (count == 5)
                {
                    currentS = i;
                    break;
                }
            }
        }

        private void ClearShopList()
        {
            for (int i = 1; i <= 4; i++)
            {
                var tmpctr = this.Controls.Find("ShopName_" + i, true).FirstOrDefault();
                if (tmpctr != null)
                {
                    var temp = (Label)tmpctr;
                    temp.Text = "";
                }
                var tmpctr2 = this.Controls.Find("shoplogo_" + i, true).FirstOrDefault();
                if (tmpctr2 != null)
                {
                    var temp = (PictureBox)tmpctr2;
                    temp.Image = null;
                }
                var tmpctr3 = this.Controls.Find("shopstar_" + i, true).FirstOrDefault();
                if (tmpctr3 != null)
                {
                    var temp = (Label)tmpctr3;
                    temp.Text = "";
                }
            }
        }


        private void shopCtr1_Load(object sender, EventArgs e)
        {

        }


        private void shopCtr1_Load_1(object sender, EventArgs e)
        {

        }

        private void Select_Click(object sender, EventArgs e)
        {
            var a = (Button)sender;
            int number = Int32.Parse(a.Name.Split('_')[1]);
            panel4.Controls.Clear();
            panel5.Controls.Clear();
            panel2.Visible = false;
            panel3.Visible = true;

            var tmpctr = this.Controls.Find("ShopName_" + number, true).FirstOrDefault();
            if (tmpctr != null)
            {
                var temp = (Label)tmpctr;
                MainShopName.Text = temp.Text;
            }
            var tmpctr2 = this.Controls.Find("shoplogo_" + number, true).FirstOrDefault();
            if (tmpctr2 != null)
            {
                var temp = (PictureBox)tmpctr2;
                MainShopLogo.Image = temp.Image;
            }
            var tmpctr3 = this.Controls.Find("shopstar_" + number, true).FirstOrDefault();
            if (tmpctr3 != null)
            {
                var temp = (Label)tmpctr3;
                MainShopStar.Text = temp.Text;
            }

            Shop tempshop = null;
            for (int i = 0; i < shops.Count; i++)
            {
                if (shops[i].getName().Equals(MainShopName.Text))
                {
                    tempshop = shops[i];
                }
            }
            seldShop = tempshop;
            address_new.Text = tempshop.getNew().Replace("\n", "") + "\n" + tempshop.getOld().Replace("\n", "");


            for (int i = 0; i < tempshop.getItems().Count; i++)
            {
                var tempfood = new Food();
                tempfood.setName(tempshop.getItems()[i].getName());
                var image = Image.FromFile(Application.StartupPath + "\\image\\" + tempshop.getItems()[i].getImage());
                tempfood.setImage(image);
                tempfood.setPrice(tempshop.getItems()[i].getPrice());

                tempfood.Size = new Size(254, 117);
                tempfood.Location = new Point(8, 3 + (114 * i));

                panel5.Controls.Add(tempfood);
            }

            int count = 0;
            for (int i = 0; i < tempshop.getReviews().Count; i++)
            {
                var tempreview = new Review();
                tempreview.setName(tempshop.getReviews()[i].getNickName());
                tempreview.setScore(tempshop.getReviews()[i].getScore());
                tempreview.setComment(tempshop.getReviews()[i].getComment());

                tempreview.Size = new Size(251, 138);
                tempreview.Location = new Point(12, 3 + (125 * i));

                panel4.Controls.Add(tempreview);
                reviewidx++;
                count++;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            page = 0;
            pagelbl.Text = "1페이지";
            button1.Enabled = false;
            button2.Enabled = true;
            panel2.Visible = false;
            panel1.Visible = true;
            panel5.Visible = true;
            panel6.Visible = false;
            panel4.Visible = false;
            onMenu = true;
            label17.ForeColor = Color.Gray;
            showMenu.ForeColor = Color.Black;
            seldShop = null;
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {
            if (onMenu)
            {
                label17.ForeColor = Color.Black;
                showMenu.ForeColor = Color.Gray;
            
                onMenu = false;

                panel5.Visible = false;
                panel6.Visible = true;
                NickName.Visible = true;
                panel4.Visible = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            BeginSend("2|" + index + "|" + seldShop.getId() + "|"+NickNameBox.Text + "|"+(radioButton1.Checked ? "1" : radioButton2.Checked ? "2" : radioButton3.Checked ? "3" : radioButton4.Checked ? "4" : "5") + "|" + CommentBox.Text);
            seldShop.addReview(new ReviewC(NickNameBox.Text, CommentBox.Text, (radioButton1.Checked ? 1 : radioButton2.Checked ? 2 : radioButton3.Checked ? 3 : radioButton4.Checked ? 4 : 5), seldShop.getId()));

            var tempreview = new Review();
            tempreview.setName(NickNameBox.Text);
            tempreview.setScore((radioButton1.Checked ? 1 : radioButton2.Checked ? 2 : radioButton3.Checked ? 3 : radioButton4.Checked ? 4 : 5));
            tempreview.setComment(CommentBox.Text);

            tempreview.Size = new Size(251, 158);
            tempreview.Location = new Point(15, 3 + (148 * reviewidx));

            panel4.Controls.Add(tempreview);
            reviewidx++;
        }

        private void showMenu_Click(object sender, EventArgs e)
        {
            if (!onMenu)
            {
                label17.ForeColor = Color.Gray;
                showMenu.ForeColor = Color.Black;

                onMenu = true;

                panel5.Visible = true;
                panel6.Visible = false;
                panel4.Visible = false;
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
