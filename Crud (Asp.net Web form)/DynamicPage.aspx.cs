using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Crud__Asp.net_Web_form_
{
    public partial class DynamicPage : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-J6THV9C\\SQL2019EXP;Initial Catalog=Aspnet;Integrated Security=True");
        SqlCommand com;
        int currentModuleId;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadForms();
            //BindDataToGridView();
        }
        //public void BindDataToGridView()
        //{
        //    DataTable dt = GetColumnById(9);
        //    foreach (DataColumn column in dt.Columns)
        //    {
        //        TemplateField field = new TemplateField();
        //        field.HeaderText = column.ColumnName;
        //        ColumnControlData.Columns.Add(field);
        //    }
        //    ColumnControlData.DataSource = dt;
        //    ColumnControlData.DataBind();
        //}
        public DataTable GetColumnById(int ModuleId)
        {
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "Sp_GetAllColumnDataById";
            com.Parameters.Add("ModuleId", SqlDbType.Int).Value = ModuleId;
            com.Parameters.Add("IsActive", SqlDbType.Bit).Value = 0;
            com.CommandTimeout = 0;
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            com.ExecuteNonQuery();
            con.Close();
            return dt;
        }
        

        protected void LoadForms()
        {
            DataTable dt = GetColumnById(9);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string columnName = row.Field<string>("ColumnName");
                    int controlId = row.Field<int>("ControlId");
                    string DefaultText = row.Field<string>("DefaultValue");
                    int minValue = row.Field<int>("MinValue");
                    int maxValue = row.Field<int>("MaxValue");
                    string choiceValue = row.Field<string>("ChoiceValue");
                    bool DefaultCheckBoxValue = row.Field<bool>("DefaultCheckBoxValue");
                    string[] choices = choiceValue.Split(',');
                    Panel1.Controls.Add(new LiteralControl("<div class=\"row justify-content-center align-items-center\" > "));
                    Panel1.Controls.Add(new LiteralControl("<div class=\"col-md-2 mt-4 text-center \" > "));
                    Label label = new Label();
                    label.Text = columnName;
                    label.CssClass = "form-label m-0";
                    Panel1.Controls.Add(label);
                    Panel1.Controls.Add(new LiteralControl("</div>"));
                    Panel1.Controls.Add(new LiteralControl("<div class=\"col-md-4 mt-4\">"));

                    switch (controlId)
                    {
                        case 0: 
                            TextBox textBox = new TextBox();
                            textBox.ID = "Txt" + columnName.Replace(" ", "");
                            textBox.CssClass = "form-control float-end";
                            textBox.MaxLength = 10; 
                            textBox.Text= DefaultText;
                            Panel1.Controls.Add(textBox);
                            break;
                        case 1: 
                            TextBox multiLineTextBox = new TextBox();
                            multiLineTextBox.ID = "Txt" + columnName.Replace(" ", "");
                            multiLineTextBox.CssClass = "form-control float-end";
                            multiLineTextBox.TextMode = TextBoxMode.MultiLine;
                            multiLineTextBox.Rows = 5; 
                            Panel1.Controls.Add(multiLineTextBox);
                            break;
                        case 2: 
                            DropDownList dropDownList = new DropDownList();
                            dropDownList.ID = "Ddl" + columnName.Replace(" ", "");
                            dropDownList.CssClass = "form-control float-end";
                            foreach (string choice in choices)
                            {
                                dropDownList.Items.Add(new ListItem(choice.Trim()));
                            }
                            dropDownList.Text = DefaultText;

                            Panel1.Controls.Add(dropDownList);
                            break;
                        case 3: 
                            TextBox numberTextBox = new TextBox();
                            numberTextBox.ID = "Txt" + columnName.Replace(" ", "");
                            numberTextBox.CssClass = "form-control float-end";
                            numberTextBox.Attributes["type"] = "number"; 
                            numberTextBox.Text = DefaultText;
                            numberTextBox.Attributes["min"] = minValue.ToString(); 
                            numberTextBox.Attributes["max"] = maxValue.ToString();
                            Panel1.Controls.Add(numberTextBox);
                            break;
                        case 4: 
                            TextBox dateTextBox = new TextBox();
                            dateTextBox.ID = "Txt" + columnName.Replace(" ", "");
                            dateTextBox.CssClass = "form-control float-end";
                            dateTextBox.Attributes["type"] = "date";
                            dateTextBox.Text = DefaultText;
                            Panel1.Controls.Add(dateTextBox);
                            break;
                        case 5: 
                            CheckBox checkBox = new CheckBox();
                            checkBox.ID = "Chk" + columnName.Replace(" ", "");
                            checkBox.CssClass = "form-control float-end";
                            checkBox.Checked = DefaultCheckBoxValue; 
                            Panel1.Controls.Add(checkBox);
                            break;
                        case 6: 
                            FileUpload fileUpload = new FileUpload();
                            fileUpload.ID = "File" + columnName.Replace(" ", "");
                            Panel1.Controls.Add(fileUpload);
                            break;
                        default:
                            break;
                    }

                    Panel1.Controls.Add(new LiteralControl("</div>"));
                    Panel1.Controls.Add(new LiteralControl("</div>"));
                }
            }
        }
        protected void AddDataButton_Click(object sender, EventArgs e)
        {

        }
        protected void EditButton_Click(object sender, EventArgs e)
        {

        }
        protected void DeleteClick(object sender, EventArgs e)
        {

        }
        protected void DeleteAllClick(object sender, EventArgs e)
        {

        }
    }
}