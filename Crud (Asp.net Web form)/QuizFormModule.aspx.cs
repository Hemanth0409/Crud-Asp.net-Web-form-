using DocumentFormat.OpenXml.Bibliography;
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
        static string quizModuleId;
        string quizModuleName;
        static string quizVideoId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                quizModuleId = Request.QueryString["QuizModuleId"];
                quizModuleName = Request.QueryString["QuizModuleName"];
                quizVideoId = Request.QueryString["QuizVideoModuleId"];
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
                txtDescription.Style["font-style"] = "bold";
            }
        }

        public class QuizFormData
        {
            public string title { get; set; }
            public string description { get; set; }
            public List<Question> questions { get; set; }
        }

        public class Question
        {
            public string questionText { get; set; }
            public string questionType { get; set; }
            public List<QuestionOption> options { get; set; }
        }
        public class QuestionOption
        {
            public string id { get; set; }
            public string text { get; set; }
        }

        protected void submitFormClick(object sender, EventArgs e)
        {
            string jsonData = jsonDataField.Value;
            QuizFormData formData = JsonConvert.DeserializeObject<QuizFormData>(jsonData);
            InsertQuizDataIntoDatabase(formData);
        }
        protected void InsertQuizDataIntoDatabase(QuizFormData formData)
        {
            string connectionString = "Data Source=DESKTOP-J6THV9C\\SQL2019EXP;Initial Catalog=Aspnet;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                if (quizModuleId != null && quizVideoId != null)
                {
                    foreach (Question question in formData.questions)
                    {
                        SqlCommand cmdQuestion = new SqlCommand("Sp_QuizFormModule", con);
                        cmdQuestion.CommandType = CommandType.StoredProcedure;
                        cmdQuestion.Parameters.AddWithValue("@Quiz_FormControlId", 0);
                        cmdQuestion.Parameters.AddWithValue("@Quiz_ModuleId", Convert.ToInt32(quizModuleId));
                        cmdQuestion.Parameters.AddWithValue("@Quiz_ModuleVideoId", Convert.ToInt32(quizVideoId));
                        cmdQuestion.Parameters.AddWithValue("@IsRequired", false);
                        cmdQuestion.Parameters.AddWithValue("@Quiz_FormQuestion", question.questionText);
                        cmdQuestion.Parameters.AddWithValue("@Quiz_FieldType", Convert.ToInt32(question.questionType));
                        cmdQuestion.Parameters.AddWithValue("@StatementType", "INSERT");

                        cmdQuestion.ExecuteNonQuery();
                        int quizFormControlId = GetInsertedQuizFormControlId(con);
                        foreach (QuestionOption option in question.options)
                        {
                            SqlCommand cmdOption = new SqlCommand("Sp_QuizOptionValues", con);
                            cmdOption.CommandType = CommandType.StoredProcedure;
                            cmdOption.Parameters.AddWithValue("@Quiz_formFieldValueId", 0);
                            cmdOption.Parameters.AddWithValue("@Quiz_FormControlId", quizFormControlId);
                            cmdOption.Parameters.AddWithValue("@Quiz_formFieldValueOptions", option.text);
                            cmdOption.Parameters.AddWithValue("@IsCorrect", 0);
                            cmdOption.Parameters.AddWithValue("@OptionFieldId", Convert.ToInt32(option.id));
                            cmdOption.Parameters.AddWithValue("@StatementType", "INSERT");
                            cmdOption.ExecuteNonQuery();
                        }
                    }
                }
            }
        }



        protected int GetInsertedQuizFormControlId(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("SELECT max(Quiz_FormControlId) from Quiz_FormDataControl", con);
            object result = cmd.ExecuteScalar();
            if (result != null && result != DBNull.Value)
            {
                return Convert.ToInt32(result);
            }
            else
            {
                throw new InvalidOperationException("SCOPE_IDENTITY() returned null.");
            }
        }

    }
}