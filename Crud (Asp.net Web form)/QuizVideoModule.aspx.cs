using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Crud__Asp.net_Web_form_
{
    public partial class QuizVideoModule : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-J6THV9C\\SQL2019EXP;Initial Catalog=Aspnet;Integrated Security=True");
        SqlCommand com;
        static int currentquizModuleId;
        string quizModuleId;
        static string quizModuleName;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                quizModuleId = Request.QueryString["QuizModuleId"];
              quizModuleName = Request.QueryString["QuizModuleName"];
                if (quizModuleId != null && quizModuleName != null)
                {
                    currentquizModuleId = Convert.ToInt32(quizModuleId);
                    txtQuizModuleName.InnerText = quizModuleName;
                }
                BindVideoModuleData();
            }
        }

        protected bool QuizVideoModuleMethod(int? quizModuleVideoId, int quizModuleId, string VideoTitle, string videoFilePath, int videoOrder, string statementType)
        {
            com = new SqlCommand("Sp_QuizModuleVideo", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Quiz_ModuleVideoId", quizModuleVideoId);
            com.Parameters.AddWithValue("@Quiz_ModuleId", quizModuleId);
            com.Parameters.AddWithValue("@Quiz_VideoTitle", VideoTitle);
            com.Parameters.AddWithValue("@Quiz_VideoFilePath", videoFilePath);
            com.Parameters.AddWithValue("@Quiz_VideoOrder", videoOrder);
            com.Parameters.AddWithValue("@Statement_Type", statementType);

            try
            {
                con.Open();
                com.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        protected void AddVideoMethod(object sender, EventArgs e)
        {
            perviousVideoSrc.Visible = false;
            btnUploadVideo.Text = "Upload";     
            VideoTable.Visible = false;
            videoFormId.Visible = true;
            btnAdd.Visible = false;
        }
        protected void UploadVideoMethod(object sender, EventArgs e)
        {
            string videoTitleUpload = videoTitle.Value;
            string videoFileName = Path.GetFileName(videoFile.PostedFile.FileName);
            string fileExtension = Path.GetExtension(videoFileName);

            // Check if a file is uploaded
            bool fileUploaded = !string.IsNullOrEmpty(videoFileName) && fileExtension.Equals(".mp4", StringComparison.OrdinalIgnoreCase);

            // Check if file upload is required
            if (!fileUploaded && Session["QuizModuleVideoId"] == null)
            {
                Response.Write("<script>alert('Please upload an MP4 video file.')</script>");
                return;
            }

            string videoFilePath = string.Empty;

            // Save the uploaded file
            if (fileUploaded)
            {
                videoFilePath = Path.Combine("/UploadedVideo/", videoFileName);
                videoFile.PostedFile.SaveAs(Server.MapPath(videoFilePath));
            }
            else
            {
                videoFilePath = Session["ExistingUrl"].ToString();
            }

            int maxVideoOrder = 0;

            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-J6THV9C\\SQL2019EXP;Initial Catalog=Aspnet;Integrated Security=True"))
            {
                string query = "SELECT MAX(VideoOrder) FROM Quiz_ModuleVideo WHERE Quiz_ModuleId = @ModuleId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ModuleId", currentquizModuleId);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        maxVideoOrder = Convert.ToInt32(result);
                    }
                }
            }

            int newVideoOrder = maxVideoOrder + 1;

            bool isUpdate = Session["QuizModuleVideoId"] != null;
            int existingVideoOrder = 0;
            string existingUrl = string.Empty;

            if (isUpdate)
            {
                existingVideoOrder = Convert.ToInt32(Session["ExistingVideoOrder"]);
                existingUrl = Session["ExistingUrl"].ToString();
            }

            if (isUpdate)
            {
                if (QuizVideoModuleMethod(Convert.ToInt32(Session["QuizModuleVideoId"]), currentquizModuleId, videoTitleUpload, videoFilePath, existingVideoOrder, "Update"))
                {
                    if (fileUploaded && existingUrl != videoFilePath)
                    {
                        File.Delete(Server.MapPath(existingUrl));
                    }

                    if (fileUploaded)
                    {
                        string uploadedFilePath = Path.Combine(Server.MapPath("/UploadedVideo/"), videoFileName);
                        File.Move(uploadedFilePath, Path.Combine(Server.MapPath("/UploadedVideo/"), videoFileName));
                    }

                    Response.Write("<script>alert('Video updated successfully.')</script>");
                }
                else
                {
                    if (fileUploaded)
                    {
                        File.Delete(Server.MapPath(videoFilePath));
                    }

                    Response.Write("<script>alert('Failed to update video. Please try again.')</script>");
                }
            }
            else
            {
                if (QuizVideoModuleMethod(null, currentquizModuleId, videoTitleUpload, videoFilePath, newVideoOrder, "Insert"))
                {
                    if (fileUploaded)
                    {
                        string uploadedFilePath = Path.Combine(Server.MapPath("/UploadedVideo/"), videoFileName);
                        File.Move(uploadedFilePath, Path.Combine(Server.MapPath("/UploadedVideo/"), videoFileName));
                    }

                    Response.Write("<script>alert('Video uploaded successfully.')</script>");
                }
                else
                {
                    if (fileUploaded)
                    {
                        File.Delete(Server.MapPath(videoFilePath));
                    }

                    Response.Write("<script>alert('Failed to upload video. Please try again.')</script>");
                }
            }

            Response.Redirect(Request.RawUrl);
            CancelVideoMethod(sender, e);
        }
        protected void EditVideoClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            HiddenField hdnId = (HiddenField)row.FindControl("hdnId");
            Label lblVideoTitle = (Label)row.FindControl("lblVideoTitle");
            Session["QuizModuleVideoId"] = hdnId.Value;
            con.Open();
            SqlCommand comm = new SqlCommand("Sp_QuizModuleVideo", con);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@Quiz_ModuleVideoId", Convert.ToInt32(hdnId.Value));
            comm.Parameters.AddWithValue("@Statement_Type", "SelectById");
            SqlDataReader sqlDataReader = comm.ExecuteReader();
            if (sqlDataReader.HasRows)
            {

                while (sqlDataReader.Read())
                {
                    if (hdnId != null)
                    {
                        btnUploadVideo.Text = "Save";
                        videoTitle.Value = sqlDataReader["VideoTitle"].ToString();
                        perviousVideoSrc.Visible = true;
                        videoSrc.Src = sqlDataReader["VideoFilePath"].ToString();
                        Session["ExistingUrl"] = sqlDataReader["VideoFilePath"].ToString();
                        Session["ExistingVideoOrder"] = sqlDataReader["VideoOrder"].ToString();

                    }
                }
                videoFormId.Visible = true;
                VideoTable.Visible = false;
                btnAdd.Visible = false;
            }
            sqlDataReader.Close();
            con.Close();
        }

        protected void DeleteRecord(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            HiddenField hdnId = (HiddenField)row.FindControl("hdnId");
            int quizModuleVideoId = Convert.ToInt32(hdnId.Value);

            con.Open();
            SqlCommand cmd = new SqlCommand("Sp_QuizModuleVideo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Quiz_ModuleVideoId", quizModuleVideoId);
            cmd.Parameters.AddWithValue("@Statement_Type", "DELETE");
            cmd.ExecuteNonQuery();
            con.Close();

            BindVideoModuleData();
        }


        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindVideoModuleData();
        }
        protected void AddQuizFormClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            HiddenField hdnId = (HiddenField)row.FindControl("hdnId");
            Label lblVideoTitle = (Label)row.FindControl("lblVideoTitle");  
            int quizModuleVideoId = Convert.ToInt32(hdnId.Value);
            TableCell moduleNameCell = row.Cells[1];
            string quizVideoName = moduleNameCell.Text.Trim();
            string redirectUrl = $"~/QuizFormModule.aspx?QuizModuleId={currentquizModuleId}&QuizVideoModuleId={quizModuleVideoId}&QuizModuleName={quizModuleName}&QuizVideoName={quizVideoName}";
            Response.Redirect(redirectUrl, false);
        }
        protected void CancelVideoMethod(object sender, EventArgs e)
        {
            Session["QuizModuleVideoId"] = string.Empty;
            Session["ExistingVideoOrder"] = string.Empty;
            Session["ExistingUrl"] = string.Empty;
            videoTitle.Value = string.Empty;
            btnAdd.Visible = true;
            VideoTable.Visible = true;
            videoFormId.Visible = false;
        }

        private void BindVideoModuleData()
        {
            DataTable videoModuleData = new DataTable();
            SqlCommand cmd = new SqlCommand("Sp_QuizModuleVideo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Statement_Type", "Select");

            try
            {
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
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
        }
    }
}
