using Guna.UI.WinForms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Cabinet_Medical
{
    public partial class Doctors : Form
    {
        public Doctors()
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
                string Query = "insert into counters.doctors(Name, Phone, Role, Gender) values('" + this.textBox1.Text + "','" + this.textBox2.Text + "','" + this.comboBox2.GetItemText(comboBox2.SelectedItem) + "','" + this.comboBox1.GetItemText(comboBox1.SelectedItem) +  "');";
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
                string Query = "select * from counters.doctors;";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MyConn2.Open();
                //For offline connection we weill use  MySqlDataAdapter class.
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MyCommand2;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                DoctorsGV.DataSource = dTable;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Doctors_Load(object sender, EventArgs e)
        {
            string MyConnection2 = "datasource=localhost;port=3306;username=root;password=root";
            string Query = "select * from counters.doctors;";
            MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
            MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
            MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
            MyAdapter.SelectCommand = MyCommand2;
            DataTable dTable = new DataTable();
            MyAdapter.Fill(dTable);
            DoctorsGV.DataSource = dTable;


            this.DoctorsGV.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSeaGreen;

            DoctorsGV.ColumnHeadersVisible = true;
            DoctorsGV.Columns[0].HeaderText = "ID";
            DoctorsGV.Columns[1].HeaderText = "Doctor name";
            DoctorsGV.Columns[2].HeaderText = "Doctor phone";
            DoctorsGV.Columns[3].HeaderText = "Role";
            DoctorsGV.Columns[4].HeaderText = "Doctor gender";
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (DoctorsGV.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Sigur doriți să ștergeți această linie?", "Confirmare ștergere", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int selectedRowIndex = DoctorsGV.SelectedRows[0].Index;

                    // Obținerea valorii cheii primare a rândului selectat (presupunând că există o coloană cu cheia primară numită "ID")
                    int selectedRowID = Convert.ToInt32(DoctorsGV.Rows[selectedRowIndex].Cells["id"].Value);

                    // Crearea conexiunii la baza de date
                    string connString = "datasource=localhost;database=counters;port=3306;username=root;password=root";
                    MySqlConnection connection = new MySqlConnection(connString);

                    try
                    {
                        // Deschiderea conexiunii
                        connection.Open();

                        // Ștergerea liniei din baza de date
                        string deleteQuery = "DELETE FROM patients WHERE id = @id";
                        MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                        deleteCommand.Parameters.AddWithValue("@id", selectedRowID);
                        deleteCommand.ExecuteNonQuery();

                        // Ștergerea rândului din DataGridView
                        DoctorsGV.Rows.RemoveAt(selectedRowIndex);

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
                string Query = "select * from counters.doctors;";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                //  MyConn2.Open();
                //For offline connection we weill use  MySqlDataAdapter class.
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MyCommand2;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                DoctorsGV.DataSource = dTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DoctorsGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string MyConnection2 = "datasource=localhost;port=3306;username=root;password=root";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand("UPDATE counters.doctors SET name=@name, phone=@phone, role=@role, gender=@gender WHERE id=@id", MyConn2);
                MyConn2.Open();
                MyCommand2.Parameters.AddWithValue("@id", DoctorsGV.Rows[e.RowIndex].Cells["id"].Value.ToString());
                MyCommand2.Parameters.AddWithValue("@name", DoctorsGV.Rows[e.RowIndex].Cells["name"].Value.ToString());
                MyCommand2.Parameters.AddWithValue("@phone", DoctorsGV.Rows[e.RowIndex].Cells["phone"].Value.ToString());
                string role = DoctorsGV.Rows[e.RowIndex].Cells["role"].Value.ToString();
                if (role == "Nurse" || role == "Resident doctor" || role== "Fellow doctor" || role== "Attending doctor" || role== "Specialist doctor" || role == "Lab assistent")
                {
                    MyCommand2.Parameters.AddWithValue("@role", role);
                }
                else
                {
                    MessageBox.Show("Role must be 'Nurse' or 'Resident doctor' or 'Fellow doctor' or 'Attending doctor' or 'Specialist doctor' or 'Lab assistent'");
                }
                string gender = DoctorsGV.Rows[e.RowIndex].Cells["gender"].Value.ToString();
                if (gender == "Women" || gender == "Male")
                {
                    MyCommand2.Parameters.AddWithValue("@gender", gender);
                }
                else
                {
                    MessageBox.Show("Gender must be 'Male' or 'Female'");
                }
                MyCommand2.ExecuteNonQuery();
                MyConn2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Patients patientsForm = new Patients();
            patientsForm.Show();

            this.Hide();
        }

        

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Tests testForm = new Tests();
            testForm.Show();

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
