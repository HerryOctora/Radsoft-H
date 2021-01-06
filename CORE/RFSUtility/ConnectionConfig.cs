using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace RFSUtility
{
    public class ConnectionConfig
    {
        public SqlConnection Con;
        public ConnectionConfig()
        {
            Con = new SqlConnection();
            //Con.ConnectionString = "Data Source=192.168.1.13;Initial Catalog=BO;Persist Security Info=True;User ID=sa;Password=Rahasia123";
           // Con.ConnectionString = "Data Source=.;Initial Catalog=RCORE_V3;Persist Security Info=True;User ID=sa;Password=as";
        }
        public void OpenConnection()
        {
            if (Con.State == ConnectionState.Closed)
                Con.Open();
        }

        public void CloseConnection()
        {
            if (Con.State == ConnectionState.Open)
                Con.Close();
        }
    }
}