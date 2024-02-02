using Microsoft.AspNet.FriendlyUrls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Crud__Asp.net_Web_form_
{
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-DBQ88HK\\SQLEXPRESS2019;Initial Catalog=Aspnet;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Exec LoginProcedureAccess @userName='" + UserNameCheck.Value + "',@Password='" + PassswordCheck.Value + "'", conn);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Logged in Sucessfully');window.location ='Index.aspx';",
                true);

            }
            else
            {
                Label4.Text = ("Login Failed");
            }
            conn.Close();

            int UserId= LoginCheck(UserNameCheck.Value, PassswordCheck.Value);
            if (UserId != 0)
            {
                Session["CurrentUserId"]= UserId;
            }
        }

        public int LoginCheck(string UserName,string Password)
        {
            conn.Open();
            SqlCommand com = new SqlCommand();

            com.Connection = conn;
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "Sp_LoginProcedure";
            com.Parameters.Add("UserName", SqlDbType.VarChar,25).Value = UserName;
            com.Parameters.Add("Password", SqlDbType.VarChar,25).Value = Password;
            com.Parameters.Add("UserId", SqlDbType.Int).Direction = ParameterDirection.Output;
            com.ExecuteNonQuery();           
            conn.Close();
            return Convert.ToInt32(com.Parameters["UserId"].Value);

        }
    }
}