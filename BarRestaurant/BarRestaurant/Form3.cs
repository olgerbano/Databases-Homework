using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BarRestaurant
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
                        
        }
       
        private void Button1_Click(object sender, EventArgs e)
        {
            
            SqlCommand command3 = new SqlCommand("SELECT nume FROM produse where tip = 'racoritoare' and instock > @zero;", connection: conn);
            command3.Parameters.AddWithValue("@zero", 0);
            SqlDataReader reader2 = command3.ExecuteReader();

            while (reader2.Read())
            {
                listBox1.Items.Add(reader2["nume"].ToString());
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            
            SqlCommand command4 = new SqlCommand("SELECT nume FROM produse where tip = 'bere' and instock > @zero;", connection: conn);
            command4.Parameters.AddWithValue("@zero", 0);
            SqlDataReader reader3 = command4.ExecuteReader();

            while (reader3.Read())
            {
                listBox2.Items.Add(reader3["nume"].ToString());
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            
            SqlCommand command5 = new SqlCommand("SELECT nume FROM produse where tip = 'pizza' and instock > @zero;", connection: conn);
            command5.Parameters.AddWithValue("@zero", 0);
            SqlDataReader reader4 = command5.ExecuteReader();

            while (reader4.Read())
            {
                listBox3.Items.Add(reader4["nume"].ToString());
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            
            SqlCommand command6 = new SqlCommand("SELECT nume FROM produse where tip = 'ciorba'and instock > @zero;", connection: conn);
            command6.Parameters.AddWithValue("@zero", 0);
            SqlDataReader reader5 = command6.ExecuteReader();

            while (reader5.Read())
            {
                listBox4.Items.Add(reader5["nume"].ToString());
            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {
          // label1.Text = nume;
        }

        private void ListBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        public SqlConnection conn;
        public string name;
        public int numarmasa;
        public int liber;
        public int count;
        private void Form3_Load(object sender, EventArgs e)
        {

            name = LoginC1.nume;
            numarmasa = Initial.nrmasa;
            conn = Db.GetConnection();
            SqlCommand command1 = new SqlCommand("select Count(Liber),Liber from masa where nrmasa = @numarmasa and Liber = @zero Group By Liber", connection: conn);
            command1.Parameters.AddWithValue("@numarmasa", numarmasa);
            command1.Parameters.AddWithValue("@zero", 0);
            SqlDataReader reader = command1.ExecuteReader();

            while (reader.Read())
            {
                count = reader.GetInt32(0);
                liber = reader.GetInt32(1);

            }
            listBox7.Items.Add(liber.ToString());
            if (liber == 1 || count == 0)
            {
                SqlCommand command2 = new SqlCommand("insert into masa(idangajat, nrmasa, Liber) SELECT idangajat,@numarmasa,0 from angajat where nume = @name;", connection: conn);
                command2.Parameters.AddWithValue("@numarmasa", numarmasa);
                command2.Parameters.AddWithValue("@name", name);
                command2.ExecuteNonQuery();
                listBox5.Items.Add(name);
                listBox6.Items.Add(numarmasa.ToString());

                SqlCommand command3 = new SqlCommand("insert into comanda select idmasa from masa where nrmasa=@numarmasa;", connection: conn);
                command3.Parameters.AddWithValue("@numarmasa", numarmasa);
                command3.ExecuteNonQuery();
            }
            else if (liber == 0)
            {
                listBox5.Items.Add(name);
                listBox6.Items.Add(numarmasa.ToString());
            }

            SqlCommand command8 = new SqlCommand("Select p.nume from produse p " +
                "JOIN detalii_plata d ON(p.idproduse = d.idproduse) " +
                "JOIN comanda c ON(d.idcomanda = c.idcomanda) " +
                "JOIN masa m ON(c.idmasa = m.idmasa) where m.nrmasa = @numarmasa AND Liber = 0;", connection: conn);
            command8.Parameters.AddWithValue("@numarmasa", numarmasa);
            SqlDataReader reader3 = command8.ExecuteReader();

            while (reader3.Read())
            {
                listBox8.Items.Add(reader3["nume"].ToString());
            }

            SqlCommand command10 = new SqlCommand("Select COUNT(p.idproduse) from produse p " +
                "JOIN detalii_plata d ON(p.idproduse = d.idproduse) " +
                "JOIN comanda c ON(d.idcomanda = c.idcomanda) " +
                "JOIN masa m ON(c.idmasa = m.idmasa) where m.nrmasa = @numarmasa AND Liber = 0;", connection: conn);

            command10.Parameters.AddWithValue("@numarmasa", numarmasa);
            SqlDataReader reader33 = command10.ExecuteReader();

            while (reader33.Read())
            {
                count = reader33.GetInt32(0);
            }
            if (count > 0)
            {

                SqlCommand command9 = new SqlCommand("Select SUM(pret) from produse p " +
                    "JOIN detalii_plata d ON(p.idproduse = d.idproduse) " +
                    "JOIN comanda c ON(d.idcomanda = c.idcomanda) " +
                    "JOIN masa m ON(c.idmasa = m.idmasa) where m.nrmasa = @numarmasa AND Liber = 0 ;", connection: conn);

                command9.Parameters.AddWithValue("@numarmasa", numarmasa);
                SqlDataReader reader333 = command9.ExecuteReader();

                while (reader333.Read())
                {
                    richTextBox1.Text = reader333.GetInt32(0).ToString();
                }
            }

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            
            this.Hide();
            Initial f1 = new Initial();
            f1.ShowDialog();
            //this.Close();
            
        }

        private void SN()
        {
            IPAddress ipAd = IPAddress.Parse("127.0.0.1");
            // use local m/c IP address, and 
            // use the same in the client

            try {
                
                
                    /* Initializes the Listener */
                    TcpListener myList = new TcpListener(ipAd, 8001);
               
                /* Start Listeneting at the specified port */
           

                    myList.Start();
                    Socket s = myList.AcceptSocket();
                    String str = "New Comand";
                        ASCIIEncoding asen = new ASCIIEncoding();
                    byte[] ba = asen.GetBytes(str);
                    

                    s.Send(ba);
                }
                catch (Exception exception)
                {
                    string error = "An error occured while connecting [" + exception.Message + "]\n";
                    throw new Exception(error);
                }
        }
        private void Button6_Click(object sender, EventArgs e)
        {


            int c1 = listBox1.Items.Count - 1;
            int c2 = listBox2.Items.Count - 1;
            int c3 = listBox3.Items.Count - 1;
            int c4 = listBox4.Items.Count - 1;
            for (int i = c1; i >= 0; i--)
            {
                if (listBox1.GetSelected(i))
                {
                    listBox8.Items.Add(listBox1.Items[i]);
                    listBox1.SetSelected(index: listBox1.SelectedIndex, value: false);
                    SqlCommand command4 = new SqlCommand("insert into detalii_plata(idcomanda, idproduse) Select " +
                        "(Select c.idcomanda as idcomada from comanda c JOIN masa m ON(m.idmasa = c.idmasa) Where m.nrmasa = @numarmasa and Liber = 0) ," +
                        "(select idproduse as idproduse from produse where nume = @nume);", connection: conn);
                    command4.Parameters.AddWithValue("@numarmasa", numarmasa);
                    command4.Parameters.AddWithValue("@nume", listBox1.Items[i]);
                    command4.ExecuteNonQuery();

                   SqlCommand command44 = new SqlCommand("Update produse " +
                        "Set instock = (select instock from produse where nume = @nume) -1 " +
                        "where nume = @nume;", connection: conn);
                   command44.Parameters.AddWithValue("@nume", listBox1.Items[i]);
                   command44.ExecuteNonQuery();
                    listBox1.Items.Clear();

                    SqlCommand command3 = new SqlCommand("SELECT nume FROM produse where tip = 'racoritoare' and instock > @zero;", connection: conn);
                    command3.Parameters.AddWithValue("@zero", 0);
                    SqlDataReader reader2 = command3.ExecuteReader();

                    while (reader2.Read())
                    {
                        listBox1.Items.Add(reader2["nume"].ToString());
                    }

                }
            }
            for (int i = c2; i >= 0; i--)
            {
                if (listBox2.GetSelected(i))
                {
                    listBox8.Items.Add(listBox2.Items[i]);
                    listBox2.SetSelected(index: listBox2.SelectedIndex, value: false);
                    SqlCommand command5 = new SqlCommand("insert into detalii_plata(idcomanda, idproduse) Select " +
                        "(Select c.idcomanda as idcomada from comanda c JOIN masa m ON(m.idmasa = c.idmasa) Where m.nrmasa = @numarmasa and Liber = 0) ," +
                        "(select idproduse as idproduse from produse where nume = @nume);", connection: conn);
                    command5.Parameters.AddWithValue("@numarmasa", numarmasa);
                    command5.Parameters.AddWithValue("@nume", listBox2.Items[i]);
                    command5.ExecuteNonQuery();

                    SqlCommand command44 = new SqlCommand("Update produse " +
                        "Set instock = (select instock from produse where nume = @nume) -1 " +
                        "where nume = @nume;", connection: conn);
                    command44.Parameters.AddWithValue("@nume", listBox2.Items[i]);
                    command44.ExecuteNonQuery();

                    listBox2.Items.Clear();

                    SqlCommand command4 = new SqlCommand("SELECT nume FROM produse where tip = 'bere' and instock > @zero;", connection: conn);
                    command4.Parameters.AddWithValue("@zero", 0);
                    SqlDataReader reader3 = command4.ExecuteReader();

                    while (reader3.Read())
                    {
                        listBox2.Items.Add(reader3["nume"].ToString());
                    }

                }
            }
            for (int i = c3; i >= 0; i--)
            {
                if (listBox3.GetSelected(i))
                {
                    listBox8.Items.Add(listBox3.Items[i]);
                    listBox3.SetSelected(index: listBox3.SelectedIndex, value: false);
                    SqlCommand command6 = new SqlCommand("insert into detalii_plata(idcomanda, idproduse) Select " +
                       "(Select c.idcomanda as idcomada from comanda c JOIN masa m ON(m.idmasa = c.idmasa) Where m.nrmasa = @numarmasa and Liber = 0) ," +
                        "(select idproduse as idproduse from produse where nume = @nume);", connection: conn);
                    command6.Parameters.AddWithValue("@numarmasa", numarmasa);
                    command6.Parameters.AddWithValue("@nume", listBox3.Items[i]);
                    command6.ExecuteNonQuery();

                    SqlCommand command44 = new SqlCommand("Update produse " +
                        "Set instock = (select instock from produse where nume = @nume) -1 " +
                        "where nume = @nume;", connection: conn);
                    command44.Parameters.AddWithValue("@nume", listBox3.Items[i]);
                    command44.ExecuteNonQuery();

                    listBox3.Items.Clear();

                    SqlCommand command5 = new SqlCommand("SELECT nume FROM produse where tip = 'pizza' and instock > @zero;", connection: conn);
                    command5.Parameters.AddWithValue("@zero", 0);
                    SqlDataReader reader4 = command5.ExecuteReader();

                    while (reader4.Read())
                    {
                        listBox3.Items.Add(reader4["nume"].ToString());
                    }
                }
            }
            for (int i = c4; i >= 0; i--)
            {
                if (listBox4.GetSelected(i))
                {
                    listBox8.Items.Add(listBox4.Items[i]);
                    listBox4.SetSelected(index: listBox4.SelectedIndex, value: false);
                    SqlCommand command7 = new SqlCommand("insert into detalii_plata(idcomanda, idproduse) Select " +
                        "(Select c.idcomanda as idcomada from comanda c JOIN masa m ON(m.idmasa = c.idmasa) Where m.nrmasa = @numarmasa and Liber = 0) ," +
                        "(select idproduse as idproduse from produse where nume = @nume);", connection: conn);
                    command7.Parameters.AddWithValue("@numarmasa", numarmasa);
                    command7.Parameters.AddWithValue("@nume", listBox4.Items[i]);
                    command7.ExecuteNonQuery();

                    SqlCommand command44 = new SqlCommand("Update produse " +
                        "Set instock = (select instock from produse where nume = @nume) -1 " +
                        "where nume = @nume;", connection: conn);
                    command44.Parameters.AddWithValue("@nume", listBox4.Items[i]);
                    command44.ExecuteNonQuery();

                    listBox4.Items.Clear();

                    SqlCommand command6 = new SqlCommand("SELECT nume FROM produse where tip = 'ciorba'and instock > @zero;", connection: conn);
                    command6.Parameters.AddWithValue("@zero", 0);
                    SqlDataReader reader5 = command6.ExecuteReader();

                    while (reader5.Read())
                    {
                        listBox4.Items.Add(reader5["nume"].ToString());
                    }

                }
            }

            SqlCommand command19 = new SqlCommand("Select SUM(pret) from produse p " +
                "JOIN detalii_plata d ON(p.idproduse = d.idproduse) " +
                "JOIN comanda c ON(d.idcomanda = c.idcomanda) " +
                "JOIN masa m ON(c.idmasa = m.idmasa) where m.nrmasa = @numarmasa and Liber = 0;", connection: conn);

            command19.Parameters.AddWithValue("@numarmasa", numarmasa);
            SqlDataReader reader30 = command19.ExecuteReader();
            richTextBox1.Text = "";
            while (reader30.Read())
            {
                richTextBox1.Text = reader30.GetInt32(0).ToString();
            }

        }

        private void Button7_Click(object sender, EventArgs e)
        {
            SqlCommand command17 = new SqlCommand("INSERT INTO plata(idmasa,Datat,Ora,Suma) " +
                "Select " +
                 "(Select idmasa from masa where nrmasa = @numarmasa and Liber = 0)," +
                 "(Select GETDATE()),(SELECT CONVERT(VARCHAR(8), GETDATE(), 108))," +
                 "(Select SUM(pret) from produse p " +
                 "JOIN detalii_plata d ON(p.idproduse = d.idproduse) " +
                 "JOIN comanda c ON(d.idcomanda = c.idcomanda) " +
                 "JOIN masa m ON(c.idmasa = m.idmasa) " +
                 "where m.nrmasa = @numarmasa and Liber = 0);", connection: conn);
            command17.Parameters.AddWithValue("@numarmasa", numarmasa);
            command17.ExecuteNonQuery();

           

            SqlCommand command18 = new SqlCommand("UPDATE masa " +
                "SET Liber = @one " +
                "WHERE nrmasa = @numarmasa and Liber = @zero;", connection: conn);
            command18.Parameters.AddWithValue("@numarmasa", numarmasa);
            command18.Parameters.AddWithValue("@one", 1);
            command18.Parameters.AddWithValue("@zero",0);
            command18.ExecuteNonQuery();
            MessageBox.Show("DONE!");
           // SN();
        }
    }
}
