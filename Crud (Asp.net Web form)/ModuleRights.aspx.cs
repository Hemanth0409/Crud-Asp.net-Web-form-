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
                LoadModuleRights();
            }
        }

        protected void PopulateGridView()
        {
            DataTable dt = GetEmployeeDetail();
            int uniqueId = 0;
            foreach (DataColumn column in dt.Columns)
            {
                if (column.ColumnName != "EmployeeID" && column.ColumnName != "EmployeeName")
                {
                    TemplateField field = new TemplateField();
                    field.HeaderText = column.ColumnName;
                    DynamicCheckBoxTemplate checkBoxTemplate = new DynamicCheckBoxTemplate(uniqueId++);

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

        public void LoadModuleRights()
        {
            DataTable moduleRightsData = GetEmployeeDetail();

            for (int i = 0; i < ModuleRightsGridView.Rows.Count; i++)
            {
                GridViewRow row = ModuleRightsGridView.Rows[i];

                for (int j = 1; j < ModuleRightsGridView.Columns.Count; j++)
                {
                    string columnName = ModuleRightsGridView.Columns[j].HeaderText;
                    CheckBox ModuleRightsValue = (CheckBox)ModuleRightsGridView.Rows[i].FindControl("chk_" + (j-1));

                    if (ModuleRightsValue != null)
                    {
                        int moduleRightValue = Convert.ToInt32(moduleRightsData.Rows[i][columnName]);
                        ModuleRightsValue.Checked = moduleRightValue == 1;
                    }
                }
            }
        }

        public DataTable GetModuleRights(string ModuleName)
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "Sp_GetModuleRights";
            com.Parameters.Add("ModuleName", SqlDbType.NVarChar).Value = ModuleName;
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
                string columnName = checkbox.ColumnName;
                int rowIndex = checkbox.RowIndex;
                int employeeId = Convert.ToInt32(((HiddenField)ModuleRightsGridView.Rows[rowIndex-1].FindControl("hdnId")).Value);
                ModuleRightsSql(columnName, employeeId, true, 10, "UPDATE");
            }


            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Module Rights Successfully Updated');", true);
        }
    }

    public class DynamicCheckBoxTemplate : ITemplate
    {
        private int columnIndex;

        public DynamicCheckBoxTemplate(int columnIndex)
        {
            this.columnIndex = columnIndex;
        }

        public void InstantiateIn(Control container)
        {
            CheckBox checkBox = new CheckBox();
            checkBox.ID = "chk_" + columnIndex;
            container.Controls.Add(checkBox);
        }
    }

    public class CheckedCheckbox
    {
        public string ColumnName { get; set; }
        public int RowIndex { get; set; }
    }
}
