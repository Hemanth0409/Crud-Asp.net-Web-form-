using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using System.Net.NetworkInformation;
using System.Xml.Linq;

namespace Crud__Asp.net_Web_form_
{

    public partial class Dynamic_Module : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-J6THV9C\\SQL2019EXP;Initial Catalog=Aspnet;Integrated Security=True");
        SqlCommand com;
        //ConnectFile demo; 
        protected void Page_Load(object sender, EventArgs e)
        {
            BindDataToGridView();
        }
        public void BindDataToGridView()
        {
            ModuleDetails(0, "", true, "");
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                ModuleData.DataSource = dt;
                ModuleData.DataBind();
            }
            ViewState["dt"] = dt;
            ViewState["sort"] = "ASC";
        }
        protected void RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow gdRow = (GridViewRow)ModuleData.Rows[e.RowIndex];

            HiddenField hdnId = (HiddenField)gdRow.FindControl("hdnId");

            con.Open();
            ModuleDetails(Convert.ToInt32(hdnId.Value), "", true, "DELETE");
            ModuleData.EditIndex = -1;
            BindDataToGridView();
            con.Close();
        }
        protected void ModuleGridView_RowCommand(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            HiddenField hdnId = (HiddenField)row.FindControl("hdnId");
            Session["ModuleId"] = hdnId.Value;
            Response.Redirect("~/AddColumn.aspx", false);
        }
        protected void EditButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            HiddenField hdnId = (HiddenField)row.FindControl("hdnId");
            Session["ModuleId"] = hdnId.Value;
            con.Open();
            SqlCommand comm = new SqlCommand("exec Sp_selectById @ModuleId='" + hdnId.Value + "'", con);
            SqlDataReader sqlDataReader = comm.ExecuteReader();
            while (sqlDataReader.Read())
            {
                TxtModule.Value = sqlDataReader.GetValue(1).ToString();
                string IsActiveCheck = sqlDataReader.GetValue(2).ToString();
                var check = IsActiveCheck == "True" ? CheckBox1.Checked = true : CheckBox1.Checked = false;
            }
            con.Close();
        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ModuleData.PageIndex = e.NewPageIndex;
            BindDataToGridView();
        }
        public string ModuleDetails(int ModuleId,
            string ModuleName,
            bool IsActive,
            string StatementType)
        {
            con.Open();
             com = new SqlCommand();

            com.Connection = con;
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "Sp_Module_Data";
            com.Parameters.Add("ModuleId", SqlDbType.Int).Value = ModuleId;
            com.Parameters.Add("ModuleName", SqlDbType.VarChar, 25).Value = ModuleName;
            com.Parameters.Add("IsActive", SqlDbType.Bit).Value = IsActive;
            com.Parameters.Add("StatementType", SqlDbType.VarChar, 25).Value = StatementType;
            com.CommandTimeout = 0;
            com.ExecuteNonQuery();            
            con.Close();
            return com.ToString();
        }

        protected void Create_Click(object sender, EventArgs e)
        {
            if (TxtModule.Value == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Module Name');", true);
            }
            else
            {
                if (Session["ModuleId"] == null)
                {
                    ModuleDetails(0, TxtModule.Value, CheckBox1.Checked ? true : false, "INSERT");                   
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Successfully Inserted');", true);
                }
                else if (Session["ModuleId"] != null)
                {
                    ModuleDetails(Convert.ToInt32(Session["ModuleId"]), TxtModule.Value, CheckBox1.Checked ? true : false, "UPDATE");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Successfully Updated');", true);
                }
                Reset_Click(sender, e);
                BindDataToGridView();
            }
        }
        protected void DeleteRecord(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            HiddenField hdnId = (HiddenField)row.FindControl("hdnId");
            ModuleDetails(Convert.ToInt32(hdnId.Value), "", true, "DELETE");
            ModuleData.EditIndex = -1;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Successfully Deleted');", true);
            BindDataToGridView();
        }

        protected void Reset_Click(object sender, EventArgs e)
        {
            Session["ModuleId"] = null;
            TxtModule.Value = string.Empty;
            CheckBox1.Checked = false;
        }
    }
}