using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Crud__Asp.net_Web_form_
{
    public partial class QuizFormModule : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-J6THV9C\\SQL2019EXP;Initial Catalog=Aspnet;Integrated Security=True");
        SqlCommand com;
        static int currentquizModuleId;
        string quizModuleId;
        string quizModuleName;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                quizModuleId = Request.QueryString["QuizModuleId"];
                quizModuleName = Request.QueryString["QuizModuleName"];
                string quizVideoId = Request.QueryString["QuizVideoModuleId"];
                string quizVideoName = Request.QueryString["QuizVideoName"];
                //if (quizModuleId != null && quizModuleName != null)
                //{
                //    currentquizModuleId = Convert.ToInt32(quizModuleId);
                //    txtQuizModuleName.InnerText=quizModuleName;
                //    txtQuizModuleID.InnerText = quizModuleId;
                //    txtVideoId.InnerText = quizVideoId;
                //    txtVideoModuleName.InnerText = quizVideoName;
                //}
                txtTitle.Style["font-weight"] = "bold";
                txtDescription.Style["font-style"] = "italic";
            }
        }
    }
}