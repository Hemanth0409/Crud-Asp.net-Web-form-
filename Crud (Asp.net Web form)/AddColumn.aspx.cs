using Antlr.Runtime.Misc;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Crud__Asp.net_Web_form_
{
    public partial class AddColumn : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-DBQ88HK\\SQLEXPRESS2019;Initial Catalog=Aspnet;Integrated Security=True");

        int currentModuleId;
        protected void Page_Load(object sender, EventArgs e)
        {
            // ListView.Visible = true;
            formViewId.Visible = true;
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
            con.Open();
            com.Connection = con;
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "Sp_GetAllColumnDataById";
            com.Parameters.Add("ModuleId", SqlDbType.Int).Value = ModuleId;
            com.CommandTimeout = 0;

            com.ExecuteNonQuery();
            con.Close();
            return com.ToString();
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
            SqlCommand com = new SqlCommand();

            com.Connection = con;
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "Sp_ColumnControl";
            com.Parameters.Add("ColumnCountrolId", SqlDbType.Int).Value = ColumnControlId;
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
        }
        protected void Create_Click(object sender, EventArgs e)
        {
            int lineToDisplayValue;
            if (!int.TryParse(LinesToDisplay.Value, out lineToDisplayValue))
            {

                lineToDisplayValue = 0;
            }
            int ColumnControlIdValue = Session["ColumnControlId"] != null ? Convert.ToInt32(Session["ColumnControlId"]) : 0;

            int ColumnIdValue = int.Parse(RadioBtnIdForDisplay.SelectedItem.Value ?? "0");

            string ColumnName = TxtColumnName.Value ?? string.Empty;

            bool requiredField = YesButton.Checked;


            string dataForChoiceValue = DataForChoiceTxt.Value ?? string.Empty;

            char choiceTypeValue = (RadioButton1.Checked) ? 'D' : (RadioButton2.Checked) ? 'R' : (RadioButton3.Checked) ? 'C' : 'N';

            string defaultValueTxt = DefaultTxt.Value ?? string.Empty;
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

            con.Open();
            ColumnControlDetails(ColumnControlIdValue, 1, ColumnIdValue, ColumnName, requiredField, charSize,
                lineToDisplayValue, dataForChoiceValue, choiceTypeValue.ToString(), defaultValueTxt, maxValue, minValue,
                dateTimeFormatValue, displayColumn, defaultCheckBox, "INSERT");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Successfully Inserted');", true);
            con.Close();
            Reset_Click(sender, e);
        }
        protected void EditButton_Click(object sender, EventArgs e)
        {
            ListView.Visible = false;
            formViewId.Visible = true;

            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            HiddenField hdnId = (HiddenField)row.FindControl("hdnId");
            Session["ColumnControlId"] = hdnId;
            con.Open();
            SqlCommand comm = new SqlCommand("exec Sp_SelectColumnById @ColumnControlId='" + hdnId.Value + "'", con);
            SqlDataReader sqlDataReader = comm.ExecuteReader();
            while (sqlDataReader.Read())
            {
              

            }
            con.Close();
        }
        public void DisplayView(object sender, EventArgs e)
        {

            if (RadioBtnIdForDisplay.SelectedItem.Text == "Single Line Txt")
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
            else if (RadioBtnIdForDisplay.SelectedItem.Text == "Multi Line Txt")
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
            else if (RadioBtnIdForDisplay.SelectedItem.Text == "Choice (menu)")
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
            else if (RadioBtnIdForDisplay.SelectedItem.Text == "Number")
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
            else if (RadioBtnIdForDisplay.SelectedItem.Text == "Date and time")
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
            else if (RadioBtnIdForDisplay.SelectedItem.Text == "Check box (Y/N)")
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
            else if (RadioBtnIdForDisplay.SelectedItem.Text == "File Upload")
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