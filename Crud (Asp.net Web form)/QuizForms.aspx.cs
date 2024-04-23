using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Crud__Asp.net_Web_form_
{
    public partial class QuizForms : System.Web.UI.Page
    {
        int currentquizModuleId;
        string quizModuleId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["QuizModuleId"]))
            {
                quizModuleId= Request.QueryString["QuizModuleId"];

            }
            currentquizModuleId= Convert.ToInt32(quizModuleId);
        }
    }
}