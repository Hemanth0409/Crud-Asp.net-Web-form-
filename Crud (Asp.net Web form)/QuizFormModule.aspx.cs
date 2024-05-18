using DocumentFormat.OpenXml.Bibliography;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Interop;

namespace Crud__Asp.net_Web_form_
{
    public partial class QuizFormModule : System.Web.UI.Page
    {
        protected HiddenField jsonDataField;

        private static string connectionString = "Data Source=DESKTOP-J6THV9C\\SQL2019EXP;Initial Catalog=Aspnet;Integrated Security=True";
        private static string quizModuleId;
        private static string quizVideoId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                quizModuleId = Request.QueryString["QuizModuleId"];
                quizVideoId = Request.QueryString["QuizVideoModuleId"];
                string quizModuleName = Request.QueryString["QuizModuleName"];
                string quizVideoName = Request.QueryString["QuizVideoName"];

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
            public bool isRequired { get; set; }

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
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlTransaction transaction = con.BeginTransaction())
                {
                    try
                    { 
                        if (quizModuleId != null && quizVideoId != null)
                        {
                            int quizTitleId = InsertQuizTitle(con, transaction, formData.title, formData.description);
                            foreach (var question in formData.questions)
                            {
                                int quizFormControlId = InsertQuizQuestion(con, transaction, quizTitleId, question);
                                foreach (var option in question.options)
                                {
                                    InsertQuizOption(con, transaction, quizFormControlId, option);
                                }
                            }
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("An error occurred while inserting data into the database.", ex);
                    }
                }
            }
        }

        private int InsertQuizTitle(SqlConnection con, SqlTransaction transaction, string title, string description)
        {
            using (SqlCommand cmd = new SqlCommand("Sp_QuizFormTitle", con, transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Quiz_titleID", 0);
                cmd.Parameters.AddWithValue("@Quiz_ModuleId", Convert.ToInt32(quizModuleId));
                cmd.Parameters.AddWithValue("@Quiz_ModuleVideoId", Convert.ToInt32(quizVideoId));
                cmd.Parameters.AddWithValue("@Quiz_Title", title);
                cmd.Parameters.AddWithValue("@Quiz_Description", description);
                cmd.Parameters.AddWithValue("@StatementType", "INSERT");

                cmd.ExecuteNonQuery();
                return GetLastInsertedId(con, transaction, "Quiz_FormTitle", "Quiz_TitleId");
            }
        }

        private int InsertQuizQuestion(SqlConnection con, SqlTransaction transaction, int quizTitleId, Question question)
        {
            using (SqlCommand cmd = new SqlCommand("Sp_QuizFormModule", con, transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Quiz_FormControlId", 0);
                cmd.Parameters.AddWithValue("@Quiz_ModuleId", Convert.ToInt32(quizModuleId));
                cmd.Parameters.AddWithValue("@Quiz_ModuleVideoId", Convert.ToInt32(quizVideoId));
                cmd.Parameters.AddWithValue("@IsRequired", question.isRequired);
                cmd.Parameters.AddWithValue("@Quiz_FormQuestion", question.questionText);
                cmd.Parameters.AddWithValue("@Quiz_FieldType", Convert.ToInt32(question.questionType));
                cmd.Parameters.AddWithValue("@Quiz_TitleId", quizTitleId);
                cmd.Parameters.AddWithValue("@StatementType", "INSERT");

                cmd.ExecuteNonQuery();
                return GetLastInsertedId(con, transaction, "Quiz_FormDataControl", "Quiz_FormControlId");
            }
        }

        private void InsertQuizOption(SqlConnection con, SqlTransaction transaction, int quizFormControlId, QuestionOption option)
        {
            using (SqlCommand cmd = new SqlCommand("Sp_QuizOptionValues", con, transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Quiz_formFieldValueId", 0);
                cmd.Parameters.AddWithValue("@Quiz_FormControlId", quizFormControlId);
                cmd.Parameters.AddWithValue("@Quiz_formFieldValueOptions", option.text);
                cmd.Parameters.AddWithValue("@IsCorrect", 0);
                cmd.Parameters.AddWithValue("@OptionFieldId", Convert.ToInt32(option.id));
                cmd.Parameters.AddWithValue("@StatementType", "INSERT");
                cmd.ExecuteNonQuery();
            }
        }
        public static bool IsTitleExists(string title)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Quiz_FormTitle WHERE Quiz_Title = @Quiz_Title", con))
                {
                    cmd.Parameters.AddWithValue("@Quiz_Title", title);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private int GetLastInsertedId(SqlConnection con, SqlTransaction transaction, string tableName, string columnName)
        {
            using (SqlCommand cmd = new SqlCommand($"SELECT MAX({columnName}) FROM {tableName}", con, transaction))
            {
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    throw new InvalidOperationException("Unable to retrieve last inserted ID.");
                }
            }
        }
    }
}
