using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lexa.Submittals
{
    public partial class Selproject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SuccessProject.Visible = false;
            if (!IsPostBack)
            {

                string constring = ConfigurationManager.ConnectionStrings["EstimationNewConnectionString"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(constring))
                {
                    //   SqlCommand cmd22 = new SqlCommand("select max(revid) from ArchitectDrawings where projectid = '" + projectidtxt2.Text + "' and submittal = '" + submittaltxt2.Text + "' and workscope = '" + workscopetxt2.Text + "' ", conn);

                    SqlCommand revnocmd = new SqlCommand("SELECT tblprojectselect.slno, tblprojectselect.empid, tblprojectselect.projectid, newproject.projectname FROM  tblprojectselect INNER JOIN  newproject ON tblprojectselect.projectid = newproject.projectid where tblprojectselect.empid = '" + Session["empid"] + "' ", conn);

                    SqlCommand cmd33 = new SqlCommand("select count(empid) from tblprojectselect where empid = '" + Session["empid"] + "'", conn);
                    conn.Open();

                    int count11 = Convert.ToInt32(cmd33.ExecuteScalar());

                    using (SqlDataReader rdr = revnocmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {

                            TextBox2.Text = rdr["projectid"].ToString();

                        }
                    }

                }

            }
        }

        protected void ASPxFormLayout1_E6_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["EstimationNewConnectionString"].ConnectionString;
                //SqlCommand cmd33 = new SqlCommand("select count(tid) from ArchitectDrawings where projectid = '" + ASPxFormLayout1_E1.Value + "' and transid = '" + TransID_E2.Text + "' and revno = '" + ASPxFormLayout1_E4.Text + "' ", conn);

                SqlCommand cmd33 = new SqlCommand("select count(empid) from tblprojectselect where empid = '" + Session["empid"] + "'", conn);
                conn.Open();

                int count11 = Convert.ToInt32(cmd33.ExecuteScalar());

                if (count11 == 1)
                {
                    String query1 = "update tblprojectselect set projectid=@projectid where empid=@empid";
                    string constring1 = ConfigurationManager.ConnectionStrings["EstimationNewConnectionString"].ConnectionString;
                    SqlConnection con1 = new SqlConnection(constring1);
                    con1.Open();
                    SqlCommand pidcmd11 = new SqlCommand();

                    pidcmd11.Parameters.AddWithValue("@projectid", cmbprojectid.Value);
                    //   pidcmd11.Parameters.AddWithValue("@projectname", projectnametxt.Text);
                    pidcmd11.Parameters.AddWithValue("@empid", Session["empid"]);

                    pidcmd11.CommandText = query1;
                    pidcmd11.Connection = con1;
                    pidcmd11.ExecuteNonQuery();

                    SuccessProject.Visible = true;
                    // UpdateProject.Visible = true;
                    //  this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('Updated..', 'Project Updated Successfully ...!!!', 'success');", true);
                    ASPxFormLayout1.DataBind();

                    SqlCommand revnocmd = new SqlCommand("SELECT tblprojectselect.slno, tblprojectselect.empid, tblprojectselect.projectid, newproject.projectname FROM  tblprojectselect INNER JOIN  newproject ON tblprojectselect.projectid = newproject.projectid where tblprojectselect.empid = '" + Session["empid"] + "' ", conn);

                    SqlCommand cmd333 = new SqlCommand("select count(empid) from tblprojectselect where empid = '" + Session["empid"] + "'", conn);
                    //  conn.Open();

                    int count111 = Convert.ToInt32(cmd333.ExecuteScalar());

                    using (SqlDataReader rdr = revnocmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            TextBox2.Text = rdr["projectid"].ToString();

                        }
                    }

                    //  ProjectkeySqlDataSource1.DataBind();
                    //  ASPxGridView2.DataBind();
                }
                else if (count11 == 0)
                {

                    String query1 = "insert into tblprojectselect (empid,projectid) values (@empid, @projectid)";
                    string constring1 = ConfigurationManager.ConnectionStrings["EstimationNewConnectionString"].ConnectionString;
                    SqlConnection con1 = new SqlConnection(constring1);
                    con1.Open();
                    SqlCommand pidcmd11 = new SqlCommand();

                    pidcmd11.Parameters.AddWithValue("@projectid", cmbprojectid.Value);
                    //   pidcmd11.Parameters.AddWithValue("@projectname", projectnametxt.Text);
                    pidcmd11.Parameters.AddWithValue("@empid", Session["empid"]);

                    pidcmd11.CommandText = query1;
                    pidcmd11.Connection = con1;
                    pidcmd11.ExecuteNonQuery();

                    SuccessProject.Visible = true;
                    //   this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('Updated..', 'Project Updated Successfully ...!!!', 'success');", true);
                    ASPxFormLayout1.DataBind();

                    SqlCommand revnocmd = new SqlCommand("SELECT tblprojectselect.slno, tblprojectselect.empid, tblprojectselect.projectid, newproject.projectname FROM  tblprojectselect INNER JOIN  newproject ON tblprojectselect.projectid = newproject.projectid where tblprojectselect.empid = '" + Session["empid"] + "' ", conn);

                    SqlCommand cmd333 = new SqlCommand("select count(empid) from tblprojectselect where empid = '" + Session["empid"] + "'", conn);
                    //  conn.Open();

                    int count111 = Convert.ToInt32(cmd333.ExecuteScalar());

                    using (SqlDataReader rdr = revnocmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            TextBox2.Text = rdr["projectid"].ToString();

                        }
                    }

                    //   ProjectkeySqlDataSource1.DataBind();
                    //  ASPxGridView2.DataBind();

                }

            }
        }

        protected void cmbprojectid_TextChanged(object sender, EventArgs e)
        {
            TextBox1.Text = cmbprojectid.Visible.ToString();
        }
    }
}