using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
         //   EmployeeHireUpdate1();
         //   sendattemail();
        }

        public void sendvacationemail1()
        {
            string lblvacdate = DateTime.Now.ToString("yyyy-MM-dd");

            //  string lblvacdate = "2019-08-14";

            //  string connstring1 = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            String ConnStr1 = "Data Source=attsrvr-01.eastdeltasa.com;Network Library=DBMSSOCN;Initial Catalog=eastdelta2;User ID=sqladmin;Password=sql@admin;";
            using (SqlConnection conn61 = new SqlConnection(ConnStr1))
            {
                String SQL = "SELECT zhr_emp.empid, projtab.empname, projtab.empjob, projtab.iqama, projtab.empnation, projtab.empmobile, projtab.projectid, projtab.projectname, zhr_emp.date_in, zhr_emp.action_date, zhr_emp.status, zhr_emp.eindicator, emp.email, projtab.projectlocation FROM zhr_emp INNER JOIN projtab ON zhr_emp.empid = projtab.empid INNER JOIN emp ON zhr_emp.empid = emp.empid WHERE zhr_emp.status = 'Vacation' and (zhr_emp.date_in  = '" + lblvacdate + "') AND (zhr_emp.eindicator = 0 and zhr_emp.indicator = 0) ";

                SqlDataAdapter TitlesAdpt1 = new SqlDataAdapter(SQL, ConnStr1);
                DataSet Titles1 = new DataSet();
                // No need to open or close the connection
                //   since the SqlDataAdapter will do this automatically.
                TitlesAdpt1.Fill(Titles1);

                conn61.Open();

                //  GridView1.DataSource = Titles1.Tables[0];
                //  GridView1.DataBind();

                if (Titles1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow Title1 in Titles1.Tables[0].Rows)
                    {
                        string location = Title1[13].ToString();
                        if (location == "Riyadh")
                        {
                            // if ((row.FindControl("chkSelect") as CheckBox).Checked)
                            //{
                            string status = Title1[6].ToString();
                            //  int empid = Convert.ToInt32(row.Cells[0].Text);


                            //if (status == "Vacation")
                            //{
                            //CheckBox status = (row.Cells[0].FindControl("CheckBox2") as CheckBox);
                            int empid = Convert.ToInt32(Title1[0]);
                            string projectname = Title1[7].ToString();
                            string empname = Title1[1].ToString();
                            string iqama = Title1[3].ToString();
                            string empjob = Title1[2].ToString();
                            string datein = Title1[8].ToString();
                            string actiondate = Title1[9].ToString();
                            string empnation = Title1[4].ToString();
                            string projectid = Title1[6].ToString();
                            string mobile = Title1[5].ToString();
                            int emailindicator = Convert.ToInt32(Title1[11]);
                            string emailid = Title1[12].ToString();

                            //  string constring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                            using (SqlConnection conn = new SqlConnection(ConnStr1))
                            {

                                string empvac = "select count(empid) from tblnotify where category = 'Employee Vacation' and reportdate = '" + lblvacdate + "' and empid='" + empid + "'";
                                string empvacemail = "select count(empid) from tblnotify where category = 'Vacation Email' and reportdate = '" + lblvacdate + "' and empid='" + empid + "'";
                                conn.Open();

                                SqlCommand zhrempvac = new SqlCommand(empvac, conn);
                                int countempvac = Convert.ToInt32(zhrempvac.ExecuteScalar());

                                SqlCommand zhrempvacemail = new SqlCommand(empvacemail, conn);
                                int countempvacemail = Convert.ToInt32(zhrempvacemail.ExecuteScalar());

                                if (countempvac == 0)
                                {
                                    string moveemptoprojstr = "update projtab set projectid=@projectid, projectname = @projectname, devicename = @devicename , deviceslno = @deviceslno , sitengname = @siteengname , siteengmobile = @siteengmobile , siteengemail = @siteengemail, projectlocation = @projectlocation, workschedule=@workschedule, wsid=@wsid  where empid = @empid";
                                    string insertstr = "insert into tempproject (projectid, projectname, deviceslno, devicename, sitengname, siteengmobile, siteengemail, empid, empname, iqama, empjob, empnation, empmobile, projectlocation, date, fromprojectname,username) values (@projectid1, @projectname1, @deviceslno1, @devicename1, @siteengname1, @siteengmobile1, @siteengemail1, @empid1, @empname1, @iqama1, @empjob1, @empnation1, @empmobile1, @projectlocation1, @date1, @fromprojectname1, @username) ";
                                    string movementstr = "insert into tblmoveto (empid, toprojectid,fromprojectid, reportdate, username,flag) values (@empid2, @toprojectid, @fromprojectid, @reportdate, @username, @flag) ";
                                    SqlCommand moveempstoprojcmd = new SqlCommand(moveemptoprojstr, conn);
                                    SqlCommand insertstrcmd = new SqlCommand(insertstr, conn);
                                    SqlCommand movementstrcmd = new SqlCommand(movementstr, conn);

                                    moveempstoprojcmd.Parameters.AddWithValue("@empid", empid);
                                    moveempstoprojcmd.Parameters.AddWithValue("@projectid", "PROJ-VACRUH");
                                    moveempstoprojcmd.Parameters.AddWithValue("@projectname", "EDSCO VACATIONS RUH");
                                    moveempstoprojcmd.Parameters.AddWithValue("@projectlocation", "Riyadh");
                                    moveempstoprojcmd.Parameters.AddWithValue("@devicename", "RUH.BranchOffice.M15");
                                    moveempstoprojcmd.Parameters.AddWithValue("@deviceslno", "2425683040030");
                                    moveempstoprojcmd.Parameters.AddWithValue("@siteengname", "Fekri Mahmoud Ali Mohamed Ibrahim");
                                    moveempstoprojcmd.Parameters.AddWithValue("@siteengmobile", "+966581075922");
                                    moveempstoprojcmd.Parameters.AddWithValue("@siteengemail", "Fekry@eastdeltasa.com");
                                    moveempstoprojcmd.Parameters.AddWithValue("@workschedule", "JED Head Office 08:00-18:00");
                                    moveempstoprojcmd.Parameters.AddWithValue("@wsid", "1");

                                    insertstrcmd.Parameters.AddWithValue("@projectid1", "PROJ-VACRUH");
                                    insertstrcmd.Parameters.AddWithValue("@projectname1", "EDSCO VACATIONS RUH");
                                    insertstrcmd.Parameters.AddWithValue("@projectlocation1", "Riyadh");
                                    insertstrcmd.Parameters.AddWithValue("@devicename1", "RUH.BranchOffice.M15");
                                    insertstrcmd.Parameters.AddWithValue("@deviceslno1", "2425683040030");
                                    insertstrcmd.Parameters.AddWithValue("@siteengname1", "Fekri Mahmoud Ali Mohamed Ibrahim");
                                    insertstrcmd.Parameters.AddWithValue("@siteengmobile1", "+966581075922");
                                    insertstrcmd.Parameters.AddWithValue("@siteengemail1", "Fekry@eastdeltasa.com");
                                    insertstrcmd.Parameters.AddWithValue("@empid1", empid);
                                    insertstrcmd.Parameters.AddWithValue("@empname1", empname);
                                    insertstrcmd.Parameters.AddWithValue("@iqama1", iqama);
                                    insertstrcmd.Parameters.AddWithValue("@empjob1", empjob);
                                    insertstrcmd.Parameters.AddWithValue("@empnation1", empnation);
                                    insertstrcmd.Parameters.AddWithValue("@empmobile1", mobile);
                                    insertstrcmd.Parameters.AddWithValue("@fromprojectname1", projectname);
                                    insertstrcmd.Parameters.AddWithValue("@date1", DateTime.Now.ToString("dd-MMM-yyyy"));
                                    insertstrcmd.Parameters.AddWithValue("@username", "SYSTEM");

                                    movementstrcmd.Parameters.AddWithValue("@empid2", empid);
                                    movementstrcmd.Parameters.AddWithValue("@toprojectid", "PROJ-VACRUH");
                                    movementstrcmd.Parameters.AddWithValue("@fromprojectid", projectid);
                                    movementstrcmd.Parameters.AddWithValue("@reportdate", DateTime.Now.ToString("yyyy-MM-dd"));
                                    movementstrcmd.Parameters.AddWithValue("@username", "SYSTEM");
                                    movementstrcmd.Parameters.AddWithValue("@flag", "0");

                                    //  conn.Open();

                                    moveempstoprojcmd.ExecuteNonQuery();
                                    insertstrcmd.ExecuteNonQuery();
                                    movementstrcmd.ExecuteNonQuery();
                                    //Errmsgmoveempslabel.Text = "Employee No. " + AddEmps_empidtxt.Text + "  Successfully Moved To " + AddEmps_searchprojecttxt.Text + " Project ";

                                    //insert entry in notification table starts

                                    string zhratt = "update ZHR_TB_ATT_A set PROJECTNAME = @projectname, DEVICENAME = @devicename , PROJECTLOCATION = @projectlocation, WSID=@wsid, WSNAME_PMO=@wsname, TYPE=@type  where PERNR = @empid1 and TDATE=@TDATE ";
                                    //string zhratt1 = "update ZHR_TB_ATT_A set PROJECTNAME = @projectname, DEVICENAME = @devicename , PROJECTLOCATION = @projectlocation, WSID=@wsid, WSNAME_PMO=@wsname, TYPE=@type  where PERNR = @empid and TDATE=@TDATE1";

                                    string totalshedule = "update tbltotalschedule set projectid=@projectid, projectname = @projectname, devicename = @devicename , projectlocation = @projectlocation, wsid=@wsid, workschedule=@workschedule, sitengname=@siteengname1,siteengemail=@siteengemail1, changedate=@changedate where empid = @empid and dailydate=@dailydate";
                                    string indicator = "update zhr_emp set indicator='1' where empid= '" + empid + "' ";

                                    //string totalshedule1 = "update tbltotalschedule set projectid=@projectid, projectname = @projectname, devicename = @devicename , projectlocation = @projectlocation, wsid=@wsid, workschedule=@workschedule, sitengname=@siteengname1,siteengemail=@siteengemail1, changedate=@changedate where empid = @empid and dailydate=@dailydate";

                                    SqlCommand zhrattcmd = new SqlCommand(zhratt, conn);
                                    SqlCommand zhrindicator = new SqlCommand(indicator, conn);
                                    //  SqlCommand zhrattcmd1 = new SqlCommand(zhratt1, conn1);

                                    SqlCommand totalshedulecmd = new SqlCommand(totalshedule, conn);
                                    //  SqlCommand totalshedulecmd1 = new SqlCommand(totalshedule1, conn1);

                                    // zhrattcmd.Parameters.AddWithValue("@empid", empid);
                                    zhrattcmd.Parameters.AddWithValue("@empid1", empid);
                                    zhrattcmd.Parameters.AddWithValue("@projectname", "EDSCO VACATIONS RUH");
                                    zhrattcmd.Parameters.AddWithValue("@projectlocation", "Riyadh");
                                    zhrattcmd.Parameters.AddWithValue("@devicename", "RUH.BranchOffice.M15");
                                    zhrattcmd.Parameters.AddWithValue("@wsid", "1");
                                    zhrattcmd.Parameters.AddWithValue("@wsname", "JED Head Office 08:00-18:00");
                                    zhrattcmd.Parameters.AddWithValue("@type", "D");
                                    zhrattcmd.Parameters.AddWithValue("@TDATE", lblvacdate);


                                    totalshedulecmd.Parameters.AddWithValue("@empid", empid);
                                    totalshedulecmd.Parameters.AddWithValue("@projectid", "PROJ-VACRUH");
                                    totalshedulecmd.Parameters.AddWithValue("@projectname", "EDSCO VACATIONS");
                                    totalshedulecmd.Parameters.AddWithValue("@projectlocation", "Riyadh");
                                    totalshedulecmd.Parameters.AddWithValue("@devicename", "RUH.BranchOffice.M15");
                                    totalshedulecmd.Parameters.AddWithValue("@wsid", "1");
                                    totalshedulecmd.Parameters.AddWithValue("@workschedule", "JED Head Office 08:00-18:00");
                                    totalshedulecmd.Parameters.AddWithValue("@dailydate", lblvacdate);
                                    totalshedulecmd.Parameters.AddWithValue("@changedate", lblvacdate);
                                    totalshedulecmd.Parameters.AddWithValue("@siteengname1", "Fekri Mahmoud Ali Mohamed Ibrahim");
                                    totalshedulecmd.Parameters.AddWithValue("@siteengemail1", "Fekry@eastdeltasa.com");

                                    //   conn1.Open();

                                    zhrattcmd.ExecuteNonQuery();
                                    zhrindicator.ExecuteNonQuery();
                                    // zhrattcmd1.ExecuteNonQuery();
                                    totalshedulecmd.ExecuteNonQuery();
                                    // totalshedulecmd1.ExecuteNonQuery();

                                    //Errmsgmoveempslabel.Text = "Employee No. " + AddEmps_empidtxt.Text + "  Successfully Moved To " + AddEmps_searchprojecttxt.Text + " Project ";

                                    //insert entry in notification table starts
                                    string reportstr1 = "INSERT into tblnotify (empid, action, category,username,reportdate,entrydate) values (@empid, @action, @category,@username,@reportdate,@entrydate)";

                                    SqlCommand reportstrcmd1 = new SqlCommand(reportstr1, conn);

                                    reportstrcmd1.Parameters.AddWithValue("@empid", empid);
                                    reportstrcmd1.Parameters.AddWithValue("@action", "Emp No. " + empid + "  Successfully Moved From " + projectname + " To EDSCO VACATION RUH Project ");
                                    reportstrcmd1.Parameters.AddWithValue("@category", "Employee Vacation");
                                    reportstrcmd1.Parameters.AddWithValue("@username", "SYSTEM");
                                    reportstrcmd1.Parameters.AddWithValue("@reportdate", lblvacdate);
                                    reportstrcmd1.Parameters.AddWithValue("@entrydate", DateTime.Now);
                                    reportstrcmd1.ExecuteNonQuery();

                                    //insert entry in notification table ends

                                    // update PMS notifications starts
                                    String ConnStr116 = "Data Source=192.168.1.85;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sqladmin;Password=Sqlpms@123456789;";
                                    using (SqlConnection conn616 = new SqlConnection(ConnStr116))
                                    {
                                        string reportstr15 = "INSERT into tblnotify (revno, action, category,username,reportdate,entrydate) values (@revno, @action, @category,@username,@reportdate,@entrydate)";
                                        SqlCommand reportstrcmd15 = new SqlCommand(reportstr15, conn616);
                                        reportstrcmd15.Parameters.AddWithValue("@revno", empid);
                                        reportstrcmd15.Parameters.AddWithValue("@action", "Emp No. " + empid + "  Successfully Moved From " + projectname + " To EDSCO VACATION RUH Project ");
                                        reportstrcmd15.Parameters.AddWithValue("@category", "Employee Vacation");
                                        reportstrcmd15.Parameters.AddWithValue("@username", "SYSTEM");
                                        reportstrcmd15.Parameters.AddWithValue("@reportdate", lblvacdate);
                                        reportstrcmd15.Parameters.AddWithValue("@entrydate", DateTime.Now);
                                        conn616.Open();
                                        reportstrcmd15.ExecuteNonQuery();

                                    }
                                    // update PMS notifications Ends      
                                }

                                if (emailid != "" && countempvacemail == 0)
                                {
                                    // vacation emails start

                                    string body = "";
                                    string body1 = "";
                                    String ConnStr = "Data Source=attsrvr-01.eastdeltasa.com;Network Library=DBMSSOCN;Initial Catalog=eastdelta2;User ID=sqladmin;Password=sql@admin;";
                                    //String SQL = "SELECT slno, reportname FROM dailyattreport where reportdate='2018-09-02'";
                                    //  String SQL = "SELECT [TDATE], [PERNR], [ENAME], [CHECKIN], [CHECKOUT], [NEXTDAYIN], [NEXTDAYOUT], [PROJECTNAME], [PROJECTLOCATION], [PMO_REMARK], [SITE_REMARK], [AREA_SP_REMARK] FROM [ZHR_TB_ATT_A] WHERE (([TDATE] = '" + HRAttdatetxt.Text + "') AND ([RECORD] = 'T') AND ([PMO_REMARK] = 'Absent') AND PROJECTNAME='" + projectname + "') ORDER BY PROJECTNAME,PROJECTLOCATION";
                                    String SQL2 = "SELECT zhr_emp.empid, projtab.empname, projtab.iqama, projtab.empjob, projtab.empnation, projtab.empmobile, projtab.projectname, zhr_emp.date_in, zhr_emp.date_out, zhr_emp.status FROM zhr_emp INNER JOIN projtab ON zhr_emp.empid = projtab.empid INNER JOIN emp ON zhr_emp.empid = emp.empid WHERE zhr_emp.empid = '" + empid + "' and (zhr_emp.date_in  = '" + lblvacdate + "') AND (zhr_emp.eindicator = 0) and zhr_emp .status  = 'Vacation' and emp.email <> '' ";

                                    SqlDataAdapter TitlesAdpt2 = new SqlDataAdapter(SQL2, ConnStr);
                                    DataSet Titles2 = new DataSet();
                                    // No need to open or close the connection
                                    //   since the SqlDataAdapter will do this automatically.
                                    TitlesAdpt2.Fill(Titles2);
                                    //  body1 += "Dear Eng. " + empname + ",<br /><br /> Kindly find the below ABSENT records for today, Please check the list and add your comments If any, with note that sanction list automatically applied.<br /><br /> NOTE: This email is Auto generated from the system. So Please don't reply to this email. <br /><br />";
                                    body1 += "Dear Sir, <br /><br /> This is an auto-generated email to notify that the below user is leaving for Vacation/Business trip as per the below mentioned dates. <br /><br /> NOTE: Out of office and Email forwarding to be enabled for this user. <br /><br />";

                                    body = "<table border=" + 1 + " cellpadding=" + 1 + " cellspacing=" + 1 + " font-family=Segoe UI>";
                                    body += "<tr bgcolor='LightGreen' font-family='Century Gothic'>";
                                    body += "<td>EMPID</td>";
                                    body += "<td>EMPLOYEE NAME</td>";
                                    body += "<td>IQAMA</td>";
                                    body += "<td>PROFESSION</td>";
                                    body += "<td>NATIONALITY</td>";
                                    body += "<td>MOBILE</td>";
                                    body += "<td>PROJECT NAME</td>";
                                    body += "<td>START DATE</td>";
                                    body += "<td>END DATE</td>";
                                    body += "<td>STATUS</td>";

                                    body += "</tr>";

                                    foreach (DataRow Title in Titles2.Tables[0].Rows)
                                    {
                                        body += "<tr>";
                                        // body += "<td>" + Title[0] + "</td>";
                                        body += "<td>" + Title[0] + "</td>";
                                        //body += "<td>" + String.Format("{0:c}", Title[0]) + "</td>";
                                        body += "<td>" + String.Format("{0:c}", Title[1]) + "</td>";
                                        body += "<td>" + String.Format("{0:c}", Title[2]) + "</td>";
                                        body += "<td>" + String.Format("{0:c}", Title[3]) + "</td>";
                                        body += "<td>" + String.Format("{0:c}", Title[4]) + "</td>";
                                        body += "<td>" + String.Format("{0:c}", Title[5]) + "</td>";
                                        body += "<td>" + String.Format("{0:c}", Title[6]) + "</td>";
                                        body += "<td>" + String.Format("{0:c}", Title[7]) + "</td>";
                                        body += "<td>" + String.Format("{0:c}", Title[8]) + "</td>";
                                        //body += "<td>" + String.Format("{0:c}", Title[8]) + "</td>";
                                        body += "<td>" + Title[9] + "</td>";
                                        body += "</tr>";
                                    }
                                    body += "</table>";

                                    //now set up the mail settings
                                    MailMessage message = new MailMessage();
                                    message.IsBodyHtml = true;
                                    //  message.From = new MailAddress(NewTextBox222.Text);
                                    message.From = new MailAddress("PMS-Attendance@eastdeltasa.com");
                                    //can add more recipient
                                    // message.To.Add(new MailAddress(emailid));


                                    message.To.Add(new MailAddress("support@eastdeltasa.com"));

                                    //  message.To.Add(new MailAddress("khadershareef@eastdeltasa.com"));
                                    //add cc         

                                    message.CC.Add(new MailAddress("khadershareef@eastdeltasa.com"));
                                    message.CC.Add(new MailAddress(emailid));

                                    message.Subject = " Auto Email Notification of Vacation Employees dated " + DateTime.Now;
                                    message.Body = body1 + body;

                                    //SmtpClient client = new SmtpClient();
                                    //client.Send(message);

                                    SmtpClient smtp = new SmtpClient();
                                    smtp.Host = "mail.eastdeltasa.com";
                                    smtp.Port = 587;
                                    System.Net.NetworkCredential credential = new System.Net.NetworkCredential("khadershareef@eastdeltasa.com", "admin@123");
                                    smtp.UseDefaultCredentials = false;
                                    smtp.Credentials = credential;
                                    smtp.Send(message);
                                    message = null;
                                    // lbltechHR1.Text = "VOX REDSEA SUBMITTAL MAIL SENT SUCCESSFULLY...!!!";

                                    //  string constring11 = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                                    using (SqlConnection conn1 = new SqlConnection(ConnStr))
                                    {
                                        string indicator = "update zhr_emp set eindicator='1' where empid = '" + empid + "' ";
                                        string reportstr1 = "INSERT into tblnotify (empid, action, category,username,reportdate,entrydate) values (@empid, @action, @category,@username,@reportdate,@entrydate)";

                                        conn1.Open();

                                        SqlCommand zhrindicator = new SqlCommand(indicator, conn1);
                                        zhrindicator.ExecuteNonQuery();


                                        SqlCommand reportstrcmd1 = new SqlCommand(reportstr1, conn1);

                                        reportstrcmd1.Parameters.AddWithValue("@empid", empid);
                                        reportstrcmd1.Parameters.AddWithValue("@action", "Successfully Sent Vacation Emails dated From " + lblvacdate + " ");
                                        reportstrcmd1.Parameters.AddWithValue("@category", "Vacation Email");
                                        reportstrcmd1.Parameters.AddWithValue("@username", "SYSTEM");
                                        reportstrcmd1.Parameters.AddWithValue("@reportdate", lblvacdate);
                                        reportstrcmd1.Parameters.AddWithValue("@entrydate", DateTime.Now);
                                        reportstrcmd1.ExecuteNonQuery();
                                    }
                                    //vacation email ends    


                                    // update PMS notifications starts
                                    String ConnStr116 = "Data Source=192.168.1.85;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sqladmin;Password=Sqlpms@123456789;";
                                    using (SqlConnection conn616 = new SqlConnection(ConnStr116))
                                    {
                                        string reportstr15 = "INSERT into tblnotify (revno, action, category,username,reportdate,entrydate) values (@revno, @action, @category,@username,@reportdate,@entrydate)";
                                        SqlCommand reportstrcmd15 = new SqlCommand(reportstr15, conn616);
                                        reportstrcmd15.Parameters.AddWithValue("@revno", empid);
                                        reportstrcmd15.Parameters.AddWithValue("@action", "Successfully Sent Vacation Emails dated From " + lblvacdate + " ");
                                        reportstrcmd15.Parameters.AddWithValue("@category", "Vacation Email");
                                        reportstrcmd15.Parameters.AddWithValue("@username", "SYSTEM");
                                        reportstrcmd15.Parameters.AddWithValue("@reportdate", lblvacdate);
                                        reportstrcmd15.Parameters.AddWithValue("@entrydate", DateTime.Now);
                                        conn616.Open();
                                        reportstrcmd15.ExecuteNonQuery();

                                    }
                                    // update PMS notifications Ends      
                                }
                            }
                        }
                        else if (location == "Dammam")
                        {
                            // if ((row.FindControl("chkSelect") as CheckBox).Checked)
                            //{
                            string status = Title1[6].ToString();
                            //  int empid = Convert.ToInt32(row.Cells[0].Text);


                            //if (status == "Vacation")
                            //{
                            //CheckBox status = (row.Cells[0].FindControl("CheckBox2") as CheckBox);
                            int empid = Convert.ToInt32(Title1[0]);
                            string projectname = Title1[7].ToString();
                            string empname = Title1[1].ToString();
                            string iqama = Title1[3].ToString();
                            string empjob = Title1[2].ToString();
                            string datein = Title1[8].ToString();
                            string actiondate = Title1[9].ToString();
                            string empnation = Title1[4].ToString();
                            string projectid = Title1[6].ToString();
                            string mobile = Title1[5].ToString();
                            int emailindicator = Convert.ToInt32(Title1[11]);
                            string emailid = Title1[12].ToString();

                            //  string constring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                            using (SqlConnection conn = new SqlConnection(ConnStr1))
                            {

                                string empvac = "select count(empid) from tblnotify where category = 'Employee Vacation' and reportdate = '" + lblvacdate + "' and empid='" + empid + "'";
                                string empvacemail = "select count(empid) from tblnotify where category = 'Vacation Email' and reportdate = '" + lblvacdate + "' and empid='" + empid + "'";
                                conn.Open();

                                SqlCommand zhrempvac = new SqlCommand(empvac, conn);
                                int countempvac = Convert.ToInt32(zhrempvac.ExecuteScalar());

                                SqlCommand zhrempvacemail = new SqlCommand(empvacemail, conn);
                                int countempvacemail = Convert.ToInt32(zhrempvacemail.ExecuteScalar());

                                if (countempvac == 0)
                                {
                                    string moveemptoprojstr = "update projtab set projectid=@projectid, projectname = @projectname, devicename = @devicename , deviceslno = @deviceslno , sitengname = @siteengname , siteengmobile = @siteengmobile , siteengemail = @siteengemail, projectlocation = @projectlocation, workschedule=@workschedule, wsid=@wsid  where empid = @empid";
                                    string insertstr = "insert into tempproject (projectid, projectname, deviceslno, devicename, sitengname, siteengmobile, siteengemail, empid, empname, iqama, empjob, empnation, empmobile, projectlocation, date, fromprojectname,username) values (@projectid1, @projectname1, @deviceslno1, @devicename1, @siteengname1, @siteengmobile1, @siteengemail1, @empid1, @empname1, @iqama1, @empjob1, @empnation1, @empmobile1, @projectlocation1, @date1, @fromprojectname1, @username) ";
                                    string movementstr = "insert into tblmoveto (empid, toprojectid,fromprojectid, reportdate, username,flag) values (@empid2, @toprojectid, @fromprojectid, @reportdate, @username, @flag) ";
                                    SqlCommand moveempstoprojcmd = new SqlCommand(moveemptoprojstr, conn);
                                    SqlCommand insertstrcmd = new SqlCommand(insertstr, conn);
                                    SqlCommand movementstrcmd = new SqlCommand(movementstr, conn);

                                    moveempstoprojcmd.Parameters.AddWithValue("@empid", empid);
                                    moveempstoprojcmd.Parameters.AddWithValue("@projectid", "PROJ-VACDAM");
                                    moveempstoprojcmd.Parameters.AddWithValue("@projectname", "EDSCO VACATIONS DAM");
                                    moveempstoprojcmd.Parameters.AddWithValue("@projectlocation", "Dammam");
                                    moveempstoprojcmd.Parameters.AddWithValue("@devicename", "DAM.Bra.Office.M06");
                                    moveempstoprojcmd.Parameters.AddWithValue("@deviceslno", "2425683040001");
                                    moveempstoprojcmd.Parameters.AddWithValue("@siteengname", "Mohammed Ahmed Alkhidhr Ali");
                                    moveempstoprojcmd.Parameters.AddWithValue("@siteengmobile", "544118915");
                                    moveempstoprojcmd.Parameters.AddWithValue("@siteengemail", "alkhedr@eastdeltasa.com");
                                    moveempstoprojcmd.Parameters.AddWithValue("@workschedule", "Dammam Office 08:00-18:00");
                                    moveempstoprojcmd.Parameters.AddWithValue("@wsid", "3");

                                    insertstrcmd.Parameters.AddWithValue("@projectid1", "PROJ-VACDAM");
                                    insertstrcmd.Parameters.AddWithValue("@projectname1", "EDSCO VACATIONS DAM");
                                    insertstrcmd.Parameters.AddWithValue("@projectlocation1", "Dammam");
                                    insertstrcmd.Parameters.AddWithValue("@devicename1", "DAM.Bra.Office.M06");
                                    insertstrcmd.Parameters.AddWithValue("@deviceslno1", "2425683040001");
                                    insertstrcmd.Parameters.AddWithValue("@siteengname1", "Mohammed Ahmed Alkhidhr Ali");
                                    insertstrcmd.Parameters.AddWithValue("@siteengmobile1", "544118915");
                                    insertstrcmd.Parameters.AddWithValue("@siteengemail1", "alkhedr@eastdeltasa.com");
                                    insertstrcmd.Parameters.AddWithValue("@empid1", empid);
                                    insertstrcmd.Parameters.AddWithValue("@empname1", empname);
                                    insertstrcmd.Parameters.AddWithValue("@iqama1", iqama);
                                    insertstrcmd.Parameters.AddWithValue("@empjob1", empjob);
                                    insertstrcmd.Parameters.AddWithValue("@empnation1", empnation);
                                    insertstrcmd.Parameters.AddWithValue("@empmobile1", mobile);
                                    insertstrcmd.Parameters.AddWithValue("@fromprojectname1", projectname);
                                    insertstrcmd.Parameters.AddWithValue("@date1", DateTime.Now.ToString("dd-MMM-yyyy"));
                                    insertstrcmd.Parameters.AddWithValue("@username", "SYSTEM");

                                    movementstrcmd.Parameters.AddWithValue("@empid2", empid);
                                    movementstrcmd.Parameters.AddWithValue("@toprojectid", "PROJ-VACDAM");
                                    movementstrcmd.Parameters.AddWithValue("@fromprojectid", projectid);
                                    movementstrcmd.Parameters.AddWithValue("@reportdate", DateTime.Now.ToString("yyyy-MM-dd"));
                                    movementstrcmd.Parameters.AddWithValue("@username", "SYSTEM");
                                    movementstrcmd.Parameters.AddWithValue("@flag", "0");

                                    //  conn.Open();

                                    moveempstoprojcmd.ExecuteNonQuery();
                                    insertstrcmd.ExecuteNonQuery();
                                    movementstrcmd.ExecuteNonQuery();
                                    //Errmsgmoveempslabel.Text = "Employee No. " + AddEmps_empidtxt.Text + "  Successfully Moved To " + AddEmps_searchprojecttxt.Text + " Project ";

                                    //insert entry in notification table starts

                                    string zhratt = "update ZHR_TB_ATT_A set PROJECTNAME = @projectname, DEVICENAME = @devicename , PROJECTLOCATION = @projectlocation, WSID=@wsid, WSNAME_PMO=@wsname, TYPE=@type  where PERNR = @empid1 and TDATE=@TDATE ";
                                    //string zhratt1 = "update ZHR_TB_ATT_A set PROJECTNAME = @projectname, DEVICENAME = @devicename , PROJECTLOCATION = @projectlocation, WSID=@wsid, WSNAME_PMO=@wsname, TYPE=@type  where PERNR = @empid and TDATE=@TDATE1";

                                    string totalshedule = "update tbltotalschedule set projectid=@projectid, projectname = @projectname, devicename = @devicename , projectlocation = @projectlocation, wsid=@wsid, workschedule=@workschedule, sitengname=@siteengname1,siteengemail=@siteengemail1, changedate=@changedate where empid = @empid and dailydate=@dailydate";
                                    string indicator = "update zhr_emp set indicator='1' where empid= '" + empid + "' ";

                                    //string totalshedule1 = "update tbltotalschedule set projectid=@projectid, projectname = @projectname, devicename = @devicename , projectlocation = @projectlocation, wsid=@wsid, workschedule=@workschedule, sitengname=@siteengname1,siteengemail=@siteengemail1, changedate=@changedate where empid = @empid and dailydate=@dailydate";

                                    SqlCommand zhrattcmd = new SqlCommand(zhratt, conn);
                                    SqlCommand zhrindicator = new SqlCommand(indicator, conn);
                                    //  SqlCommand zhrattcmd1 = new SqlCommand(zhratt1, conn1);

                                    SqlCommand totalshedulecmd = new SqlCommand(totalshedule, conn);
                                    //  SqlCommand totalshedulecmd1 = new SqlCommand(totalshedule1, conn1);

                                    // zhrattcmd.Parameters.AddWithValue("@empid", empid);
                                    zhrattcmd.Parameters.AddWithValue("@empid1", empid);
                                    zhrattcmd.Parameters.AddWithValue("@projectname", "EDSCO VACATIONS DAM");
                                    zhrattcmd.Parameters.AddWithValue("@projectlocation", "Dammam");
                                    zhrattcmd.Parameters.AddWithValue("@devicename", "DAM.Bra.Office.M06");
                                    zhrattcmd.Parameters.AddWithValue("@wsid", "3");
                                    zhrattcmd.Parameters.AddWithValue("@wsname", "Dammam Office 08:00-18:00");
                                    zhrattcmd.Parameters.AddWithValue("@type", "D");
                                    zhrattcmd.Parameters.AddWithValue("@TDATE", lblvacdate);


                                    totalshedulecmd.Parameters.AddWithValue("@empid", empid);
                                    totalshedulecmd.Parameters.AddWithValue("@projectid", "PROJ-VACDAM");
                                    totalshedulecmd.Parameters.AddWithValue("@projectname", "EDSCO VACATIONS DAM");
                                    totalshedulecmd.Parameters.AddWithValue("@projectlocation", "Dammam");
                                    totalshedulecmd.Parameters.AddWithValue("@devicename", "DAM.Bra.Office.M06");
                                    totalshedulecmd.Parameters.AddWithValue("@wsid", "3");
                                    totalshedulecmd.Parameters.AddWithValue("@workschedule", "Dammam Office 08:00-18:00");
                                    totalshedulecmd.Parameters.AddWithValue("@dailydate", lblvacdate);
                                    totalshedulecmd.Parameters.AddWithValue("@changedate", lblvacdate);
                                    totalshedulecmd.Parameters.AddWithValue("@siteengname1", "Mohammed Ahmed Alkhidhr Ali");
                                    totalshedulecmd.Parameters.AddWithValue("@siteengemail1", "alkhedr@eastdeltasa.com");

                                    //   conn1.Open();

                                    zhrattcmd.ExecuteNonQuery();
                                    zhrindicator.ExecuteNonQuery();
                                    // zhrattcmd1.ExecuteNonQuery();
                                    totalshedulecmd.ExecuteNonQuery();
                                    // totalshedulecmd1.ExecuteNonQuery();

                                    //Errmsgmoveempslabel.Text = "Employee No. " + AddEmps_empidtxt.Text + "  Successfully Moved To " + AddEmps_searchprojecttxt.Text + " Project ";

                                    //insert entry in notification table starts
                                    string reportstr1 = "INSERT into tblnotify (empid, action, category,username,reportdate,entrydate) values (@empid, @action, @category,@username,@reportdate,@entrydate)";

                                    SqlCommand reportstrcmd1 = new SqlCommand(reportstr1, conn);

                                    reportstrcmd1.Parameters.AddWithValue("@empid", empid);
                                    reportstrcmd1.Parameters.AddWithValue("@action", "Emp No. " + empid + "  Successfully Moved From " + projectname + " To EDSCO VACATION DAM Project ");
                                    reportstrcmd1.Parameters.AddWithValue("@category", "Employee Vacation");
                                    reportstrcmd1.Parameters.AddWithValue("@username", "SYSTEM");
                                    reportstrcmd1.Parameters.AddWithValue("@reportdate", lblvacdate);
                                    reportstrcmd1.Parameters.AddWithValue("@entrydate", DateTime.Now);
                                    reportstrcmd1.ExecuteNonQuery();

                                    //insert entry in notification table ends

                                    // update PMS notifications starts
                                    String ConnStr116 = "Data Source=192.168.1.85;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sqladmin;Password=Sqlpms@123456789;";
                                    using (SqlConnection conn616 = new SqlConnection(ConnStr116))
                                    {
                                        string reportstr15 = "INSERT into tblnotify (revno, action, category,username,reportdate,entrydate) values (@revno, @action, @category,@username,@reportdate,@entrydate)";
                                        SqlCommand reportstrcmd15 = new SqlCommand(reportstr15, conn616);
                                        reportstrcmd15.Parameters.AddWithValue("@revno", empid);
                                        reportstrcmd15.Parameters.AddWithValue("@action", "Emp No. " + empid + "  Successfully Moved From " + projectname + " To EDSCO VACATION DAM Project ");
                                        reportstrcmd15.Parameters.AddWithValue("@category", "Employee Vacation");
                                        reportstrcmd15.Parameters.AddWithValue("@username", "SYSTEM");
                                        reportstrcmd15.Parameters.AddWithValue("@reportdate", lblvacdate);
                                        reportstrcmd15.Parameters.AddWithValue("@entrydate", DateTime.Now);
                                        conn616.Open();
                                        reportstrcmd15.ExecuteNonQuery();

                                    }
                                    // update PMS notifications Ends      
                                }

                                if (emailid != "" && countempvacemail == 0)
                                {
                                    // vacation emails start

                                    string body = "";
                                    string body1 = "";
                                    String ConnStr = "Data Source=attsrvr-01.eastdeltasa.com;Network Library=DBMSSOCN;Initial Catalog=eastdelta2;User ID=sqladmin;Password=sql@admin;";
                                    //String SQL = "SELECT slno, reportname FROM dailyattreport where reportdate='2018-09-02'";
                                    //  String SQL = "SELECT [TDATE], [PERNR], [ENAME], [CHECKIN], [CHECKOUT], [NEXTDAYIN], [NEXTDAYOUT], [PROJECTNAME], [PROJECTLOCATION], [PMO_REMARK], [SITE_REMARK], [AREA_SP_REMARK] FROM [ZHR_TB_ATT_A] WHERE (([TDATE] = '" + HRAttdatetxt.Text + "') AND ([RECORD] = 'T') AND ([PMO_REMARK] = 'Absent') AND PROJECTNAME='" + projectname + "') ORDER BY PROJECTNAME,PROJECTLOCATION";
                                    String SQL2 = "SELECT zhr_emp.empid, projtab.empname, projtab.iqama, projtab.empjob, projtab.empnation, projtab.empmobile, projtab.projectname, zhr_emp.date_in, zhr_emp.date_out, zhr_emp.status FROM zhr_emp INNER JOIN projtab ON zhr_emp.empid = projtab.empid INNER JOIN emp ON zhr_emp.empid = emp.empid WHERE zhr_emp.empid = '" + empid + "' and (zhr_emp.date_in  = '" + lblvacdate + "') AND (zhr_emp.eindicator = 0) and zhr_emp .status  = 'Vacation' and emp.email <> '' ";

                                    SqlDataAdapter TitlesAdpt2 = new SqlDataAdapter(SQL2, ConnStr);
                                    DataSet Titles2 = new DataSet();
                                    // No need to open or close the connection
                                    //   since the SqlDataAdapter will do this automatically.
                                    TitlesAdpt2.Fill(Titles2);
                                    //  body1 += "Dear Eng. " + empname + ",<br /><br /> Kindly find the below ABSENT records for today, Please check the list and add your comments If any, with note that sanction list automatically applied.<br /><br /> NOTE: This email is Auto generated from the system. So Please don't reply to this email. <br /><br />";
                                    body1 += "Dear Sir, <br /><br /> This is an auto-generated email to notify that the below user is leaving for Vacation/Business trip as per the below mentioned dates. <br /><br /> NOTE: Out of office and Email forwarding to be enabled for this user. <br /><br />";

                                    body = "<table border=" + 1 + " cellpadding=" + 1 + " cellspacing=" + 1 + " font-family=Segoe UI>";
                                    body += "<tr bgcolor='LightGreen' font-family='Century Gothic'>";
                                    body += "<td>EMPID</td>";
                                    body += "<td>EMPLOYEE NAME</td>";
                                    body += "<td>IQAMA</td>";
                                    body += "<td>PROFESSION</td>";
                                    body += "<td>NATIONALITY</td>";
                                    body += "<td>MOBILE</td>";
                                    body += "<td>PROJECT NAME</td>";
                                    body += "<td>START DATE</td>";
                                    body += "<td>END DATE</td>";
                                    body += "<td>STATUS</td>";

                                    body += "</tr>";

                                    foreach (DataRow Title in Titles2.Tables[0].Rows)
                                    {
                                        body += "<tr>";
                                        // body += "<td>" + Title[0] + "</td>";
                                        body += "<td>" + Title[0] + "</td>";
                                        //body += "<td>" + String.Format("{0:c}", Title[0]) + "</td>";
                                        body += "<td>" + String.Format("{0:c}", Title[1]) + "</td>";
                                        body += "<td>" + String.Format("{0:c}", Title[2]) + "</td>";
                                        body += "<td>" + String.Format("{0:c}", Title[3]) + "</td>";
                                        body += "<td>" + String.Format("{0:c}", Title[4]) + "</td>";
                                        body += "<td>" + String.Format("{0:c}", Title[5]) + "</td>";
                                        body += "<td>" + String.Format("{0:c}", Title[6]) + "</td>";
                                        body += "<td>" + String.Format("{0:c}", Title[7]) + "</td>";
                                        body += "<td>" + String.Format("{0:c}", Title[8]) + "</td>";
                                        //body += "<td>" + String.Format("{0:c}", Title[8]) + "</td>";
                                        body += "<td>" + Title[9] + "</td>";
                                        body += "</tr>";
                                    }
                                    body += "</table>";

                                    //now set up the mail settings
                                    MailMessage message = new MailMessage();
                                    message.IsBodyHtml = true;
                                    //  message.From = new MailAddress(NewTextBox222.Text);
                                    message.From = new MailAddress("PMS-Attendance@eastdeltasa.com");
                                    //can add more recipient
                                    // message.To.Add(new MailAddress(emailid));


                                    message.To.Add(new MailAddress("support@eastdeltasa.com"));

                                    //  message.To.Add(new MailAddress("khadershareef@eastdeltasa.com"));
                                    //add cc         

                                    message.CC.Add(new MailAddress("khadershareef@eastdeltasa.com"));
                                    message.CC.Add(new MailAddress(emailid));

                                    message.Subject = " Auto Email Notification of Vacation Employees dated " + DateTime.Now;
                                    message.Body = body1 + body;

                                    //SmtpClient client = new SmtpClient();
                                    //client.Send(message);

                                    SmtpClient smtp = new SmtpClient();
                                    smtp.Host = "mail.eastdeltasa.com";
                                    smtp.Port = 587;
                                    System.Net.NetworkCredential credential = new System.Net.NetworkCredential("khadershareef@eastdeltasa.com", "admin@123");
                                    smtp.UseDefaultCredentials = false;
                                    smtp.Credentials = credential;
                                    smtp.Send(message);
                                    message = null;
                                    // lbltechHR1.Text = "VOX REDSEA SUBMITTAL MAIL SENT SUCCESSFULLY...!!!";

                                    //  string constring11 = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                                    using (SqlConnection conn1 = new SqlConnection(ConnStr))
                                    {
                                        string indicator = "update zhr_emp set eindicator='1' where empid = '" + empid + "' ";
                                        string reportstr1 = "INSERT into tblnotify (empid, action, category,username,reportdate,entrydate) values (@empid, @action, @category,@username,@reportdate,@entrydate)";

                                        conn1.Open();

                                        SqlCommand zhrindicator = new SqlCommand(indicator, conn1);
                                        zhrindicator.ExecuteNonQuery();


                                        SqlCommand reportstrcmd1 = new SqlCommand(reportstr1, conn1);

                                        reportstrcmd1.Parameters.AddWithValue("@empid", empid);
                                        reportstrcmd1.Parameters.AddWithValue("@action", "Successfully Sent Vacation Emails dated From " + lblvacdate + " ");
                                        reportstrcmd1.Parameters.AddWithValue("@category", "Vacation Email");
                                        reportstrcmd1.Parameters.AddWithValue("@username", "SYSTEM");
                                        reportstrcmd1.Parameters.AddWithValue("@reportdate", lblvacdate);
                                        reportstrcmd1.Parameters.AddWithValue("@entrydate", DateTime.Now);
                                        reportstrcmd1.ExecuteNonQuery();
                                    }
                                    //vacation email ends    


                                    // update PMS notifications starts
                                    String ConnStr116 = "Data Source=192.168.1.85;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sqladmin;Password=Sqlpms@123456789;";
                                    using (SqlConnection conn616 = new SqlConnection(ConnStr116))
                                    {
                                        string reportstr15 = "INSERT into tblnotify (revno, action, category,username,reportdate,entrydate) values (@revno, @action, @category,@username,@reportdate,@entrydate)";
                                        SqlCommand reportstrcmd15 = new SqlCommand(reportstr15, conn616);
                                        reportstrcmd15.Parameters.AddWithValue("@revno", empid);
                                        reportstrcmd15.Parameters.AddWithValue("@action", "Successfully Sent Vacation Emails dated From " + lblvacdate + " ");
                                        reportstrcmd15.Parameters.AddWithValue("@category", "Vacation Email");
                                        reportstrcmd15.Parameters.AddWithValue("@username", "SYSTEM");
                                        reportstrcmd15.Parameters.AddWithValue("@reportdate", lblvacdate);
                                        reportstrcmd15.Parameters.AddWithValue("@entrydate", DateTime.Now);
                                        conn616.Open();
                                        reportstrcmd15.ExecuteNonQuery();

                                    }
                                    // update PMS notifications Ends      
                                }
                            }
                        }
                        else if (location == "Abha")
                        {
                            // if ((row.FindControl("chkSelect") as CheckBox).Checked)
                            //{
                            string status = Title1[6].ToString();
                            //  int empid = Convert.ToInt32(row.Cells[0].Text);


                            //if (status == "Vacation")
                            //{
                            //CheckBox status = (row.Cells[0].FindControl("CheckBox2") as CheckBox);
                            int empid = Convert.ToInt32(Title1[0]);
                            string projectname = Title1[7].ToString();
                            string empname = Title1[1].ToString();
                            string iqama = Title1[3].ToString();
                            string empjob = Title1[2].ToString();
                            string datein = Title1[8].ToString();
                            string actiondate = Title1[9].ToString();
                            string empnation = Title1[4].ToString();
                            string projectid = Title1[6].ToString();
                            string mobile = Title1[5].ToString();
                            int emailindicator = Convert.ToInt32(Title1[11]);
                            string emailid = Title1[12].ToString();

                            //  string constring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                            using (SqlConnection conn = new SqlConnection(ConnStr1))
                            {

                                string empvac = "select count(empid) from tblnotify where category = 'Employee Vacation' and reportdate = '" + lblvacdate + "' and empid='" + empid + "'";
                                string empvacemail = "select count(empid) from tblnotify where category = 'Vacation Email' and reportdate = '" + lblvacdate + "' and empid='" + empid + "'";
                                conn.Open();

                                SqlCommand zhrempvac = new SqlCommand(empvac, conn);
                                int countempvac = Convert.ToInt32(zhrempvac.ExecuteScalar());

                                SqlCommand zhrempvacemail = new SqlCommand(empvacemail, conn);
                                int countempvacemail = Convert.ToInt32(zhrempvacemail.ExecuteScalar());

                                if (countempvac == 0)
                                {
                                    string moveemptoprojstr = "update projtab set projectid=@projectid, projectname = @projectname, devicename = @devicename , deviceslno = @deviceslno , sitengname = @siteengname , siteengmobile = @siteengmobile , siteengemail = @siteengemail, projectlocation = @projectlocation, workschedule=@workschedule, wsid=@wsid  where empid = @empid";
                                    string insertstr = "insert into tempproject (projectid, projectname, deviceslno, devicename, sitengname, siteengmobile, siteengemail, empid, empname, iqama, empjob, empnation, empmobile, projectlocation, date, fromprojectname,username) values (@projectid1, @projectname1, @deviceslno1, @devicename1, @siteengname1, @siteengmobile1, @siteengemail1, @empid1, @empname1, @iqama1, @empjob1, @empnation1, @empmobile1, @projectlocation1, @date1, @fromprojectname1, @username) ";
                                    string movementstr = "insert into tblmoveto (empid, toprojectid,fromprojectid, reportdate, username,flag) values (@empid2, @toprojectid, @fromprojectid, @reportdate, @username, @flag) ";
                                    SqlCommand moveempstoprojcmd = new SqlCommand(moveemptoprojstr, conn);
                                    SqlCommand insertstrcmd = new SqlCommand(insertstr, conn);
                                    SqlCommand movementstrcmd = new SqlCommand(movementstr, conn);

                                    moveempstoprojcmd.Parameters.AddWithValue("@empid", empid);
                                    moveempstoprojcmd.Parameters.AddWithValue("@projectid", "PROJ-VACABH");
                                    moveempstoprojcmd.Parameters.AddWithValue("@projectname", "EDSCO VACATIONS ABH");
                                    moveempstoprojcmd.Parameters.AddWithValue("@projectlocation", "Abha");
                                    moveempstoprojcmd.Parameters.AddWithValue("@devicename", "");
                                    moveempstoprojcmd.Parameters.AddWithValue("@deviceslno", "");
                                    moveempstoprojcmd.Parameters.AddWithValue("@siteengname", "Mohamed ElAraby Abdou Shahin");
                                    moveempstoprojcmd.Parameters.AddWithValue("@siteengmobile", "544109605");
                                    moveempstoprojcmd.Parameters.AddWithValue("@siteengemail", "mohamedelaraby@eastdeltasa.com");
                                    moveempstoprojcmd.Parameters.AddWithValue("@workschedule", "JED Head Office 08:00-18:00");
                                    moveempstoprojcmd.Parameters.AddWithValue("@wsid", "1");

                                    insertstrcmd.Parameters.AddWithValue("@projectid1", "PROJ-VACABH");
                                    insertstrcmd.Parameters.AddWithValue("@projectname1", "EDSCO VACATIONS ABH");
                                    insertstrcmd.Parameters.AddWithValue("@projectlocation1", "Abha");
                                    insertstrcmd.Parameters.AddWithValue("@devicename1", "");
                                    insertstrcmd.Parameters.AddWithValue("@deviceslno1", "");
                                    insertstrcmd.Parameters.AddWithValue("@siteengname1", "Mohamed ElAraby Abdou Shahin");
                                    insertstrcmd.Parameters.AddWithValue("@siteengmobile1", "544109605");
                                    insertstrcmd.Parameters.AddWithValue("@siteengemail1", "mohamedelaraby@eastdeltasa.com");
                                    insertstrcmd.Parameters.AddWithValue("@empid1", empid);
                                    insertstrcmd.Parameters.AddWithValue("@empname1", empname);
                                    insertstrcmd.Parameters.AddWithValue("@iqama1", iqama);
                                    insertstrcmd.Parameters.AddWithValue("@empjob1", empjob);
                                    insertstrcmd.Parameters.AddWithValue("@empnation1", empnation);
                                    insertstrcmd.Parameters.AddWithValue("@empmobile1", mobile);
                                    insertstrcmd.Parameters.AddWithValue("@fromprojectname1", projectname);
                                    insertstrcmd.Parameters.AddWithValue("@date1", DateTime.Now.ToString("dd-MMM-yyyy"));
                                    insertstrcmd.Parameters.AddWithValue("@username", "SYSTEM");

                                    movementstrcmd.Parameters.AddWithValue("@empid2", empid);
                                    movementstrcmd.Parameters.AddWithValue("@toprojectid", "PROJ-VACABH");
                                    movementstrcmd.Parameters.AddWithValue("@fromprojectid", projectid);
                                    movementstrcmd.Parameters.AddWithValue("@reportdate", DateTime.Now.ToString("yyyy-MM-dd"));
                                    movementstrcmd.Parameters.AddWithValue("@username", "SYSTEM");
                                    movementstrcmd.Parameters.AddWithValue("@flag", "0");

                                    //  conn.Open();

                                    moveempstoprojcmd.ExecuteNonQuery();
                                    insertstrcmd.ExecuteNonQuery();
                                    movementstrcmd.ExecuteNonQuery();
                                    //Errmsgmoveempslabel.Text = "Employee No. " + AddEmps_empidtxt.Text + "  Successfully Moved To " + AddEmps_searchprojecttxt.Text + " Project ";

                                    //insert entry in notification table starts

                                    string zhratt = "update ZHR_TB_ATT_A set PROJECTNAME = @projectname, DEVICENAME = @devicename , PROJECTLOCATION = @projectlocation, WSID=@wsid, WSNAME_PMO=@wsname, TYPE=@type  where PERNR = @empid1 and TDATE=@TDATE ";
                                    //string zhratt1 = "update ZHR_TB_ATT_A set PROJECTNAME = @projectname, DEVICENAME = @devicename , PROJECTLOCATION = @projectlocation, WSID=@wsid, WSNAME_PMO=@wsname, TYPE=@type  where PERNR = @empid and TDATE=@TDATE1";

                                    string totalshedule = "update tbltotalschedule set projectid=@projectid, projectname = @projectname, devicename = @devicename , projectlocation = @projectlocation, wsid=@wsid, workschedule=@workschedule, sitengname=@siteengname1,siteengemail=@siteengemail1, changedate=@changedate where empid = @empid and dailydate=@dailydate";
                                    string indicator = "update zhr_emp set indicator='1' where empid= '" + empid + "' ";

                                    //string totalshedule1 = "update tbltotalschedule set projectid=@projectid, projectname = @projectname, devicename = @devicename , projectlocation = @projectlocation, wsid=@wsid, workschedule=@workschedule, sitengname=@siteengname1,siteengemail=@siteengemail1, changedate=@changedate where empid = @empid and dailydate=@dailydate";

                                    SqlCommand zhrattcmd = new SqlCommand(zhratt, conn);
                                    SqlCommand zhrindicator = new SqlCommand(indicator, conn);
                                    //  SqlCommand zhrattcmd1 = new SqlCommand(zhratt1, conn1);

                                    SqlCommand totalshedulecmd = new SqlCommand(totalshedule, conn);
                                    //  SqlCommand totalshedulecmd1 = new SqlCommand(totalshedule1, conn1);

                                    // zhrattcmd.Parameters.AddWithValue("@empid", empid);
                                    zhrattcmd.Parameters.AddWithValue("@empid1", empid);
                                    zhrattcmd.Parameters.AddWithValue("@projectname", "EDSCO VACATIONS ABH");
                                    zhrattcmd.Parameters.AddWithValue("@projectlocation", "Abha");
                                    zhrattcmd.Parameters.AddWithValue("@devicename", "");
                                    zhrattcmd.Parameters.AddWithValue("@wsid", "1");
                                    zhrattcmd.Parameters.AddWithValue("@wsname", "JED Head Office 08:00-18:00");
                                    zhrattcmd.Parameters.AddWithValue("@type", "D");
                                    zhrattcmd.Parameters.AddWithValue("@TDATE", lblvacdate);


                                    totalshedulecmd.Parameters.AddWithValue("@empid", empid);
                                    totalshedulecmd.Parameters.AddWithValue("@projectid", "PROJ-VACABH");
                                    totalshedulecmd.Parameters.AddWithValue("@projectname", "EDSCO VACATIONS ABH");
                                    totalshedulecmd.Parameters.AddWithValue("@projectlocation", "Abha");
                                    totalshedulecmd.Parameters.AddWithValue("@devicename", "");
                                    totalshedulecmd.Parameters.AddWithValue("@wsid", "1");
                                    totalshedulecmd.Parameters.AddWithValue("@workschedule", "JED Head Office 08:00-18:00");
                                    totalshedulecmd.Parameters.AddWithValue("@dailydate", lblvacdate);
                                    totalshedulecmd.Parameters.AddWithValue("@changedate", lblvacdate);
                                    totalshedulecmd.Parameters.AddWithValue("@siteengname1", "Mohamed ElAraby Abdou Shahin");
                                    totalshedulecmd.Parameters.AddWithValue("@siteengemail1", "mohamedelaraby@eastdeltasa.com");

                                    //   conn1.Open();

                                    zhrattcmd.ExecuteNonQuery();
                                    zhrindicator.ExecuteNonQuery();
                                    // zhrattcmd1.ExecuteNonQuery();
                                    totalshedulecmd.ExecuteNonQuery();
                                    // totalshedulecmd1.ExecuteNonQuery();

                                    //Errmsgmoveempslabel.Text = "Employee No. " + AddEmps_empidtxt.Text + "  Successfully Moved To " + AddEmps_searchprojecttxt.Text + " Project ";

                                    //insert entry in notification table starts
                                    string reportstr1 = "INSERT into tblnotify (empid, action, category,username,reportdate,entrydate) values (@empid, @action, @category,@username,@reportdate,@entrydate)";

                                    SqlCommand reportstrcmd1 = new SqlCommand(reportstr1, conn);

                                    reportstrcmd1.Parameters.AddWithValue("@empid", empid);
                                    reportstrcmd1.Parameters.AddWithValue("@action", "Emp No. " + empid + "  Successfully Moved From " + projectname + " To EDSCO VACATION ABHA Project ");
                                    reportstrcmd1.Parameters.AddWithValue("@category", "Employee Vacation");
                                    reportstrcmd1.Parameters.AddWithValue("@username", "SYSTEM");
                                    reportstrcmd1.Parameters.AddWithValue("@reportdate", lblvacdate);
                                    reportstrcmd1.Parameters.AddWithValue("@entrydate", DateTime.Now);
                                    reportstrcmd1.ExecuteNonQuery();

                                    //insert entry in notification table ends

                                    // update PMS notifications starts
                                    String ConnStr116 = "Data Source=192.168.1.85;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sqladmin;Password=Sqlpms@123456789;";
                                    using (SqlConnection conn616 = new SqlConnection(ConnStr116))
                                    {
                                        string reportstr15 = "INSERT into tblnotify (revno, action, category,username,reportdate,entrydate) values (@revno, @action, @category,@username,@reportdate,@entrydate)";
                                        SqlCommand reportstrcmd15 = new SqlCommand(reportstr15, conn616);
                                        reportstrcmd15.Parameters.AddWithValue("@revno", empid);
                                        reportstrcmd15.Parameters.AddWithValue("@action", "Emp No. " + empid + "  Successfully Moved From " + projectname + " To EDSCO VACATION ABHA Project ");
                                        reportstrcmd15.Parameters.AddWithValue("@category", "Employee Vacation");
                                        reportstrcmd15.Parameters.AddWithValue("@username", "SYSTEM");
                                        reportstrcmd15.Parameters.AddWithValue("@reportdate", lblvacdate);
                                        reportstrcmd15.Parameters.AddWithValue("@entrydate", DateTime.Now);
                                        conn616.Open();
                                        reportstrcmd15.ExecuteNonQuery();

                                    }
                                    // update PMS notifications Ends      
                                }

                                if (emailid != "" && countempvacemail == 0)
                                {
                                    // vacation emails start

                                    string body = "";
                                    string body1 = "";
                                    String ConnStr = "Data Source=attsrvr-01.eastdeltasa.com;Network Library=DBMSSOCN;Initial Catalog=eastdelta2;User ID=sqladmin;Password=sql@admin;";
                                    //String SQL = "SELECT slno, reportname FROM dailyattreport where reportdate='2018-09-02'";
                                    //  String SQL = "SELECT [TDATE], [PERNR], [ENAME], [CHECKIN], [CHECKOUT], [NEXTDAYIN], [NEXTDAYOUT], [PROJECTNAME], [PROJECTLOCATION], [PMO_REMARK], [SITE_REMARK], [AREA_SP_REMARK] FROM [ZHR_TB_ATT_A] WHERE (([TDATE] = '" + HRAttdatetxt.Text + "') AND ([RECORD] = 'T') AND ([PMO_REMARK] = 'Absent') AND PROJECTNAME='" + projectname + "') ORDER BY PROJECTNAME,PROJECTLOCATION";
                                    String SQL2 = "SELECT zhr_emp.empid, projtab.empname, projtab.iqama, projtab.empjob, projtab.empnation, projtab.empmobile, projtab.projectname, zhr_emp.date_in, zhr_emp.date_out, zhr_emp.status FROM zhr_emp INNER JOIN projtab ON zhr_emp.empid = projtab.empid INNER JOIN emp ON zhr_emp.empid = emp.empid WHERE zhr_emp.empid = '" + empid + "' and (zhr_emp.date_in  = '" + lblvacdate + "') AND (zhr_emp.eindicator = 0) and zhr_emp .status  = 'Vacation' and emp.email <> '' ";

                                    SqlDataAdapter TitlesAdpt2 = new SqlDataAdapter(SQL2, ConnStr);
                                    DataSet Titles2 = new DataSet();
                                    // No need to open or close the connection
                                    //   since the SqlDataAdapter will do this automatically.
                                    TitlesAdpt2.Fill(Titles2);
                                    //  body1 += "Dear Eng. " + empname + ",<br /><br /> Kindly find the below ABSENT records for today, Please check the list and add your comments If any, with note that sanction list automatically applied.<br /><br /> NOTE: This email is Auto generated from the system. So Please don't reply to this email. <br /><br />";
                                    body1 += "Dear Sir, <br /><br /> This is an auto-generated email to notify that the below user is leaving for Vacation/Business trip as per the below mentioned dates. <br /><br /> NOTE: Out of office and Email forwarding to be enabled for this user. <br /><br />";

                                    body = "<table border=" + 1 + " cellpadding=" + 1 + " cellspacing=" + 1 + " font-family=Segoe UI>";
                                    body += "<tr bgcolor='LightGreen' font-family='Century Gothic'>";
                                    body += "<td>EMPID</td>";
                                    body += "<td>EMPLOYEE NAME</td>";
                                    body += "<td>IQAMA</td>";
                                    body += "<td>PROFESSION</td>";
                                    body += "<td>NATIONALITY</td>";
                                    body += "<td>MOBILE</td>";
                                    body += "<td>PROJECT NAME</td>";
                                    body += "<td>START DATE</td>";
                                    body += "<td>END DATE</td>";
                                    body += "<td>STATUS</td>";

                                    body += "</tr>";

                                    foreach (DataRow Title in Titles2.Tables[0].Rows)
                                    {
                                        body += "<tr>";
                                        // body += "<td>" + Title[0] + "</td>";
                                        body += "<td>" + Title[0] + "</td>";
                                        //body += "<td>" + String.Format("{0:c}", Title[0]) + "</td>";
                                        body += "<td>" + String.Format("{0:c}", Title[1]) + "</td>";
                                        body += "<td>" + String.Format("{0:c}", Title[2]) + "</td>";
                                        body += "<td>" + String.Format("{0:c}", Title[3]) + "</td>";
                                        body += "<td>" + String.Format("{0:c}", Title[4]) + "</td>";
                                        body += "<td>" + String.Format("{0:c}", Title[5]) + "</td>";
                                        body += "<td>" + String.Format("{0:c}", Title[6]) + "</td>";
                                        body += "<td>" + String.Format("{0:c}", Title[7]) + "</td>";
                                        body += "<td>" + String.Format("{0:c}", Title[8]) + "</td>";
                                        //body += "<td>" + String.Format("{0:c}", Title[8]) + "</td>";
                                        body += "<td>" + Title[9] + "</td>";
                                        body += "</tr>";
                                    }
                                    body += "</table>";

                                    //now set up the mail settings
                                    MailMessage message = new MailMessage();
                                    message.IsBodyHtml = true;
                                    //  message.From = new MailAddress(NewTextBox222.Text);
                                    message.From = new MailAddress("PMS-Attendance@eastdeltasa.com");
                                    //can add more recipient
                                    // message.To.Add(new MailAddress(emailid));


                                    message.To.Add(new MailAddress("support@eastdeltasa.com"));

                                    //  message.To.Add(new MailAddress("khadershareef@eastdeltasa.com"));
                                    //add cc         

                                    message.CC.Add(new MailAddress("khadershareef@eastdeltasa.com"));
                                    message.CC.Add(new MailAddress(emailid));

                                    message.Subject = " Auto Email Notification of Vacation Employees dated " + DateTime.Now;
                                    message.Body = body1 + body;

                                    //SmtpClient client = new SmtpClient();
                                    //client.Send(message);

                                    SmtpClient smtp = new SmtpClient();
                                    smtp.Host = "mail.eastdeltasa.com";
                                    smtp.Port = 587;
                                    System.Net.NetworkCredential credential = new System.Net.NetworkCredential("khadershareef@eastdeltasa.com", "admin@123");
                                    smtp.UseDefaultCredentials = false;
                                    smtp.Credentials = credential;
                                    smtp.Send(message);
                                    message = null;
                                    // lbltechHR1.Text = "VOX REDSEA SUBMITTAL MAIL SENT SUCCESSFULLY...!!!";

                                    //  string constring11 = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                                    using (SqlConnection conn1 = new SqlConnection(ConnStr))
                                    {
                                        string indicator = "update zhr_emp set eindicator='1' where empid = '" + empid + "' ";
                                        string reportstr1 = "INSERT into tblnotify (empid, action, category,username,reportdate,entrydate) values (@empid, @action, @category,@username,@reportdate,@entrydate)";

                                        conn1.Open();

                                        SqlCommand zhrindicator = new SqlCommand(indicator, conn1);
                                        zhrindicator.ExecuteNonQuery();


                                        SqlCommand reportstrcmd1 = new SqlCommand(reportstr1, conn1);

                                        reportstrcmd1.Parameters.AddWithValue("@empid", empid);
                                        reportstrcmd1.Parameters.AddWithValue("@action", "Successfully Sent Vacation Emails dated From " + lblvacdate + " ");
                                        reportstrcmd1.Parameters.AddWithValue("@category", "Vacation Email");
                                        reportstrcmd1.Parameters.AddWithValue("@username", "SYSTEM");
                                        reportstrcmd1.Parameters.AddWithValue("@reportdate", lblvacdate);
                                        reportstrcmd1.Parameters.AddWithValue("@entrydate", DateTime.Now);
                                        reportstrcmd1.ExecuteNonQuery();
                                    }
                                    //vacation email ends    


                                    // update PMS notifications starts
                                    String ConnStr116 = "Data Source=192.168.1.85;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sqladmin;Password=Sqlpms@123456789;";
                                    using (SqlConnection conn616 = new SqlConnection(ConnStr116))
                                    {
                                        string reportstr15 = "INSERT into tblnotify (revno, action, category,username,reportdate,entrydate) values (@revno, @action, @category,@username,@reportdate,@entrydate)";
                                        SqlCommand reportstrcmd15 = new SqlCommand(reportstr15, conn616);
                                        reportstrcmd15.Parameters.AddWithValue("@revno", empid);
                                        reportstrcmd15.Parameters.AddWithValue("@action", "Successfully Sent Vacation Emails dated From " + lblvacdate + " ");
                                        reportstrcmd15.Parameters.AddWithValue("@category", "Vacation Email");
                                        reportstrcmd15.Parameters.AddWithValue("@username", "SYSTEM");
                                        reportstrcmd15.Parameters.AddWithValue("@reportdate", lblvacdate);
                                        reportstrcmd15.Parameters.AddWithValue("@entrydate", DateTime.Now);
                                        conn616.Open();
                                        reportstrcmd15.ExecuteNonQuery();

                                    }
                                    // update PMS notifications Ends      
                                }
                            }
                        }
                        else if (location == "Jeddah")
                        {
                            // if ((row.FindControl("chkSelect") as CheckBox).Checked)
                            //{
                            string status = Title1[6].ToString();
                            //  int empid = Convert.ToInt32(row.Cells[0].Text);


                            //if (status == "Vacation")
                            //{
                            //CheckBox status = (row.Cells[0].FindControl("CheckBox2") as CheckBox);
                            int empid = Convert.ToInt32(Title1[0]);
                            string projectname = Title1[7].ToString();
                            string empname = Title1[1].ToString();
                            string iqama = Title1[3].ToString();
                            string empjob = Title1[2].ToString();
                            string datein = Title1[8].ToString();
                            string actiondate = Title1[9].ToString();
                            string empnation = Title1[4].ToString();
                            string projectid = Title1[6].ToString();
                            string mobile = Title1[5].ToString();
                            int emailindicator = Convert.ToInt32(Title1[11]);
                            string emailid = Title1[12].ToString();

                            //  string constring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                            using (SqlConnection conn = new SqlConnection(ConnStr1))
                            {

                                string empvac = "select count(empid) from tblnotify where category = 'Employee Vacation' and reportdate = '" + lblvacdate + "' and empid='" + empid + "'";
                                string empvacemail = "select count(empid) from tblnotify where category = 'Vacation Email' and reportdate = '" + lblvacdate + "' and empid='" + empid + "'";
                                conn.Open();

                                SqlCommand zhrempvac = new SqlCommand(empvac, conn);
                                int countempvac = Convert.ToInt32(zhrempvac.ExecuteScalar());

                                SqlCommand zhrempvacemail = new SqlCommand(empvacemail, conn);
                                int countempvacemail = Convert.ToInt32(zhrempvacemail.ExecuteScalar());

                                if (countempvac == 0)
                                {
                                    string moveemptoprojstr = "update projtab set projectid=@projectid, projectname = @projectname, devicename = @devicename , deviceslno = @deviceslno , sitengname = @siteengname , siteengmobile = @siteengmobile , siteengemail = @siteengemail, projectlocation = @projectlocation, workschedule=@workschedule, wsid=@wsid  where empid = @empid";
                                    string insertstr = "insert into tempproject (projectid, projectname, deviceslno, devicename, sitengname, siteengmobile, siteengemail, empid, empname, iqama, empjob, empnation, empmobile, projectlocation, date, fromprojectname,username) values (@projectid1, @projectname1, @deviceslno1, @devicename1, @siteengname1, @siteengmobile1, @siteengemail1, @empid1, @empname1, @iqama1, @empjob1, @empnation1, @empmobile1, @projectlocation1, @date1, @fromprojectname1, @username) ";
                                    string movementstr = "insert into tblmoveto (empid, toprojectid,fromprojectid, reportdate, username,flag) values (@empid2, @toprojectid, @fromprojectid, @reportdate, @username, @flag) ";
                                    SqlCommand moveempstoprojcmd = new SqlCommand(moveemptoprojstr, conn);
                                    SqlCommand insertstrcmd = new SqlCommand(insertstr, conn);
                                    SqlCommand movementstrcmd = new SqlCommand(movementstr, conn);

                                    moveempstoprojcmd.Parameters.AddWithValue("@empid", empid);
                                    moveempstoprojcmd.Parameters.AddWithValue("@projectid", "PROJ-VACJED");
                                    moveempstoprojcmd.Parameters.AddWithValue("@projectname", "EDSCO VACATIONS JED");
                                    moveempstoprojcmd.Parameters.AddWithValue("@projectlocation", "Jeddah");
                                    moveempstoprojcmd.Parameters.AddWithValue("@devicename", "JED.ENTRANCE-M17");
                                    moveempstoprojcmd.Parameters.AddWithValue("@deviceslno", "2425683040041");
                                    moveempstoprojcmd.Parameters.AddWithValue("@siteengname", "Louay Mahmoud Salem");
                                    moveempstoprojcmd.Parameters.AddWithValue("@siteengmobile", "+966591392475");
                                    moveempstoprojcmd.Parameters.AddWithValue("@siteengemail", "Louay@eastdeltasa.com");
                                    moveempstoprojcmd.Parameters.AddWithValue("@workschedule", "JED Head Office 08:00-18:00");
                                    moveempstoprojcmd.Parameters.AddWithValue("@wsid", "1");

                                    insertstrcmd.Parameters.AddWithValue("@projectid1", "PROJ-VACJED");
                                    insertstrcmd.Parameters.AddWithValue("@projectname1", "EDSCO VACATIONS JED");
                                    insertstrcmd.Parameters.AddWithValue("@projectlocation1", "Jeddah");
                                    insertstrcmd.Parameters.AddWithValue("@devicename1", "JED.ENTRANCE-M17");
                                    insertstrcmd.Parameters.AddWithValue("@deviceslno1", "2425683040041");
                                    insertstrcmd.Parameters.AddWithValue("@siteengname1", "Louay Mahmoud Salem");
                                    insertstrcmd.Parameters.AddWithValue("@siteengmobile1", "+966591392475");
                                    insertstrcmd.Parameters.AddWithValue("@siteengemail1", "Louay@eastdeltasa.com");
                                    insertstrcmd.Parameters.AddWithValue("@empid1", empid);
                                    insertstrcmd.Parameters.AddWithValue("@empname1", empname);
                                    insertstrcmd.Parameters.AddWithValue("@iqama1", iqama);
                                    insertstrcmd.Parameters.AddWithValue("@empjob1", empjob);
                                    insertstrcmd.Parameters.AddWithValue("@empnation1", empnation);
                                    insertstrcmd.Parameters.AddWithValue("@empmobile1", mobile);
                                    insertstrcmd.Parameters.AddWithValue("@fromprojectname1", projectname);
                                    insertstrcmd.Parameters.AddWithValue("@date1", DateTime.Now.ToString("dd-MMM-yyyy"));
                                    insertstrcmd.Parameters.AddWithValue("@username", "SYSTEM");

                                    movementstrcmd.Parameters.AddWithValue("@empid2", empid);
                                    movementstrcmd.Parameters.AddWithValue("@toprojectid", "PROJ-VACJED");
                                    movementstrcmd.Parameters.AddWithValue("@fromprojectid", projectid);
                                    movementstrcmd.Parameters.AddWithValue("@reportdate", DateTime.Now.ToString("yyyy-MM-dd"));
                                    movementstrcmd.Parameters.AddWithValue("@username", "SYSTEM");
                                    movementstrcmd.Parameters.AddWithValue("@flag", "0");

                                    //  conn.Open();

                                    moveempstoprojcmd.ExecuteNonQuery();
                                    insertstrcmd.ExecuteNonQuery();
                                    movementstrcmd.ExecuteNonQuery();
                                    //Errmsgmoveempslabel.Text = "Employee No. " + AddEmps_empidtxt.Text + "  Successfully Moved To " + AddEmps_searchprojecttxt.Text + " Project ";

                                    //insert entry in notification table starts

                                    string zhratt = "update ZHR_TB_ATT_A set PROJECTNAME = @projectname, DEVICENAME = @devicename , PROJECTLOCATION = @projectlocation, WSID=@wsid, WSNAME_PMO=@wsname, TYPE=@type  where PERNR = @empid1 and TDATE=@TDATE ";
                                    //string zhratt1 = "update ZHR_TB_ATT_A set PROJECTNAME = @projectname, DEVICENAME = @devicename , PROJECTLOCATION = @projectlocation, WSID=@wsid, WSNAME_PMO=@wsname, TYPE=@type  where PERNR = @empid and TDATE=@TDATE1";

                                    string totalshedule = "update tbltotalschedule set projectid=@projectid, projectname = @projectname, devicename = @devicename , projectlocation = @projectlocation, wsid=@wsid, workschedule=@workschedule, sitengname=@siteengname1,siteengemail=@siteengemail1, changedate=@changedate where empid = @empid and dailydate=@dailydate";
                                    string indicator = "update zhr_emp set indicator='1' where empid= '" + empid + "' ";

                                    //string totalshedule1 = "update tbltotalschedule set projectid=@projectid, projectname = @projectname, devicename = @devicename , projectlocation = @projectlocation, wsid=@wsid, workschedule=@workschedule, sitengname=@siteengname1,siteengemail=@siteengemail1, changedate=@changedate where empid = @empid and dailydate=@dailydate";

                                    SqlCommand zhrattcmd = new SqlCommand(zhratt, conn);
                                    SqlCommand zhrindicator = new SqlCommand(indicator, conn);
                                    //  SqlCommand zhrattcmd1 = new SqlCommand(zhratt1, conn1);

                                    SqlCommand totalshedulecmd = new SqlCommand(totalshedule, conn);
                                    //  SqlCommand totalshedulecmd1 = new SqlCommand(totalshedule1, conn1);

                                    // zhrattcmd.Parameters.AddWithValue("@empid", empid);
                                    zhrattcmd.Parameters.AddWithValue("@empid1", empid);
                                    zhrattcmd.Parameters.AddWithValue("@projectname", "EDSCO VACATIONS JED");
                                    zhrattcmd.Parameters.AddWithValue("@projectlocation", "Jeddah");
                                    zhrattcmd.Parameters.AddWithValue("@devicename", "JED.ENTRANCE-M17");
                                    zhrattcmd.Parameters.AddWithValue("@wsid", "1");
                                    zhrattcmd.Parameters.AddWithValue("@wsname", "JED Head Office 08:00-18:00");
                                    zhrattcmd.Parameters.AddWithValue("@type", "D");
                                    zhrattcmd.Parameters.AddWithValue("@TDATE", lblvacdate);


                                    totalshedulecmd.Parameters.AddWithValue("@empid", empid);
                                    totalshedulecmd.Parameters.AddWithValue("@projectid", "PROJ-VACJED");
                                    totalshedulecmd.Parameters.AddWithValue("@projectname", "EDSCO VACATIONS JED");
                                    totalshedulecmd.Parameters.AddWithValue("@projectlocation", "Jeddah");
                                    totalshedulecmd.Parameters.AddWithValue("@devicename", "JED.ENTRANCE-M17");
                                    totalshedulecmd.Parameters.AddWithValue("@wsid", "1");
                                    totalshedulecmd.Parameters.AddWithValue("@workschedule", "JED Head Office 08:00-18:00");
                                    totalshedulecmd.Parameters.AddWithValue("@dailydate", lblvacdate);
                                    totalshedulecmd.Parameters.AddWithValue("@changedate", lblvacdate);
                                    totalshedulecmd.Parameters.AddWithValue("@siteengname1", "Louay Mahmoud Salem");
                                    totalshedulecmd.Parameters.AddWithValue("@siteengemail1", "Louay@eastdeltasa.com");

                                    //   conn1.Open();

                                    zhrattcmd.ExecuteNonQuery();
                                    zhrindicator.ExecuteNonQuery();
                                    // zhrattcmd1.ExecuteNonQuery();
                                    totalshedulecmd.ExecuteNonQuery();
                                    // totalshedulecmd1.ExecuteNonQuery();

                                    //Errmsgmoveempslabel.Text = "Employee No. " + AddEmps_empidtxt.Text + "  Successfully Moved To " + AddEmps_searchprojecttxt.Text + " Project ";

                                    //insert entry in notification table starts
                                    string reportstr1 = "INSERT into tblnotify (empid, action, category,username,reportdate,entrydate) values (@empid, @action, @category,@username,@reportdate,@entrydate)";

                                    SqlCommand reportstrcmd1 = new SqlCommand(reportstr1, conn);

                                    reportstrcmd1.Parameters.AddWithValue("@empid", empid);
                                    reportstrcmd1.Parameters.AddWithValue("@action", "Emp No. " + empid + "  Successfully Moved From " + projectname + " To EDSCO VACATION JED Project ");
                                    reportstrcmd1.Parameters.AddWithValue("@category", "Employee Vacation");
                                    reportstrcmd1.Parameters.AddWithValue("@username", "SYSTEM");
                                    reportstrcmd1.Parameters.AddWithValue("@reportdate", lblvacdate);
                                    reportstrcmd1.Parameters.AddWithValue("@entrydate", DateTime.Now);
                                    reportstrcmd1.ExecuteNonQuery();

                                    //insert entry in notification table ends

                                    // update PMS notifications starts
                                    String ConnStr116 = "Data Source=192.168.1.85;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sqladmin;Password=Sqlpms@123456789;";
                                    using (SqlConnection conn616 = new SqlConnection(ConnStr116))
                                    {
                                        string reportstr15 = "INSERT into tblnotify (revno, action, category,username,reportdate,entrydate) values (@revno, @action, @category,@username,@reportdate,@entrydate)";
                                        SqlCommand reportstrcmd15 = new SqlCommand(reportstr15, conn616);
                                        reportstrcmd15.Parameters.AddWithValue("@revno", empid);
                                        reportstrcmd15.Parameters.AddWithValue("@action", "Emp No. " + empid + "  Successfully Moved From " + projectname + " To EDSCO VACATION JED Project ");
                                        reportstrcmd15.Parameters.AddWithValue("@category", "Employee Vacation");
                                        reportstrcmd15.Parameters.AddWithValue("@username", "SYSTEM");
                                        reportstrcmd15.Parameters.AddWithValue("@reportdate", lblvacdate);
                                        reportstrcmd15.Parameters.AddWithValue("@entrydate", DateTime.Now);
                                        conn616.Open();
                                        reportstrcmd15.ExecuteNonQuery();

                                    }
                                    // update PMS notifications Ends      
                                }

                                if (emailid != "" && countempvacemail == 0)
                                {
                                    // vacation emails start

                                    string body = "";
                                    string body1 = "";
                                    String ConnStr = "Data Source=attsrvr-01.eastdeltasa.com;Network Library=DBMSSOCN;Initial Catalog=eastdelta2;User ID=sqladmin;Password=sql@admin;";
                                    //String SQL = "SELECT slno, reportname FROM dailyattreport where reportdate='2018-09-02'";
                                    //  String SQL = "SELECT [TDATE], [PERNR], [ENAME], [CHECKIN], [CHECKOUT], [NEXTDAYIN], [NEXTDAYOUT], [PROJECTNAME], [PROJECTLOCATION], [PMO_REMARK], [SITE_REMARK], [AREA_SP_REMARK] FROM [ZHR_TB_ATT_A] WHERE (([TDATE] = '" + HRAttdatetxt.Text + "') AND ([RECORD] = 'T') AND ([PMO_REMARK] = 'Absent') AND PROJECTNAME='" + projectname + "') ORDER BY PROJECTNAME,PROJECTLOCATION";
                                    String SQL2 = "SELECT zhr_emp.empid, projtab.empname, projtab.iqama, projtab.empjob, projtab.empnation, projtab.empmobile, projtab.projectname, zhr_emp.date_in, zhr_emp.date_out, zhr_emp.status FROM zhr_emp INNER JOIN projtab ON zhr_emp.empid = projtab.empid INNER JOIN emp ON zhr_emp.empid = emp.empid WHERE zhr_emp.empid = '" + empid + "' and (zhr_emp.date_in  = '" + lblvacdate + "') AND (zhr_emp.eindicator = 0) and zhr_emp .status  = 'Vacation' and emp.email <> '' ";

                                    SqlDataAdapter TitlesAdpt2 = new SqlDataAdapter(SQL2, ConnStr);
                                    DataSet Titles2 = new DataSet();
                                    // No need to open or close the connection
                                    //   since the SqlDataAdapter will do this automatically.
                                    TitlesAdpt2.Fill(Titles2);
                                    //  body1 += "Dear Eng. " + empname + ",<br /><br /> Kindly find the below ABSENT records for today, Please check the list and add your comments If any, with note that sanction list automatically applied.<br /><br /> NOTE: This email is Auto generated from the system. So Please don't reply to this email. <br /><br />";
                                    body1 += "Dear Sir, <br /><br /> This is an auto-generated email to notify that the below user is leaving for Vacation/Business trip as per the below mentioned dates. <br /><br /> NOTE: Out of office and Email forwarding to be enabled for this user. <br /><br />";

                                    body = "<table border=" + 1 + " cellpadding=" + 1 + " cellspacing=" + 1 + " font-family=Segoe UI>";
                                    body += "<tr bgcolor='LightGreen' font-family='Century Gothic'>";
                                    body += "<td>EMPID</td>";
                                    body += "<td>EMPLOYEE NAME</td>";
                                    body += "<td>IQAMA</td>";
                                    body += "<td>PROFESSION</td>";
                                    body += "<td>NATIONALITY</td>";
                                    body += "<td>MOBILE</td>";
                                    body += "<td>PROJECT NAME</td>";
                                    body += "<td>START DATE</td>";
                                    body += "<td>END DATE</td>";
                                    body += "<td>STATUS</td>";

                                    body += "</tr>";

                                    foreach (DataRow Title in Titles2.Tables[0].Rows)
                                    {
                                        body += "<tr>";
                                        // body += "<td>" + Title[0] + "</td>";
                                        body += "<td>" + Title[0] + "</td>";
                                        //body += "<td>" + String.Format("{0:c}", Title[0]) + "</td>";
                                        body += "<td>" + String.Format("{0:c}", Title[1]) + "</td>";
                                        body += "<td>" + String.Format("{0:c}", Title[2]) + "</td>";
                                        body += "<td>" + String.Format("{0:c}", Title[3]) + "</td>";
                                        body += "<td>" + String.Format("{0:c}", Title[4]) + "</td>";
                                        body += "<td>" + String.Format("{0:c}", Title[5]) + "</td>";
                                        body += "<td>" + String.Format("{0:c}", Title[6]) + "</td>";
                                        body += "<td>" + String.Format("{0:c}", Title[7]) + "</td>";
                                        body += "<td>" + String.Format("{0:c}", Title[8]) + "</td>";
                                        //body += "<td>" + String.Format("{0:c}", Title[8]) + "</td>";
                                        body += "<td>" + Title[9] + "</td>";
                                        body += "</tr>";
                                    }
                                    body += "</table>";

                                    //now set up the mail settings
                                    MailMessage message = new MailMessage();
                                    message.IsBodyHtml = true;
                                    //  message.From = new MailAddress(NewTextBox222.Text);
                                    message.From = new MailAddress("PMS-Attendance@eastdeltasa.com");
                                    //can add more recipient
                                    // message.To.Add(new MailAddress(emailid));


                                    message.To.Add(new MailAddress("support@eastdeltasa.com"));

                                    //  message.To.Add(new MailAddress("khadershareef@eastdeltasa.com"));
                                    //add cc         

                                    message.CC.Add(new MailAddress("khadershareef@eastdeltasa.com"));
                                    message.CC.Add(new MailAddress(emailid));

                                    message.Subject = " Auto Email Notification of Vacation Employees dated " + DateTime.Now;
                                    message.Body = body1 + body;

                                    //SmtpClient client = new SmtpClient();
                                    //client.Send(message);

                                    SmtpClient smtp = new SmtpClient();
                                    smtp.Host = "mail.eastdeltasa.com";
                                    smtp.Port = 587;
                                    System.Net.NetworkCredential credential = new System.Net.NetworkCredential("khadershareef@eastdeltasa.com", "admin@123");
                                    smtp.UseDefaultCredentials = false;
                                    smtp.Credentials = credential;
                                    smtp.Send(message);
                                    message = null;
                                    // lbltechHR1.Text = "VOX REDSEA SUBMITTAL MAIL SENT SUCCESSFULLY...!!!";

                                    //  string constring11 = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                                    using (SqlConnection conn1 = new SqlConnection(ConnStr))
                                    {
                                        string indicator = "update zhr_emp set eindicator='1' where empid = '" + empid + "' ";
                                        string reportstr1 = "INSERT into tblnotify (empid, action, category,username,reportdate,entrydate) values (@empid, @action, @category,@username,@reportdate,@entrydate)";

                                        conn1.Open();

                                        SqlCommand zhrindicator = new SqlCommand(indicator, conn1);
                                        zhrindicator.ExecuteNonQuery();


                                        SqlCommand reportstrcmd1 = new SqlCommand(reportstr1, conn1);

                                        reportstrcmd1.Parameters.AddWithValue("@empid", empid);
                                        reportstrcmd1.Parameters.AddWithValue("@action", "Successfully Sent Vacation Emails dated From " + lblvacdate + " ");
                                        reportstrcmd1.Parameters.AddWithValue("@category", "Vacation Email");
                                        reportstrcmd1.Parameters.AddWithValue("@username", "SYSTEM");
                                        reportstrcmd1.Parameters.AddWithValue("@reportdate", lblvacdate);
                                        reportstrcmd1.Parameters.AddWithValue("@entrydate", DateTime.Now);
                                        reportstrcmd1.ExecuteNonQuery();
                                    }
                                    //vacation email ends    


                                    // update PMS notifications starts
                                    String ConnStr116 = "Data Source=192.168.1.85;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sqladmin;Password=Sqlpms@123456789;";
                                    using (SqlConnection conn616 = new SqlConnection(ConnStr116))
                                    {
                                        string reportstr15 = "INSERT into tblnotify (revno, action, category,username,reportdate,entrydate) values (@revno, @action, @category,@username,@reportdate,@entrydate)";
                                        SqlCommand reportstrcmd15 = new SqlCommand(reportstr15, conn616);
                                        reportstrcmd15.Parameters.AddWithValue("@revno", empid);
                                        reportstrcmd15.Parameters.AddWithValue("@action", "Successfully Sent Vacation Emails dated From " + lblvacdate + " ");
                                        reportstrcmd15.Parameters.AddWithValue("@category", "Vacation Email");
                                        reportstrcmd15.Parameters.AddWithValue("@username", "SYSTEM");
                                        reportstrcmd15.Parameters.AddWithValue("@reportdate", lblvacdate);
                                        reportstrcmd15.Parameters.AddWithValue("@entrydate", DateTime.Now);
                                        conn616.Open();
                                        reportstrcmd15.ExecuteNonQuery();

                                    }
                                    // update PMS notifications Ends      
                                }
                            }
                        }

                    }
                }
            }
        }

        public void EmployeeHireUpdate1()
        {
            //   string lblvacdate = DateTime.Now.ToString("yyyy-MM-dd");
            string lblvacdate = "2022-12-19";
               String ConnStr1 = "Data Source=attsrvr-01.eastdeltasa.com;Network Library=DBMSSOCN;Initial Catalog=eastdelta2;User ID=sqladmin;Password=sql@admin;";
            //  string ConnStr1 = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

            using (SqlConnection mainconn = new SqlConnection(ConnStr1))
            {
                String SQL = "SELECT empid, date_in, status, indicator, eindicator from zhr_emp where status ='Hired' and action_date ='" + lblvacdate + "' ";

                SqlDataAdapter TitlesAdpt11 = new SqlDataAdapter(SQL, ConnStr1);
                DataSet Titlesnew = new DataSet();
                // No need to open or close the connection
                //   since the SqlDataAdapter will do this automatically.
                TitlesAdpt11.Fill(Titlesnew);

                mainconn.Open();

                if (Titlesnew.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow EmailTitle in Titlesnew.Tables[0].Rows)
                    {
                        // if ((row.FindControl("chkSelect") as CheckBox).Checked)
                        //{
                        // string status = Title1[6].ToString();
                        //  int empid = Convert.ToInt32(row.Cells[0].Text);


                        //if (status == "Vacation")
                        //{
                        //CheckBox status = (row.Cells[0].FindControl("CheckBox2") as CheckBox);
                        int empid = Convert.ToInt32(EmailTitle[0]);
                        string datein = EmailTitle[1].ToString();
                        string status = EmailTitle[2].ToString();
                        int hiredindicator = Convert.ToInt32(EmailTitle[3]);
                        int emailindicator = Convert.ToInt32(EmailTitle[4]);
                        string zeros = "00000";

                        //   string ConnStr12 = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                        //   String ConnStr12 = "Data Source=attsrvr-01.eastdeltasa.com;Network Library=DBMSSOCN;Initial Catalog=eastdelta2;User ID=sqladmin;Password=sql@admin;";
                        //using (SqlConnection conn15 = new SqlConnection(ConnStr12))
                        //{
                        //  string indicator = "INSERT into zhr_emp set eindicator='1' where empid = '" + empid + "' ";

                        string empemailsearch = "select count(empid) from tblnotify where category = 'Employee Hire' and reportdate = '" + lblvacdate + "' and empid = '" + empid + "' ";
                        string empemailsearchemail = "select count(empid) from tblnotify where category = 'Employee Hire Email' and reportdate = '" + lblvacdate + "' and empid ='" + empid + "' ";
                        string empemailcheck = "select count(empid) from zhr_emp where status = 'Hired' and empid ='" + empid + "' and  action_date < '" + lblvacdate + "'  ";

                        //  conn15.Open();

                        SqlCommand zhrempemailsearch = new SqlCommand(empemailsearch, mainconn);
                        int empemail = Convert.ToInt32(zhrempemailsearch.ExecuteScalar());

                        SqlCommand zhrempemailsearchemail = new SqlCommand(empemailsearchemail, mainconn);
                        int empemail1 = Convert.ToInt32(zhrempemailsearchemail.ExecuteScalar());

                        SqlCommand zempemailcheck = new SqlCommand(empemailcheck, mainconn);
                        int empemailchk = Convert.ToInt32(zempemailcheck.ExecuteScalar());

                        if (empemail == 0 && empemailchk == 0)
                        {
                            string hireindicator = "INSERT into emp ( empid, empname, empjob, empmobile, projectlocation, badgenumber) SELECT distinct zhr_employeein.eeno, zhr_employeein.name, zhr_employeein.position, zhr_employeein.mobile, 'Jeddah', '" + zeros + empid + "' FROM zhr_employeein INNER JOIN zhr_emp ON zhr_employeein.eeno = zhr_emp.empid where  zhr_emp .status = 'Hired' and zhr_emp .action_date = '" + lblvacdate + "' and zhr_employeein.eeno = '" + empid + "' ";
                            string reportstr11 = "INSERT into tblnotify (empid, action, category,username,reportdate,entrydate) values (@empid, @action, @category,@username,@reportdate,@entrydate)";
                            string indicator22 = "update zhr_emp set indicator='1' where empid= '" + empid + "' ";

                            SqlCommand zhrindicatorhire = new SqlCommand(hireindicator, mainconn);
                            zhrindicatorhire.ExecuteNonQuery();

                            SqlCommand zhrindicator22 = new SqlCommand(indicator22, mainconn);
                            zhrindicator22.ExecuteNonQuery();

                            SqlCommand reportstrcmd11 = new SqlCommand(reportstr11, mainconn);
                            reportstrcmd11.Parameters.AddWithValue("@empid", empid);
                            reportstrcmd11.Parameters.AddWithValue("@action", "Successfully updated new employee details joined date " + lblvacdate + " ");
                            reportstrcmd11.Parameters.AddWithValue("@category", "Employee Hire");
                            reportstrcmd11.Parameters.AddWithValue("@username", "SYSTEM");
                            reportstrcmd11.Parameters.AddWithValue("@reportdate", lblvacdate);
                            reportstrcmd11.Parameters.AddWithValue("@entrydate", DateTime.Now);
                            reportstrcmd11.ExecuteNonQuery();

                            String ConnStr1165 = "Data Source=attsrvr-01.eastdeltasa.com;Network Library=DBMSSOCN;Initial Catalog=eastdelta2;User ID=sqladmin;Password=sql@admin;";
                            using (SqlConnection conn66 = new SqlConnection(ConnStr1165))
                            {
                                string reportstr1 = "INSERT into tblnotify (empid, action, category,username,reportdate,entrydate) values (@empid, @action, @category,@username,@reportdate,@entrydate)";

                                SqlCommand reportstrcmd1 = new SqlCommand(reportstr1, conn66);
                                reportstrcmd1.Parameters.AddWithValue("@empid", empid);
                                reportstrcmd1.Parameters.AddWithValue("@action", "Successfully updated new employee details joined date " + lblvacdate + " ");
                                reportstrcmd1.Parameters.AddWithValue("@category", "Employee Hire");
                                reportstrcmd1.Parameters.AddWithValue("@username", "SYSTEM");
                                reportstrcmd1.Parameters.AddWithValue("@reportdate", lblvacdate);
                                reportstrcmd1.Parameters.AddWithValue("@entrydate", DateTime.Now);
                                conn66.Open();
                                reportstrcmd1.ExecuteNonQuery();

                                // lblerrorHR.Text = "HR Manager Closed Attendance File Successfully..! ";

                            }

                            // update PMS notifications starts
                            String ConnStr1166 = "Data Source=192.168.1.85;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sqladmin;Password=Sqlpms@123456789;";
                            using (SqlConnection conn61 = new SqlConnection(ConnStr1166))
                            {
                                string reportstr15 = "INSERT into tblnotify (revno, action, category,username,reportdate,entrydate) values (@revno, @action, @category,@username,@reportdate,@entrydate)";
                                SqlCommand reportstrcmd15 = new SqlCommand(reportstr15, conn61);
                                reportstrcmd15.Parameters.AddWithValue("@revno", "");
                                reportstrcmd15.Parameters.AddWithValue("@action", "Successfully updated new employee details joined date " + lblvacdate + " ");
                                reportstrcmd15.Parameters.AddWithValue("@category", "Employee Hire");
                                reportstrcmd15.Parameters.AddWithValue("@username", "SYSTEM");
                                reportstrcmd15.Parameters.AddWithValue("@reportdate", lblvacdate);
                                reportstrcmd15.Parameters.AddWithValue("@entrydate", DateTime.Now);
                                conn61.Open();
                                reportstrcmd15.ExecuteNonQuery();

                            }
                            // update PMS notifications Ends                          

                            // vacation emails start

                            string body = "";
                            string body1 = "";
                            String ConnStr = "Data Source=attsrvr-01.eastdeltasa.com;Network Library=DBMSSOCN;Initial Catalog=eastdelta2;User ID=sqladmin;Password=sql@admin;";
                            //String SQL = "SELECT slno, reportname FROM dailyattreport where reportdate='2018-09-02'";
                            //  String SQL = "SELECT [TDATE], [PERNR], [ENAME], [CHECKIN], [CHECKOUT], [NEXTDAYIN], [NEXTDAYOUT], [PROJECTNAME], [PROJECTLOCATION], [PMO_REMARK], [SITE_REMARK], [AREA_SP_REMARK] FROM [ZHR_TB_ATT_A] WHERE (([TDATE] = '" + HRAttdatetxt.Text + "') AND ([RECORD] = 'T') AND ([PMO_REMARK] = 'Absent') AND PROJECTNAME='" + projectname + "') ORDER BY PROJECTNAME,PROJECTLOCATION";
                            String SQL2 = "SELECT zhr_employeein.eeno as 'Emp ID', zhr_employeein.name as 'Employee Name', zhr_employeein.position as 'Position', zhr_employeein.mobile as Mobile, zhr_emp.date_in as 'Join Date'  FROM zhr_employeein INNER JOIN zhr_emp ON zhr_employeein.eeno = zhr_emp.empid where  zhr_emp .status = 'Hired' and zhr_emp .action_date = '" + lblvacdate + "' and zhr_employeein.eeno = '" + empid + "'";

                            ArrayList tolist_emails = new ArrayList();
                            ArrayList cclist_emails = new ArrayList();
                            int i = 0;
                            string mEdscoemail;

                            SqlCommand mEdsco_Email = new SqlCommand("select emailid from tblhire where type = 'TO' and status = 'Hire'", mainconn);
                            SqlDataReader mread_Email = mEdsco_Email.ExecuteReader();
                            while (mread_Email.Read())
                            {
                                mEdscoemail = mread_Email.GetValue(i).ToString();
                                tolist_emails.Add(mEdscoemail); //Add email to a arraylist
                                                                //    emails = read_Email.GetValue(i).ToString();
                                                                //list_emails = email.Split(',');
                                                                //  list_emails = email;

                                i = i + 1 - 1; //increment or ++i
                            }
                            mread_Email.Close();

                            int p = 0;
                            string mClientemail;
                            SqlCommand mClient_Email = new SqlCommand("select emailid from tblhire where type = 'CC' and status = 'Hire'", mainconn);
                            SqlDataReader mread_Email1 = mClient_Email.ExecuteReader();
                            while (mread_Email1.Read())
                            {
                                mClientemail = mread_Email1.GetValue(p).ToString();
                                cclist_emails.Add(mClientemail); //Add email to a arraylist
                                                                 // emails = read_Email.GetValue(i).ToString();
                                                                 // list_emails = email.Split(',');
                                                                 // list_emails = email;

                                p = p + 1 - 1; //increment or ++i
                            }
                            mread_Email1.Close();

                            SqlDataAdapter TitlesAdpt2 = new SqlDataAdapter(SQL2, ConnStr);
                            DataSet Titles2 = new DataSet();
                            // No need to open or close the connection
                            //   since the SqlDataAdapter will do this automatically.
                            TitlesAdpt2.Fill(Titles2);
                            //  body1 += "Dear Eng. " + empname + ",<br /><br /> Kindly find the below ABSENT records for today, Please check the list and add your comments If any, with note that sanction list automatically applied.<br /><br /> NOTE: This email is Auto generated from the system. So Please don't reply to this email. <br /><br />";
                            body1 += "Dear Sir, <br /><br /> This is an auto-generated email to notify that the below user is hired and updated in the system. So Please allocate him to project. <br /><br /> NOTE: Please don't reply to this email as this is auto generated email from system. <br /><br />";

                            body = "<table border=" + 1 + " cellpadding=" + 1 + " cellspacing=" + 1 + " font-family=Segoe UI>";
                            body += "<tr bgcolor='LightGreen' font-family='Century Gothic'>";
                            body += "<td>EMPID</td>";
                            body += "<td>EMPLOYEE NAME</td>";
                            body += "<td>POSITION</td>";
                            body += "<td>MOBILE</td>";
                            body += "<td>JOIN DATE</td>";

                            body += "</tr>";

                            foreach (DataRow Title in Titles2.Tables[0].Rows)
                            {
                                body += "<tr>";
                                // body += "<td>" + Title[0] + "</td>";
                                body += "<td>" + Title[0] + "</td>";
                                //body += "<td>" + String.Format("{0:c}", Title[0]) + "</td>";
                                body += "<td>" + String.Format("{0:c}", Title[1]) + "</td>";
                                body += "<td>" + String.Format("{0:c}", Title[2]) + "</td>";
                                body += "<td>" + String.Format("{0:c}", Title[3]) + "</td>";
                                body += "<td>" + String.Format("{0:c}", Title[4]) + "</td>";

                                //body += "<td>" + String.Format("{0:c}", Title[8]) + "</td>";
                                // body += "<td>" + Title[3] + "</td>";
                                body += "</tr>";
                            }
                            body += "</table>";

                            //now set up the mail settings
                            MailMessage message = new MailMessage();
                            message.IsBodyHtml = true;
                            //  message.From = new MailAddress(NewTextBox222.Text);
                            message.From = new MailAddress("PMS-Attendance@eastdeltasa.com");
                            for (int j = 0; j < tolist_emails.Count; j++)
                            {
                                message.To.Add(new MailAddress(tolist_emails[j].ToString()));
                            }

                            for (int m = 0; m < cclist_emails.Count; m++)
                            {
                                message.CC.Add(new MailAddress(cclist_emails[m].ToString()));
                            }
                            //can add more recipient
                            // message.To.Add(new MailAddress(emailid));                       

                            message.Subject = "Auto Email Notification of Hired Employees dated " + DateTime.Now;
                            message.Body = body1 + body;

                            //SmtpClient client = new SmtpClient();
                            //client.Send(message);

                            SmtpClient smtp = new SmtpClient();
                            smtp.Host = "mail.eastdeltasa.com";
                            smtp.Port = 587;
                            System.Net.NetworkCredential credential = new System.Net.NetworkCredential("khadershareef@eastdeltasa.com", "admin@123");
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = credential;
                            smtp.Send(message);
                            message = null;
                            // lbltechHR1.Text = "VOX REDSEA SUBMITTAL MAIL SENT SUCCESSFULLY...!!!";

                            //  string constring11 = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                            using (SqlConnection conn16 = new SqlConnection(ConnStr))
                            {
                                string indicator3 = "update zhr_emp set eindicator='1' where empid = '" + empid + "' ";
                                string emailreportstr = "INSERT into tblnotify (empid, action, category,username,reportdate,entrydate) values (@empid, @action, @category,@username,@reportdate,@entrydate)";

                                conn16.Open();

                                SqlCommand zhrindicator31 = new SqlCommand(indicator3, conn16);
                                zhrindicator31.ExecuteNonQuery();


                                SqlCommand emailreportstrcmd = new SqlCommand(emailreportstr, conn16);

                                emailreportstrcmd.Parameters.AddWithValue("@empid", empid);
                                emailreportstrcmd.Parameters.AddWithValue("@action", "Successfully Sent New Employee Hire Emails dated " + lblvacdate + " ");
                                emailreportstrcmd.Parameters.AddWithValue("@category", "Employee Hire Email");
                                emailreportstrcmd.Parameters.AddWithValue("@username", "SYSTEM");
                                emailreportstrcmd.Parameters.AddWithValue("@reportdate", lblvacdate);
                                emailreportstrcmd.Parameters.AddWithValue("@entrydate", DateTime.Now);
                                emailreportstrcmd.ExecuteNonQuery();
                            }

                            //vacation email ends
                        }

                    }
                }
            }
        }

        public void sendattemail()
        {
            string dayname = DateTime.Now.DayOfWeek.ToString();

            if (dayname == "Saturday")
            {
                var yesterday = DateTime.Today.AddDays(-2);
                string lbldate = yesterday.ToString("yyyy-MM-dd");

                // send email starts

                String ConnStr1 = "Data Source=attsrvr-01.eastdeltasa.com;Network Library=DBMSSOCN;Initial Catalog=eastdelta2;User ID=sqladmin;Password=sql@admin;";
                //String SQL = "SELECT slno, reportname FROM dailyattreport where reportdate='2018-09-02'";
                //String SQL = "SELECT [TDATE], [PERNR], [ENAME], [CHECKIN], [CHECKOUT], [PROJECTNAME], [PMO_REMARK], [AREA_SP_REMARK],[PMO_REMARK1], [HR_REMARK]FROM [ZHR_TB_ATT_A] WHERE (([TDATE] = '" + HRAttdatetxt.Text + "') AND ([RECORD] = 'Y') AND ([PMO_REMARK] = 'Absent') AND PROJECTNAME='" + projectname + "') ORDER BY PROJECTNAME,PROJECTLOCATION";
                String SQL1 = "SELECT ZHR_TB_ATT_A.TDATE, ZHR_TB_ATT_A.PERNR, ZHR_TB_ATT_A.ENAME, ZHR_TB_ATT_A.PROJECTNAME, emp.email, ZHR_TB_ATT_A.PMO_REMARK FROM emp INNER JOIN ZHR_TB_ATT_A ON emp.empid = ZHR_TB_ATT_A.PERNR WHERE (emp.email <> '' and emp.email is not null and emp.email <> 'No Email') and  ZHR_TB_ATT_A.TDATE ='" + lbldate + "' and RECORD = 'Y' and (ZHR_TB_ATT_A.PMO_REMARK <> '' and ZHR_TB_ATT_A.PMO_REMARK <> 'DAY OFF') and ( ZHR_TB_ATT_A.PMO_REMARK <> 'Business Trip' and ZHR_TB_ATT_A.PMO_REMARK <> 'Annual leave' and ZHR_TB_ATT_A.PMO_REMARK <> 'Exceptional Annual Leave' and ZHR_TB_ATT_A.PMO_REMARK <> 'Emergency Unpaid w Deds.' and  ZHR_TB_ATT_A.PMO_REMARK <> 'Unpaid leave' and ZHR_TB_ATT_A.PMO_REMARK <> 'Marriage leave' and ZHR_TB_ATT_A.PMO_REMARK <> 'Maternity leave' and ZHR_TB_ATT_A.PMO_REMARK <> 'Sick leave' and ZHR_TB_ATT_A.PMO_REMARK <> 'Exam Leave' and ZHR_TB_ATT_A.PMO_REMARK <> 'Child Birth leave' and ZHR_TB_ATT_A.PMO_REMARK <> 'Special Need Maternity Le') and ZHR_TB_ATT_A.PROJECTNAME not like '%VACATIONS%' and ZHR_TB_ATT_A.PERNR <> 1136 order by ZHR_TB_ATT_A.PERNR ";
                SqlDataAdapter TitlesAdpt1 = new SqlDataAdapter(SQL1, ConnStr1);
                DataSet Titles1 = new DataSet();
                // No need to open or close the connection
                //   since the SqlDataAdapter will do this automatically.
                TitlesAdpt1.Fill(Titles1);

                foreach (DataRow Title1 in Titles1.Tables[0].Rows)
                {

                    //  string emailid = (row.FindControl("lnkEmail") as HyperLink).Text;
                    string emailid = Title1[4].ToString();
                    //  string lbldate = Title1[0].ToString();
                    //string projectname = Title1[3].ToString ();
                    //string projectname = row.Cells[2].Text;
                    //   string empname = row.Cells[2].Text;
                    string empname = Title1[2].ToString();
                    // string location = row.Cells[5].Text;
                    int empid = Convert.ToInt32(Title1[1]);
                    string empstatus = Title1[5].ToString();

                    string body = "";
                    string body1 = "";
                    string body2 = "";
                    String ConnStr = "Data Source=attsrvr-01.eastdeltasa.com;Network Library=DBMSSOCN;Initial Catalog=eastdelta2;User ID=sqladmin;Password=sql@admin;";
                    //String SQL = "SELECT slno, reportname FROM dailyattreport where reportdate='2018-09-02'";
                    //String SQL = "SELECT [TDATE], [PERNR], [ENAME], [CHECKIN], [CHECKOUT], [PROJECTNAME], [PMO_REMARK], [AREA_SP_REMARK],[PMO_REMARK1], [HR_REMARK]FROM [ZHR_TB_ATT_A] WHERE (([TDATE] = '" + HRAttdatetxt.Text + "') AND ([RECORD] = 'Y') AND ([PMO_REMARK] = 'Absent') AND PROJECTNAME='" + projectname + "') ORDER BY PROJECTNAME,PROJECTLOCATION";
                    String SQL = "SELECT [TDATE] AS DATE, [PERNR] AS EMPID, [ENAME] AS 'EMPLOYEE NAME', [CHECKIN], [CHECKOUT], [DELAY_DUR] AS 'DELAY TIME', [PROJECTNAME], [PROJECTLOCATION] AS LOCATION, [PMO_REMARK] as STATUS FROM [ZHR_TB_ATT_A] WHERE ([TDATE] = '" + lbldate + "' AND [RECORD] = 'Y') and pernr='" + empid + "' ";
                    SqlDataAdapter TitlesAdpt = new SqlDataAdapter(SQL, ConnStr);
                    DataSet Titles = new DataSet();
                    // No need to open or close the connection
                    //   since the SqlDataAdapter will do this automatically.
                    TitlesAdpt.Fill(Titles);
                    //      body1 += "Dear Eng. " + empname + ",<br /><br /> Kindly find the below Attendance records dated " + lbldate.Text + " . This is to inform you that your Check In or Check Out dated " + lbldate.Text + " has an issue. So Please login to our online ESS Portal http://ess.eastdeltasa.com and apply permission.  <br /><br />";
                    body1 += " إشارة إلى المخالفة التي إرتكبتها والموضحة أدناة التأخر في الحضور للعمل حسب الأوقات الرسمية المقررة بتاريخ " + lbldate + " يعتبر هذا اخطار انذار بالتاخير <br /><br />";
                    body2 += " With reference to subject above and an indication to the attendance violation committed on " + lbldate + ", for delay in attending office according to the working hours scheduled. <br/> This is a notice of delay warning.<br /><br />";
                    body = "<table border=" + 1 + " cellpadding=" + 1 + " cellspacing=" + 1 + " width=100% font-family=Segoe UI>";
                    body += "<tr bgcolor='#4da6ff' font-family=Segoe UI>";
                    body += "<td>DATE</td>";
                    body += "<td>EMPID</td>";
                    body += "<td>EMPLOYEE NAME</td>";
                    body += "<td>CHECKIN</td>";
                    body += "<td>CHECKOUT</td>";
                    body += "<td>DELAY_DUR</td>";
                    body += "<td>PROJECT NAME</td>";
                    body += "<td>PROJECTLOCATION</td>";
                    body += "<td>PMO REMARKS</td>";
                    body += "</tr>";

                    foreach (DataRow Title in Titles.Tables[0].Rows)
                    {
                        body += "<tr>";
                        // body += "<td>" + Title[0] + "</td>";
                        body += "<td>" + String.Format("{0:c}", Title[0]) + "</td>";
                        body += "<td>" + Title[1] + "</td>";
                        body += "<td>" + String.Format("{0:c}", Title[2]) + "</td>";
                        body += "<td>" + String.Format("{0:c}", Title[3]) + "</td>";
                        body += "<td>" + String.Format("{0:c}", Title[4]) + "</td>";
                        body += "<td>" + String.Format("{0:c}", Title[5]) + "</td>";
                        body += "<td>" + String.Format("{0:c}", Title[6]) + "</td>";
                        body += "<td>" + String.Format("{0:c}", Title[7]) + "</td>";
                        body += "<td>" + String.Format("{0:c}", Title[8]) + "</td>";
                        body += "</tr>";
                    }
                    body += "</table>";

                    //now set up the mail settings
                    MailMessage message = new MailMessage();
                    message.IsBodyHtml = true;
                    //message.From = new MailAddress(NewTextBox222.Text);
                    message.From = new MailAddress("PMS-Attendance@eastdeltasa.com");
                    //can add more recipient
                    message.To.Add(new MailAddress(emailid));

                    //   message.To.Add(new MailAddress("khadershareef@eastdeltasa.com"));

                    //add cc

                    message.CC.Add(new MailAddress("khadershareef@eastdeltasa.com"));

                    message.Subject = "Attendance Auto Notification dated " + lbldate;

                    message.Body = body1 + body2 + body;

                    //SmtpClient client = new SmtpClient();
                    //client.Send(message);

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "mail.eastdeltasa.com";
                    smtp.Port = 587;
                    System.Net.NetworkCredential credential = new System.Net.NetworkCredential("khadershareef@eastdeltasa.com", "admin@123");
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = credential;
                    smtp.Send(message);
                    message = null;
                    // lblerrorHR.Text = "Mail Sent Successfully ..!!!";

                    using (SqlConnection conn66 = new SqlConnection(ConnStr1))
                    {
                        string reportstr16 = "INSERT into tblnotify (empid, action, category,username,reportdate,entrydate) values (@empid, @action, @category,@username,@reportdate,@entrydate)";

                        SqlCommand reportstrcmd16 = new SqlCommand(reportstr16, conn66);
                        reportstrcmd16.Parameters.AddWithValue("@empid", empid);
                        reportstrcmd16.Parameters.AddWithValue("@action", "Auto Email Notification For Attendance sent for " + empstatus);
                        reportstrcmd16.Parameters.AddWithValue("@category", "Attendance Email");
                        reportstrcmd16.Parameters.AddWithValue("@username", "SYSTEM");
                        reportstrcmd16.Parameters.AddWithValue("@reportdate", lbldate);
                        reportstrcmd16.Parameters.AddWithValue("@entrydate", DateTime.Now);
                        conn66.Open();
                        reportstrcmd16.ExecuteNonQuery();

                        // lblerrorHR.Text = "HR Manager Closed Attendance File Successfully..! ";

                    }

                }
                // send email ends
                

            }
            else if (dayname != "Friday")
            {
                var yesterday = DateTime.Today.AddDays(-1);
                string lbldate = yesterday.ToString("yyyy-MM-dd");

                // send email starts

                String ConnStr1 = "Data Source=attsrvr-01.eastdeltasa.com;Network Library=DBMSSOCN;Initial Catalog=eastdelta2;User ID=sqladmin;Password=sql@admin;";

                //  String SQL1 = "SELECT ZHR_TB_ATT_A.TDATE, ZHR_TB_ATT_A.PERNR, ZHR_TB_ATT_A.ENAME, ZHR_TB_ATT_A.PROJECTNAME, emp.email, ZHR_TB_ATT_A.PMO_REMARK FROM emp INNER JOIN ZHR_TB_ATT_A ON emp.empid = ZHR_TB_ATT_A.PERNR WHERE (emp.email <> '') AND (emp.iqama <> 'Terminated') and  ZHR_TB_ATT_A.TDATE ='" + lbldate + "' and RECORD = 'Y' and (ZHR_TB_ATT_A.PMO_REMARK <> '' and ZHR_TB_ATT_A.PMO_REMARK <> 'DAY OFF' and ZHR_TB_ATT_A.PMO_REMARK <> 'Business Trip' and ZHR_TB_ATT_A.PMO_REMARK <> 'Annual leave') order by ZHR_TB_ATT_A.PERNR";

                String SQL1 = "SELECT ZHR_TB_ATT_A.TDATE, ZHR_TB_ATT_A.PERNR, ZHR_TB_ATT_A.ENAME, ZHR_TB_ATT_A.PROJECTNAME, emp.email, ZHR_TB_ATT_A.PMO_REMARK FROM emp INNER JOIN ZHR_TB_ATT_A ON emp.empid = ZHR_TB_ATT_A.PERNR WHERE (emp.email <> '' and emp.email is not null and emp.email <> 'No Email') and  ZHR_TB_ATT_A.TDATE ='" + lbldate + "' and RECORD = 'Y' and (ZHR_TB_ATT_A.PMO_REMARK <> '' and ZHR_TB_ATT_A.PMO_REMARK <> 'DAY OFF') and ( ZHR_TB_ATT_A.PMO_REMARK <> 'Business Trip' and ZHR_TB_ATT_A.PMO_REMARK <> 'Annual leave' and ZHR_TB_ATT_A.PMO_REMARK <> 'Exceptional Annual Leave' and ZHR_TB_ATT_A.PMO_REMARK <> 'Emergency Unpaid w Deds.' and  ZHR_TB_ATT_A.PMO_REMARK <> 'Unpaid leave' and ZHR_TB_ATT_A.PMO_REMARK <> 'Marriage leave' and ZHR_TB_ATT_A.PMO_REMARK <> 'Maternity leave' and ZHR_TB_ATT_A.PMO_REMARK <> 'Sick leave' and ZHR_TB_ATT_A.PMO_REMARK <> 'Exam Leave' and ZHR_TB_ATT_A.PMO_REMARK <> 'Child Birth leave' and ZHR_TB_ATT_A.PMO_REMARK <> 'Special Need Maternity Le') and ZHR_TB_ATT_A.PROJECTNAME not like '%VACATIONS%' and ZHR_TB_ATT_A.PERNR <> 1136 order by ZHR_TB_ATT_A.PERNR ";

                SqlDataAdapter TitlesAdpt1 = new SqlDataAdapter(SQL1, ConnStr1);
                DataSet Titles1 = new DataSet();
                // No need to open or close the connection
                //   since the SqlDataAdapter will do this automatically.
                TitlesAdpt1.Fill(Titles1);

                foreach (DataRow Title1 in Titles1.Tables[0].Rows)
                {

                    //  string emailid = (row.FindControl("lnkEmail") as HyperLink).Text;
                    string emailid = Title1[4].ToString();
                    //  string lbldate = Title1[0].ToString();
                    //string projectname = Title1[3].ToString ();
                    //string projectname = row.Cells[2].Text;
                    //   string empname = row.Cells[2].Text;
                    string empname = Title1[2].ToString();
                    // string location = row.Cells[5].Text;
                    int empid = Convert.ToInt32(Title1[1]);
                    string empstatus = Title1[5].ToString();

                    string body = "";
                    string body1 = "";
                    string body2 = "";
                    String ConnStr = "Data Source=attsrvr-01.eastdeltasa.com;Network Library=DBMSSOCN;Initial Catalog=eastdelta2;User ID=sqladmin;Password=sql@admin;";
                    //String SQL = "SELECT slno, reportname FROM dailyattreport where reportdate='2018-09-02'";
                    //String SQL = "SELECT [TDATE], [PERNR], [ENAME], [CHECKIN], [CHECKOUT], [PROJECTNAME], [PMO_REMARK], [AREA_SP_REMARK],[PMO_REMARK1], [HR_REMARK]FROM [ZHR_TB_ATT_A] WHERE (([TDATE] = '" + HRAttdatetxt.Text + "') AND ([RECORD] = 'Y') AND ([PMO_REMARK] = 'Absent') AND PROJECTNAME='" + projectname + "') ORDER BY PROJECTNAME,PROJECTLOCATION";
                    String SQL = "SELECT [TDATE] AS DATE, [PERNR] AS EMPID, [ENAME] AS 'EMPLOYEE NAME', [CHECKIN], [CHECKOUT], [DELAY_DUR] AS 'DELAY TIME', [PROJECTNAME], [PROJECTLOCATION] AS LOCATION, [PMO_REMARK] as STATUS FROM [ZHR_TB_ATT_A] WHERE ([TDATE] = '" + lbldate + "' AND [RECORD] = 'Y') and pernr='" + empid + "' ";
                    SqlDataAdapter TitlesAdpt = new SqlDataAdapter(SQL, ConnStr);
                    DataSet Titles = new DataSet();
                    // No need to open or close the connection
                    //   since the SqlDataAdapter will do this automatically.
                    TitlesAdpt.Fill(Titles);
                    //      body1 += "Dear Eng. " + empname + ",<br /><br /> Kindly find the below Attendance records dated " + lbldate.Text + " . This is to inform you that your Check In or Check Out dated " + lbldate.Text + " has an issue. So Please login to our online ESS Portal http://ess.eastdeltasa.com and apply permission.  <br /><br />";
                    body1 += " إشارة إلى المخالفة التي إرتكبتها والموضحة أدناة التأخر في الحضور للعمل حسب الأوقات الرسمية المقررة بتاريخ " + lbldate + " يعتبر هذا اخطار انذار بالتاخير <br /><br />";
                    body2 += " With reference to subject above and an indication to the attendance violation committed on " + lbldate + ", for delay in attending office according to the working hours scheduled. <br/> This is a notice of delay warning.<br /><br />";
                    body = "<table border=" + 1 + " cellpadding=" + 1 + " cellspacing=" + 1 + " width=100% font-family=Segoe UI>";
                    body += "<tr bgcolor='#4da6ff' font-family=Segoe UI>";
                    body += "<td>DATE</td>";
                    body += "<td>EMPID</td>";
                    body += "<td>EMPLOYEE NAME</td>";
                    body += "<td>CHECKIN</td>";
                    body += "<td>CHECKOUT</td>";
                    body += "<td>DELAY_DUR</td>";
                    body += "<td>PROJECT NAME</td>";
                    body += "<td>PROJECTLOCATION</td>";
                    body += "<td>PMO REMARKS</td>";
                    body += "</tr>";

                    foreach (DataRow Title in Titles.Tables[0].Rows)
                    {
                        body += "<tr>";
                        // body += "<td>" + Title[0] + "</td>";
                        body += "<td>" + String.Format("{0:c}", Title[0]) + "</td>";
                        body += "<td>" + Title[1] + "</td>";
                        body += "<td>" + String.Format("{0:c}", Title[2]) + "</td>";
                        body += "<td>" + String.Format("{0:c}", Title[3]) + "</td>";
                        body += "<td>" + String.Format("{0:c}", Title[4]) + "</td>";
                        body += "<td>" + String.Format("{0:c}", Title[5]) + "</td>";
                        body += "<td>" + String.Format("{0:c}", Title[6]) + "</td>";
                        body += "<td>" + String.Format("{0:c}", Title[7]) + "</td>";
                        body += "<td>" + String.Format("{0:c}", Title[8]) + "</td>";
                        body += "</tr>";
                    }
                    body += "</table>";

                    //now set up the mail settings
                    MailMessage message = new MailMessage();
                    message.IsBodyHtml = true;
                    //message.From = new MailAddress(NewTextBox222.Text);
                    message.From = new MailAddress("PMS-Attendance@eastdeltasa.com");
                    //can add more recipient
                    message.To.Add(new MailAddress(emailid));

                    //    message.To.Add(new MailAddress("khadershareef@eastdeltasa.com"));

                    //add cc

                    message.CC.Add(new MailAddress("khadershareef@eastdeltasa.com"));

                    message.Subject = "Attendance Auto Notification dated " + lbldate;

                    message.Body = body1 + body2 + body;

                    //SmtpClient client = new SmtpClient();
                    //client.Send(message);

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "mail.eastdeltasa.com";
                    smtp.Port = 587;
                    System.Net.NetworkCredential credential = new System.Net.NetworkCredential("khadershareef@eastdeltasa.com", "admin@123");
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = credential;
                    smtp.Send(message);
                    message = null;
                    // lblerrorHR.Text = "Mail Sent Successfully ..!!!";

                    using (SqlConnection conn66 = new SqlConnection(ConnStr1))
                    {
                        string reportstr16 = "INSERT into tblnotify (empid, action, category,username,reportdate,entrydate) values (@empid, @action, @category,@username,@reportdate,@entrydate)";

                        SqlCommand reportstrcmd16 = new SqlCommand(reportstr16, conn66);
                        reportstrcmd16.Parameters.AddWithValue("@empid", empid);
                        reportstrcmd16.Parameters.AddWithValue("@action", "Auto Email Notification For Attendance sent for " + empstatus);
                        reportstrcmd16.Parameters.AddWithValue("@category", "Attendance Email");
                        reportstrcmd16.Parameters.AddWithValue("@username", "SYSTEM");
                        reportstrcmd16.Parameters.AddWithValue("@reportdate", lbldate);
                        reportstrcmd16.Parameters.AddWithValue("@entrydate", DateTime.Now);
                        conn66.Open();
                        reportstrcmd16.ExecuteNonQuery();

                        // lblerrorHR.Text = "HR Manager Closed Attendance File Successfully..! ";

                    }

                }
                // send email ends
               
            }

        }
    }
}