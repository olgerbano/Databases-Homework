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

namespace BARMANAGER
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public SqlConnection conn;
        int id;
        string nn;
        private string data;
        private string ora;

        private void Button1_Click(object sender, EventArgs e)
        {
            SqlCommand command1 = new SqlCommand("select count(idangajat) from angajat " +
                "where nume = @nume and prenume = @prenume and parola IS NULL; ", connection: conn);
            command1.Parameters.AddWithValue("@nume", textBox1.Text);
            command1.Parameters.AddWithValue("@prenume", textBox2.Text);
            SqlDataReader reader = command1.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (reader.GetInt32(0) != 0)
                    {
                        SqlCommand command2 = new SqlCommand("update angajat " +
                            "SET nume = @nume, prenume = @prenume, parola = @parola " +
                            "where nume = @nume and prenume = @prenume and parola IS NULL;", connection: conn);
                        command2.Parameters.AddWithValue("@nume", textBox1.Text);
                        command2.Parameters.AddWithValue("@prenume", textBox2.Text);
                        int pass = int.Parse(textBox3.Text);
                        command2.Parameters.AddWithValue("@parola", pass);
                        command2.ExecuteNonQuery();
                        MessageBox.Show("Welcome back " + textBox1.Text + " " + textBox2.Text);
                    }
                    else
                    {
                        SqlCommand command = new SqlCommand("INSERT INTO angajat VALUES (@nume,@prenume,@parola);", connection: conn);
                        command.Parameters.AddWithValue("@nume", textBox1.Text);
                        command.Parameters.AddWithValue("@prenume", textBox2.Text);
                        int pass = int.Parse(textBox3.Text);
                        command.Parameters.AddWithValue("@parola", pass);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Adaugat angajat " + textBox1.Text + " " + textBox2.Text);
                    }
                }
            }
            

            

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            listBox1.Items.Clear();
            Afis_angajati();
        }
        private void Afis_angajati()
        {
            SqlCommand command = new SqlCommand(cmdText: "select CONCAT(nume, ' ',prenume) AS NUME from angajat " +
                "where parola is not null; ", connection: conn);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    listBox1.Items.Add(reader["NUME"].ToString());
                }
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            conn = Db.GetConnection();

            listBox1.Items.Clear();
            dataGridView2.DataSource = GetPlata();
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView3.DataSource = GetProduse();
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            Afis_angajati();

            /*
            Timer timer = new Timer
            {
                Interval = (10 * 1000) // 10 secs
            };
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
            */
            
    

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            dataGridView2.DataSource = GetPlata();
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView3.DataSource = GetProduse();
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            Afis_angajati();
        }

        private DataTable GetPlata()
        {
            DataTable pl = new DataTable();
            SqlCommand command = new SqlCommand("Select CONCAT(a.nume, ' ',a.prenume) AS ANGAJAT,p.suma AS SUMA,p.datat AS DATA,p.ora AS ORA from angajat a " +
                "Join masa m ON(m.idangajat = a.idangajat) " +
                "Join plata p ON(p.idmasa = m.idmasa) Order by datat Desc,ora DESC", connection: conn);
            SqlDataReader reader = command.ExecuteReader();
            pl.Load(reader);
            return pl;
        }
        private DataTable GetProduse()
        {
            DataTable pl1 = new DataTable();
            SqlCommand command221 = new SqlCommand("select nume,pret,instock from produse;", connection: conn);
            SqlDataReader reader = command221.ExecuteReader();
            pl1.Load(reader);
            return pl1;
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("update angajat " +
                "SET parola = CAST(NULL AS INT) " +
                "where nume = @nume and " +
                "prenume = @prenume and " +
                "parola = @parola;", connection: conn);
            command.Parameters.AddWithValue("@nume", textBox4.Text);
            command.Parameters.AddWithValue("@prenume", textBox5.Text);
            int pass = int.Parse(textBox6.Text);
            command.Parameters.AddWithValue("@parola", pass);
            command.ExecuteNonQuery();
            MessageBox.Show("Sters angajat " + textBox4.Text + " " + textBox5.Text);
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";

            listBox1.Items.Clear();
            Afis_angajati();

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("INSERT INTO produse VALUES (@nume,@pret,@tip,@instock);", connection: conn);
            int pret = int.Parse(textBox8.Text);
            int instock = int.Parse(textBox10.Text);
            command.Parameters.AddWithValue("@pret", pret);
            command.Parameters.AddWithValue("@instock", instock);
            command.Parameters.AddWithValue("@nume", textBox9.Text);
            command.Parameters.AddWithValue("@tip", textBox7.Text);
            command.ExecuteNonQuery();
            MessageBox.Show("Adaugat produs " + textBox9.Text);
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (textBox13.Text.Equals(""))
            {
                SqlCommand command = new SqlCommand("Update produse Set instock = @instock where nume = @nume;", connection: conn);
                command.Parameters.AddWithValue("@instock", int.Parse(textBox12.Text));
                command.Parameters.AddWithValue("@nume", textBox14.Text);
                command.ExecuteNonQuery();
                MessageBox.Show("Modificat instock ");


            }
            else if (textBox12.Text.Equals(""))
            {
                SqlCommand command = new SqlCommand("Update produse Set pret = @pret where nume = @nume;", connection: conn);
                int pret = int.Parse(textBox13.Text);
                command.Parameters.AddWithValue("@pret",pret );
                command.Parameters.AddWithValue("@nume", textBox14.Text);
                command.ExecuteNonQuery();
                MessageBox.Show("Modificat pret ");
              
            }
            else if (!textBox12.Text.Equals("") && !textBox13.Text.Equals(""))
            {
                SqlCommand command = new SqlCommand("Update produse Set pret = @pret,instock = @instock where nume = @nume;", connection: conn);
                command.Parameters.AddWithValue("@pret", int.Parse(textBox13.Text));
                command.Parameters.AddWithValue("@instock", int.Parse(textBox12.Text));
                command.Parameters.AddWithValue("@nume", textBox14.Text);
                command.ExecuteNonQuery();
                MessageBox.Show("Modificat pret si instock ");
            }
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
        }

        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //id = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["idplata"].Value.ToString());
            //nn = dataGridView2.Rows[e.RowIndex].Cells["nume"].Value.ToString();
            data = dataGridView2.Rows[e.RowIndex].Cells["DATA"].Value.ToString();
            ora = dataGridView2.Rows[e.RowIndex].Cells["ORA"].Value.ToString();
            DataTable pl = new DataTable();
            //SqlCommand command = new SqlCommand("Select * from plata where idplata = @id ", connection: conn);
            //command.Parameters.AddWithValue("@id", id);
            SqlCommand command = new SqlCommand("Select * from plata where datat = @data and ora = @ora ", connection: conn);
            command.Parameters.AddWithValue("@data", data);
            command.Parameters.AddWithValue(parameterName: "@ora", value: ora);
            SqlDataReader reader = command.ExecuteReader();
            pl.Load(reader);
            foreach(DataRow dr in pl.Rows)
            {
                DataTable pl1 = new DataTable();
                SqlCommand command1 = new SqlCommand("select pr.nume AS PRODUCT,pr.pret AS PRET from produse pr " +
                    "JOIN detalii_plata d ON(pr.idproduse = d.idproduse) " +
                    "JOIN comanda c ON(d.idcomanda = c.idcomanda) " +
                    "JOIN masa m ON(c.idmasa = m.idmasa) " +
                    "join plata p ON(m.idmasa = p.idmasa) " +
                    "where p.ora = @ora and p.datat = @datat;", connection: conn);
                command1.Parameters.AddWithValue("@ora", dr["ora"].ToString());
                command1.Parameters.AddWithValue("@datat", dr["datat"].ToString());
                SqlDataReader reader1 = command1.ExecuteReader();
                pl1.Load(reader1);
                dataGridView1.DataSource = pl1;
                dataGridView1.AutoSizeColumnsMode =  DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            dataGridView2.DataSource = GetPlata();
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView3.DataSource = GetProduse();
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            Afis_angajati();
        }
    }
}
