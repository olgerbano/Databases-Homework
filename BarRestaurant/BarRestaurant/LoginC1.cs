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
    public partial class LoginC1 : Form
    {
        public LoginC1()
        {
            InitializeComponent();
            textBox1.PasswordChar = '*';
        }
        public static string nume;
        private void Form3()
        {
            this.Hide();
            Form3 f3 = new Form3();
            f3.ShowDialog(); // Shows Form3
           
        }
        public int count;
        public void Button1_Click(object sender, EventArgs e)
        {
            SqlCommand command1 = new SqlCommand("Select Count(nrmasa) from masa where nrmasa = @nrmasa and Liber = @zero", connection: conn);
            command1.Parameters.AddWithValue("@nrmasa", Initial.nrmasa);
            command1.Parameters.AddWithValue("@zero", 0);
            SqlDataReader reader1 = command1.ExecuteReader();
            while (reader1.Read())
            {
                count = reader1.GetInt32(0);
                
            }
            if (count == 0)
            {
                SqlCommand command = new SqlCommand("SELECT nume,parola FROM angajat where parola IS NOT NULL;", connection: conn);
                SqlDataReader reader = command.ExecuteReader();
                int pass = int.Parse(textBox1.Text);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader.GetInt32(1) == pass)
                        {

                            nume = reader["nume"].ToString();

                            Form3();
                            //listBox1.Items.Add(nume);



                        }
                    }
                }
            }
            else if(count == 1)
            {
                SqlCommand command = new SqlCommand("Select nume,a.parola from angajat a Join masa b ON a.idangajat = b.idangajat where b.nrmasa = @nrmasa and a.parola IS NOT NULL;  ", connection: conn);
                command.Parameters.AddWithValue("@nrmasa", Initial.nrmasa);
                SqlDataReader reader = command.ExecuteReader();
                int pass = int.Parse(textBox1.Text);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader.GetInt32(1) == pass)
                        {

                            nume = reader["nume"].ToString();
                            this.Close();
                            
                            Form3();
                            //listBox1.Items.Add(nume);



                        }
                    }
                }
            }


            

        }
        public SqlConnection conn;
        private void LoginC1_Load(object sender, EventArgs e)
        {
            conn = Db.GetConnection();
        }
    }
}
