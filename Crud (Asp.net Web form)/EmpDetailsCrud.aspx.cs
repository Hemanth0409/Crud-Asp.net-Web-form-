using Microsoft.Ajax.Utilities;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Crud__Asp.net_Web_form_
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-DBQ88HK\\SQLEXPRESS2019;Initial Catalog=Aspnet;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                BindCountry();
                BindDataToGridView();
                formViewId.Visible = false;
            }
        }
        public void BindCountry()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("exec DisplayCountry", con);
            SqlDataReader reader = cmd.ExecuteReader();
            Txtcountry.DataSource = reader;
            Txtcountry.Items.Clear();
            Txtcountry.Items.Add("Select a Country");
            Txtcountry.DataTextField = "CountryName";
            Txtcountry.DataValueField = "CountryId";
            Txtcountry.DataBind();
            con.Close();
        }

        public void BindState()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("exec DisplayState @id='" + Txtcountry.SelectedValue + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            Txtstate.DataSource = reader;
            Txtstate.Items.Clear();
            Txtstate.Items.Add("Select a State");
            Txtstate.DataTextField = "StateName";
            Txtstate.DataValueField = "StateId";
            Txtstate.DataBind();
            con.Close();
        }
        protected void country_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindState();
        }
        public void BindDataToGridView()
        {

            SqlCommand comm = new SqlCommand("exec SelectData ", con);
            SqlDataAdapter adapter = new SqlDataAdapter(comm);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                EmpDetails.DataSource = dt;
                EmpDetails.DataBind();
            }
            ViewState["dt"] = dt;
            ViewState["sort"] = "ASC";
        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EmpDetails.PageIndex = e.NewPageIndex;
            BindDataToGridView();
        }

        protected void RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow gdRow = (GridViewRow)EmpDetails.Rows[e.RowIndex];

            HiddenField hdnId = (HiddenField)gdRow.FindControl("hdnId");

            con.Open();
            string sql = string.Format("exec DeleteData @id='" + hdnId.Value + "'");
            SqlCommand comm = new SqlCommand(sql, con);
            comm.ExecuteNonQuery();
            EmpDetails.EditIndex = -1;
            BindDataToGridView();
            con.Close();
        }
        protected void Create_Click(object sender, EventArgs e)
        {
            string gender = string.Empty;
            string cbSelect = string.Empty;

            if (RadioMale.Checked)
            {
                gender = RadioMale.Text;
            }
            else
            {
                gender = RadioFemale.Text;
            }


            string s = string.Empty;
            foreach (ListItem li in Language.Items)
            {
                if (li.Selected == true)
                {
                    s += li.Text + ",";
                }
            }

            if (Session["Id"] != null)
            {
                con.Open();
                SqlCommand updatecom = new SqlCommand("exec UpdateData  @id='" + Session["Id"] + "', @name='" + TxtName.Value + "',@email='" + Txtemail.Value + "',@contact='" + int.Parse(TxtContact.Value) + "',@age='" + int.Parse(Txtage.Value) + "',@country='" + Txtcountry.SelectedItem + "',@state='" + Txtstate.SelectedItem + "',@Address='" + TxtAddress.Value + "',@Joined_Date='" + Convert.ToDateTime(TxtjoinDate.Value).ToString() + "',@gender='" + gender + "',@Language='" + s + "',@Username='" + UserName.Value + "',@password='" + Password.Value + "'", con);
                updatecom.ExecuteNonQuery();
                con.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Successfully Updated');", true);
                BindDataToGridView();
            }
            else
            {
                con.Open();
                SqlCommand comm = new SqlCommand("exec InsertData  @name='" + TxtName.Value + "',@email='" + Txtemail.Value + "',@contact='" + int.Parse(TxtContact.Value) + "',@age='" + int.Parse(Txtage.Value) + "',@country='" + Txtcountry.SelectedItem + "',@state='" + Txtstate.SelectedItem + "',@Address='" + TxtAddress.Value + "',@Joined_Date='" + Convert.ToDateTime(TxtjoinDate.Value).ToString() + "',@gender='" + gender + "',@Language='" + s + "',@Username='" + UserName.Value + "',@password='" + Password.Value + "'", con);
                comm.ExecuteNonQuery();
                con.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Successfully Inserted');", true);
                BindDataToGridView();

            }
            Reset_Click(sender, e);

        }
        protected void Search_Click(object sender, EventArgs e)
        {
            if (searchText.Value != "")
            {
                string searchQuery = "SELECT * FROM EmployeeDetails where Name LIKE '%" + searchText.Value + "' or email like'%" + searchText.Value + "' ";
                SqlCommand comm = new SqlCommand(searchQuery, con);
                SqlDataAdapter adapter = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                EmpDetails.DataSource = dt;
                EmpDetails.DataBind();

            }
            else
            {
                BindDataToGridView();
            }

        }
        protected void ResetSearch_Click(object sender, EventArgs e)
        {
            searchText.Value = string.Empty;
            BindDataToGridView();
        }

        protected void Reset_Click(object sender, EventArgs e)
        {
            TxtName.Value = string.Empty;
            Txtemail.Value = string.Empty;
            Txtage.Value = string.Empty;
            TxtContact.Value = string.Empty;
            TxtAddress.Value = string.Empty;
            TxtjoinDate.Value = string.Empty;
            RadioMale.Checked = false;
            RadioFemale.Checked = false;
            UserName.Value = string.Empty;
            Password.Value = string.Empty;
            Txtcountry.ClearSelection();
            Txtstate.ClearSelection();



            foreach (ListItem li in Language.Items)
            {
                li.Selected = false;
            }

            ListView.Visible = true;
            formViewId.Visible = false;

        }
        protected void ExportToExcel(object sender, EventArgs e)
        {
            ExportGridView("UserInfo.xls");
        }

        private void ExportGridView(string fileName)
        {
            string exportedFile = Server.MapPath(@"~\Files\Export\UserInfo.xls");

            if (File.Exists(exportedFile))
            {
                Response.ClearContent();
                Response.ClearHeaders();
                Response.AddHeader("content-disposition", "attachment; filename=UserInfo.xls");
                Response.ContentType = "application/ms-excel";
                Response.WriteFile(exportedFile);
                Response.Flush();
                Response.Close();
            }
        }

        //public override void VerifyRenderingInServerForm(Control control)
        //{

        //}


        protected void LoadExcelData(object sender, EventArgs e)
        {
            if (UploadedFile1.HasFile)
            {
                string fileName = Path.GetFileName(UploadedFile1.FileName);
                string filePath = Server.MapPath("~/Files/Import/" + fileName);
                UploadedFile1.SaveAs(filePath);
                LoadDataFromExcel(filePath, ".xlsx", "yes");
            }
        }

        public void LoadDataFromExcel(string fpath, string extension, string hdr)
        {

            string excelon = ConfigurationManager.ConnectionStrings["excelcon"].ConnectionString;
            excelon = string.Format(excelon, fpath, hdr);
            OleDbConnection excelcon2 = new OleDbConnection(excelon);

            OleDbCommand cmd = new OleDbCommand("select [Name],[Email],[Contact],[Age],[Address],[Country],[State],[Joined_Date],[gender],[Language],[UserName],[Password] FROM [Sheet1$] where name is not null and email is not null and UserName is not null and Password is not null", excelcon2);
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            excelcon2.Open();
            DbDataReader dr = cmd.ExecuteReader();
            con.Open();
            SqlBulkCopy bulkInsert = new SqlBulkCopy(con);
           
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    bulkInsert.ColumnMappings.Add(dr.GetName(i), dr.GetName(i));
                }
            
            bulkInsert.DestinationTableName = "EmployeeDetails";
            bulkInsert.WriteToServer(dr);
            con.Close();
            excelcon2.Close();
            BindDataToGridView();
        }


        protected void CountryLinkButton_Click(object sender, EventArgs e)
        {
            PropertyAttributePanel.Visible = true;
            this.PropertyAttributeModalPopupExtender.TargetControlID = "CountryLinkButton";
            hdnPropertyAttributeIframe.Value = "Country.aspx";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Assign Source To IFrame", "AssignSourceToIframe()", true);
            PropertyAttributeHeaderLabel.Text = "Country";
            PropertyAttributeModalPopupExtender.OnCancelScript = "OnCancel('Country')";
            PropertyAttributeModalPopupExtender.Show();
        }
        protected void StateLinkButton_Click(object sender, EventArgs e)
        {
            PropertyAttributePanel.Visible = true;
            this.PropertyAttributeModalPopupExtender.TargetControlID = "StateLinkButton";
            hdnPropertyAttributeIframe.Value = "State.aspx";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Assign Source To IFrame", "AssignSourceToIframe()", true);
            PropertyAttributeHeaderLabel.Text = "State";
            PropertyAttributeModalPopupExtender.OnCancelScript = "OnCancel('State')";
            PropertyAttributeModalPopupExtender.Show();
        }
        protected void EditButton_Click(object sender, EventArgs e)
        {
            ListView.Visible = false;
            formViewId.Visible = true;

            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            HiddenField hdnId = (HiddenField)row.FindControl("hdnId");
            Session["Id"] = hdnId.Value;
            con.Open();
            SqlCommand comm = new SqlCommand("exec selectDeleteById @Id='" + hdnId.Value + "',@StatementType='select'", con);
            SqlDataReader sqlDataReader = comm.ExecuteReader();
            while (sqlDataReader.Read())
            {
                TxtName.Value = sqlDataReader.GetValue(1).ToString();
                Txtemail.Value = sqlDataReader.GetValue(2).ToString();
                Txtage.Value = sqlDataReader.GetValue(4).ToString();
                TxtContact.Value = sqlDataReader.GetValue(3).ToString();
                Txtcountry.SelectedItem.Text = sqlDataReader.GetValue(6).ToString();
                Txtstate.SelectedItem.Text = sqlDataReader.GetValue(7).ToString();
                TxtAddress.Value = sqlDataReader.GetValue(5).ToString();
                TxtjoinDate.Value = Convert.ToDateTime(sqlDataReader.GetValue(8)).Date.ToString("yyyy-MM-dd");
                string gender = sqlDataReader.GetValue(9).ToString();
                string selectedItems = sqlDataReader.GetValue(10).ToString();
                UserName.Value = sqlDataReader.GetValue(11).ToString();
                Password.Value = sqlDataReader.GetValue(12).ToString();
                if (gender.Equals("Male"))
                {
                    RadioMale.Checked = true;
                }
                else if (gender.Equals("Female"))
                {
                    RadioFemale.Checked = true;
                }


                for (int i = 0; i < Language.Items.Count; i++)
                {
                    if (selectedItems.Contains(Language.Items[i].Value))
                    {
                        Language.Items[i].Selected = true;
                    }
                }

                selectedItems = selectedItems.TrimEnd(',');

            }
            con.Close();
        }
        public void AddEmployee(object sender, EventArgs e)
        {
            ListView.Visible = false;
            formViewId.Visible = true;

        }
    }
}
