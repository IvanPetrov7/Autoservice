using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BD_Rabotaet
{
    public class Database
    {
        private SqlConnectionStringBuilder sqlConnectionBuilder = new SqlConnectionStringBuilder();

        private SqlConnection sqlConnection = new SqlConnection();
        public void InitializeConnectionString(string User_ID = null, string Password = null)
        {
            if(User_ID !=null && Password != null)
            {
                sqlConnectionBuilder.UserID = User_ID;
                sqlConnectionBuilder.Password = Password;
            }
            sqlConnectionBuilder.DataSource = "localhost";
            sqlConnectionBuilder.InitialCatalog = "autoceh";
            sqlConnectionBuilder.Encrypt = true;
            sqlConnectionBuilder.TrustServerCertificate = true;
            sqlConnectionBuilder.PersistSecurityInfo = false;

            sqlConnection.ConnectionString = sqlConnectionBuilder.ConnectionString;
        }
        public void OpenConnection()
        {
            if(sqlConnection.ConnectionString == null) 
                return;
            
            if(sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }

        public void CloseConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        public SqlConnection GetConnection()
        {
            return sqlConnection;
        }

    }
}
