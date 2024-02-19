using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Crud__Asp.net_Web_form_
{
    public class DynamicCheckBoxTemplate : ITemplate
    {
        private string columnName;

        public DynamicCheckBoxTemplate(string columnName)
        {
            this.columnName = columnName;
        }
        public void InstantiateIn(Control container)
        {
            CheckBox checkBox = new CheckBox();
            checkBox.ID = "chk" + columnName;
            checkBox.DataBinding += CheckBox_DataBinding;
            container.Controls.Add(checkBox);
        }
        private void CheckBox_DataBinding(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            GridViewRow container = (GridViewRow)checkBox.NamingContainer;
            checkBox.Checked = Convert.ToBoolean(DataBinder.Eval(container.DataItem, columnName));
        }
    }
}