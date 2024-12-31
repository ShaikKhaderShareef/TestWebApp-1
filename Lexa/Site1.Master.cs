using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lexa
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string constring = ConfigurationManager.ConnectionStrings["LogonServices"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(constring))
                {
                    //   SqlCommand logincmd = new SqlCommand("SELECT tbllogin.empid, tbllogin.username, emp.email, tbllogin.role, emp.photo, emp.empname, emp.empjob FROM tbllogin INNER JOIN emp ON tbllogin.empid = emp.empid where tbllogin.empid=@empid ", conn);
                    SqlCommand logincmd = new SqlCommand("SELECT tbllogin.empid, tbllogin.username, emp.email, tbllogin.role, emp.photo, emp.empname, emp.empjob, tblcompany.companyname, tblcompany.companyshort FROM  tbllogin INNER JOIN  emp ON tbllogin.empid = emp.empid INNER JOIN tblcompany ON tbllogin.companyid = tblcompany.companyid WHERE (tbllogin.empid = 1136) and tbllogin.companyid = 1 ", conn);
                    //logincmd.Parameters.Add("empid", SqlDbType.VarChar).Value = Session["empid"];
                    //logincmd.Parameters.Add("companyid", SqlDbType.VarChar).Value = Session["companyid"];

                    conn.Open();

                    //if (Session["empid"] is null)
                    //{
                    //    Response.Redirect("/Login.aspx");
                    //}
                    //else
                    //{
                        //  BuildMenu(ASPxMenu1, SqlDataSource1);

                        BuildMenu();

                        using (SqlDataAdapter loginadapter = new SqlDataAdapter(logincmd))
                        {
                            DataTable logintable = new DataTable();
                            loginadapter.Fill(logintable);

                            byte[] imagem = (byte[])(logintable.Rows[0][4]);
                            string PROFILE_PIC = Convert.ToBase64String(imagem);

                            //    Image1.ImageUrl = String.Format("data:image/jpg;base64,{0}", PROFILE_PIC);
                            //   Image2.ImageUrl = String.Format("data:image/jpg;base64,{0}", PROFILE_PIC);
                            // Image2.ImageUrl = String.Format("data:image/jpg;base64,{0}", PROFILE_PIC);
                            // Image3.ImageUrl = String.Format("data:image/jpg;base64,{0}", PROFILE_PIC);
                            //  lbluser2.Text = logintable.Rows[0][5].ToString();
                            // lblemail.Text = logintable.Rows[0][2].ToString();
                            //    lblempjob.Text = logintable.Rows[0][6].ToString();
                            //   lblusername.Text = logintable.Rows[0][5].ToString();
                            //   lblusername.Text = logintable.Rows[0][5].ToString();

                        }
                   // }

                }
            }
        }

        protected void BuildMenu()
        {

            //ASPxRibbon ribbon = new ASPxRibbon();
            //ribbon.ID = "ASPxRibbon1";

            string ConnStr1 = ConfigurationManager.ConnectionStrings["LogonServices"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(ConnStr1))
            {
                String MAINSQL = "SELECT distinct Menuemps.modulename, Menuemps.sort FROM tblaccessgroup INNER JOIN  Menuemps ON tblaccessgroup.groupid = Menuemps.groupid WHERE(tblaccessgroup.empid = 1136) AND(tblaccessgroup.companyid = 1) and Menuemps.sort is not null order by Menuemps.sort, Menuemps.modulename  ";
                SqlDataAdapter TitlesAdpt11 = new SqlDataAdapter(MAINSQL, ConnStr1);
                DataSet Titles11 = new DataSet();
                // No need to open or close the connection
                //   since the SqlDataAdapter will do this automatically.
                TitlesAdpt11.Fill(Titles11);
                conn.Open();

                if (Titles11.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow Title12 in Titles11.Tables[0].Rows)
                    {
                        string empmodule = Title12[0].ToString();
                        //   int parentid_ = Convert.ToInt32(Title12[1]);

                     //   ribbon.Theme = "MetropolisBlue";                        
                        ribbon.Font.Name = "Trebuchet MS";
                        ribbon.Font.Size = 10;
                        //   ribbon.Font.Bold = true;
                        ribbon.Attributes.Add("style", "position: [top]");

                        RibbonTab tab6 = new RibbonTab(empmodule);

                        if (ribbon != null)
                        {
                            // ribbon.Tabs.Add(tab6);
                            ribbon.Tabs.Add(tab6);
                        }

                        // add groups starts
                        // String MAINSQL2 = "select distinct ribbongroup from menuemps1 where companyid = 1 and groupid = 1 and modulename = '"+empmodule+"'";
                        String MAINSQL2 = "SELECT distinct Menuemps.ribbongroup FROM tblaccessgroup INNER JOIN  Menuemps ON tblaccessgroup.groupid = Menuemps.groupid WHERE(tblaccessgroup.empid = 1136) AND(tblaccessgroup.companyid = 1) and  Menuemps.modulename = '" + empmodule + "' ";
                        SqlDataAdapter TitlesAdpt112 = new SqlDataAdapter(MAINSQL2, ConnStr1);
                        DataSet Titles112 = new DataSet();
                        // No need to open or close the connection
                        //   since the SqlDataAdapter will do this automatically.
                        TitlesAdpt112.Fill(Titles112);
                        foreach (DataRow Title122 in Titles112.Tables[0].Rows)
                        {
                            string empgroup = Title122[0].ToString();
                            //   int parentid_ = Convert.ToInt32(Title122[1]);

                            //    var YesterdayButton2 = new RibbonOptionButtonItem(empgroup, empgroup, RibbonItemSize.Small);
                            var YesterdayButton22 = new RibbonGroup(empgroup);
                            //    YesterdayButton2.OptionGroupName = "Group1";
                            YesterdayButton22.Image.IconID = "outlookinspired_shipmentawaiting_svg_16x16";
                            //   tab6.Groups.Add(dra[i].Field<string>("ribbongroup")).Items.Add(YesterdayButton2);
                            //    tab6.Groups.Add(empgroup).Items.Add(YesterdayButton2);
                            tab6.Groups.Add(YesterdayButton22);
                            // YesterdayButton2.NavigateUrl = url_;

                            //   string sqlSelect = "select item, navigateurl from menuemps1 where companyid = 1 and groupid = 1 and modulename = '"+empmodule+"' and ribbongroup = '"+empgroup+"'";
                            string sqlSelect = "SELECT Menuemps.item,  Menuemps.navigateurl FROM tblaccessgroup INNER JOIN  Menuemps ON tblaccessgroup.groupid = Menuemps.groupid WHERE(tblaccessgroup.empid = 1136) AND(tblaccessgroup.companyid = 1) and  Menuemps.modulename = '" + empmodule + "' and Menuemps.ribbongroup = '" + empgroup + "' ORDER BY Menuemps.ID";
                            SqlDataAdapter da = new SqlDataAdapter(sqlSelect, conn);
                            DataTable dt1 = new DataTable();
                            da.Fill(dt1);
                            //   TitlesAdpt22.Fill(dt1);
                            DataRow[] dra = new DataRow[dt1.Rows.Count];
                            dt1.Rows.CopyTo(dra, 0);
                            for (int i = 0; i < dra.Length; i++)
                            {
                                //  string empmodule = dra[i].Field<string>("Text");
                                //  int parentid_ = dra[i].Field<int>("ID");

                                string text_ = dra[i].Field<string>("item");
                                string url_ = dra[i].Field<string>("navigateurl");

                                var YesterdayButton2 = new RibbonOptionButtonItem(text_, text_, RibbonItemSize.Small);
                                YesterdayButton2.OptionGroupName = "Group1";
                                YesterdayButton2.SmallImage.IconID = "diagramicons_palette_svg_16x16";
                                tab6.Groups.Find(t => t.Text == empgroup).Items.Add(YesterdayButton2);
                                YesterdayButton2.NavigateUrl = url_;

                                //    string path = Server.MapPath("..");
                                System.Diagnostics.Debug.WriteLine($"Path: {url_}");

                                Page.Form.Controls.Add(ribbon);
                                Panel1.Controls.Add(ribbon);

                            }
                        }
                        // add groups ends     

                    }

                }

            }
        }
    }
}