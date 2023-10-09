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
    public partial class Form4 : Form
    {
        Database database;
        Form1 form1;
        BindingSource bindingSourceZ = new BindingSource();
        BindingSource bindingSourceZs = new BindingSource();
        BindingSource bindingSourceSnZ = new BindingSource();
        BindingSource bindingSourceM = new BindingSource();
        BindingSource bindingSourceMM = new BindingSource();
        BindingSource bindingSourceZki = new BindingSource();
        BindingSource bindingSourceZks = new BindingSource();
        BindingSource bindingSourceZP = new BindingSource();
        BindingSource bindingSourceC = new BindingSource();
        BindingSource bindingSourceW = new BindingSource();
        BindingSource bindingSourceLZP = new BindingSource();
        int last_id_zakaza = 0;

        public Form4(Database database, Form1 form1)
        {
            InitializeComponent();
            this.database = database;
            this.form1 = form1;
        }

        void LoadData()
        {
            try
            {
                LoadCars();
                LoadZakazchiki();
                LoadZakazs();
                LoadMarks();
                LoadZakazchiks();
                LoadAllZakaz();
                LoadWorks();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void LoadMarks()
        {
            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = new SqlCommand("exec Load_Marks", database.GetConnection());
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                bindingSourceMM.DataSource = dataTable;
                comboBox3.DataSource = bindingSourceMM.DataSource;
                comboBox6.DataSource = bindingSourceMM.DataSource;
                comboBox3.DisplayMember = "Name";
                comboBox3.ValueMember = "mark";
                comboBox6.DisplayMember = "Name";
                comboBox6.ValueMember = "mark";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void LoadModels(string mark)
        {
            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = new SqlCommand("exec Load_Cars " + mark, database.GetConnection());
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                bindingSourceM.DataSource = dataTable;
                comboBox7.DataSource = bindingSourceM.DataSource;
                comboBox7.DisplayMember = "Name";
                comboBox7.ValueMember = "model";
                comboBox7.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void LoadZakazchiks()
        {
            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = new SqlCommand("exec Load_Zakazchik_name", database.GetConnection());
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                bindingSourceZks.DataSource = dataTable;
                comboBox2.DataSource = bindingSourceZks.DataSource;
                comboBox2.DisplayMember = "Name";
                comboBox2.ValueMember = "FIO";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void LoadCars()
        {
            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = new SqlCommand("exec Marks_Models", database.GetConnection());
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                bindingSourceC.DataSource = dataTable;
                dataGridView5.DataSource = bindingSourceC;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void LoadZakazchiki()
        {
            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = new SqlCommand("exec Zakazchiki", database.GetConnection());
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                bindingSourceZki.DataSource = dataTable;
                dataGridView4.DataSource = bindingSourceZki;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void LoadZakazs()
        {
            try
            {
                comboBox1.Items.Clear();
                SqlCommand sqlCommand = new SqlCommand("exec Set_num_Zakaz", database.GetConnection());
                try
                {
                    SqlDataReader readerP = sqlCommand.ExecuteReader();
                    List<string[]> dataP = new List<string[]>();
                    while (readerP.Read())
                    {
                        dataP.Add(new string[1]);
                        for (int i = 0; i < 1; i++)
                            dataP[dataP.Count - 1][i] = readerP[i].ToString();
                    }
                    readerP.Close();
                    foreach (string[] s in dataP)
                    {
                        comboBox1.Items.Add(s[0]);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void LoadAllZakaz()
        {
            try 
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = new SqlCommand("exec Zakaz_Table", database.GetConnection());
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                bindingSourceZ.DataSource = dataTable;
                dataGridView3.DataSource = bindingSourceZ;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void LoadWorks()
        {
            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = new SqlCommand("exec Load_Works", database.GetConnection());
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                bindingSourceW.DataSource = dataTable;
                comboBox4.DataSource = bindingSourceW.DataSource;
                comboBox4.DisplayMember = "name";
                comboBox4.ValueMember = "id_work";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void LoadWorkers(string work)
        {
            try
            {
                string sqlExpression = @"dbo.Load_Workers @name";
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, database.GetConnection());
                SqlParameter Param = new SqlParameter("@name", SqlDbType.VarChar, 100);
                Param.Value = work;
                sqlCommand.Parameters.Add(Param);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = sqlCommand;
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                bindingSourceW.DataSource = dataTable;
                comboBox5.DataSource = bindingSourceW.DataSource;
                comboBox5.DisplayMember = "Name";
                comboBox5.ValueMember = "FIO";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form4_Shown(object sender, EventArgs e)
        {
            LoadData();
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            database.CloseConnection();
            form1.Close();
        }


        void ZakazPrice(string zakaz)
        {
            SqlCommand sqlCommand = new SqlCommand("exec Zakaz_Price " + zakaz, database.GetConnection());
            try
            {
                SqlDataReader readerP = sqlCommand.ExecuteReader();
                List<string[]> dataP = new List<string[]>();
                while (readerP.Read())
                {
                    dataP.Add(new string[1]);
                    for (int i = 0; i < 1; i++)
                        dataP[dataP.Count - 1][i] = readerP[i].ToString();
                }
                readerP.Close();
                foreach (string[] s in dataP)
                {
                    textBox5.Clear();
                    textBox5.AppendText(s[0]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void LoadZakazPrice(string zakaz)
        {
            SqlCommand sqlCommand = new SqlCommand("exec Load_zakaz_price " + zakaz, database.GetConnection());
            try
            {
                SqlDataReader readerLZP = sqlCommand.ExecuteReader();
                List<string[]> dataLZP = new List<string[]>();
                while (readerLZP.Read())
                {
                    dataLZP.Add(new string[1]);
                    for (int i = 0; i < 1; i++)
                        dataLZP[dataLZP.Count - 1][i] = readerLZP[i].ToString();
                }
                readerLZP.Close();
                foreach (string[] s in dataLZP)
                {
                    textBox6.Clear();
                    textBox6.AppendText(s[0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) e.Handled = true;
        }


        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mark = comboBox3.Text;
            comboBox7.SelectedIndex = -1;
            LoadModels(mark);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string choice = comboBox1.Text;
            LoadZakazPrice(choice);
            int a = Int32.Parse(comboBox1.Text);
            bindingSourceZ.Filter = $"ID = '{a}'";
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string work = comboBox4.Text;
            LoadWorkers(work);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlExpression = @"dbo.Insert_new_zakazchik @fio, @telnum";
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, database.GetConnection());
                SqlParameter Paramfio = new SqlParameter("@fio", SqlDbType.VarChar, 80);
                Paramfio.Value = textBox1.Text;
                sqlCommand.Parameters.Add(Paramfio);
                SqlParameter Paramtelnum = new SqlParameter("@telnum", SqlDbType.VarChar, 12);
                Paramtelnum.Value = textBox2.Text;
                sqlCommand.Parameters.Add(Paramtelnum);
                int number = sqlCommand.ExecuteNonQuery();
                MessageBox.Show($"Добавлено объектов: {number}");
                textBox1.Clear();
                textBox2.Clear();
                LoadZakazchiki();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) e.Handled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try 
            { 
                string sqlExpression = @"dbo.Insert_new_model @mark, @model, @coef";
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, database.GetConnection());
                SqlParameter Parammark = new SqlParameter("@mark", SqlDbType.VarChar, 50);
                Parammark.Value = comboBox6.Text;
                sqlCommand.Parameters.Add(Parammark);
                SqlParameter Parammodel = new SqlParameter("@model", SqlDbType.VarChar, 50);
                Parammodel.Value = textBox7.Text;
                sqlCommand.Parameters.Add(Parammodel);
                SqlParameter Paramcoef = new SqlParameter("@coef", SqlDbType.Real);
                Paramcoef.Value = textBox8.Text;
                sqlCommand.Parameters.Add(Paramcoef);
                int number = sqlCommand.ExecuteNonQuery();
                MessageBox.Show($"Добавлено объектов: {number}");
                textBox7.Clear();
                textBox8.Clear();
                LoadCars();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try 
            { 
                string sqlExpression = @"dbo.Insert_new_car @mark, @model, @coef";
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, database.GetConnection());
                SqlParameter Parammark = new SqlParameter("@mark", SqlDbType.VarChar, 50);
                Parammark.Value = textBox4.Text;
                sqlCommand.Parameters.Add(Parammark);
                SqlParameter Parammodel = new SqlParameter("@model", SqlDbType.VarChar, 50);
                Parammodel.Value = textBox3.Text;
                sqlCommand.Parameters.Add(Parammodel);
                SqlParameter Paramcoef = new SqlParameter("@coef", SqlDbType.Real);
                Paramcoef.Value = textBox9.Text;
                sqlCommand.Parameters.Add(Paramcoef);
                int number = sqlCommand.ExecuteNonQuery();
                MessageBox.Show($"Добавлено объектов: {number}");
                textBox4.Clear();
                textBox3.Clear();
                textBox9.Clear();
                LoadCars();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
}

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlExpression = @"dbo.Insert_new_zakaz @zakazchik, @car, @date";
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, database.GetConnection());
                SqlParameter Paramzakazch = new SqlParameter("@zakazchik", SqlDbType.VarChar, 80);
                Paramzakazch.Value = comboBox2.Text;
                sqlCommand.Parameters.Add(Paramzakazch);
                SqlParameter Paramcar = new SqlParameter("@car", SqlDbType.VarChar, 50);
                Paramcar.Value = comboBox7.Text;
                sqlCommand.Parameters.Add(Paramcar);
                SqlParameter Paramdate = new SqlParameter("@date", SqlDbType.VarChar, 20);
                Paramdate.Value = DateTime.Now; 
                sqlCommand.Parameters.Add(Paramdate);
                comboBox7.SelectedIndex = -1;
                SqlDataReader id_zakaza = sqlCommand.ExecuteReader();
                id_zakaza.Read();
                last_id_zakaza = Int32.Parse(String.Format("{0}", id_zakaza[0]));
                MessageBox.Show($"ID: {last_id_zakaza}");
                id_zakaza.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
}

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlExpression = @"dbo.Insert_new_work @work, @worker, @numw, @comm, @idzakaza";
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, database.GetConnection());
                SqlParameter Paramwork = new SqlParameter("@work", SqlDbType.VarChar, 80);
                Paramwork.Value = comboBox4.Text;
                sqlCommand.Parameters.Add(Paramwork);
                SqlParameter Paramworker = new SqlParameter("@worker", SqlDbType.VarChar, 50);
                Paramworker.Value = comboBox5.Text;
                sqlCommand.Parameters.Add(Paramworker);
                SqlParameter Paramnumw = new SqlParameter("@numw", SqlDbType.VarChar, 50);
                Paramnumw.Value = Int32.Parse(textBox10.Text);
                sqlCommand.Parameters.Add(Paramnumw);
                SqlParameter Paramcomm = new SqlParameter("@comm", SqlDbType.VarChar, 50);
                Paramcomm.Value = textBox11.Text;
                sqlCommand.Parameters.Add(Paramcomm);
                SqlParameter Paramidz = new SqlParameter("@idzakaza", SqlDbType.VarChar, 50);
                Paramidz.Value = last_id_zakaza;
                sqlCommand.Parameters.Add(Paramidz);
                int number = sqlCommand.ExecuteNonQuery();
                MessageBox.Show($"Добавлено объектов: {number}");
                LoadZakazs();
                ZakazPrice(last_id_zakaza.ToString());
                LoadNewZakaz(last_id_zakaza.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void LoadNewZakaz(string id)
        {
            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = new SqlCommand("exec Load_new_zakaz_table " + id, database.GetConnection());
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                bindingSourceZ.DataSource = dataTable;
                dataGridView1.DataSource = bindingSourceZ;
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
                string sqlExpression = @"dbo.insert_estimated_date_price @date, @idz, @price";
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, database.GetConnection());
                SqlParameter Paramdate = new SqlParameter("@date", SqlDbType.VarChar, 20);
                Paramdate.Value = dateTimePicker2.Value.ToString("dd.MM.yyyy");
                sqlCommand.Parameters.Add(Paramdate);
                SqlParameter Paramidz = new SqlParameter("@idz", SqlDbType.Int);
                Paramidz.Value = last_id_zakaza;
                sqlCommand.Parameters.Add(Paramidz);
                SqlParameter Paramprice = new SqlParameter("@price", SqlDbType.Real);
                Paramprice.Value = Single.Parse(textBox5.Text); 
                sqlCommand.Parameters.Add(Paramprice);
                int number = sqlCommand.ExecuteNonQuery();
                MessageBox.Show($"Добавлено объектов: {number}");
                MessageBox.Show($"Добавлено объектов: {Int32.Parse(textBox5.Text)}");
                MessageBox.Show($"ID: {last_id_zakaza}");
                last_id_zakaza = 0;
                LoadZakazs();
                LoadAllZakaz();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
