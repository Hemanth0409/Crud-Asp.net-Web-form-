using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Crud__Asp.net_Web_form_
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-DBQ88HK\\SQLEXPRESS2019;Initial Catalog=Aspnet;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindCountry();
                BindDataToGridView();
            }

        }
        public void BindDataToGridView()
        {

            SqlCommand comm = new SqlCommand("select * from EmployeeDetails order by id", con);
            SqlDataAdapter adapter = new SqlDataAdapter(comm);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                EmpDetails.DataSource = dt;
                EmpDetails.DataBind();
            }

        }

        //protected void RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    EmpDetails.EditIndex = e.NewEditIndex;
        //    BindDataToGridView();
        //}
        //protected void UpdateRow(object sender, GridViewUpdateEventArgs e)
        //{
        //    GridViewRow gdRow = (GridViewRow)EmpDetails.Rows[e.RowIndex];
        //    HiddenField hdnId = (HiddenField)gdRow.FindControl("hdnId");

        //    con.Open();
        //    string sql = string.Format("Update EmployeeDetails set name=@Name,email=@Email,contact=@Contact,age=@Age,country=@Country,state=@State,address=@Address,Joined_Date=@Joined_Date where id='" + hdnId.Value + "'");

        //    SqlCommand comm = new SqlCommand(sql, con);
        //    comm.Parameters.AddWithValue("@Name", (EmpDetails.Rows[e.RowIndex].FindControl("name") as TextBox).Text.Trim());
        //    comm.Parameters.AddWithValue("@Email", (EmpDetails.Rows[e.RowIndex].FindControl("email") as TextBox).Text.Trim());
        //    comm.Parameters.AddWithValue("@Contact", (EmpDetails.Rows[e.RowIndex].FindControl("contact") as TextBox).Text.Trim());
        //    comm.Parameters.AddWithValue("@Age", (EmpDetails.Rows[e.RowIndex].FindControl("age") as TextBox).Text.Trim());
        //    comm.Parameters.AddWithValue("@Country", (EmpDetails.Rows[e.RowIndex].FindControl("country") as TextBox).Text.Trim());
        //    comm.Parameters.AddWithValue("@State", (EmpDetails.Rows[e.RowIndex].FindControl("state") as TextBox).Text.Trim());
        //    comm.Parameters.AddWithValue("@Address", (EmpDetails.Rows[e.RowIndex].FindControl("address") as TextBox).Text.Trim());
        //    comm.Parameters.AddWithValue("@Joined_Date", (EmpDetails.Rows[e.RowIndex].FindControl("Joined_Date") as TextBox).Text.Trim());

        //    comm.ExecuteNonQuery();
        //    EmpDetails.EditIndex = -1;
        //    BindDataToGridView();
        //    con.Close();
        //}

        //protected void CancelingEditedRow(object sender, GridViewCancelEditEventArgs e)
        //{
        //    EmpDetails.EditIndex--;
        //    BindDataToGridView();
        //}

        protected void RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow gdRow = (GridViewRow)EmpDetails.Rows[e.RowIndex];

            HiddenField hdnId = (HiddenField)gdRow.FindControl("hdnId");

            con.Open();
            string sql = string.Format("Delete from EmployeeDetails where id ={0}", hdnId.Value);
            SqlCommand comm = new SqlCommand(sql, con);
            comm.ExecuteNonQuery();
            EmpDetails.EditIndex = -1;
            BindDataToGridView();
            con.Close();
        }


        public void BindCountry()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select CountryName,CountryId from Country", con);
            SqlDataReader reader = cmd.ExecuteReader();
            Txtcountry.DataSource = reader;
            Txtcountry.Items.Clear();
            Txtcountry.Items.Add("Select a Country");
            Txtcountry.DataTextField = "CountryName";
            Txtcountry.DataValueField = "CountryId";
            Txtcountry.DataBind();
            con.Close();
        }
        public void BindState()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select StateName,StateId from State where CountryId='" + Txtcountry.SelectedValue + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            Txtstate.DataSource = reader;
            Txtstate.Items.Clear();
            Txtstate.Items.Add("Select a State");
            Txtstate.DataTextField = "StateName";
            Txtstate.DataValueField = "StateId";
            Txtstate.DataBind();
            con.Close();
        }



        protected void country_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindState();
        }

        protected void Create_Click(object sender, EventArgs e)
        {
            string gender = string.Empty;
            string cbSelect = string.Empty;
          
            if (RadioMale.Checked)
            {
                gender = RadioMale.Text;
            }
            else
            {
                gender = RadioFemale.Text;
            }


            string s =string.Empty;
            foreach (ListItem li in Language.Items)
            {
                if (li.Selected==true)
                {
                   s += li.Text;
                }
            }

            if (Session["Id"] != null)
            {
                con.Open();
                SqlCommand updatecom = new SqlCommand("Update EmployeeDetails set name=('" + TxtName.Value + "'),email=('" + Txtemail.Value + "'),contact=('" + int.Parse(TxtContact.Value) + "'),age=('" + int.Parse(Txtage.Value) + "'),Address=('" + TxtAddress.Value + "'),country=('" + Txtcountry.SelectedItem + "'),state=('" + Txtstate.SelectedItem + "'),Joined_Date=('" + Convert.ToDateTime(TxtjoinDate.Value).ToString() + "'),gender=('" + gender + "') ,Language=('" + s + "') where id='" + Session["Id"] + "'", con);
                updatecom.ExecuteNonQuery();
                con.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Successfully Updated');", true);
                BindDataToGridView();
            }
            else
            {
                //(name, email, contact, age, address, country, state, joined_Date, gender)
                con.Open();
                SqlCommand comm = new SqlCommand("Insert into EmployeeDetails values('" + TxtName.Value + "','" + Txtemail.Value + "','" + int.Parse(TxtContact.Value) + "','" + int.Parse(Txtage.Value) + "','" + TxtAddress.Value + "','" + Txtcountry.SelectedItem + "','" + Txtstate.SelectedItem + "','" + Convert.ToDateTime(TxtjoinDate.Value) + "','" + gender + "','" + s + "')", con);
                comm.ExecuteNonQuery();
                con.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Successfully Inserted');", true);
                BindDataToGridView();

            }
            Reset_Click(sender,e);

        }
        protected void Search_Click(object sender, EventArgs e)
        {


            string searchQuery = "SELECT * FROM EmployeeDetails where 1=1 ";

            if (!string.IsNullOrEmpty(searchText.Value))
            {
                searchQuery += " AND (Name LIKE '%" + searchText.Value + "%' OR Email LIKE '%" + searchText.Value + "%' OR Age LIKE '%" + searchText.Value + "%' OR Country LIKE '%" + searchText.Value + "%')";
            }
            SqlCommand comm = new SqlCommand(searchQuery, con);
            SqlDataAdapter adapter = new SqlDataAdapter(comm);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            EmpDetails.DataSource = dt;
            EmpDetails.DataBind();

        }


        protected void Reset_Click(object sender, EventArgs e)
        {
            TxtName.Value = string.Empty;
            Txtemail.Value = string.Empty;
            Txtage.Value = string.Empty;
            TxtContact.Value = string.Empty;
            TxtAddress.Value = string.Empty;
            TxtjoinDate.Value = string.Empty;
            RadioMale.Checked = false;
            RadioFemale.Checked = false;    
            Txtcountry.ClearSelection();
            Txtstate.ClearSelection();
            foreach (ListItem li in Language.Items)
            {
                li.Selected= false;
            }

        }
        protected void EditButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            HiddenField hdnId = (HiddenField)row.FindControl("hdnId");
            Session["Id"] = hdnId.Value;
            con.Open();
            SqlCommand comm = new SqlCommand("Select * from EmployeeDetails where Id='" + hdnId.Value + "'", con);
            SqlDataReader sqlDataReader = comm.ExecuteReader();
            while (sqlDataReader.Read())
            {
                TxtName.Value = sqlDataReader.GetValue(1).ToString();
                Txtemail.Value = sqlDataReader.GetValue(2).ToString();
                Txtage.Value = sqlDataReader.GetValue(4).ToString();
                TxtContact.Value = sqlDataReader.GetValue(3).ToString();
                Txtcountry.SelectedItem.Text = sqlDataReader.GetValue(6).ToString();
                Txtstate.SelectedItem.Text = sqlDataReader.GetValue(7).ToString();
                TxtAddress.Value = sqlDataReader.GetValue(5).ToString();
                TxtjoinDate.Value = Convert.ToDateTime(sqlDataReader.GetValue(8)).Date.ToString("yyyy-MM-dd");
                string gender = sqlDataReader.GetValue(9).ToString();
                string language =sqlDataReader.GetValue(10).ToString();

                if (language.Equals("English"))
                {

                }

                if (gender.Equals("Male"))
                {
                    RadioMale.Checked = true;
                }
                else if (gender.Equals("Female"))
                {
                    RadioFemale.Checked = true;
                }
            }
            con.Close();
        }


    }
}