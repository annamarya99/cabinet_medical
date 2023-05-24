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

namespace Cabinet_Medical
{
    public partial class Results : Form
    {
        public Results()
        {
            InitializeComponent();
        }

        private void Results_Load(object sender, EventArgs e)
        {
            // Crearea conexiunii la baza de date
            string connString = "datasource=localhost;database=counters;port=3306;username=root;password=root";
            MySqlConnection connection = new MySqlConnection(connString);

            try
            {
                // Deschiderea conexiunii
                connection.Open();

                // Interogarea bazei de date pentru a obține ID-urile pacienților
                string query = "SELECT idcounters FROM patients";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                // Adăugarea ID-urilor pacienților în ComboBox
                while (reader.Read())
                {
                    int pacientID = reader.GetInt32("idcounters");
                    pacientIDcomboBox2.Items.Add(pacientID);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("A apărut o eroare la încărcarea pacienților: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Închiderea conexiunii
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            try
            {
                // Deschiderea conexiunii
                connection.Open();

                // Interogarea bazei de date pentru a obține ID-urile doctorilor
                string query = "SELECT id FROM doctors";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                // Adăugarea ID-urilor pacienților în ComboBox
                while (reader.Read())
                {
                    int doctorID = reader.GetInt32("id");
                    doctorIDcomboBox2.Items.Add(doctorID);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("A apărut o eroare la încărcarea doctorilor: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Închiderea conexiunii
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            try
            {
                // Deschiderea conexiunii
                connection.Open();

                // Interogarea bazei de date pentru a obține ID-urile doctorilor
                string query = "SELECT idtests FROM tests";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                // Adăugarea ID-urilor pacienților în ComboBox
                while (reader.Read())
                {
                    int testID = reader.GetInt32("idtests");
                    testsIDcomboBox2.Items.Add(testID);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("A apărut o eroare la încărcarea testelor: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Închiderea conexiunii
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Tests testForm = new Tests();
            testForm.Show();

            this.Hide();
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

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
