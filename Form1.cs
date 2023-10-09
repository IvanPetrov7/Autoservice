using System;
using System.Configuration;
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
    public partial class Form1 : Form
    {
        Database database = new Database();
        private int id;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Логин не введен!");
                return;
            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("Пароль не введен!");
                return;
            }

            database.InitializeConnectionString(textBox1.Text, textBox2.Text);
            database.CloseConnection();
            try
            {
                database.OpenConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            if (textBox1.Text == "KrashenAA")
            {
                string sqlExpression = @"dbo.get_log @log";
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, database.GetConnection());
                SqlParameter Paraml = new SqlParameter("@log", SqlDbType.VarChar, 20);
                Paraml.Value = textBox1.Text;
                sqlCommand.Parameters.Add(Paraml);
                SqlDataReader log = sqlCommand.ExecuteReader();
                if (log.Read())
                {
                    id = Int32.Parse(String.Format("{0}", log[0]));
                }
                log.Close();
                Form3 form3 = new Form3(database, this, id);
                form3.Show();
                this.Hide();
            }
            if (textBox1.Text == "priemshik")
            {
                Form4 form4 = new Form4(database, this);
                form4.Show();
                this.Hide();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            database.CloseConnection();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
