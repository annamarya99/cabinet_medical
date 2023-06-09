﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace Cabinet_Medical
{
    public partial class Patients : Form
    {
        public int count = 101;
        //List<Pacient> pacientsList = new List<Pacient> ();
        
        public Patients()
        {
            InitializeComponent();
           
           

            GraphicsPath newgraph = new GraphicsPath();
            newgraph.StartFigure();
            newgraph.AddArc(new Rectangle(0, 0, 20, 20), 180, 90);
            newgraph.AddLine(20, 0, button1.Width - 20, 0);
            newgraph.AddArc(new Rectangle(panel3.Width - 20, 0, 20, 20), -90, 90);
            newgraph.AddLine(button1.Width, 20, button1.Width, button1.Height - 20);
            newgraph.AddArc(new Rectangle(button1.Width - 20, button1.Height - 20, 20, 20), 0, 90);
            newgraph.AddLine(button1.Width - 20, button1.Height, 20, button1.Height);
            newgraph.AddArc(new Rectangle(0, button1.Height - 20, 20, 20), 90, 90);
            newgraph.CloseAllFigures();
            button1.Region = new Region(newgraph);
        }


        private void Patients_Load(object sender, EventArgs e)
        {
            string MyConnection2 = "datasource=localhost;port=3306;username=root;password=root";
            string Query = "select * from counters.patients;";
            MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
            MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
            MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
            MyAdapter.SelectCommand = MyCommand2;
            DataTable dTable = new DataTable();
            MyAdapter.Fill(dTable);
            gunaDataGridView1.DataSource = dTable;


            this.gunaDataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSeaGreen;

            gunaDataGridView1.ColumnHeadersVisible = true;
            gunaDataGridView1.Columns[0].HeaderText = "ID";
            gunaDataGridView1.Columns[1].HeaderText = "Pacient name";
            gunaDataGridView1.Columns[2].HeaderText = "Pacient phone";
            gunaDataGridView1.Columns[3].HeaderText = "Pacient adress";
            gunaDataGridView1.Columns[4].HeaderText = "Pacient gender";
            gunaDataGridView1.Columns[5].HeaderText = "Pacient birth date";
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //This is my connection string i have assigned the database file address path
                string MyConnection2 = "datasource=localhost;port=3306;username=root;password=root";
                //This is my insert query in which i am taking input from the user through windows forms
                string Query = "insert into counters.patients(name, phone, adress, gender, dateBirth) values('" + this.textBox1.Text + "','" + this.textBox2.Text + "','" + this.textBox3.Text + "','" + this.comboBox1.GetItemText(comboBox1.SelectedItem) + "','" + this.dateTimePicker1.Text + "');";
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
                string Query = "select * from counters.patients;";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                 MyConn2.Open();
                //For offline connection we weill use  MySqlDataAdapter class.
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MyCommand2;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                gunaDataGridView1.DataSource = dTable;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (gunaDataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Sigur doriți să ștergeți această linie?", "Confirmare ștergere", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int selectedRowIndex = gunaDataGridView1.SelectedRows[0].Index;

                    // Obținerea valorii cheii primare a rândului selectat (presupunând că există o coloană cu cheia primară numită "ID")
                    int selectedRowID = Convert.ToInt32(gunaDataGridView1.Rows[selectedRowIndex].Cells["idcounters"].Value);

                    // Crearea conexiunii la baza de date
                    string connString = "datasource=localhost;database=counters;port=3306;username=root;password=root";
                    MySqlConnection connection = new MySqlConnection(connString);

                    try
                    {
                        // Deschiderea conexiunii
                        connection.Open();

                        // Ștergerea liniei din baza de date
                        string deleteQuery = "DELETE FROM patients WHERE idcounters = @idcounters";
                        MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                        deleteCommand.Parameters.AddWithValue("@idcounters", selectedRowID);
                        deleteCommand.ExecuteNonQuery();

                        // Ștergerea rândului din DataGridView
                        gunaDataGridView1.Rows.RemoveAt(selectedRowIndex);

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
                string Query = "select * from counters.patients;";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                //  MyConn2.Open();
                //For offline connection we weill use  MySqlDataAdapter class.
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MyCommand2;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                gunaDataGridView1.DataSource = dTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



      

        

        private void gunaDataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string MyConnection2 = "datasource=localhost;port=3306;username=root;password=root";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand("UPDATE counters.patients SET name=@name, phone=@phone, adress=@adress, gender=@gender, dateBirth=@newDate WHERE idcounters=@id", MyConn2);
                MyConn2.Open();
                MyCommand2.Parameters.AddWithValue("@id", gunaDataGridView1.Rows[e.RowIndex].Cells["idcounters"].Value.ToString());
                MyCommand2.Parameters.AddWithValue("@name", gunaDataGridView1.Rows[e.RowIndex].Cells["name"].Value.ToString());
                MyCommand2.Parameters.AddWithValue("@phone", gunaDataGridView1.Rows[e.RowIndex].Cells["phone"].Value.ToString());
                MyCommand2.Parameters.AddWithValue("@adress", gunaDataGridView1.Rows[e.RowIndex].Cells["adress"].Value.ToString());
                string gender = gunaDataGridView1.Rows[e.RowIndex].Cells["gender"].Value.ToString();
                if (gender == "Women" || gender == "Male")
                {
                    MyCommand2.Parameters.AddWithValue("@gender", gender);
                }
                else
                {
                    MessageBox.Show("Gender must be 'Male' or 'Female'");
                }
                DateTime newDate;
                if (DateTime.TryParseExact(gunaDataGridView1.Rows[e.RowIndex].Cells["dateBirth"].Value.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out newDate))
                {
                    MyCommand2.Parameters.AddWithValue("@newDate", newDate.ToString("dd-MM-yyyy"));
                }
                else
                {
                    MessageBox.Show("Invalid date format. Please use format 'dd-MM-yyyy'.");
                    return;
                }
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

