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
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-DBQ88HK\\SQLEXPRESS2019;Initial Catalog=Aspnet;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string ModuleDetails(int ModuleId, string ModuleName, bool IsActive, string StatementType)
        {
            SqlCommand com = new SqlCommand();

            com.Connection = con;
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "Sp_Module_Data";
            com.Parameters.Add("ModuleId", SqlDbType.Int).Value = ModuleId;
            com.Parameters.Add("ModuleName", SqlDbType.VarChar, 25).Value = ModuleName;            
            com.Parameters.Add("IsActive", SqlDbType.Bit).Value = IsActive;
            com.Parameters.Add("StatementType", SqlDbType.VarChar, 25).Value = StatementType;
            com.CommandTimeout = 0;
            com.ExecuteNonQuery();
            return com.ToString();
        }
        protected void Create_Click(object sender, EventArgs e)
        {
            if (Session["Id"] != null)
            {
                con.Open();
                if (CheckBox1.Checked == true)
                {
                    ModuleDetails(Convert.ToInt32(Session["Id"]), TxtModule.Value, true, "UPDATE");
                }
                else
                {
                    ModuleDetails(Convert.ToInt32(Session["Id"]), TxtModule.Value, false, "UPDATE");

                }
                con.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Successfully Updated');", true);
              
            }
            else
            {
                con.Open();
                if (CheckBox1.Checked == true)
                {
                    ModuleDetails(0, TxtModule.Value, true, "INSERTE");
                }
                else
                {
                    ModuleDetails(0, TxtModule.Value, false, "INSERT");

                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Successfully Inserted');", true);
            }
        }
        protected void Reset_Click(object sender, EventArgs e)
        {

        }
    }
}