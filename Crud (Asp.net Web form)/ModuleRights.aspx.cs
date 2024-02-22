using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Crud__Asp.net_Web_form_
{
    public class DynamicCheckBoxTemplate : ITemplate
    {
        private string checkboxId;
        private string columnName;

        public DynamicCheckBoxTemplate(int  checkboxId,string columnName)
        {
            this.checkboxId = checkboxId.ToString();
           this.columnName = columnName;
        }

        public void InstantiateIn(Control container)
        {
            CheckBox checkBox = new CheckBox();
            checkBox.ID = "chk_" + checkboxId +columnName.Replace(" ", "_"); 
            checkBox.DataBinding += CheckBox_DataBinding;
            checkBox.EnableViewState = true;
            container.Controls.Add(checkBox);
        }

        private void CheckBox_DataBinding(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            GridViewRow container = (GridViewRow)checkBox.NamingContainer;
            DataRowView dataItem = (DataRowView)container.DataItem;
            if (dataItem != null)
            {
                checkBox.Checked = Convert.ToBoolean(DataBinder.Eval(container.DataItem, columnName));
            }
        }
    }

    public partial class ModuleRights : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-J6THV9C\\SQL2019EXP;Initial Catalog=Aspnet;Integrated Security=True");
        SqlCommand com;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PopulateGridView();
            }
        }


        private int checkboxIdCounter = 1;

        protected void PopulateGridView()
        {
            DataTable dt = GetEmployeeDetail();
            foreach (DataColumn column in dt.Columns)
            {
                if (column.ColumnName != "EmployeeID" && column.ColumnName != "EmployeeName")
                {
                    TemplateField field = new TemplateField();
                    field.HeaderText = column.ColumnName;
                    field.ItemTemplate = new DynamicCheckBoxTemplate(checkboxIdCounter++,column.ColumnName); // Pass the counter as ID
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
            for (int i = 1; i < ModuleRightsGridView.Columns.Count; i++)
            {
                string moduleName = ModuleRightsGridView.Columns[i].HeaderText;
                for (int j = 0; j < ModuleRightsGridView.Rows.Count; j++)
                {
                    int empId = Convert.ToInt32(((HiddenField)ModuleRightsGridView.Rows[j].FindControl("hdnId")).Value);
                    string checkBoxId = "chk_" +i + moduleName.Replace(" ", "_"); 
                    CheckBox checkBoxValue = (CheckBox)ModuleRightsGridView.Rows[j].FindControl(checkBoxId);
                    if (checkBoxValue != null)
                    {
                        bool moduleRight = checkBoxValue.Checked;
                        ModuleRightsSql(moduleName, empId, moduleRight, 10, "UPDATE");
                    }
                }
            }
        }

    }
}
