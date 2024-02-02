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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            CreateGridViewColumns();
        }
        static int currentUserId;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                currentUserId = Convert.ToInt32(Session["CurrentUserId"]);
                LoadClients();
                LoadModuleRights();
            }

        }

        public void CreateGridViewColumns()
        {
            DataTable ModuleDataTable;
            for (int i = ModuleRightsGridView.Columns.Count - 1; i >= 0; i--)
            {
                if (ModuleRightsGridView.Columns[i].HeaderText == "Employees")
                    continue;
                else
                    ModuleRightsGridView.Columns.RemoveAt(i);
            }
            ModuleDataTable = GetAllModule();

            int iWidth = (ModuleDataTable.Rows.Count * 100);
            ModuleRightsGridView.Width = iWidth;

            if (iWidth < 600)
            {
                ModuleRightsGridView.Width = 600;
                ModuleRightsGridView.CssClass = "table table-striped";
            }
            for (int i = 0; i < ModuleDataTable.Rows.Count; i++)
            {
                TemplateField objTemplateField = new TemplateField();
                objTemplateField.HeaderText = Convert.ToString(ModuleDataTable.Rows[i]["ModuleName"]);
                objTemplateField.FooterText = Convert.ToString(ModuleDataTable.Rows[i]["ModuleId"]);
                objTemplateField.ItemTemplate = new GridViewCheckBoxClass(i.ToString());
                objTemplateField.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                objTemplateField.ItemStyle.Width = 150;
                ModuleRightsGridView.Columns.Add(objTemplateField);
            }
            ModuleCount.Value = ModuleDataTable.Rows.Count.ToString();

        }
        private void LoadClients()
        {
            DataTable EmployeeDetailsDt;
            EmployeeDetailsDt = GetEmployeeDetail();
            ModuleRightsGridView.DataSource = EmployeeDetailsDt;
            ModuleRightsGridView.DataBind();
            EmpCount.Value = EmployeeDetailsDt.Rows.Count.ToString();
        }

        public DataTable GetEmployeeDetail()
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Sp_SelectEmployeeDetails";
            adapter.SelectCommand = cmd;
            adapter.Fill(dt);
            return dt;
        }
        private void LoadModuleRights()
        {
            DataTable ModuleRightsDataTable;
            DataTable objDataTable;

            ModuleRightsDataTable = GetModuleRights();

            for (int i = 1; i < ModuleRightsGridView.Columns.Count; i++)
            {
                int iModuleCode = Convert.ToInt32(ModuleRightsGridView.Columns[i].FooterText);
                for (int j = 0; j < ModuleRightsGridView.Rows.Count; j++)
                {
                    string employeeCode = ((HiddenField)ModuleRightsGridView.Rows[j].FindControl("hndEmpId")).Value;
                    DataView RightsView = new DataView(ModuleRightsDataTable);
                    RightsView.RowFilter = "EmployeeId=" + employeeCode.ToString() + "AND ModuleId=" + iModuleCode;

                    if (RightsView.Count > 0)
                    {
                        Boolean boolRight = Convert.ToBoolean(RightsView[0]["ModuleRight"]);
                        CheckBox ModuleRightsCheckBox = (CheckBox)ModuleRightsGridView.Rows[j].FindControl("ModuleCheckBox" + (i - 1));
                        if (ModuleRightsCheckBox != null)
                        {
                            ModuleRightsCheckBox.Checked = boolRight;
                        }
                    }
                }
            }
            for (int iModule = 1; iModule > 0; iModule--)
            {
                for (int j = 0; j < ModuleRightsGridView.Rows.Count; j++)
                {
                    int iEmployeeCode = Convert.ToInt32(((HiddenField)ModuleRightsGridView.Rows[j].FindControl("hndEmpId")).Value);
                    CheckBox ModuleRightsCheckBox = (CheckBox)ModuleRightsGridView.Rows[j].FindControl("ModuleCheckBox" + (ModuleRightsGridView.Columns.Count - 1));
                    if (ModuleRightsCheckBox != null)
                    {
                        ModuleRightsCheckBox.Checked = false;
                    }
                    objDataTable = GetEmployeeDetailById(iEmployeeCode);
                    if (objDataTable.Rows.Count > 0)
                    {
                        ModuleRightsDataTable = new DataTable();
                        if (ModuleRightsDataTable.Rows.Count > 0)
                        {
                            ModuleRightsCheckBox = (CheckBox)ModuleRightsGridView.Rows[j].FindControl("ModuleCheckBox" + (ModuleRightsGridView.Columns.Count - iModule));
                            if (ModuleRightsDataTable != null)
                            {
                                ModuleRightsCheckBox.Checked = true;
                            }
                        }
                    }
                }
            }
        }
        public DataTable GetEmployeeDetailById(int EmployeeId)
        {
            SqlCommand com = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            com.Connection = con;
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "SelectEmployeeById";
            com.Parameters.Add("@id", SqlDbType.Int).Value = EmployeeId;
            DataTable dataTable = new DataTable();
            adapter.SelectCommand = com;
            adapter.Fill(dataTable);
            return dataTable;
        }
        public DataTable GetAllModule()
        {
            con.Open();
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "Sp_GetAllModuleData";
            com.CommandTimeout = 0;
            com.ExecuteNonQuery();
            return GetDataTable(com);
        }
        public DataTable GetModuleRights()
        {
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "Sp_GetModuleRights";
            com.CommandTimeout = 0;
            com.ExecuteNonQuery();
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
        public string ModuleRightsSql(int EmpModuleRightsId,
            int ModuleId,
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
            com.Parameters.Add("EmpModuleRightsId", SqlDbType.Int).Value = EmpModuleRightsId;
            com.Parameters.Add("ModuleId", SqlDbType.Int).Value = ModuleId;
            com.Parameters.Add("EmployeeId", SqlDbType.Int).Value = EmployeeId;
            com.Parameters.Add("ModuleRight", SqlDbType.Bit).Value = ModuleRight;
            com.Parameters.Add("CreatedById", SqlDbType.Int).Value = CreatedById;
            com.Parameters.Add("StatementType", SqlDbType.VarChar, 25).Value = statementType;
            com.CommandTimeout = 0;
            com.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                ModuleRightsGridView.DataSource = dt;
                ModuleRightsGridView.DataBind();
            }
            ViewState["dt"] = dt;
            ViewState["sort"] = "ASC";

            return com.ToString();
        }


        public bool ModuleRightsCheck(int ModuleId, int EmployeeId)
        {

            SqlCommand com = new SqlCommand();

            com.Connection = con;
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "Sp_CheckModuleRightsForEmployee";
            com.Parameters.Add("ModuleId", SqlDbType.Int).Value = ModuleId;
            com.Parameters.Add("EmployeeId", SqlDbType.Int).Value = EmployeeId;
            com.Parameters.Add("RightsCheck", SqlDbType.Bit).Direction = ParameterDirection.Output;
            com.ExecuteNonQuery();
            return Convert.ToBoolean(com.Parameters["RightsCheck"].Value);
        }
        public void SaveModuleRights(object sender, EventArgs e)
        {
            DataTable ModuleRightsDataTable;
            ModuleRightsDataTable = GetModuleRights();
            for (int i = 1; i < ModuleRightsGridView.Columns.Count; i++)
            {
                int ModuleIdCode = Convert.ToInt32(ModuleRightsGridView.Columns[i].FooterText);
                for (int j = 1; j < ModuleRightsGridView.Rows.Count; j++)
                {
                    int EmployeeCode = Convert.ToInt32(((HiddenField)ModuleRightsGridView.Rows[j].FindControl("hndEmpId")).Value);

                    DataView RightsView = new DataView(ModuleRightsDataTable);
                    RightsView.RowFilter = "EmployeeId=" + EmployeeCode + "AND ModuleId=" + ModuleIdCode;
                    CheckBox ModuleRightsCheckBox = (CheckBox)ModuleRightsGridView.Rows[j].FindControl("ModuleCheckBox" + (i - 1));

                    if (RightsView.Count > 0)
                    {
                        Boolean boolRights = Convert.ToBoolean(RightsView[0]["ModuleRight"]);
                        if (ModuleRightsCheckBox != null)
                        {
                            if (ModuleRightsCheckBox.Checked != boolRights)
                            {
                                bool RightsCheckValue = ModuleRightsCheck(ModuleIdCode, EmployeeCode);
                                if (RightsCheckValue)
                                {
                                    ModuleRightsSql(0, ModuleIdCode, EmployeeCode, ModuleRightsCheckBox.Checked, currentUserId, "UPDATE");

                                }
                            }
                        }
                    }
                    else
                    {
                        ModuleRightsSql(0, ModuleIdCode, EmployeeCode, ModuleRightsCheckBox.Checked, currentUserId, "INSERT");
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Successfully Updated');", true);
                }
            }
        }
    }
}