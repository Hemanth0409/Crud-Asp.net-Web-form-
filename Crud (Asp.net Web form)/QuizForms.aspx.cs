using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Crud__Asp.net_Web_form_
{
    public partial class QuizForms : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-J6THV9C\\SQL2019EXP;Initial Catalog=Aspnet;Integrated Security=True");
        SqlCommand com;
        int currentquizModuleId;
        string quizModuleId;
        string quizModuleName;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string quizModuleId = Request.QueryString["QuizModuleId"];
                string quizModuleName = Request.QueryString["QuizModuleName"];
                if (quizModuleId != null && quizModuleName != null)
                {
                    currentquizModuleId = Convert.ToInt32(quizModuleId);
                    txtQuizModuleId.InnerText = quizModuleName;
                }
                BindVideoModuleData();
            }
        }
        protected void QuizVideoModuleMethod(int? quizModuleVideoId, int quizModuleId, string VideoTitle, string videoFilePath, int videoOrder, string statementType)
        {
            com = new SqlCommand("Sp_QuizModuleVideo", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Quiz_ModuleVideoId", quizModuleVideoId);
            com.Parameters.AddWithValue("@Quiz_ModuleId", quizModuleId);
            com.Parameters.AddWithValue("@Quiz_VideoTitle", VideoTitle);
            com.Parameters.AddWithValue("@Quiz_VideoFilePath", videoFilePath);
            com.Parameters.AddWithValue("@Quiz_VideoOrder", videoOrder);
            com.Parameters.AddWithValue("@Statement_Type", statementType);
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
        }
    private void BindVideoModuleData()
{
    DataTable videoModuleData = new DataTable();

    SqlCommand cmd = new SqlCommand("Sp_QuizModuleVideo", con);
    cmd.CommandType = CommandType.StoredProcedure;
    cmd.Parameters.AddWithValue("@Statement_Type", "Select");
    con.Open();
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(videoModuleData);

    DataColumn orderColumn = new DataColumn("Order");
    videoModuleData.Columns.Add(orderColumn);

    for (int i = 0; i < videoModuleData.Rows.Count; i++)
    {
        videoModuleData.Rows[i]["Order"] = i + 1;
    }

    GridView1.DataSource = videoModuleData;
    GridView1.DataBind();
}



    }
}