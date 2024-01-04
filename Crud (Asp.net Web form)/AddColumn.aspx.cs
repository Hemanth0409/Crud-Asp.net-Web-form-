using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Crud__Asp.net_Web_form_
{
    public partial class AddColumn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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
        protected void Create_Click(object sender, EventArgs e)
        {

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