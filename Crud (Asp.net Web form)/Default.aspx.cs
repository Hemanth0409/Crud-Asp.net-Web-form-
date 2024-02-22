using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Crud__Asp.net_Web_form_
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LodRecord();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-J6THV9C\\SQL2019EXP;Initial Catalog=Aspnet;Integrated Security=True;Trust Server Certificate=True");


        public void LodRecord()
        {

            SqlCommand comm = new SqlCommand("select * from EmpData", con);
            SqlDataAdapter adapter = new SqlDataAdapter(comm);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void Insert_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand comm = new SqlCommand("Insert into EmpData values('" + int.Parse(TextBox1.Text) + "','" + (TextBox2.Text) + "','" + int.Parse(TextBox3.Text) + "','" + int.Parse(TextBox4.Text) + "','" + DropDownList1.SelectedValue + "')", con);
            comm.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Successfully Inserted');", true);
            LodRecord();
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand comm = new SqlCommand("Update EmpData set Name='" + (TextBox2.Text) + "',Salary='" + int.Parse(TextBox3.Text) + "',Contact='" + Int64.Parse(TextBox4.Text) + "',Age='" + DropDownList1.SelectedValue + "'where id='" + int.Parse(TextBox1.Text) + "'", con);
            comm.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Successfully Updated');", true);
            LodRecord();
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand comm = new SqlCommand("Delete from  EmpData where id='" + int.Parse(TextBox1.Text) + "'", con);
            comm.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Successfully Deleted');", true);
            LodRecord();
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            SqlCommand comm = new SqlCommand("Select *  from  EmpData where id='" + int.Parse(TextBox1.Text) + "'", con);
            SqlDataAdapter adapter = new SqlDataAdapter(comm);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GetData_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand comm = new SqlCommand("Select *  from  EmpData where id='" + int.Parse(TextBox1.Text) + "'", con);
            SqlDataReader sqlDataReader = comm.ExecuteReader();
            while (sqlDataReader.Read())
            {
                TextBox2.Text = sqlDataReader.GetValue(1).ToString();
                TextBox3.Text = sqlDataReader.GetValue(2).ToString();
                TextBox4.Text = sqlDataReader.GetValue(3).ToString();
                DropDownList1.SelectedValue = sqlDataReader.GetValue(4).ToString();


            }
        }
    }
}