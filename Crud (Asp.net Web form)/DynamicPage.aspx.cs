using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Crud__Asp.net_Web_form_
{
    public partial class DynamicPage : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-DBQ88HK\\SQLEXPRESS2019;Initial Catalog=Aspnet;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {

            Dictionary<string, string> formFields = new Dictionary<string, string>
        {
            {"Name", "Demo Check"},
            {"Age", "25"},
            {"Gender", "Male"}
        };


            con.Open();

            foreach (var field in formFields)
            {
                string insertQuery = "INSERT INTO DynamicFormData (FieldName, FieldValue) VALUES (@FieldName, @FieldValue)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                {
                    cmd.Parameters.AddWithValue("@FieldName", field.Key);
                    cmd.Parameters.AddWithValue("@FieldValue", field.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}