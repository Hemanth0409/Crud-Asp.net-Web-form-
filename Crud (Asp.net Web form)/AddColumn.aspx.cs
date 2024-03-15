using Antlr.Runtime.Misc;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Crud__Asp.net_Web_form_
{
    public partial class AddColumn : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-J6THV9C\\SQL2019EXP;Initial Catalog=Aspnet;Integrated Security=True");
        SqlCommand com;
        int currentModuleId;
        protected void Page_Load(object sender, EventArgs e)
        {
            currentModuleId = Convert.ToInt32(Session["ModuleId"]);
            BindDataToGridView();

        }

        public void BindDataToGridView()
        {
            GetColumnById(currentModuleId);
        
        }

        public string GetColumnById(int ModuleId)
        {
            SqlCommand com = new SqlCommand();           
            com.Connection = con;
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "Sp_GetAllColumnDataById";
            com.Parameters.Add("ModuleName", SqlDbType.NVarChar).Value = "";
            com.Parameters.Add("ModuleDataId", SqlDbType.Int).Value = ModuleId;
            com.Parameters.Add("IsActive", SqlDbType.Bit).Value = 1;
            com.CommandTimeout = 0;
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                ColumnControlData.DataSource = dt;
                ColumnControlData.DataBind();
            }
            ViewState["dt"] = dt;
            ViewState["sort"] = "ASC";
            com.ExecuteNonQuery();
            con.Close();
            return com.ToString();
        }
        protected void DeleteRecord(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            HiddenField hdnId = (HiddenField)row.FindControl("hdnId");        
            ColumnControlDetails(Convert.ToInt32(hdnId.Value), 0, 0, "", true, 0, 0, "", "", "", 0, 0, "", true, true, "DELETE");
            ColumnControlData.EditIndex = -1;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Successfully Deleted');", true);
            BindDataToGridView();         
        }

        public string ColumnControlDetails(
        int ColumnControlId,
        int ModuleId,
        int ColumnId,
        string ColumnName,
        bool RequiredField,
        int MaximumCharacters,
        int NumberOfLinesDisplay,
        string ChoiceValue,
        string ChoiceType,
        string DefaultValue,
        int MaxValue,
        int MinValue,
        string DateTimeFormat,
        bool IsActive,
        bool DefaultCheckBoxValue,
        string StatementType)
        {
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "Sp_ColumnControl";
            com.Parameters.Add("ColumnControlId", SqlDbType.Int).Value = ColumnControlId;
            com.Parameters.Add("ModuleId", SqlDbType.Int).Value = ModuleId;
            com.Parameters.Add("ControlId", SqlDbType.Int).Value = ColumnId;
            com.Parameters.Add("ColumnName", SqlDbType.VarChar, 25).Value = ColumnName;
            com.Parameters.Add("RequiredField", SqlDbType.Bit).Value = RequiredField;
            com.Parameters.Add("MaximumCharacters", SqlDbType.Int).Value = MaximumCharacters;
            com.Parameters.Add("NumberOfLinesDisplay", SqlDbType.Int).Value = NumberOfLinesDisplay;
            com.Parameters.Add("ChoiceValue", SqlDbType.VarChar, 100).Value = ChoiceValue;
            com.Parameters.Add("ChoiceType", SqlDbType.Char, 1).Value = ChoiceType;
            com.Parameters.Add("DefaultValue", SqlDbType.VarChar, 100).Value = DefaultValue;
            com.Parameters.Add("MaxValue", SqlDbType.Int).Value = MaxValue;
            com.Parameters.Add("MinValue", SqlDbType.Int).Value = MinValue;
            com.Parameters.Add("DateTimeFormat", SqlDbType.Char, 1).Value = DateTimeFormat;
            com.Parameters.Add("IsActive", SqlDbType.Bit).Value = IsActive;
            com.Parameters.Add("DefaultCheckBoxValue", SqlDbType.Bit).Value = DefaultCheckBoxValue;
            com.Parameters.Add("Fieldname", SqlDbType.VarChar, 50).Value = "";
            com.Parameters.Add("StatementType", SqlDbType.VarChar, 25).Value = StatementType;
            com.CommandTimeout = 0;
                com.ExecuteNonQuery();
            con.Close();
            return com.ToString();
        }
        protected void Reset_Click(object sender, EventArgs e)
        {
            TxtColumnName.Value = string.Empty;
            YesButton.Checked = false;
            NoButton.Checked = false;
            Characters.Value = string.Empty;
            LinesToDisplay.Value = string.Empty;
            DataForChoiceTxt.Value = string.Empty;
            RadioButton1.Checked = false;
            RadioButton2.Checked = false;
            RadioButton3.Checked = false;
            DefaultTxt.Value = string.Empty;
            txtMin.Value = string.Empty;
            txtMax.Value = string.Empty;
            RadioForDisplayDate.ClearSelection();
            IsActive.Checked = false;
            DefaultValue.ClearSelection();
            Session["ColumnControlId"] = null;
            Response.Redirect("DynamicModule.aspx", false);

        }

        protected void Create_Click(object sender, EventArgs e)
        {
            //ListView.Visible = false;
            //formViewId.Visible = true;
            int lineToDisplayValue;
            if (!int.TryParse(LinesToDisplay.Value, out lineToDisplayValue))
            {

                lineToDisplayValue = 0;
            }
            

            string ColumnName = TxtColumnName.Value ?? string.Empty;

            bool requiredField = YesButton.Checked;


            string dataForChoiceValue = DataForChoiceTxt.Value ?? string.Empty;

            char choiceTypeValue = (RadioButton1.Checked) ? 'D' : (RadioButton2.Checked) ? 'R' : (RadioButton3.Checked) ? 'C' : 'N';

            string defaultValueTxt = DefaultTxt.Value ?? string.Empty;
            string defaultDateValue = TxtjoinDate.Value =="" ?  DateTime.Now.Date.ToString("yyyy-MM-dd"): TxtjoinDate.Value;


            int charSize;
            if (!int.TryParse(Characters.Value, out charSize))
            {
                charSize = 0;
            }
            int minValue;
            if (!int.TryParse(txtMin.Value, out minValue))
            {
                minValue = 0;
            }

            int maxValue;
            if (!int.TryParse(txtMax.Value, out maxValue))
            {
                maxValue = 0;
            }


            string dateTimeFormatValue = "N";

            if (RadioForDisplayDate.SelectedItem != null)
            {
                dateTimeFormatValue = (RadioForDisplayDate.SelectedItem.Text == "None") ? "N" :
                                      (RadioForDisplayDate.SelectedItem.Text == "CurrentDate") ? "C" :
                                      (RadioForDisplayDate.SelectedItem.Text == "Enter Date") ? "E" : null;
            }
            bool displayColumn = IsActive.Checked;

            bool defaultCheckBox = DefaultValue.SelectedItem.Value == "No" ? defaultCheckBox = false : defaultCheckBox = true;

            if (Session["ColumnControlId"] != null)
            {
            string defaultValueRecord = defaultValueTxt != defaultDateValue  ? defaultDateValue : defaultValueTxt ;

                ColumnControlDetails(Convert.ToInt32(Session["ColumnControlId"]), currentModuleId, int.Parse(RadioBtnIdForDisplay.SelectedItem.Value), ColumnName, requiredField, charSize,
                    lineToDisplayValue, dataForChoiceValue, choiceTypeValue.ToString(), defaultValueRecord, maxValue, minValue,
                    dateTimeFormatValue, displayColumn, defaultCheckBox, "UPDATE");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Successfully Updated');", true);
           
                BindDataToGridView();
            }
            else
            {
            string defaultValueRecord = defaultValueTxt == "" ? defaultDateValue : defaultValueTxt;
                ColumnControlDetails(0, currentModuleId, int.Parse(RadioBtnIdForDisplay.SelectedItem.Value), ColumnName, requiredField, charSize,
                    lineToDisplayValue, dataForChoiceValue, choiceTypeValue.ToString(), defaultValueRecord, maxValue, minValue,
                    dateTimeFormatValue, displayColumn, defaultCheckBox, "INSERT");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Successfully Inserted');", true);
                BindDataToGridView();
            }
            Reset_Click(sender, e);
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {            
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            HiddenField hdnId = (HiddenField)row.FindControl("hdnId");
            Session["ColumnControlId"] = hdnId.Value;
            con.Open();
            SqlCommand comm = new SqlCommand("exec Sp_SelectColumnById @ColumnControlId='" + hdnId.Value + "'", con);
            SqlDataReader sqlDataReader = comm.ExecuteReader();
            while (sqlDataReader.Read())
            {
                string requiredField;
                string isActive;
                string ChoiceTypeData;
                RadioBtnIdForDisplay.SelectedIndex = Convert.ToInt32(sqlDataReader.GetValue(2).ToString());
                DisplayView(sender, e);
                RadioBtnIdForDisplay.Enabled = false;
                TxtColumnName.Value = sqlDataReader.GetValue(3).ToString();
                requiredField = sqlDataReader.GetValue(4).ToString();
                Characters.Value = sqlDataReader.GetValue(5).ToString();
                LinesToDisplay.Value = sqlDataReader.GetValue(6).ToString();
                DataForChoiceTxt.Value = sqlDataReader.GetValue(7).ToString();
                ChoiceTypeData = sqlDataReader.GetValue(8).ToString();
                DefaultTxt.Value = sqlDataReader.GetValue(9).ToString();
                txtMax.Value = sqlDataReader.GetValue(10).ToString();
                txtMin.Value = sqlDataReader.GetValue(11).ToString();
                isActive = sqlDataReader.GetValue(13).ToString();
                string DefaultCheckBoxValue = sqlDataReader.GetValue(14).ToString();

                if (DefaultCheckBoxValue.Equals("True"))
                {
                    DefaultValue.SelectedIndex = 0;
                }
                else
                {
                    DefaultValue.SelectedIndex = 1;
                }

                if (ChoiceTypeData.Equals("D"))
                {
                    RadioButton1.Checked = true;
                }
                else if (ChoiceTypeData.Equals("C"))
                {
                    RadioButton2.Checked = true;

                }
                else if (ChoiceTypeData.Equals("R"))
                {
                    RadioButton3.Checked = true;

                }
                if (isActive.Equals("True"))
                {
                    IsActive.Checked = true;
                }
                else
                {
                    IsActive.Checked = false;
                }
                if (requiredField.Equals("True"))
                {
                    YesButton.Checked = true;
                }
                else if (requiredField.Equals("False"))
                {
                    NoButton.Checked = true;
                }
            }
            con.Close();
        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ColumnControlData.PageIndex = e.NewPageIndex;
            BindDataToGridView();
        }
        public void AddColumnFields(object sender, EventArgs e)
        {
            ListView.Visible = false;
            formViewId.Visible = true;
        }
        public void DisplayView(object sender, EventArgs e)
        {
            if (RadioBtnIdForDisplay.SelectedItem.Text == "Single Line Txt" || RadioBtnIdForDisplay.SelectedItem.Value == "0")
            {
                RequiredfieldView.Visible = true;
                CharactersView.Visible = true;
                DefaultTxtView.Visible = true;
                LinesToDisplayView.Visible = false;
                SperateDataView.Visible = false;
                ChoiceSelectView.Visible = false;
                MinMaxValueView.Visible = false;
                DataView.Visible = false;
                YesNoView.Visible = false;
            }
            else if (RadioBtnIdForDisplay.SelectedItem.Text == "Multi Line Txt" || RadioBtnIdForDisplay.SelectedItem.Value == "1")
            {
                RequiredfieldView.Visible = true;
                CharactersView.Visible = false;
                DefaultTxtView.Visible = false;
                LinesToDisplayView.Visible = true;
                SperateDataView.Visible = false;
                ChoiceSelectView.Visible = false;
                MinMaxValueView.Visible = false;
                DataView.Visible = false;
                YesNoView.Visible = false;
            }
            else if (RadioBtnIdForDisplay.SelectedItem.Text == "Choice (menu)" || RadioBtnIdForDisplay.SelectedItem.Value == "2")
            {

                RequiredfieldView.Visible = true;
                CharactersView.Visible = false;
                DefaultTxtView.Visible = true;
                LinesToDisplayView.Visible = false;
                SperateDataView.Visible = true;
                ChoiceSelectView.Visible = true;
                MinMaxValueView.Visible = false;
                DataView.Visible = false;
                YesNoView.Visible = false;
            }
            else if (RadioBtnIdForDisplay.SelectedItem.Text == "Number" || RadioBtnIdForDisplay.SelectedItem.Value == "3")
            {
                RequiredfieldView.Visible = true;
                CharactersView.Visible = false;
                DefaultTxtView.Visible = true;
                LinesToDisplayView.Visible = false;
                SperateDataView.Visible = false;
                ChoiceSelectView.Visible = false;
                MinMaxValueView.Visible = true;
                DataView.Visible = false;
                YesNoView.Visible = false;
            }
            else if (RadioBtnIdForDisplay.SelectedItem.Text == "Date and time" || RadioBtnIdForDisplay.SelectedItem.Value == "4")
            {
                RequiredfieldView.Visible = true;
                CharactersView.Visible = false;
                DefaultTxtView.Visible = false;
                LinesToDisplayView.Visible = false;
                SperateDataView.Visible = false;
                ChoiceSelectView.Visible = false;
                MinMaxValueView.Visible = false;
                DataView.Visible = true;
                YesNoView.Visible = false;
            }
            else if (RadioBtnIdForDisplay.SelectedItem.Text == "Check box (Y/N)" || RadioBtnIdForDisplay.SelectedItem.Value == "5")
            {
                RequiredfieldView.Visible = true;
                CharactersView.Visible = false;
                DefaultTxtView.Visible = false;
                LinesToDisplayView.Visible = false;
                SperateDataView.Visible = false;
                ChoiceSelectView.Visible = false;
                MinMaxValueView.Visible = false;
                DataView.Visible = false;
                YesNoView.Visible = true;
            }
            else if (RadioBtnIdForDisplay.SelectedItem.Text == "File Upload" || RadioBtnIdForDisplay.SelectedItem.Value == "6")
            {
                RequiredfieldView.Visible = true;
                CharactersView.Visible = false;
                DefaultTxtView.Visible = false;
                LinesToDisplayView.Visible = false;
                SperateDataView.Visible = false;
                ChoiceSelectView.Visible = false;
                MinMaxValueView.Visible = false;
                DataView.Visible = false;
                YesNoView.Visible = false;
            }
        }
        public void DisplayDate(object sender, EventArgs e)
        {
            if (RadioForDisplayDate.SelectedItem.Text == "Enter Date")
            {
                DateField.Visible = true;
            }
            else if (RadioForDisplayDate.SelectedItem.Text != "Enter Date")
            {
                DateField.Visible = false;
            }
        }

    }
}