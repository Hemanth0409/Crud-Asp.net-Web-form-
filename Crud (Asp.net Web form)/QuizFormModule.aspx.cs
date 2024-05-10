using Newtonsoft.Json;
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

    public partial class QuizFormModule : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.HiddenField jsonDataField;

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
        public class QuestionOption
        {
            public string id { get; set; }
            public string text { get; set; }
        }

        public class Question
        {
            public string questionText { get; set; }
            public string questionType { get; set; }
            public List<QuestionOption> options { get; set; }
        }

  
        protected void submitFormClick(object sender, EventArgs e)
        {
            string jsonData = jsonDataField.Value;
            List<Question> questions = JsonConvert.DeserializeObject<List<Question>>(jsonData);
            InsertQuizDataIntoDatabase(questions);
        } 

        protected void InsertQuizDataIntoDatabase(List<Question> questions)
        {
            string connectionString = "Data Source=DESKTOP-J6THV9C\\SQL2019EXP;Initial Catalog=Aspnet;Integrated Security=True"; // Replace with your actual connection string

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                foreach (Question question in questions)
                {
                    SqlCommand cmdQuestion = new SqlCommand("YourQuestionInsertProcedure", con);
                    cmdQuestion.CommandType = CommandType.StoredProcedure;
                    cmdQuestion.Parameters.AddWithValue("@QuestionText", question.questionText);
                    cmdQuestion.Parameters.AddWithValue("@QuestionType", question.questionType);
                    cmdQuestion.ExecuteNonQuery();

                    int quizFormControlId = GetInsertedQuizFormControlId(con);

                    foreach (QuestionOption option in question.options)
                    {
                        SqlCommand cmdOption = new SqlCommand("YourOptionInsertProcedure", con);
                        cmdOption.CommandType = CommandType.StoredProcedure;
                        cmdOption.Parameters.AddWithValue("@QuizFormControlId", quizFormControlId);
                        cmdOption.Parameters.AddWithValue("@OptionId", option.id);
                        cmdOption.Parameters.AddWithValue("@OptionText", option.text);
                        cmdOption.ExecuteNonQuery();
                    }
                }
            }
        }
        protected int GetInsertedQuizFormControlId(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("SELECT SCOPE_IDENTITY()", con);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
}