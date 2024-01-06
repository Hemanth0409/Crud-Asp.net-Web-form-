using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Crud__Asp.net_Web_form_
{
    public partial class AddColumn : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-DBQ88HK\\SQLEXPRESS2019;Initial Catalog=Aspnet;Integrated Security=True");

        //string ModuleId = Session["MemberId"] as string;

        protected void Page_Load(object sender, EventArgs e)
        {

        }
       
        public string ColumnControlDetails(
            int ColumnControlId,
        int ModuleId,
        int ControlId,
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
        string Fieldname,
        string StatementType)
        {
            SqlCommand com = new SqlCommand();

            com.Connection = con;
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "Sp_ColumnControl";
            com.Parameters.Add("ColumnControlId", SqlDbType.Int).Value = ColumnControlId;
            com.Parameters.Add("ModuleId", SqlDbType.Int).Value = ModuleId;
            com.Parameters.Add("ControlId", SqlDbType.Int).Value = ControlId;
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
            com.Parameters.Add("Fieldname", SqlDbType.VarChar, 50).Value = Fieldname;
            com.Parameters.Add("StatementType", SqlDbType.VarChar, 25).Value = StatementType;
            com.CommandTimeout = 0;

            com.ExecuteNonQuery();
            return com.ToString();
        }
        protected void Create_Click(object sender, EventArgs e)
        {
            con.Open();
            //ColumnControlDetails(Convert.ToInt32(Session["ModuleId"]), TxtName.Value, Txtemail.Value, int.Parse(TxtContact.Value), int.Parse(Txtage.Value), TxtAddress.Value, Txtcountry.SelectedItem.ToString(), Txtstate.SelectedItem.ToString(), Convert.ToDateTime(TxtjoinDate.Value).ToString(), gender, language, UserName.Value, Password.Value, true, "UPDATE");
            con.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Successfully Updated');", true);
        }
        protected void Reset_Click(object sender, EventArgs e)
        {
        }
        public void DisplayView(object sender, EventArgs e)
        {
           
            if (RadioForDisplay.SelectedItem.Text == "Single Line Txt")
            {
                RequiredfieldView.Visible = true;
                CharactersView.Visible = true;
                DefaultTxtView.Visible = true;
                LinesToDisplayView.Visible = false;
                SperateDataView.Visible = false;
                ChoiceSelectView.Visible = false;
                ChoiceLineView.Visible = false;
                MinMaxValueView.Visible = false;
                DataView.Visible = false;
                YesNoView.Visible = false;
            }
            else if (RadioForDisplay.SelectedItem.Text == "Multi Line Txt")
            {
                RequiredfieldView.Visible = true;
                CharactersView.Visible = false;
                DefaultTxtView.Visible = false;
                LinesToDisplayView.Visible = true;
                SperateDataView.Visible = false;
                ChoiceSelectView.Visible = false;
                ChoiceLineView.Visible = false;
                MinMaxValueView.Visible = false;
                DataView.Visible = false;
                YesNoView.Visible = false;
            }
            else if (RadioForDisplay.SelectedItem.Text == "Choice (menu)")
            {
               
                RequiredfieldView.Visible = true;
                CharactersView.Visible = false;
                DefaultTxtView.Visible = true;
                LinesToDisplayView.Visible = false;
                SperateDataView.Visible = true;
                ChoiceSelectView.Visible = true;
                ChoiceLineView.Visible = false;
                MinMaxValueView.Visible = false;
                DataView.Visible = false;
                YesNoView.Visible = false;
            }
            else if (RadioForDisplay.SelectedItem.Text == "Number")
            {
                RequiredfieldView.Visible = true;
                CharactersView.Visible = false;
                DefaultTxtView.Visible = true;
                LinesToDisplayView.Visible = false;
                SperateDataView.Visible = false;
                ChoiceSelectView.Visible = false;
                ChoiceLineView.Visible = false;
                MinMaxValueView.Visible = true;
                DataView.Visible = false;
                YesNoView.Visible = false;
            }
            else if (RadioForDisplay.SelectedItem.Text == "Date and time")
            {
                RequiredfieldView.Visible = true;
                CharactersView.Visible = false;
                DefaultTxtView.Visible = false;
                LinesToDisplayView.Visible = false;
                SperateDataView.Visible = false;
                ChoiceSelectView.Visible = false;
                ChoiceLineView.Visible = false;
                MinMaxValueView.Visible = false;
                DataView.Visible = true;
                YesNoView.Visible = false;
            }
            else if (RadioForDisplay.SelectedItem.Text == "Check box (Y/N)")
            {
                RequiredfieldView.Visible = false;
                CharactersView.Visible = false;
                DefaultTxtView.Visible = false;
                LinesToDisplayView.Visible = false;
                SperateDataView.Visible = false;
                ChoiceSelectView.Visible = false;
                ChoiceLineView.Visible = false;
                MinMaxValueView.Visible = false;
                DataView.Visible = false;
                YesNoView.Visible = true;
            }
            else if (RadioForDisplay.SelectedItem.Text == "File Upload")
            {
                RequiredfieldView.Visible = true;
                CharactersView.Visible = false;
                DefaultTxtView.Visible = false;
                LinesToDisplayView.Visible = false;
                SperateDataView.Visible = false;
                ChoiceSelectView.Visible = false;
                ChoiceLineView.Visible = false;
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