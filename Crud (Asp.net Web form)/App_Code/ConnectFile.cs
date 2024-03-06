using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Crud__Asp.net_Web_form_
{
    public class ConnectFile
    {
        private string sConnectionString;
        private SqlConnection objSqlConnection;

        public ConnectFile()
        {
            sConnectionString = ConfigurationManager.ConnectionStrings["EmpConnectionString"].ConnectionString;
            objSqlConnection = new SqlConnection(sConnectionString);
        }
    }
}