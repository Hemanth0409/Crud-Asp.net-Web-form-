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
    public class GridViewCheckBoxClass : ITemplate
    {
        string sControlId;

        public GridViewCheckBoxClass(string _sControlId)
        {
            sControlId = _sControlId;
        }

        #region ITemplate Members

        public void InstantiateIn(Control container)
        {
            CheckBox objCheckBox = new CheckBox();
            objCheckBox.ID = "ModuleCheckBox" + sControlId;
            container.Controls.Add(objCheckBox);
        }

        #endregion
    }
    public partial class ModuleRights : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-DBQ88HK\\SQLEXPRESS2019;Initial Catalog=Aspnet;Integrated Security=True");

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            CreateGridViewColumns();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //LoadClients();
            //LoadModuleRights();
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
            ModuleDataTable = GetModuleModule();

            int iWidth=(ModuleDataTable.Rows.Count * 100);
            ModuleRightsGridView.Width= iWidth;

            if (iWidth < 600)
            {
                ModuleRightsGridView.Width = 600;
            }
            for(int i = 0; i < ModuleDataTable.Rows.Count; i++)
            {
                TemplateField objTemplateField=new TemplateField();
                objTemplateField.HeaderText = Convert.ToString(ModuleDataTable.Rows[i]["ModuleName"]);
                objTemplateField.FooterText = Convert.ToString(ModuleDataTable.Rows[i]["ModuleId"]);
                objTemplateField.ItemTemplate = new GridViewCheckBoxClass(i.ToString());
                objTemplateField.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                objTemplateField.ItemStyle.Width = 150;
                ModuleRightsGridView.Columns.Add(objTemplateField);
            }
            ModuleCount.Value = ModuleDataTable.Rows.Count.ToString();

        }

        public DataTable GetModuleModule()
        {
            con.Open();
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "Sp_GetModuleModule";
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
        public string ModuleRightsSql(int moduleRightsId,
            int moduleId ,
            bool rights,
            int createById,
            string createdDate,
            int updateById,
            string updateDate,
            string statementType
            )
        {
            con.Open();
            SqlCommand com = new SqlCommand();

            com.Connection = con;
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "Sp_ModuleRights";
            com.Parameters.Add("ModuleRightsId", SqlDbType.Int).Value = moduleRightsId;
            com.Parameters.Add("ModuleId", SqlDbType.Int).Value = moduleId;
            com.Parameters.Add("Rights", SqlDbType.Bit).Value = rights;
            com.Parameters.Add("CreatedById", SqlDbType.Int).Value = createById;
            com.Parameters.Add("CreatedDate",SqlDbType.Date).Value=createdDate;
            com.Parameters.Add("UpdateById", SqlDbType.Int).Value = updateById;
            com.Parameters.Add("UpdateDate",SqlDbType.Date).Value = updateDate;
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
            con.Close();
            return com.ToString();
        }
        protected void ModuleRightsGridView_RowDataBound(object sender,GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.className=GirdRow RowHighLightColor'");
                if(e.Row.RowIndex% 2 == 0)
                    e.Row.Attributes.Add("onmouseout", "this.className='GridRow'");
                else
                    e.Row.Attributes.Add("onmouseout", "this.className='AlternateGridRow'");
            }
        }
    }
}