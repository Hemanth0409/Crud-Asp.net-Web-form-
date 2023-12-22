using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Crud__Asp.net_Web_form_
{
    public partial class Country : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-DBQ88HK\\SQLEXPRESS2019;Initial Catalog=Aspnet;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindDataToGridView();
            }
        }
        public void InsertCountry_Click(object sender, EventArgs e)
        {         
                Button2.Visible = false;
                Button1.Visible = true;
                con.Open();
                SqlCommand InsertCom = new SqlCommand("Exec InsertCountry @countryName='" + InsertCountry.Value + "'", con);
                InsertCom.ExecuteNonQuery();
                con.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Country Inserted  Successfully');", true);
                InsertCountry.Value = string.Empty;
                BindDataToGridView();         
        }
        public void BindDataToGridView()
        {
            SqlCommand comm = new SqlCommand("exec DisplayCountry ", con);
            SqlDataAdapter adapter = new SqlDataAdapter(comm);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Countrygrid.DataSource = dt;
                Countrygrid.DataBind();
            }
            ViewState["dt"] = dt;
            ViewState["sort"] = "ASC";
        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Countrygrid.PageIndex = e.NewPageIndex;
            BindDataToGridView();
        }
        protected void RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow gdRow = (GridViewRow)Countrygrid.Rows[e.RowIndex];

            HiddenField hdnId = (HiddenField)gdRow.FindControl("hdnId");

            con.Open();
            string sql = string.Format("exec [deleteCountry] @Countryid='" + hdnId.Value + "'");
            SqlCommand comm = new SqlCommand(sql, con);
            comm.ExecuteNonQuery();
            Countrygrid.EditIndex = -1;
            BindDataToGridView();
            con.Close();
        }
        protected void EditButton_Click(object sender, EventArgs e)
        {
            Button2.Visible = true;
            Button1.Visible = false;
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            HiddenField hdnId = (HiddenField)row.FindControl("hdnId");
            Session["Id"] = hdnId.Value;
            con.Open();
            SqlCommand comm = new SqlCommand("exec selectCountry @CountryId='" + hdnId.Value + "'", con);
            SqlDataReader sqlDataReader = comm.ExecuteReader();
            while (sqlDataReader.Read())
            {
                InsertCountry.Value = sqlDataReader.GetValue(1).ToString();
            }
            con.Close();
            BindDataToGridView();
        }
        protected void UpdateCountry_Click(object sender, EventArgs e) {
            con.Open();
            SqlCommand updatecom = new SqlCommand("exec EditCountry  @CountryId='" + Session["Id"] + "', @Countryname='" + InsertCountry.Value + "'", con);
            updatecom.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Successfully Updated');", true);
            BindDataToGridView();
        }
        protected void ClearCountry_Click(object sender, EventArgs e)
        {
            Button1.Visible = true;
            Button2.Visible = false;
            Session["Id"] = null;
            InsertCountry.Value = string.Empty;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "windows.Reload();", true);
        }
        protected void Search_Click(object sender, EventArgs e)
        {
            if (searchText.Value != "")
            {
                string searchQuery = "SELECT * FROM Country where CountryName LIKE '%" + searchText.Value + "'";
                SqlCommand comm = new SqlCommand(searchQuery, con);
                SqlDataAdapter adapter = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                Countrygrid.DataSource = dt;
                Countrygrid.DataBind();

            }
            else
            {
                BindDataToGridView();
            }

        }
        protected void ResetSearch_Click(object sender, EventArgs e)
        {
            searchText.Value = string.Empty;
            BindDataToGridView();
        }

    }
}