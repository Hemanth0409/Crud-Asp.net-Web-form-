using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Crud__Asp.net_Web_form_
{
    public class DynamicCheckBoxTemplate : ITemplate
    {
        private string columnName;

        public DynamicCheckBoxTemplate(string columnName)
        {
            this.columnName = columnName;
        }
        public void InstantiateIn(Control container)
        {
            CheckBox checkBox = new CheckBox();
            checkBox.ID += "chk_" + columnName;
            checkBox.DataBinding += CheckBox_DataBinding;
            checkBox.EnableViewState = true;
            container.Controls.Add(checkBox);
        }
        private void CheckBox_DataBinding(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            GridViewRow container = (GridViewRow)checkBox.NamingContainer;
            checkBox.Checked = Convert.ToBoolean(DataBinder.Eval(container.DataItem, columnName));
        }
    }
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
            foreach (GridViewRow row in ModuleRightsGridView.Rows)
            {
                int employeeId = Convert.ToInt32(((HiddenField)row.FindControl("hdnId")).Value);

                // Loop through each cell in the row
                for (int j = 0; j < row.Cells.Count; j++)
                {
                    // Find the checkbox control in the cell
                    CheckBox checkBox = row.Cells[j].Controls.OfType<CheckBox>().FirstOrDefault();

                    if (checkBox != null)
                    {
                        bool moduleRight = checkBox.Checked;
                        string moduleName = ModuleRightsGridView.Columns[j].HeaderText;

                        // Call your SQL method to save module rights here
                        // ModuleRightsSql(moduleName, employeeId, moduleRight, 10, "UPDATE");
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Module rights saved successfully.');", true);
        }

    }
}
