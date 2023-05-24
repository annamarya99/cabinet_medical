using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Cabinet_Medical
{
    public partial class Tests : Form
    {
        public Tests()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //This is my connection string i have assigned the database file address path
                string MyConnection2 = "datasource=localhost;port=3306;username=root;password=root";
                //This is my insert query in which i am taking input from the user through windows forms
                string Query = "insert into counters.tests(Name, Cost) values('" + this.textBox1.Text + "','" + this.textBox2.Text + "');";
                //This is  MySqlConnection here i have created the object and pass my connection string.
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                //This is command class which will handle the query and connection object.
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();     // Here our query will be executed and data saved into the database.
                MessageBox.Show("Save Data");
                while (MyReader2.Read())
                {
                }
                MyConn2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {
                string MyConnection2 = "datasource=localhost;port=3306;username=root;password=root";
                //Display query
                string Query = "select * from counters.tests;";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MyConn2.Open();
                //For offline connection we weill use  MySqlDataAdapter class.
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MyCommand2;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                TestGV.DataSource = dTable;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Tests_Load(object sender, EventArgs e)
        {
            string MyConnection2 = "datasource=localhost;port=3306;username=root;password=root";
            string Query = "select * from counters.tests;";
            MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
            MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
            MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
            MyAdapter.SelectCommand = MyCommand2;
            DataTable dTable = new DataTable();
            MyAdapter.Fill(dTable);
            TestGV.DataSource = dTable;


            this.TestGV.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSeaGreen;

            TestGV.ColumnHeadersVisible = true;
            TestGV.Columns[0].HeaderText = "Test code";
            TestGV.Columns[1].HeaderText = "Test name";
            TestGV.Columns[2].HeaderText = "Cost";
            
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (TestGV.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Sigur doriți să ștergeți această linie?", "Confirmare ștergere", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int selectedRowIndex = TestGV.SelectedRows[0].Index;

                    // Obținerea valorii cheii primare a rândului selectat (presupunând că există o coloană cu cheia primară numită "ID")
                    int selectedRowID = Convert.ToInt32(TestGV.Rows[selectedRowIndex].Cells["idtests"].Value);

                    // Crearea conexiunii la baza de date
                    string connString = "datasource=localhost;database=counters;port=3306;username=root;password=root";
                    MySqlConnection connection = new MySqlConnection(connString);

                    try
                    {
                        // Deschiderea conexiunii
                        connection.Open();

                        // Ștergerea liniei din baza de date
                        string deleteQuery = "DELETE FROM tests WHERE idtests = @idtests";
                        MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                        deleteCommand.Parameters.AddWithValue("@idtests", selectedRowID);
                        deleteCommand.ExecuteNonQuery();

                        // Ștergerea rândului din DataGridView
                        TestGV.Rows.RemoveAt(selectedRowIndex);

                        MessageBox.Show("Linia a fost ștearsă cu succes.", "Ștergere reușită", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("A apărut o eroare la ștergerea liniei: " + ex.Message, "Eroare ștergere", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        // Închiderea conexiunii
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vă rugăm să selectați un rând pentru a-l șterge.", "Eroare ștergere", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                string MyConnection2 = "datasource=localhost;port=3306;username=root;password=root";
                //Display query
                string Query = "select * from counters.tests;";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                //  MyConn2.Open();
                //For offline connection we weill use  MySqlDataAdapter class.
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MyCommand2;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                TestGV.DataSource = dTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TestGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string MyConnection2 = "datasource=localhost;port=3306;username=root;password=root";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand("UPDATE counters.tests SET name=@name, cost=@cost WHERE idtests=@id", MyConn2);
                MyConn2.Open();
                MyCommand2.Parameters.AddWithValue("@id", TestGV.Rows[e.RowIndex].Cells["idtests"].Value.ToString());
                MyCommand2.Parameters.AddWithValue("@name", TestGV.Rows[e.RowIndex].Cells["name"].Value.ToString());
                MyCommand2.Parameters.AddWithValue("@cost", TestGV.Rows[e.RowIndex].Cells["cost"].Value.ToString());
                
                MyCommand2.ExecuteNonQuery();
                MyConn2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Doctors doctorsForm = new Doctors();
            doctorsForm.Show();

            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Patients patientsForm = new Patients();
            patientsForm.Show();

            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Results resultForm = new Results();
            resultForm.Show();

            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
