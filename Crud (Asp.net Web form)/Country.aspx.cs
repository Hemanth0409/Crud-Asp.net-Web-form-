using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Crud__Asp.net_Web_form_
{
    public partial class Country : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-DBQ88HK\\SQLEXPRESS2019;Initial Catalog=Aspnet;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindCountry();
            }
        }

        public void BindCountry()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("exec DisplayCountry", con);
            SqlDataReader reader = cmd.ExecuteReader();
            Txtcountry.DataSource = reader;
            Txtcountry.Items.Clear();
            Txtcountry.Items.Add("Select a Country");
            Txtcountry.DataTextField = "CountryName";
            Txtcountry.DataValueField = "CountryId";
            Txtcountry.DataBind();
            con.Close();
        }
        public void InsertCountry_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand InsertCom = new SqlCommand("Exec InsertCountry @countryName='" + InsertCountry.Value + "'", con);
            InsertCom.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Country Inserted  Successfully');", true);
            InsertCountry.Value = string.Empty;
            Page_Load(sender, e);
            BindCountry();
        }
        public void InsertState_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand InsertCom = new SqlCommand("Exec InsertState @StateName='" + InsertState.Value + "',@countryId='" + Convert.ToInt32(Txtcountry.SelectedIndex) + "'", con);
            InsertCom.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('State Inserted  Successfully');", true);
            InsertState.Value = string.Empty;
            BindCountry();
        }
    }
}