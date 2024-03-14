using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Crud__Asp.net_Web_form_
{
    public partial class Index : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-J6THV9C\\SQL2019EXP;Initial Catalog=Aspnet;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                List<string> moduleNames = GetModuleNames(2083);

                foreach (string moduleName in moduleNames)
                {
                    AddNavigationItem(moduleName);
                }
            }
        }
        private void AddNavigationItem(string moduleName)
        {
            HtmlGenericControl li = new HtmlGenericControl("li");
            navMenu.Controls.Add(li);
            HtmlAnchor anchor = new HtmlAnchor();
            anchor.HRef = $"DynamicPage.aspx?moduleName={moduleName}";
            anchor.Target = "switch-frame";
            anchor.Attributes.Add("class", "nav-link px-0 text-white");
            anchor.InnerHtml = $"<i class='fs-4 bi-house'></i><span class='ms-1 d-none d-sm-inline'>{moduleName}</span>";
            li.Controls.Add(anchor);
        }

        public List<string> GetModuleNames(int EmployeeId)
        {
            List<string> moduleNames = new List<string>();
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-J6THV9C\\SQL2019EXP;Initial Catalog=Aspnet;Integrated Security=True"))
            {
                using (SqlCommand command = new SqlCommand("SP_DisplayModuleRightForEmployee", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = EmployeeId;                   
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            string moduleName = reader["ModuleName"].ToString();
                            moduleNames.Add(moduleName);
                        }
                        reader.Close();                
                }
            }
            return moduleNames;
        }
    }
}
