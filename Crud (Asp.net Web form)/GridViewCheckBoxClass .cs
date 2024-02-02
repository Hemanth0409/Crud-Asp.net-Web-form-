using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Crud__Asp.net_Web_form_
{
   
        public class GridViewCheckBoxClass : ITemplate
        {
            string sControlId;

            public GridViewCheckBoxClass(string _sControlId)
            {
                sControlId = _sControlId;
            }


            public void InstantiateIn(Control container)
            {
                CheckBox objCheckBox = new CheckBox();
                objCheckBox.ID = "ModuleCheckBox" + sControlId;
                //objCheckBox.Attributes.Add();
                container.Controls.Add(objCheckBox);

            }
       
    }
}