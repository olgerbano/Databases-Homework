using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BarRestaurant
{
    public partial class Initial : Form
    {
        public static int nrmasa;
        public SqlConnection conn;
        public Initial()
        {
            InitializeComponent();
                       
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
        
        private void PictureBox1_Click(object sender, EventArgs e) => new LoginC1();

        public void Form1_Load(object sender, EventArgs e)
        {
            conn = Db.GetConnection();
            Timer timer = new Timer
            {
                Interval = (1 * 1000) // 1 secs
            };
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            
            
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT nrmasa,Liber FROM masa;", connection: conn);

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (reader.GetInt32(0) == 1)
                    {
                        if (reader.GetInt32(1) == 1)
                            button2.Text = "Liber";
                        else
                        {
                            button2.Text = "Ocupat";
                            button2.Visible = false;
                        }
                    }
                    if (reader.GetInt32(0) == 2)
                    {
                        if (reader.GetInt32(1) == 1)
                            button4.Text = "Liber";
                        else
                        {
                            button4.Text = "Ocupat";
                            button4.Visible = false;
                        }
                    }
                    if (reader.GetInt32(0) == 3)
                    {
                        if (reader.GetInt32(1) == 1)
                            button6.Text = "Liber";
                        else
                        {
                            button6.Text = "Ocupat";
                            button6.Visible = false;
                        }
                    }
                    if (reader.GetInt32(0) == 4)
                    {
                        if (reader.GetInt32(1) == 1)
                            button8.Text = "Liber";
                        else
                        {
                            button8.Text = "Ocupat";
                            button8.Visible = false;
                        }
                    }
                    if (reader.GetInt32(0) == 5)
                    {
                        if (reader.GetInt32(1) == 1)
                            button10.Text = "Liber";
                        else
                        {
                            button10.Text = "Ocupat";
                            button10.Visible = false;
                        }
                    }
                    if (reader.GetInt32(0) == 6)
                    {
                        if (reader.GetInt32(1) == 1)
                            button12.Text = "Liber";
                        else
                        {
                            button12.Text = "Ocupat";
                            button12.Visible = false;
                        }
                    }
                    if (reader.GetInt32(0) == 7)
                    {
                        if (reader.GetInt32(1) == 1)
                            button14.Text = "Liber";
                        else
                        {
                            button14.Text = "Ocupat";
                            button14.Visible = false;
                        }
                    }
                    if (reader.GetInt32(0) == 8)
                    {
                        if (reader.GetInt32(1) == 1)
                            button16.Text = "Liber";
                        else
                        {
                            button16.Text = "Ocupat";
                            button16.Visible = false;
                        }
                    }
                    if (reader.GetInt32(0) == 9)
                    {
                        if (reader.GetInt32(1) == 1)
                            button18.Text = "Liber";
                        else
                        {
                            button18.Text = "Ocupat";
                            button18.Visible = false;
                        }
                    }

                }
            }
        }
            private void Show_Form2Async()
        {
            this.Hide();
            LoginC1 f2 = new LoginC1();
            f2.ShowDialog();
            this.Close();
            
           
            
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            nrmasa = 1;
            Show_Form2Async();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            nrmasa = 2;
            Show_Form2Async();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            nrmasa = 3;
            Show_Form2Async();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            nrmasa = 4;
            Show_Form2Async();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            nrmasa = 5;
            Show_Form2Async();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            nrmasa = 6;
            Show_Form2Async();
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            nrmasa = 7;
            Show_Form2Async();
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            nrmasa = 8;
            Show_Form2Async();
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            nrmasa = 9;
            Show_Form2Async();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            nrmasa = 1;
            Show_Form2Async();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            nrmasa = 2;
            Show_Form2Async();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            nrmasa = 3;
            Show_Form2Async();
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            nrmasa = 4;
            Show_Form2Async();
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            nrmasa = 5;
            Show_Form2Async();
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            nrmasa = 6;
            Show_Form2Async();
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            nrmasa = 7;
            Show_Form2Async();
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            nrmasa = 8;
            Show_Form2Async();
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            nrmasa = 9;
            Show_Form2Async();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
