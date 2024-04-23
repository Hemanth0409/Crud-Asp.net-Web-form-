using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Crud__Asp.net_Web_form_
{
    public partial class QuizModule : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-J6THV9C\\SQL2019EXP;Initial Catalog=Aspnet;Integrated Security=True");
        SqlCommand com;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataToGridView();
                Reset_Click(sender, e);
            }
        }

        public void BindDataToGridView()
        {
            ExecuteStoredProcedure(0, "", 10, true, "Select");
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                QuizModuleData.DataSource = dt;
                QuizModuleData.DataBind();
            }
            ViewState["dt"] = dt;
            ViewState["sort"] = "ASC";
        }

        protected void RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow gdRow = QuizModuleData.Rows[e.RowIndex];
            HiddenField hdnId = (HiddenField)gdRow.FindControl("hdnId");

            ExecuteStoredProcedure(Convert.ToInt32(hdnId.Value), "", 10, true, "Delete");
            BindDataToGridView();
        }

        protected void ModuleGridView_RowCommand(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            HiddenField hdnId = (HiddenField)row.FindControl("hdnId");
            Session["QuizModuleId"] = hdnId.Value;
            string redirectUrl = "~/QuizForms.aspx?QuizModuleId=" + hdnId.Value;
            Response.Redirect(redirectUrl, false);
        }


        protected void EditButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            HiddenField hdnId = (HiddenField)row.FindControl("hdnId");
            Session["QuizModuleId"] = hdnId.Value;
            ExecuteStoredProcedure(Convert.ToInt32(hdnId.Value), "", 10, true, "SelectById");
            PopulateFormFields();
        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            QuizModuleData.PageIndex = e.NewPageIndex;
            BindDataToGridView();
        }

        protected void Create_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtQuizModule.Value))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Module Name');", true);
            }
            else
            {
                string statementType = Session["QuizModuleId"] == null ? "Insert" : "Update";
                int quizModuleId = Session["QuizModuleId"] == null ? 0 : Convert.ToInt32(Session["QuizModuleId"]);
                ExecuteStoredProcedure(quizModuleId, txtQuizModule.Value, 10, CheckBox1.Checked, statementType);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", $"alert('Successfully {statementType}ed');", true);
                Reset_Click(sender, e);
                BindDataToGridView();
            }
        }

        protected void DeleteRecord(object sender, EventArgs e)
        {
            // Implement deletion logic here if needed
        }

        protected void Reset_Click(object sender, EventArgs e)
        {

            Session["QuizModuleId"] = null;
            txtQuizModule.Value = string.Empty;
            CheckBox1.Checked = false;
        }

        protected void ExecuteStoredProcedure(int? quizModuleId, string quizModuleName, int? createdById, bool? isAvailable, string statementType)
        {        
            com = new SqlCommand("Sp_QuizModule", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Quiz_ModuleId", quizModuleId);
            com.Parameters.AddWithValue("@Quiz_ModuleName", quizModuleName);
            com.Parameters.AddWithValue("@CreatedById", createdById);
            com.Parameters.AddWithValue("@IsAvailable", isAvailable);
            com.Parameters.AddWithValue("@Statement_Type", statementType);
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
        }

        protected void PopulateFormFields()
        {
            con.Open();
            SqlCommand comm = new SqlCommand("exec Sp_selectById @QuizModuleId='" + Session["QuizModuleId"] + "'", con);
            SqlDataReader sqlDataReader = comm.ExecuteReader();
            while (sqlDataReader.Read())
            {
                txtQuizModule.Value = sqlDataReader.GetValue(1).ToString();
                string IsActiveCheck = sqlDataReader.GetValue(2).ToString();
                CheckBox1.Checked = IsActiveCheck == "True";
            }
            con.Close();
        }
    }
}
