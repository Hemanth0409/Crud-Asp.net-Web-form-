using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace Crud__Asp.net_Web_form_
{
    public partial class ModuleRights : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-J6THV9C\\SQL2019EXP;Initial Catalog=Aspnet;Integrated Security=True");

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
            //int uniqueId = 0;
            foreach (DataColumn column in dt.Columns)
            {
                if (column.ColumnName != "EmployeeID" && column.ColumnName != "EmployeeName")
                {
                    TemplateField field = new TemplateField();
                    field.HeaderText = column.ColumnName;
                    DynamicCheckBoxTemplate checkBoxTemplate = new DynamicCheckBoxTemplate(column.ColumnName);

                    field.ItemTemplate = checkBoxTemplate;
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



        public DataTable GetModuleRights(string ModuleName)
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "Sp_GetModuleRights";
            com.Parameters.Add("ModuleName", SqlDbType.NVarChar).Value = ModuleName;
            return GetDataTable(com);

        }
        public bool ModuleRightsCheck(string ModuleName, int EmployeeId)
        {
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "Sp_CheckModuleRightsForEmployee";
            com.Parameters.Add("ModuleName", SqlDbType.NVarChar).Value = ModuleName;
            com.Parameters.Add("EmployeeId", SqlDbType.Int).Value = EmployeeId;
            com.Parameters.Add("RightsCheck", SqlDbType.Bit).Direction = ParameterDirection.Output;
            com.ExecuteNonQuery();
            return Convert.ToBoolean(com.Parameters["RightsCheck"].Value);
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

        public string ModuleRightsSql(string ModuleName, int EmployeeId, bool ModuleRight, int CreatedById, string statementType)
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

        protected void SaveButton_Click(object sender, EventArgs e)
        {

            string checkedCheckboxesJson = CheckedCheckboxesHiddenField.Value;
            List<CheckedCheckbox> checkedCheckboxes = JsonConvert.DeserializeObject<List<CheckedCheckbox>>(checkedCheckboxesJson);

            foreach (var checkbox in checkedCheckboxes)
            {
                string moduleName = checkbox.ColumnName;
                int rowIndex = checkbox.RowIndex;
                string checkBoxValue = checkbox.CheckBoxValue;

                int employeeId = Convert.ToInt32(((HiddenField)ModuleRightsGridView.Rows[rowIndex - 1].FindControl("hdnId")).Value);
                con.Open();
                bool RightsCheckValue = ModuleRightsCheck(moduleName, employeeId);
                if (checkBoxValue == "true")
                {
                    ModuleRightsSql(moduleName, employeeId, true, 10, RightsCheckValue ? "UPDATE" : "INSERT");
                }
                else if (checkBoxValue == "false")
                {
                    ModuleRightsSql(moduleName, employeeId, false, 10, "UPDATE");
                }
                con.Close();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Module Rights Successfully Updated');", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "setTimeout(function(){ window.location.href = 'ModuleRights.aspx'; },00);", true);
        }
    }

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
            checkBox.ID = "chk" + columnName;
            checkBox.DataBinding += CheckBox_DataBinding;
            container.Controls.Add(checkBox);
        }
        private void CheckBox_DataBinding(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            GridViewRow container = (GridViewRow)checkBox.NamingContainer;
            checkBox.Checked = Convert.ToBoolean(DataBinder.Eval(container.DataItem, columnName));
        }
    }
    public class CheckedCheckbox
    {
        public string ColumnName { get; set; }
        public int RowIndex { get; set; }
        public string CheckBoxValue { get; set; }
    }
}
