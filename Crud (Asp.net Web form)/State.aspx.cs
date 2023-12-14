using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Crud__Asp.net_Web_form_
{
    public partial class State : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-DBQ88HK\\SQLEXPRESS2019;Initial Catalog=Aspnet;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCountry();
                BindDataToGridView();
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
        public void BindDataToGridView()
        {
            SqlCommand comm = new SqlCommand("exec SelectCountryList @CountryId='"+Convert.ToInt32(Txtcountry.SelectedIndex)+"'", con);
            SqlDataAdapter adapter = new SqlDataAdapter(comm);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Stategrid.DataSource = dt;
                Stategrid.DataBind();
            }
            ViewState["dt"] = dt;
            ViewState["sort"] = "ASC";
        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Stategrid.PageIndex = e.NewPageIndex;
            BindDataToGridView();
        }
        protected void RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //GridViewRow gdRow = (GridViewRow)Countrygrid.Rows[e.RowIndex];

            //HiddenField hdnId = (HiddenField)gdRow.FindControl("hdnId");

            //con.Open();
            //string sql = string.Format("exec DeleteData @id='" + hdnId.Value + "'");
            //SqlCommand comm = new SqlCommand(sql, con);
            //comm.ExecuteNonQuery();
            //Countrygrid.EditIndex = -1;
            //BindDataToGridView();
            //con.Close();
        }
        protected void EditButton_Click(object sender, EventArgs e)
        {
            //Button2.Visible = true;
            //Button1.Visible = false;
            //Button btn = (Button)sender;
            //GridViewRow row = (GridViewRow)btn.NamingContainer;
            //HiddenField hdnId = (HiddenField)row.FindControl("hdnId");
            //Session["Id"] = hdnId.Value;
            //con.Open();
            //SqlCommand comm = new SqlCommand("exec selectCountry @CountryId='" + hdnId.Value + "'", con);
            //SqlDataReader sqlDataReader = comm.ExecuteReader();
            //while (sqlDataReader.Read())
            //{
            //    InsertCountry.Value = sqlDataReader.GetValue(1).ToString();
            //}
            //con.Close();
            //BindDataToGridView();
        }
    }
}