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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BD_Rabotaet
{
    public partial class Form3 : Form
    {
        Database database;
        Form1 form1;
        BindingSource bindingSourceWW = new BindingSource();
        BindingSource bindingSourceW = new BindingSource();
        private int idw;
        public Form3(Database database, Form1 form1, int id)
        {
            InitializeComponent();
            this.database = database;
            this.form1 = form1;
            this.idw = id;
        }

        private void Form3_Shown(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            LoadZakazs();
            LoadWorkTable(idw);
            busyness(idw);
        }

        void LoadZakazs()
        {
            comboBox1.Items.Clear();
            SqlCommand sqlCommand = new SqlCommand("exec Set_num_Zakaz", database.GetConnection());
            try
            {
                SqlDataReader readerSNZ = sqlCommand.ExecuteReader();
                List<string[]> dataSNZ = new List<string[]>();
                while (readerSNZ.Read())
                {
                    dataSNZ.Add(new string[1]);
                    for (int i = 0; i < 1; i++)
                        dataSNZ[dataSNZ.Count - 1][i] = readerSNZ[i].ToString();
                }
                readerSNZ.Close();
                foreach (string[] s in dataSNZ)
                {
                    comboBox1.Items.Add(s[0]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void LoadWorkerWorks(int idz, int idw)
        {
            try
            {
                string sqlExpression = @"dbo.Load_Worker_Works @idz, @idw";
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, database.GetConnection());
                SqlParameter Paramz = new SqlParameter("@idz", SqlDbType.Int);
                Paramz.Value = idz;
                sqlCommand.Parameters.Add(Paramz);
                SqlParameter Paramw = new SqlParameter("@idw", SqlDbType.Int);
                Paramw.Value = idw;
                sqlCommand.Parameters.Add(Paramw);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = sqlCommand;
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                bindingSourceW.DataSource = dataTable;
                comboBox2.DataSource = bindingSourceW.DataSource;
                comboBox2.DisplayMember = "Name";
                comboBox2.ValueMember = "name";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void LoadWorkTable(int id)
        {
            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = new SqlCommand("exec Load_Workers_Work " + id.ToString(), database.GetConnection());
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                bindingSourceWW.DataSource = dataTable;
                dataGridView1.DataSource = bindingSourceWW;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            database.CloseConnection();
            form1.Close();
        }

        void busyness(int id)
        {
            SqlCommand sqlCommand = new SqlCommand("exec workers_busyness " + id.ToString(), database.GetConnection());
            try
            {
                SqlDataReader readerb = sqlCommand.ExecuteReader();
                List<string[]> datab = new List<string[]>();
                while (readerb.Read())
                {
                    datab.Add(new string[1]);
                    for (int i = 0; i < 1; i++)
                        datab[datab.Count - 1][i] = readerb[i].ToString();
                }
                readerb.Close();
                foreach (string[] s in datab)
                {
                    textBox1.Clear();
                    textBox1.AppendText(s[0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlExpression = @"dbo.insert_end_work_date @idz, @work, @date";
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, database.GetConnection());
                SqlParameter Paramz = new SqlParameter("@idz", SqlDbType.Int);
                Paramz.Value = Int32.Parse(comboBox1.Text);
                sqlCommand.Parameters.Add(Paramz);
                SqlParameter Paramw = new SqlParameter("@work", SqlDbType.VarChar, 50);
                Paramw.Value = comboBox2.Text;
                sqlCommand.Parameters.Add(Paramw);
                SqlParameter Paramdate = new SqlParameter("@date", SqlDbType.VarChar, 20);
                Paramdate.Value = dateTimePicker1.Value.ToString("dd.MM.yyyy"); ;
                sqlCommand.Parameters.Add(Paramdate);
                int number = sqlCommand.ExecuteNonQuery();
                MessageBox.Show($"Добавлено объектов: {number}");
                busyness(idw);
                LoadWorkTable(idw);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string choice = comboBox1.Text;
            int a = Int32.Parse(comboBox1.Text);
            LoadWorkerWorks(a, idw);
        }
    }
}
