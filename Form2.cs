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

namespace BD_Rabotaet
{
    public partial class Form2 : Form
    {
        Database database;
        Form1 form1;
        public Form2(Database database, Form1 form1)
        {
            this.database = database;
            this.form1 = form1;
            InitializeComponent();
        }
        
        void LoadData(string date1, string date2) 
        {
            dataGridView1.Rows.Clear();
            string query = String.Format(@"SELECT * FROM CheckDates('{0}', '{1}')",
                date1, date2);
            SqlCommand sqlCommand = new SqlCommand(query, database.GetConnection());
            try
            {
                SqlDataReader reader = sqlCommand.ExecuteReader();
                List<string[]> data = new List<string[]>();
                while (reader.Read())
                {
                    data.Add(new string[4]);

                    data[data.Count - 1][0] = reader[0].ToString();
                    data[data.Count - 1][1] = reader[1].ToString();
                    data[data.Count - 1][2] = reader[2].ToString();
                    data[data.Count - 1][3] = reader[3].ToString();
                }
                reader.Close();

                foreach (string[] s in data)
                {
                    dataGridView1.Rows.Add(s);
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string date1 = String.Format("{2}-{1}-{0}",
                dateTimePicker1.Value.Day, dateTimePicker1.Value.Month, dateTimePicker1.Value.Year);
            string date2 = String.Format("{2}-{1}-{0}",
                dateTimePicker2.Value.Day, dateTimePicker2.Value.Month, dateTimePicker2.Value.Year);

            LoadData(date1, date2);
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e){
            database.CloseConnection();
            form1.Close();
        }
    }
}