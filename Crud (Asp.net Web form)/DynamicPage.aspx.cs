using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using ListItem = System.Web.UI.WebControls.ListItem;
using AjaxControlToolkit.HtmlEditor.ToolbarButtons;

namespace Crud__Asp.net_Web_form_
{
    public partial class DynamicPage : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-J6THV9C\\SQL2019EXP;Initial Catalog=Aspnet;Integrated Security=True");
        SqlCommand com;
        static int currentModuleId;
        int employeeId = 2083;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["moduleName"] != null)
                {
                    string moduleName = Request.QueryString["moduleName"];
                    Session["ModuleName"] = moduleName.Replace(" ", "_");
                    LoadForms(moduleName);
                    GetModuleId(moduleName);
                    BindGridData(moduleName, 2083);

                    //BindColumnNamesToListBox(moduleName);
                }
            }
        }
        //protected void BindColumnNamesToListBox(string moduleName)
        //{
        //    DataTable dt = GetColumnByName(moduleName);

        //    ColumnNameCheckBoxList.Items.Clear();
        //    SelectAllCheckBox.Checked = false;

        //    foreach (DataRow row in dt.Rows)
        //    {
        //        string columnName = row.Field<string>("ColumnName");
        //        ListItem item = new ListItem(columnName);
        //        ColumnNameCheckBoxList.Items.Add(item);
        //    }
        //}

        protected void Export_Click(object sender, EventArgs e)
        {
            string exportFormat = FileFormat.SelectedValue;

            switch (exportFormat)
            {
                case "Excel":
                    ExportToExcel();
                    break;
                case "Pdf":
                    ExportToPdf();
                    break;
                default:
                    break;
            }
        }
        protected void SetEPPlusLicense()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }
        protected void ExportToExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

                for (int i = 0; i < ColumnControlData.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = ColumnControlData.Columns[i].HeaderText;
                }

                for (int row = 0; row < ColumnControlData.Rows.Count; row++)
                {
                    for (int col = 0; col < ColumnControlData.Columns.Count; col++)
                    {
                        string cellValue = ColumnControlData.Rows[row].Cells[col].Text;

                        worksheet.Cells[row + 2, col + 1].Value = cellValue;

                        string fieldName = ColumnControlData.Columns[col].HeaderText;
                    }
                }
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=ExportExcel.xlsm");
                Response.BinaryWrite(excelPackage.GetAsByteArray());
                Response.End();
            }
        }
        protected void ExportToPdf()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, memoryStream);
                pdfDoc.Open();

                PdfPTable pdfTable = new PdfPTable(ColumnControlData.Columns.Count);

                for (int i = 0; i < ColumnControlData.Columns.Count; i++)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(ColumnControlData.Columns[i].HeaderText));
                    pdfTable.AddCell(cell);
                }
                for (int row = 0; row < ColumnControlData.Rows.Count; row++)
                {
                    for (int col = 0; col < ColumnControlData.Columns.Count; col++)
                    {
                        pdfTable.AddCell(ColumnControlData.Rows[row].Cells[col].Text);
                    }
                }
                pdfDoc.Add(pdfTable);
                pdfDoc.Close();

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=ExportedData.pdf");
                Response.OutputStream.Write(memoryStream.GetBuffer(), 0, memoryStream.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.OutputStream.Close();
                Response.End();
            }
        }

        //protected void SelectAllCheckBox_CheckedChanged(object sender, EventArgs e)
        //{
        //    bool selectAll = SelectAllCheckBox.Checked;

        //    foreach (ListItem item in ColumnNameCheckBoxList.Items)
        //    {
        //        item.Selected = selectAll;
        //    }
        //}
        public void BindGridData(string moduleName, int employeeID)
        {
            DataTable dt = DisplayColumnValue(moduleName, employeeID);

            ColumnControlData.Columns.Clear();

            foreach (DataColumn column in dt.Columns)
            {
                string moduleIdName = moduleName + "ID";
                if (column.ColumnName != moduleIdName && column.ColumnName != "EMPLOYEEID" && column.ColumnName != "MODULEID")
                {
                    BoundField boundField = new BoundField();
                    boundField.DataField = column.ColumnName;
                    boundField.HeaderText = column.ColumnName;
                    ColumnControlData.Columns.Add(boundField);
                }
            }
            ColumnControlData.DataSource = dt;
            ColumnControlData.DataBind();
        }
        public DataTable DisplayColumnValue(string ModuleName, int EmployeeId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-J6THV9C\\SQL2019EXP;Initial Catalog=Aspnet;Integrated Security=True"))
            {
                using (SqlCommand com = new SqlCommand("Sp_DisplayDynamicTable", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@TableName", SqlDbType.NVarChar).Value = ModuleName;
                    com.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = EmployeeId;
                    con.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(com))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }
        public void GetModuleId(string ModuleName)
        {
            using (SqlCommand command = new SqlCommand("Sp_GetModuleId", con))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("moduleName", SqlDbType.NVarChar).Value = ModuleName;
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int moduleId = Convert.ToInt32(reader["ModuleId"]);
                    currentModuleId = moduleId;
                }
                reader.Close();
            }
        }
        public void Print_Click(object sender, EventArgs e)
        {

        }
        public DataTable GetColumnByName(string ModuleName)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-J6THV9C\\SQL2019EXP;Initial Catalog=Aspnet;Integrated Security=True"))
            {
                using (SqlCommand com = new SqlCommand("Sp_GetAllColumnDataById", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@ModuleName", SqlDbType.NVarChar).Value = ModuleName;
                    com.Parameters.Add("@ModuleDataId", SqlDbType.Int).Value = 0;
                    com.Parameters.Add("@IsActive", SqlDbType.Bit).Value = 0;
                    com.CommandTimeout = 0;
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }
        protected string DynamicTable(string tableName, int EmployeeId, int ModuleId, string TableValue)
        {
            con.Open();
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "InsertValuesIntoTable";
            com.Parameters.Add("@tableName", SqlDbType.NVarChar).Value = tableName;
            com.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = EmployeeId;
            com.Parameters.Add("@ModuleId", SqlDbType.Int).Value = ModuleId;
            com.Parameters.Add("@keyValueString", SqlDbType.NVarChar).Value = TableValue;
            com.ExecuteNonQuery();
            con.Close();
            return com.ToString();
        }
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            string value21 = "";
            Dictionary<string, List<string>> keyValuesMap = new Dictionary<string, List<string>>();
            foreach (string key in Request.Form.AllKeys)
            {
                if (!key.Contains("__"))
                {
                    string value = Request.Form[key];
                    if (value != "updatepannel|SaveBtn" && value != "Save")
                    {
                        string actualKey = key.Split('$')[0];
                        if (keyValuesMap.ContainsKey(actualKey))
                        {
                            keyValuesMap[actualKey].Add(value == "on" ? "1" : value);
                        }
                        else
                        {
                            keyValuesMap.Add(actualKey, new List<string> { value == "on" ? "1" : value });
                        }
                    }
                }
            }
            foreach (var kvp in keyValuesMap)
            {
                string concatenatedValues = string.Join(":", kvp.Value.Select(val => val.Replace(" ", " ")));
                value21 += string.IsNullOrEmpty(value21) ? "" : ",";
                value21 += kvp.Key + "=" + concatenatedValues;
            }
            DynamicTable(Session["ModuleName"].ToString(), employeeId, currentModuleId, value21);
            BindGridData(Session["ModuleName"].ToString(), 2083);
        }
        protected void LoadForms(string moduleName)
        {
            DataTable dt = GetColumnByName(moduleName);
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
                    string choiceType = row.Field<string>("ChoiceType");
                    string[] choices = choiceValue.Split(',');
                    int maximumCharacter = row.Field<int>("MaximumCharacters");
                    Panel1.Controls.Add(new LiteralControl("<div class=\"row justify-content-center align-items-center\">"));
                    Panel1.Controls.Add(new LiteralControl("<div class=\"col-md-2 mt-4 text-center\">"));
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
                            textBox.ID = columnName.Replace(" ", "");
                            textBox.CssClass = "form-control float-end";
                            textBox.MaxLength = maximumCharacter;
                            textBox.Text = DefaultText;
                            Panel1.Controls.Add(textBox);
                            break;
                        case 1:
                            TextBox multiLineTextBox = new TextBox();
                            multiLineTextBox.ID = columnName.Replace(" ", "");
                            multiLineTextBox.CssClass = "form-control float-end";
                            multiLineTextBox.TextMode = TextBoxMode.MultiLine;
                            multiLineTextBox.Rows = 5;
                            Panel1.Controls.Add(multiLineTextBox);
                            break;
                        case 2:
                            if (choiceType == "D")
                            {
                                DropDownList dropDownList = new DropDownList();
                                dropDownList.ID = columnName.Replace(" ", "");
                                dropDownList.CssClass = "form-control float-end";
                                foreach (string choice in choices)
                                {
                                    dropDownList.Items.Add(new ListItem(choice.Trim()));
                                }
                                int defaultIndex = dropDownList.Items.IndexOf(dropDownList.Items.FindByText(DefaultText));
                                if (defaultIndex >= 0)
                                {
                                    dropDownList.SelectedIndex = defaultIndex;
                                }
                                Panel1.Controls.Add(dropDownList);
                            }
                            else if (choiceType == "R")
                            {
                                Panel radioPanel = new Panel();
                                foreach (string choice in choices)
                                {
                                    RadioButton radioButton = new RadioButton();
                                    radioButton.ID = choice.Replace(" ", "");
                                    radioButton.Text = choice.Trim();
                                    radioButton.GroupName = columnName.Replace(" ", "");
                                    radioButton.CssClass = "form-check-input border-0";
                                    if (choice.Trim() == DefaultText)
                                    {
                                        radioButton.Checked = true;
                                    }
                                    radioPanel.Controls.Add(radioButton);
                                    radioPanel.Controls.Add(new LiteralControl("<br/>"));
                                }
                                Panel1.Controls.Add(radioPanel);
                            }
                            else if (choiceType == "C")
                            {
                                CheckBoxList checkBoxList = new CheckBoxList();
                                checkBoxList.ID = columnName.Replace(" ", "");
                                checkBoxList.CssClass = "form-control float-end";
                                foreach (string choice in choices)
                                {
                                    checkBoxList.Items.Add(new ListItem(choice.Trim()));
                                }
                                string[] defaultValues = DefaultText.Split(',');
                                foreach (ListItem item in checkBoxList.Items)
                                {
                                    if (defaultValues.Contains(item.Value))
                                    {
                                        item.Selected = true;
                                    }
                                }
                                Panel1.Controls.Add(checkBoxList);
                            }
                            break;
                        case 3:
                            TextBox numberTextBox = new TextBox();
                            numberTextBox.ID = columnName.Replace(" ", "");
                            numberTextBox.CssClass = "form-control float-end";
                            numberTextBox.Attributes["type"] = "number";
                            numberTextBox.Text = DefaultText;
                            numberTextBox.Attributes["min"] = minValue.ToString();
                            numberTextBox.Attributes["max"] = maxValue.ToString();
                            Panel1.Controls.Add(numberTextBox);
                            break;
                        case 4:
                            TextBox dateTextBox = new TextBox();
                            dateTextBox.ID = columnName.Replace(" ", "");
                            dateTextBox.CssClass = "form-control float-end";
                            dateTextBox.Attributes["type"] = "date";
                            dateTextBox.Text = DefaultText;
                            Panel1.Controls.Add(dateTextBox);
                            break;
                        case 5:
                            CheckBox checkBox = new CheckBox();
                            checkBox.ID = columnName.Replace(" ", "");
                            checkBox.CssClass = "form-control float-end";
                            checkBox.Checked = DefaultCheckBoxValue;
                            Panel1.Controls.Add(checkBox);
                            break;
                        case 6:
                            FileUpload fileUpload = new FileUpload();
                            fileUpload.ID = columnName.Replace(" ", "");
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