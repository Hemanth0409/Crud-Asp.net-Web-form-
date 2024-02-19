using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Crud__Asp.net_Web_form_
{
  
    public partial class ModuleRights : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-DBQ88HK\\SQLEXPRESS2019;Initial Catalog=Aspnet;Integrated Security=True");
        SqlCommand com;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PopulateGridView();
            }
        }

        protected void PopulateGridView()
        {
            DataTable dt = GetEmployeeDetail();
            foreach (DataColumn column in dt.Columns)
            {
                if (column.ColumnName != "EmployeeID" && column.ColumnName != "EmployeeName")
                {
                    TemplateField field = new TemplateField();
                    field.HeaderText = column.ColumnName;
                    field.ItemTemplate = new DynamicCheckBoxTemplate(column.ColumnName);
                    ModuleRightsGridView.Columns.Add(field);
                }
            }
            ModuleRightsGridView.DataSource = dt;
            ModuleRightsGridView.DataBind();
        }


        public DataTable GetEmployeeDetail()
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Sp_ModuleRightsDisplayData";

            SqlParameter dynamicSQLParam = cmd.Parameters.Add("@DynamicSQL", SqlDbType.NVarChar, 4000);
            dynamicSQLParam.Direction = ParameterDirection.Output;

            SqlParameter columnNamesParam = cmd.Parameters.Add("@ColumnNames", SqlDbType.NVarChar, 4000);
            columnNamesParam.Direction = ParameterDirection.Output;

            adapter.SelectCommand = cmd;
            adapter.Fill(dt);
            return dt;
        }
        public DataTable GetModuleRights()
        {
            con.Open();
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "Sp_GetModuleRights";
            com.CommandTimeout = 0;
            com.ExecuteNonQuery();
            con.Close();
            return GetDataTable(com);
        }
        public DataTable GetDataTable(SqlCommand objCom)
        {
            objCom.Connection = con;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = objCom;
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            return dataTable;
        }
        public string ModuleRightsSql(
            string ModuleName,
            int EmployeeId,
            bool ModuleRight,
            int CreatedById,
            string statementType
            )
        {
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "Sp_ModuleRightsForEmployee";
            com.Parameters.Add("ModuleName", SqlDbType.NVarChar).Value = ModuleName;
            com.Parameters.Add("EmployeeId", SqlDbType.Int).Value = EmployeeId;
            com.Parameters.Add("ModuleRight", SqlDbType.Bit).Value = ModuleRight;
            com.Parameters.Add("CreatedById", SqlDbType.Int).Value = CreatedById;
            com.Parameters.Add("StatementType", SqlDbType.VarChar, 25).Value = statementType;
            com.CommandTimeout = 0;
            com.ExecuteNonQuery();
            return com.ToString();
        }
        protected void SaveModuleRights(object sender, EventArgs e)
        {
            for (int i = 1; i < ModuleRightsGridView.Rows.Count; i++)
            {
                GridViewRow row = ModuleRightsGridView.Rows[i];
                int employeeId = Convert.ToInt32(((HiddenField)row.FindControl("hdnId")).Value);
                for (int j = 1; j < ModuleRightsGridView.Columns.Count; j++)
                {
                    string moduleName = ModuleRightsGridView.Columns[j].HeaderText;
                    string checkboxId = "ModuleRightsGridView_chk" + moduleName+"_"+(i-1);
                    CheckBox checkBox = (CheckBox)row.FindControl(checkboxId);
                    if (checkBox != null) 
                    {
                        bool moduleRight = checkBox.Checked;
                        ModuleRightsSql(moduleName, employeeId, moduleRight, 10, "UPDATE");
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Module rights saved successfully.');", true);
            //PopulateGridView();
        }
    }
}