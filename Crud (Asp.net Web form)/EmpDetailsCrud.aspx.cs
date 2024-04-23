using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Crud__Asp.net_Web_form_
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-J6THV9C\\SQL2019EXP;Initial Catalog=Aspnet;Integrated Security=True");
        string excelon = ConfigurationManager.ConnectionStrings["excelcon"].ConnectionString;

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
            txtCountry.DataSource = reader;
            txtCountry.Items.Clear();
            txtCountry.Items.Add("Select a Country");
            txtCountry.DataTextField = "CountryName";
            txtCountry.DataValueField = "CountryId";
            txtCountry.DataBind();
            con.Close();

        }

        public void BindState()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("exec DisplayState @id='" + txtCountry.SelectedValue + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            txtState.DataSource = reader;
            txtState.Items.Clear();
            txtState.Items.Add("Select a State");
            txtState.DataTextField = "StateName";
            txtState.DataValueField = "StateId";
            txtState.DataBind();
            con.Close();
        }
        protected void country_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindState();
        }
        public void BindDataToGridView()
        {
            SqlCommand comm = new SqlCommand("EXEC SelectData", con);
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
            string gender;

            if (rdoMale.Checked)
            {
                gender = rdoMale.Text;
            }
            else
            {
                gender = rdoFemale.Text;
            }


            string language = string.Empty;
            foreach (ListItem li in Language.Items)
            {
                if (li.Selected == true)
                {
                    language += li.Text + ",";
                }
            }
            GridViewRow gdRow = (GridViewRow)EmpDetails.Rows[e.RowIndex];

            HiddenField hdnId = (HiddenField)gdRow.FindControl("hdnId");
            Session["UserId"] = hdnId.Value;
            con.Open();
            EmployeeDetails(Convert.ToInt32(hdnId.Value), "", "", 0, 0, "", "", "", "", "", "", "", "", true, "DELETE");
            EmpDetails.EditIndex = -1;
            BindDataToGridView();
            con.Close();

        }
        protected void Create_Click(object sender, EventArgs e)
        {
            string gender;
            string cbSelect;

            if (rdoMale.Checked)
            {
                gender = rdoMale.Text;
            }
            else
            {
                gender = rdoFemale.Text;
            }


            string language = string.Empty;
            foreach (ListItem li in Language.Items)
            {
                if (li.Selected == true)
                {
                    language += li.Text + ",";
                }
            }

            if (Session["Id"] != null)
            {
                con.Open();

                EmployeeDetails(Convert.ToInt32(Session["Id"]), txtName.Value, txtEmail.Value, int.Parse(txtContact.Value), int.Parse(txtAge.Value), txtAddress.Value, txtCountry.SelectedItem.ToString(), txtState.SelectedItem.ToString(), Convert.ToDateTime(txtJoinDate.Value).ToString(), gender, language, txtUserName.Value, txtPassword.Value, true, "UPDATE");
                con.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Successfully Updated');", true);
                BindDataToGridView();
            }
            else
            {
                con.Open();
                EmployeeDetails(0, txtName.Value, txtEmail.Value, int.Parse(txtContact.Value), int.Parse(txtAge.Value), txtAddress.Value, txtCountry.SelectedItem.ToString(), txtState.SelectedItem.ToString(), Convert.ToDateTime(txtJoinDate.Value).ToString(), gender, language, txtUserName.Value, txtPassword.Value, true, "INSERT");
                con.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Successfully Inserted');", true);
                BindDataToGridView();
            }
            Reset_Click(sender, e);

        }
        public string EmployeeDetails(int Id, string Name, string Email, int Contact, int Age,
            string Address, string Country, string State, string Joined_Date, string Gender,
            string Language, string txtUserName, string txtPassword, bool IsActive, string StatementType)
        {
            SqlCommand com = new SqlCommand();

            com.Connection = con;
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "Sp_EmployeeDetails";
            com.Parameters.Add("Id", SqlDbType.Int).Value = Id;
            com.Parameters.Add("Name", SqlDbType.VarChar, 25).Value = Name;
            com.Parameters.Add("Email", SqlDbType.VarChar, 25).Value = Email;
            com.Parameters.Add("Contact", SqlDbType.Int).Value = Contact;
            com.Parameters.Add("Age", SqlDbType.Int).Value = Age;
            com.Parameters.Add("Address", SqlDbType.VarChar, 50).Value = Address;
            com.Parameters.Add("Country", SqlDbType.VarChar, 25).Value = Country;
            com.Parameters.Add("State", SqlDbType.VarChar, 25).Value = State;
            com.Parameters.Add("Joined_Date", SqlDbType.Date).Value = StatementType == "DELETE" ? DateTime.ParseExact("20/11/2021", "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString() : Joined_Date;
            com.Parameters.Add("Gender", SqlDbType.VarChar, 25).Value = Gender;
            com.Parameters.Add("Language", SqlDbType.VarChar, 25).Value = Language;
            com.Parameters.Add("UserName", SqlDbType.VarChar, 25).Value = txtUserName;
            com.Parameters.Add("Password", SqlDbType.VarChar, 25).Value = txtPassword;
            com.Parameters.Add("IsActive", SqlDbType.Bit).Value = IsActive;
            com.Parameters.Add("StatementType", SqlDbType.VarChar, 25).Value = StatementType;
            com.CommandTimeout = 0;
            com.ExecuteNonQuery();
            return com.ToString();
        }
        protected void Search_Click(object sender, EventArgs e)
        {
            if (searchText.Value != "")
            {
                string searchQuery = "SELECT * FROM EmployeeDetails WHERE Name LIKE '%" + searchText.Value + "' OR email LIKE'%" + searchText.Value + "' OR Country LIKE'%" + searchText.Value + "' OR State LIKE'%" + searchText.Value + "' OR Address LIKE'%" + searchText.Value + "' ";
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
            Page_Load(sender, e);

        }

        protected void Reset_Click(object sender, EventArgs e)
        {
            txtName.Value = string.Empty;
            txtEmail.Value = string.Empty;
            txtAge.Value = string.Empty;
            txtContact.Value = string.Empty;
            txtAddress.Value = string.Empty;
            txtJoinDate.Value = string.Empty;
            rdoMale.Checked = false;
            rdoFemale.Checked = false;
            txtUserName.Value = string.Empty;
            txtPassword.Value = string.Empty;
            txtCountry.ClearSelection();
            txtState.ClearSelection();



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
            string exportedFile = Server.MapPath(@"~\Files\Export\" + fileName);

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

        protected void LoadExcelData(object sender, EventArgs e)
        {
            if (UploadedFile1.HasFile)
            {
                string fileName = Path.GetFileName(UploadedFile1.FileName);
                string filePath = Server.MapPath("~/Files/Import/" + fileName);
                UploadedFile1.SaveAs(filePath);
                ReadDataFromExcel(filePath);
            }
        }
        public Boolean ReadDataFromExcel(string filepath)
        {
            if (filepath.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Insert a file with xls or xlsx format');", true);
                return false;
            }
            string fileName = Path.GetFileName(UploadedFile1.FileName);
            string filePath = Server.MapPath("~/Files/Import/" + fileName);
            UploadedFile1.SaveAs(filePath);
            DataTable dtValue = GetData(1, filePath, ".xlsx", "yes");
            DataTable dtNew = new DataTable();
            dtNew = dtValue.Clone();
            DeleteFile(filepath);
            if (dtValue.Rows.Count > 0)
            {
                if (dtValue.Columns.Count == 12 & dtValue.Columns.Contains("Name") & dtValue.Columns.Contains("Email") & dtValue.Columns.Contains("Contact") & dtValue.Columns.Contains("Age")
                    & dtValue.Columns.Contains("Address") & dtValue.Columns.Contains("Country") & dtValue.Columns.Contains("State") & dtValue.Columns.Contains("Joined_Date") & dtValue.Columns.Contains("Gender")
                    & dtValue.Columns.Contains("Language") & dtValue.Columns.Contains("txtUserName") & dtValue.Columns.Contains("txtPassword"))
                {
                    for (int i = 0; i < dtValue.Rows.Count; i++)
                    {
                        bool UserExist = GetUserExist(dtValue.Rows[i]["txtUserName"].ToString(), dtValue.Rows[i]["txtPassword"].ToString(), dtValue.Rows[i]["Email"].ToString());

                        if (!UserExist)
                        {
                            bool CountryExist = GetCountry(dtValue.Rows[i]["Country"].ToString());
                            if (CountryExist)
                            {
                                bool StateExist = GetState(dtValue.Rows[i]["State"].ToString());
                                if (StateExist)
                                {

                                    dtNew.Rows.Add(dtValue.Rows[i].ItemArray);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('State Does not exist for the Country ');", true);

                                    return false;
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Country Does not exist');", true);

                                return false;
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('txtUserName already Exist');", true);

                            return false;
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Fill all the fields in the Excel Sheet');", true);

                    return false;
                }
                if (dtNew.Rows.Count > 0 & IsValid == true)
                {
                    for (int i = 0; i < dtNew.Rows.Count; i++)
                    {
                        con.Open();
                        EmployeeDetails(0, dtNew.Rows[i]["Name"].ToString(), dtNew.Rows[i]["Email"].ToString(), Convert.ToInt32(dtNew.Rows[i]["Contact"]), Convert.ToInt32(dtNew.Rows[i]["Age"]),
                            dtNew.Rows[i]["Address"].ToString(), dtNew.Rows[i]["Country"].ToString(), dtNew.Rows[i]["State"].ToString(), dtNew.Rows[i]["Joined_Date"].ToString(), dtNew.Rows[i]["Gender"].ToString(),
                            dtNew.Rows[i]["Language"].ToString(), dtNew.Rows[i]["txtUserName"].ToString(), dtNew.Rows[i]["txtPassword"].ToString(), true, "INSERT");
                        con.Close();
                        BindDataToGridView();
                    }
                }
            }
            return true;
        }


        public bool GetUserExist(string txtUserName, string txtPassword, string Email)
        {
            DataTable objtxtUserName = GetUserInfo(txtUserName);
            if (objtxtUserName.Select("txtUserName='" + txtUserName.Trim() + "'").Length == 0 & objtxtUserName.Select("Email='" + Email.Trim() + "'").Length == 0 & objtxtUserName.Select("txtPassword='" + txtPassword.Trim() + "'").Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public DataTable GetDataTable(SqlCommand objSqlCommand)
        {
            con.Open();
            objSqlCommand.Connection = con;
            SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter();
            objSqlDataAdapter.SelectCommand = objSqlCommand;
            DataTable objDataTable = new DataTable();
            objSqlDataAdapter.Fill(objDataTable);
            con.Close();
            return objDataTable;
        }

        public DataTable GetUserInfo(string txtUserName)
        {
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "Sp_GetUserInfo";
            com.Parameters.Add("@txtUserName", SqlDbType.VarChar, 25).Value = txtUserName;
            return GetDataTable(com);
        }
        public List<int> StateICodeList = new List<int>();
        public List<int> CountryICodeList = new List<int>();
        int CountryCode;
        public bool GetCountry(string CountryName)
        {
            DataTable countryTable = GetAllCountry();
            if (countryTable.Select("CountryName='" + CountryName.Trim() + "'").Length == 0)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < countryTable.Rows.Count; i++)
                {
                    if (Convert.ToString(countryTable.Rows[i]["CountryName"]).ToLower().Trim() == CountryName.ToLower().Trim())
                    {
                        CountryCode = Convert.ToInt32(countryTable.Rows[i]["CountryId"]);
                        CountryICodeList.Add(CountryCode);
                        return true;
                    }
                }
                return true;
            }
        }
        public DataTable GetAllCountry()
        {
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "[DisplayCountry]";
            return GetDataTable(com);
        }

        public bool GetState(string StateName)
        {

            DataTable StateTable = GetAllState(CountryCode);
            if (StateTable.Select("StateName='" + StateName.Trim() + "'").Length == 0)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < StateTable.Rows.Count; i++)
                {
                    if (Convert.ToString(StateTable.Rows[i]["StateName"]).ToLower().Trim() == StateName.ToLower().Trim())
                    {
                        int StateCode = Convert.ToInt32(StateTable.Rows[i]["StateId"]);
                        CountryICodeList.Add(StateCode);
                        return true;
                    }
                }
                return true;
            }
        }
        public DataTable GetAllState(int CountryId)
        {
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "[DisplayState]";
            com.Parameters.Add("Id", SqlDbType.Int).Value = CountryId;
            return GetDataTable(com);
        }
        public void DeleteFile(string fileName)
        {
            FileInfo objFile;
            if (File.Exists(fileName))
            {
                objFile = new FileInfo(fileName);
                objFile.IsReadOnly = false;
                File.Delete(fileName);
            }
        }
        public DataTable GetData(int RowToDelete, string fpath, string extension, string hdr)
        {
            DataTable dtValue = new DataTable();
            excelon = string.Format(excelon, fpath, hdr);
            OleDbConnection excelcon2 = new OleDbConnection(excelon);

            OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]   ", excelcon2);

            OleDbDataAdapter excelAdapter = new OleDbDataAdapter(cmd);
            excelAdapter.Fill(dtValue);
            if (dtValue.Rows.Count > 0)
            {
                for (int i = 1; i <= RowToDelete; i++)
                    dtValue.Rows.RemoveAt(dtValue.Rows.Count - 1);
                DeleteEmptyRows(dtValue);
            }
            return dtValue;
        }
        private DataTable DeleteEmptyRows(DataTable dtValue)
        {
            int columnCount = dtValue.Columns.Count;

            for (int rowIndex = dtValue.Rows.Count - 1; rowIndex >= 0; rowIndex--)
            {
                bool isNull = true;

                for (int colIndex = 0; colIndex < columnCount; colIndex++)
                {
                    if (!(dtValue.Rows[rowIndex][colIndex] == DBNull.Value))
                    {
                        isNull = false;
                        break;
                    }
                }

                if (isNull)
                    dtValue.Rows.RemoveAt(rowIndex);
            }

            return dtValue;
        }

        public void LoadDataFromExcel(string fpath, string extension, string hdr)
        {
            excelon = string.Format(excelon, fpath, hdr);
            OleDbConnection excelcon2 = new OleDbConnection(excelon);

            OleDbCommand cmd = new OleDbCommand("SELECT [Name],[Email],[Contact],[Age],[Address],[Country],[State],[Joined_Date],[gender],[Language],[txtUserName],[txtPassword] FROM [Sheet1$]", excelcon2);
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
            SqlCommand comm = new SqlCommand("EXEC selectDeleteById @Id='" + hdnId.Value + "',@StatementType='select'", con);
            SqlDataReader sqlDataReader = comm.ExecuteReader();
            while (sqlDataReader.Read())
            {
                txtName.Value = sqlDataReader.GetValue(1).ToString();
                txtEmail.Value = sqlDataReader.GetValue(2).ToString();
                txtAge.Value = sqlDataReader.GetValue(4).ToString();
                txtContact.Value = sqlDataReader.GetValue(3).ToString();
                txtCountry.SelectedItem.Text = sqlDataReader.GetValue(6).ToString();
                txtState.SelectedItem.Text = sqlDataReader.GetValue(7).ToString();
                txtAddress.Value = sqlDataReader.GetValue(5).ToString();
                txtJoinDate.Value = Convert.ToDateTime(sqlDataReader.GetValue(8)).Date.ToString("yyyy-MM-dd");
                string gender = sqlDataReader.GetValue(9).ToString();
                string selectedItems = sqlDataReader.GetValue(10).ToString();
                txtUserName.Value = sqlDataReader.GetValue(11).ToString();
                txtPassword.Value = sqlDataReader.GetValue(12).ToString();
                if (gender.Equals("Male"))
                {
                    rdoMale.Checked = true;
                }
                else if (gender.Equals("Female"))
                {
                    rdoFemale.Checked = true;
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
            UploadedFile1.Visible = false;
            LinkButton2.Visible = false;
            LinkButton3.Visible = false;
            ListView.Visible = false;
            formViewId.Visible = true;
            searchText.Visible = false;
            searchButton.Visible = false;
            ClearSearch.Visible = false;
            AddEmployeeData.Visible = false;
        }
    }
}
