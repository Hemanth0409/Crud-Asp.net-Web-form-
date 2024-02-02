using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


    public class DataAccess
    {

        private string empConnectionString;
        private SqlConnection con;



        public DataAccess()
        {
            empConnectionString = ConfigurationManager.ConnectionStrings["EmpConnectionString"].ConnectionString;
            con = new SqlConnection(empConnectionString);

        }
        public string GetColumnById(int ModuleId)
        {
            SqlCommand com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "Sp_GetAllColumnDataById";
            com.Parameters.Add("ModuleId", SqlDbType.Int).Value = ModuleId;
            com.CommandTimeout = 0;      
            com.ExecuteNonQuery();
            con.Close();
            return com.ExecuteScalar()?.ToString();
        }
    }
