using Microsoft.AspNet.FriendlyUrls;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
            SqlCommand cmd = new SqlCommand("Exec LoginProcedure @userName='" + UserNameCheck.Value + "',@Password='" + PassswordCheck.Value + "'", conn);
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
            //name email password username
        }
    }
}