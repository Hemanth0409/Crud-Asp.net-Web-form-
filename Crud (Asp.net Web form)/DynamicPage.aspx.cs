using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Crud__Asp.net_Web_form_
{
    public partial class DynamicPage : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-J6THV9C\\SQL2019EXP;Initial Catalog=Aspnet;Integrated Security=True");
        SqlCommand com;
        int currentModuleId;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadForms();
            //BindDataToGridView();
        }
        //public void BindDataToGridView()
        //{
        //    DataTable dt = GetColumnById(9);
        //    foreach (DataColumn column in dt.Columns)
        //    {
        //        TemplateField field = new TemplateField();
        //        field.HeaderText = column.ColumnName;
        //        ColumnControlData.Columns.Add(field);
        //    }
        //    ColumnControlData.DataSource = dt;
        //    ColumnControlData.DataBind();
        //}
        public DataTable GetColumnById(int ModuleId)
        {
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "Sp_GetAllColumnDataById";
            com.Parameters.Add("ModuleId", SqlDbType.Int).Value = ModuleId;
            com.Parameters.Add("IsActive", SqlDbType.Bit).Value = 0;
            com.CommandTimeout = 0;
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            com.ExecuteNonQuery();
            con.Close();
            return dt;
        }

        protected void LoadForms()
        {
            DataTable dt = GetColumnById(9);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    int Controlnumber = row.Field<int>("ControlId");
                    if (Controlnumber == 1)
                    {
                        
                    }
                }
            }
        }
        protected void AddDataButton_Click(object sender, EventArgs e)
        {

        }
        protected void EditButton_Click(object sender, EventArgs e)
        {

        }
        protected void DeleteClick(object sender, EventArgs e)
        {

        }
        protected void DeleteAllClick(object sender, EventArgs e)
        {

        }
    }
}