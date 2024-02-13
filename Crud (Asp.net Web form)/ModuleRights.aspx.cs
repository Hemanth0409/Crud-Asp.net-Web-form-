using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Crud__Asp.net_Web_form_
{

    public partial class ModuleRights : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-DBQ88HK\\SQLEXPRESS2019;Initial Catalog=Aspnet;Integrated Security=True");      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataTable dt = GetEmployeeDetail();

                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ColumnName != "EmployeeID" && column.ColumnName != "EmployeeName")
                    {
                        ModuleRightsGridView.Columns.Add(new BoundField { DataField = column.ColumnName, HeaderText = column.ColumnName });
                    }
                }
                ModuleRightsGridView.DataSource = dt;
                ModuleRightsGridView.DataBind();
            }
        }

        public DataTable GetEmployeeDetail()
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Sp_ModuleRightsDisplayData";

            SqlParameter dynamicSQLParam = cmd.Parameters.Add("@DynamicSQL", SqlDbType.NVarChar,4000);
            dynamicSQLParam.Direction = ParameterDirection.Output;

            SqlParameter columnNamesParam = cmd.Parameters.Add("@ColumnNames", SqlDbType.NVarChar,4000);
            columnNamesParam.Direction = ParameterDirection.Output;
                        
            adapter.SelectCommand = cmd;
            adapter.Fill(dt);

            return dt;
        }
        protected string AddCheckbox(int rights)
        {
            if (rights == 1)
            {
                return "<asp:CheckBox runat='server' Checked='true' />";
            }
            else
            {
                return "<asp:CheckBox runat='server' />";
            }
        }

    }
}