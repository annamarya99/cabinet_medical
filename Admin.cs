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
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string numeUtilizator = txtNumeUtilizator.Text;
            string parola = txtParola.Text;

            if (VerificaAutentificare(numeUtilizator, parola))
            {

                Patients patientsForm = new Patients();
                patientsForm.Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show("User name or password wrong!");
            }
        }
        private bool VerificaAutentificare(string numeUtilizator, string parola)
        {
            string connectionString = "datasource=localhost;port=3306;Database=counters; username=root;password=root";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM admin WHERE Username = @NumeUtilizator AND password = @Parola";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NumeUtilizator", numeUtilizator);
                    command.Parameters.AddWithValue("@Parola", parola);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }
        }
    }
}
