using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class testform1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //SubmittalEmail_Alerts1();
            //AttYesterdayUpdate_A();
            //  AttendanceJobmail1();
            //   sendattemail();

            SubmittalEmail_Alerts();
        }

        static void SubmittalEmail_Alerts()
        {
            //  String ConnStr1 = "Data Source=172.177.184.39;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sqladmin;Password=Sqlegypt@123456;";
          //  String ConnStr1 = "Data Source=fileserver-2a5c147d11735233.elb.me-south-1.amazonaws.com;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sa;Password=Sqladmin@123;";
            String ConnStr1 = "Data Source=fileserver-2a5c147d11735233.elb.me-south-1.amazonaws.com;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sa;Password=Sqladmin@123;";
            using (SqlConnection conn61 = new SqlConnection(ConnStr1))
            {
                String MAINSQL = "select projectid, delaydays, projectname from newproject where submittalalerts =1 and status <> 'COMPLETED' and  projectid = 'PROJ-1321' ";

                SqlDataAdapter TitlesAdpt11 = new SqlDataAdapter(MAINSQL, ConnStr1);
                DataSet Titles11 = new DataSet();
                // No need to open or close the connection
                //   since the SqlDataAdapter will do this automatically.
                TitlesAdpt11.Fill(Titles11);

                conn61.Open();

                //  GridView1.DataSource = Titles1.Tables[0];
                //  GridView1.DataBind();

                if (Titles11.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow Title12 in Titles11.Tables[0].Rows)
                    {

                        int delaydays_ = Convert.ToInt32(Title12[1]);
                        string projectid_ = Title12[0].ToString();
                        string projectname_ = Title12[2].ToString();

                        string mbody = "";
                        string mbody1 = "";
                        String ConnStr = "Data Source=fileserver-2a5c147d11735233.elb.me-south-1.amazonaws.com;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sa;Password=Sqladmin@123;";                        
                        //String SQL = "SELECT slno, reportname FROM dailyattreport where reportdate='2018-09-02'";
                        //  String SQL = "SELECT [TDATE], [PERNR], [ENAME], [CHECKIN], [CHECKOUT], [NEXTDAYIN], [NEXTDAYOUT], [PROJECTNAME], [PROJECTLOCATION], [PMO_REMARK], [SITE_REMARK], [AREA_SP_REMARK] FROM [ZHR_TB_ATT_A] WHERE (([TDATE] = '" + HRAttdatetxt.Text + "') AND ([RECORD] = 'T') AND ([PMO_REMARK] = 'Absent') AND PROJECTNAME='" + projectname + "') ORDER BY PROJECTNAME,PROJECTLOCATION";
                        //  String SQL = "SELECT DISTINCT (t1.tid),t1.revno , t1.submittal, t1.workscope, t1.description, t1.submissiondate, t1.preparedby,t2.Maxactioncode, DATEDIFF(DAY, submissiondate, GETDATE()) - '" + delaydays_ + "' AS TotDays FROM ArchitectDrawings t1 INNER JOIN (SELECT MAX(actioncode) as Maxactioncode, revno FROM ArchitectDrawingItems  GROUP BY revno) t2 ON t1.revno = t2.revno where (t2.Maxactioncode IS NULL or t2.Maxactioncode = '') and t1 .projectid = '" + projectid_ + "' and DATEDIFF(DAY, submissiondate, GETDATE()) > '" + delaydays_ + "' order by t1.workscope, t1.submittal, t1.tid ";
                        String MSQL = "SELECT DISTINCT t1.tid, t1.revno, t1.submittal, t1.workscope, t1.description, t1.submissiondate, emp.empname, t2.Maxactioncode, DATEDIFF(DAY, t1.submissiondate, GETDATE()) - '" + delaydays_ + "' AS TotDays, t1.tobeapproved as 'To Be Approved', t1.comment3 AS 'Consultant Comments' FROM ArchitectDrawings AS t1 INNER JOIN (SELECT MAX(actioncode) AS Maxactioncode, revno FROM ArchitectDrawingItems GROUP BY revno) AS t2 ON t1.revno = t2.revno INNER JOIN emp ON t1.empid = emp.empid WHERE (t2.Maxactioncode IS NULL OR t2.Maxactioncode = '' OR t2.Maxactioncode = 'E') AND (t1.projectid = '" + projectid_ + "') AND (DATEDIFF(DAY, t1.submissiondate, GETDATE())) > '" + delaydays_ + "' ORDER BY t1.workscope, t1.submittal, t1.tid";

                        SqlDataAdapter mTitlesAdpt = new SqlDataAdapter(MSQL, ConnStr);
                        DataSet mTitles = new DataSet();
                        // No need to open or close the connection
                        //   since the SqlDataAdapter will do this automatically.
                        mTitlesAdpt.Fill(mTitles);
                        //  body1 += "Dear Eng. " + empname + ",<br /><br /> Kindly find the below ABSENT records for today, Please check the list and add your comments If any, with note that sanction list automatically applied.<br /><br /> NOTE: This email is Auto generated from the system. So Please don't reply to this email. <br /><br />";

                        using (SqlConnection conn = new SqlConnection())
                        {
                            conn.ConnectionString = "Data Source=fileserver-2a5c147d11735233.elb.me-south-1.amazonaws.com;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sa;Password=Sqladmin@123;";

                            conn.Open();

                            ArrayList mlist_emails = new ArrayList();
                            ArrayList mClientlist_emails = new ArrayList();
                            ArrayList mConsultantlist_emails = new ArrayList();

                            //  string[] list_emails;
                            int i = 0;
                            string mEdscoemail;
                            // SqlCommand mEdsco_Email = new SqlCommand("SELECT  tblprojectkey.empemail  FROM tblprojectkey INNER JOIN newproject ON tblprojectkey.projectid = newproject.projectid WHERE (tblprojectkey.empemail <> '') AND (tblprojectkey.empemail IS NOT NULL) AND newproject.status = 'ONGOING' and  tblprojectkey .projectid = '" + projectid_ + "' and newproject.submittalalerts = 1 and tblprojectkey . type = 'EDSCO' and tblprojectkey .submittalfiles = 1 ", conn);
                            SqlCommand mEdsco_Email = new SqlCommand("Select emailid from tblpendingalerts where projectid = '" + projectid_ + "' AND type = 'TO'", conn);
                            SqlDataReader mread_Email = mEdsco_Email.ExecuteReader();
                            while (mread_Email.Read())
                            {
                                mEdscoemail = mread_Email.GetValue(i).ToString();
                                mlist_emails.Add(mEdscoemail); //Add email to a arraylist
                                                               //    emails = read_Email.GetValue(i).ToString();
                                                               //list_emails = email.Split(',');
                                                               //  list_emails = email;

                                i = i + 1 - 1; //increment or ++i
                            }
                            mread_Email.Close();

                            int p = 0;
                            string mClientemail;
                            //    SqlCommand mClient_Email = new SqlCommand("SELECT  tblprojectkey.empemail  FROM tblprojectkey INNER JOIN newproject ON tblprojectkey.projectid = newproject.projectid WHERE (tblprojectkey.empemail <> '') AND (tblprojectkey.empemail IS NOT NULL) AND newproject.status = 'ONGOING' and  tblprojectkey .projectid = '" + projectid_ + "' and newproject.submittalalerts = 1 and tblprojectkey . type = 'CLIENT' and tblprojectkey .submittalfiles = 1 ", conn);
                            SqlCommand mClient_Email = new SqlCommand("Select emailid from tblpendingalerts where projectid = '" + projectid_ + "' AND type = 'CC'", conn);
                            SqlDataReader mread_Email1 = mClient_Email.ExecuteReader();
                            while (mread_Email1.Read())
                            {
                                mClientemail = mread_Email1.GetValue(p).ToString();
                                mClientlist_emails.Add(mClientemail); //Add email to a arraylist
                                                                      //    emails = read_Email.GetValue(i).ToString();
                                                                      //list_emails = email.Split(',');
                                                                      //  list_emails = email;

                                p = p + 1 - 1; //increment or ++i
                            }
                            mread_Email1.Close();

                            //int q = 0;
                            //string mConsultemail;
                            //SqlCommand mConsultant_Email = new SqlCommand("SELECT  tblprojectkey.empemail  FROM tblprojectkey INNER JOIN newproject ON tblprojectkey.projectid = newproject.projectid WHERE (tblprojectkey.empemail <> '') AND (tblprojectkey.empemail IS NOT NULL) AND newproject.status = 'ONGOING' and  tblprojectkey .projectid = '" + projectid_ + "' and newproject.submittalalerts = 1 and tblprojectkey . type = 'CONSULTANT' and tblprojectkey .submittalfiles = 1 ", conn);
                            //SqlDataReader mread_Email2 = mConsultant_Email.ExecuteReader();
                            //while (mread_Email2.Read())
                            //{
                            //    mConsultemail = mread_Email2.GetValue(q).ToString();
                            //    mConsultantlist_emails.Add(mConsultemail); //Add email to a arraylist
                            //                                               //    emails = read_Email.GetValue(i).ToString();
                            //                                               //list_emails = email.Split(',');
                            //                                               //  list_emails = email;

                            //    q = q + 1 - 1; //increment or ++i
                            //}
                            //mread_Email2.Close();

                            mbody1 += "Dear Sir, <br /><br /> Kindly find the below submittals which has no ACTION and need to take action. <br /><br /> NOTE: This email is Auto generated from the system. So Please don't reply to this email. <br /><br />";

                            mbody = "<table border=" + 1 + " cellpadding=" + 1 + " cellspacing=" + 1 + " font-family=Segoe UI>";
                            mbody += "<tr bgcolor='LightGreen' font-family='Century Gothic'>";
                            mbody += "<td>TRANSID</td>";
                            mbody += "<td>SUBMITTAL NUMBER</td>";
                            mbody += "<td>SUBMITTAL TYPE</td>";
                            mbody += "<td>WORK SCOPE</td>";
                            mbody += "<td>DESCRIPTION</td>";
                            mbody += "<td>SUBMISSION DATE</td>";
                            mbody += "<td>PREPARED BY</td>";
                            mbody += "<td>CODE</td>";
                            mbody += "<td>DELAY DAYS</td>";
                            mbody += "<td>TO BE APPROVED BY</td>";
                            mbody += "<td>CONSULTANT COMMENTS</td>";
                            mbody += "</tr>";

                            foreach (DataRow Title in mTitles.Tables[0].Rows)
                            {
                                mbody += "<tr>";
                                // body += "<td>" + Title[0] + "</td>";
                                mbody += "<td>" + Title[0] + "</td>";
                                //body += "<td>" + String.Format("{0:c}", Title[0]) + "</td>";
                                mbody += "<td>" + String.Format("{0:c}", Title[1]) + "</td>";
                                mbody += "<td>" + String.Format("{0:c}", Title[2]) + "</td>";
                                mbody += "<td>" + String.Format("{0:c}", Title[3]) + "</td>";
                                mbody += "<td>" + String.Format("{0:c}", Title[4]) + "</td>";
                                mbody += "<td>" + Convert.ToDateTime(Title[5]).ToString("dd-MMM-yyyy") + "</td>";
                                mbody += "<td>" + String.Format("{0:c}", Title[6]) + "</td>";
                                mbody += "<td>" + String.Format("{0:c}", Title[7]) + "</td>";
                                //body += "<td>" + String.Format("{0:c}", Title[8]) + "</td>";
                                mbody += "<td>" + Title[8] + "</td>";
                                mbody += "<td>" + String.Format("{0:c}", Title[9]) + "</td>";
                                mbody += "</tr>";
                            }
                            mbody += "</table>";

                            //now set up the mail settings
                            MailMessage mmessage = new MailMessage();
                            mmessage.IsBodyHtml = true;
                            //  message.From = new MailAddress(NewTextBox222.Text);
                            mmessage.From = new MailAddress("no-reply@alramsat.com");
                            //can add more recipient
                            // message.To.Add(new MailAddress(emailid));                          
                            //  message.To .Add(new MailAddress(ListBox1.SelectedItem.Text ));
                            //   message.To.Add(new MailAddress(email3));

                            //  message.To.Add(new MailAddress(item.Text));

                            // int k = Convert.ToInt32 ( list_emails.Count.ToString ());                          

                            for (int j = 0; j < mlist_emails.Count; j++)
                            {
                                mmessage.To.Add(new MailAddress(mlist_emails[j].ToString()));
                            }

                            for (int m = 0; m < mClientlist_emails.Count; m++)
                            {
                                mmessage.CC.Add(new MailAddress(mClientlist_emails[m].ToString()));
                            }

                            //for (int n = 0; n < mConsultantlist_emails.Count; n++)
                            //{
                            //    mmessage.To.Add(new MailAddress(mConsultantlist_emails[n].ToString()));
                            //}

                            mmessage.Subject = "Outstanding Submittals Of " + projectname_ + " Auto Notification dated " + DateTime.Now;
                            mmessage.Body = mbody1 + mbody;

                            //SmtpClient client = new SmtpClient();
                            //client.Send(message);                           

                            SmtpClient rsmtp = new SmtpClient();
                            rsmtp.Host = "smtp.office365.com";
                            rsmtp.Port = 587;
                            rsmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            rsmtp.EnableSsl = true;
                            System.Net.NetworkCredential srcredential = new System.Net.NetworkCredential("no-reply@alramsat.com", "ksqlmhnxbgywbbqp");
                            rsmtp.UseDefaultCredentials = false;
                            rsmtp.Credentials = srcredential;
                            rsmtp.Send(mmessage);
                            mmessage = null;

                            SqlConnection con1 = new SqlConnection(ConnStr);
                            con1.Open();
                            string reportstr1 = "INSERT into tblnotify (revno, action, category,username,reportdate,entrydate) values (@revno, @action, @category,@username,@reportdate,@entrydate)";
                            SqlCommand reportstrcmd1 = new SqlCommand(reportstr1, con1);
                            reportstrcmd1.Parameters.AddWithValue("@revno", projectid_);
                            reportstrcmd1.Parameters.AddWithValue("@action", projectname_ + " Auto Notification dated " + DateTime.Now);
                            reportstrcmd1.Parameters.AddWithValue("@category", "Pending Submittal Alerts");
                            reportstrcmd1.Parameters.AddWithValue("@username", "No-Reply");
                            reportstrcmd1.Parameters.AddWithValue("@reportdate", DateTime.Now.ToString("yyyy-MM-dd"));
                            reportstrcmd1.Parameters.AddWithValue("@entrydate", DateTime.Now);
                            reportstrcmd1.ExecuteNonQuery();

                        }

                        // RFI SUBMITTAL EMAIL NOTIFICATION STARTS

                        //   int rfidelaydays_ = Convert.ToInt32(Title12[1]);
                        //   string rfiprojectid_ = Title12[0].ToString();
                        // var diffdate = DateTime.Today.AddDays(-10);

                        string rbody = "";
                        string rbody1 = "";
                        //   String rConnStr = "Data Source=attsrvr-01.eastdeltasa.com;Network Library=DBMSSOCN;Initial Catalog=Estimation;User ID=sqladmin;Password=sql@admin;";
                        //String SQL = "SELECT slno, reportname FROM dailyattreport where reportdate='2018-09-02'";
                        //  String SQL = "SELECT [TDATE], [PERNR], [ENAME], [CHECKIN], [CHECKOUT], [NEXTDAYIN], [NEXTDAYOUT], [PROJECTNAME], [PROJECTLOCATION], [PMO_REMARK], [SITE_REMARK], [AREA_SP_REMARK] FROM [ZHR_TB_ATT_A] WHERE (([TDATE] = '" + HRAttdatetxt.Text + "') AND ([RECORD] = 'T') AND ([PMO_REMARK] = 'Absent') AND PROJECTNAME='" + projectname + "') ORDER BY PROJECTNAME,PROJECTLOCATION";
                        String rfiSQL = "select tid, revno,submittal,workscope, subject, submissiondate, preparedby, DATEDIFF(DAY, submissiondate, GETDATE())- '" + delaydays_ + "' AS TotDays from RFISubmittal where projectid = '" + projectid_ + "' AND status='OPEN' and DATEDIFF(DAY, submissiondate, GETDATE()) > '" + delaydays_ + "' order by submittal,workscope,tid,revid";

                        SqlDataAdapter rfiTitlesAdpt = new SqlDataAdapter(rfiSQL, ConnStr);
                        DataSet rfiTitles = new DataSet();
                        // No need to open or close the connection
                        //   since the SqlDataAdapter will do this automatically.
                        rfiTitlesAdpt.Fill(rfiTitles);

                        using (SqlConnection conn = new SqlConnection())
                        {
                            conn.ConnectionString = "Data Source=fileserver-2a5c147d11735233.elb.me-south-1.amazonaws.com;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sa;Password=Sqladmin@123;";

                            conn.Open();

                            ArrayList rlist_emails = new ArrayList();
                            ArrayList rClientlist_emails = new ArrayList();
                            ArrayList rConsultantlist_emails = new ArrayList();

                            //  string[] list_emails;

                            int i = 0;
                            string rEdscoemail;
                            //  SqlCommand rEdsco_Email = new SqlCommand("SELECT  tblprojectkey.empemail  FROM tblprojectkey INNER JOIN newproject ON tblprojectkey.projectid = newproject.projectid WHERE (tblprojectkey.empemail <> '') AND (tblprojectkey.empemail IS NOT NULL) AND newproject.status = 'ONGOING' and  tblprojectkey .projectid = '" + projectid_ + "' and newproject.submittalalerts = 1 and tblprojectkey . type = 'EDSCO' and tblprojectkey .submittalfiles = 1", conn);
                            SqlCommand rEdsco_Email = new SqlCommand("Select emailid from tblpendingalerts where projectid = '" + projectid_ + "' AND type = 'TO'", conn);
                            SqlDataReader rread_Email = rEdsco_Email.ExecuteReader();
                            while (rread_Email.Read())
                            {
                                rEdscoemail = rread_Email.GetValue(i).ToString();
                                rlist_emails.Add(rEdscoemail); //Add email to a arraylist
                                                               //    emails = read_Email.GetValue(i).ToString();
                                                               //list_emails = email.Split(',');
                                                               //  list_emails = email;

                                i = i + 1 - 1; //increment or ++i
                            }
                            rread_Email.Close();

                            int p = 0;
                            string rClientemail;
                            //   SqlCommand rClient_Email = new SqlCommand("SELECT  tblprojectkey.empemail  FROM tblprojectkey INNER JOIN newproject ON tblprojectkey.projectid = newproject.projectid WHERE (tblprojectkey.empemail <> '') AND (tblprojectkey.empemail IS NOT NULL) AND newproject.status = 'ONGOING' and  tblprojectkey .projectid = '" + projectid_ + "' and newproject.submittalalerts = 1 and tblprojectkey . type = 'CLIENT' and tblprojectkey .submittalfiles = 1", conn);
                            SqlCommand rClient_Email = new SqlCommand("Select emailid from tblpendingalerts where projectid = '" + projectid_ + "' AND type = 'CC'", conn);
                            SqlDataReader rread_Email1 = rClient_Email.ExecuteReader();
                            while (rread_Email1.Read())
                            {
                                rClientemail = rread_Email1.GetValue(p).ToString();
                                rClientlist_emails.Add(rClientemail); //Add email to a arraylist
                                                                      //    emails = read_Email.GetValue(i).ToString();
                                                                      //list_emails = email.Split(',');
                                                                      //  list_emails = email;

                                p = p + 1 - 1; //increment or ++i
                            }
                            rread_Email1.Close();

                            //  body1 += "Dear Eng. " + empname + ",<br /><br /> Kindly find the below ABSENT records for today, Please check the list and add your comments If any, with note that sanction list automatically applied.<br /><br /> NOTE: This email is Auto generated from the system. So Please don't reply to this email. <br /><br />";
                            rbody1 += "Dear Sir, <br /><br /> Kindly find the below RFI submittal which has no ACTION and need to take action. <br /><br /> NOTE: This email is Auto generated from the system. So Please don't reply to this email. <br /><br />";

                            rbody = "<table border=" + 1 + " cellpadding=" + 1 + " cellspacing=" + 1 + " font-family=Segoe UI>";
                            rbody += "<tr bgcolor='LightGreen' font-family='Century Gothic'>";
                            rbody += "<td>TRANSID</td>";
                            rbody += "<td>SUBMITTAL NUMBER</td>";
                            rbody += "<td>SUBMITTAL TYPE</td>";
                            rbody += "<td>WORK SCOPE</td>";
                            rbody += "<td>SUBJECT</td>";
                            rbody += "<td>SUBMISSION DATE</td>";
                            rbody += "<td>PREPARED BY</td>";
                            rbody += "<td>DELAY DAYS</td>";

                            rbody += "</tr>";

                            foreach (DataRow rTitle in rfiTitles.Tables[0].Rows)
                            {
                                rbody += "<tr>";
                                // body += "<td>" + Title[0] + "</td>";
                                rbody += "<td>" + rTitle[0] + "</td>";
                                //body += "<td>" + String.Format("{0:c}", Title[0]) + "</td>";
                                rbody += "<td>" + String.Format("{0:c}", rTitle[1]) + "</td>";
                                rbody += "<td>" + String.Format("{0:c}", rTitle[2]) + "</td>";
                                rbody += "<td>" + String.Format("{0:c}", rTitle[3]) + "</td>";
                                rbody += "<td>" + String.Format("{0:c}", rTitle[4]) + "</td>";
                                //  rbody += "<td>" + String.Format("{0:c}", rTitle[5]) + "</td>";
                                rbody += "<td>" + Convert.ToDateTime(rTitle[5]).ToString("dd-MMM-yyyy") + "</td>";
                                rbody += "<td>" + String.Format("{0:c}", rTitle[6]) + "</td>";
                                rbody += "<td>" + rTitle[7] + "</td>";
                                rbody += "</tr>";
                            }
                            rbody += "</table>";

                            //now set up the mail settings
                            MailMessage rmessage = new MailMessage();
                            rmessage.IsBodyHtml = true;
                            //  message.From = new MailAddress(NewTextBox222.Text);
                            rmessage.From = new MailAddress("no-reply@alramsat.com");
                            //can add more recipient
                            // message.To.Add(new MailAddress(emailid));                          
                            //  message.To .Add(new MailAddress(ListBox1.SelectedItem.Text ));
                            //   message.To.Add(new MailAddress(email3));

                            //  message.To.Add(new MailAddress(item.Text));

                            // int k = Convert.ToInt32 ( list_emails.Count.ToString ());                          

                            for (int j = 0; j < rlist_emails.Count; j++)
                            {
                                rmessage.To.Add(new MailAddress(rlist_emails[j].ToString()));
                            }

                            for (int m = 0; m < rClientlist_emails.Count; m++)
                            {
                                rmessage.CC.Add(new MailAddress(rClientlist_emails[m].ToString()));
                            }

                            //for (int n = 0; n < rConsultantlist_emails.Count; n++)
                            //{
                            //    rmessage.To.Add(new MailAddress(rConsultantlist_emails[n].ToString()));
                            //}

                            rmessage.Subject = "Outstanding RFI Submittals Of " + projectname_ + " Auto Notification dated " + DateTime.Now;
                            rmessage.Body = rbody1 + rbody;

                            //SmtpClient client = new SmtpClient();
                            //client.Send(message);

                            SmtpClient rsmtp = new SmtpClient();
                            rsmtp.Host = "smtp.office365.com";
                            rsmtp.Port = 587;
                            rsmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            rsmtp.EnableSsl = true;
                            System.Net.NetworkCredential srcredential = new System.Net.NetworkCredential("no-reply@alramsat.com", "ksqlmhnxbgywbbqp");
                            rsmtp.UseDefaultCredentials = false;
                            rsmtp.Credentials = srcredential;
                            rsmtp.Send(rmessage);
                            rmessage = null;
                            // lbltechHR1.Text = "VOX REDSEA SUBMITTAL MAIL SENT SUCCESSFULLY...!!!";    

                            SqlConnection con1 = new SqlConnection(ConnStr);
                            con1.Open();
                            string reportstr1 = "INSERT into tblnotify (revno, action, category,username,reportdate,entrydate) values (@revno, @action, @category,@username,@reportdate,@entrydate)";
                            SqlCommand reportstrcmd1 = new SqlCommand(reportstr1, con1);
                            reportstrcmd1.Parameters.AddWithValue("@revno", projectid_);
                            reportstrcmd1.Parameters.AddWithValue("@action", projectname_ + " Auto Notification dated " + DateTime.Now);
                            reportstrcmd1.Parameters.AddWithValue("@category", "Pending RFI Submittal Alerts");
                            reportstrcmd1.Parameters.AddWithValue("@username", "No-Reply");
                            reportstrcmd1.Parameters.AddWithValue("@reportdate", DateTime.Now.ToString("yyyy-MM-dd"));
                            reportstrcmd1.Parameters.AddWithValue("@entrydate", DateTime.Now);
                            reportstrcmd1.ExecuteNonQuery();

                        }

                        // RFI SUBMITTAL EMAIL NOTIFICATION ENDS 

                        // SIR SUBMITTAL EMAIL NOTIFICATION STARTS

                        //   int srfidelaydays_ = Convert.ToInt32(Title12[1]);
                        //   string srfiprojectid_ = Title12[0].ToString();
                        //   string srfiprojectname_ = Title12[2].ToString();
                        string ssbody = "";
                        string ssbody1 = "";
                        //  String ConnStr = "Data Source=attsrvr-01.eastdeltasa.com;Network Library=DBMSSOCN;Initial Catalog=Estimation;User ID=sqladmin;Password=sql@admin;";
                        //String SQL = "SELECT slno, reportname FROM dailyattreport where reportdate='2018-09-02'";
                        //  String SQL = "SELECT [TDATE], [PERNR], [ENAME], [CHECKIN], [CHECKOUT], [NEXTDAYIN], [NEXTDAYOUT], [PROJECTNAME], [PROJECTLOCATION], [PMO_REMARK], [SITE_REMARK], [AREA_SP_REMARK] FROM [ZHR_TB_ATT_A] WHERE (([TDATE] = '" + HRAttdatetxt.Text + "') AND ([RECORD] = 'T') AND ([PMO_REMARK] = 'Absent') AND PROJECTNAME='" + projectname + "') ORDER BY PROJECTNAME,PROJECTLOCATION";
                        String SQL3 = "select tid, revno, workscope, subject, submissiondate, preparedby, DATEDIFF(DAY, submissiondate, GETDATE()) - '" + delaydays_ + "' AS TotDays from SIRSubmittal where projectid = '" + projectid_ + "' AND (status='' or status = 'E') and DATEDIFF(DAY, submissiondate, GETDATE()) > '" + delaydays_ + "' order by workscope,tid,revid";

                        SqlDataAdapter sTitlesAdpt = new SqlDataAdapter(SQL3, ConnStr);
                        DataSet sTitles = new DataSet();
                        // No need to open or close the connection
                        //   since the SqlDataAdapter will do this automatically.
                        sTitlesAdpt.Fill(sTitles);
                        //  body1 += "Dear Eng. " + empname + ",<br /><br /> Kindly find the below ABSENT records for today, Please check the list and add your comments If any, with note that sanction list automatically applied.<br /><br /> NOTE: This email is Auto generated from the system. So Please don't reply to this email. <br /><br />";


                        using (SqlConnection conn = new SqlConnection())
                        {
                            conn.ConnectionString = "Data Source=fileserver-2a5c147d11735233.elb.me-south-1.amazonaws.com;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sa;Password=Sqladmin@123;";

                            conn.Open();

                            ArrayList srlist_emails = new ArrayList();
                            ArrayList srClientlist_emails = new ArrayList();
                            ArrayList srConsultantlist_emails = new ArrayList();

                            //  string[] list_emails;
                            int i = 0;
                            string srEdscoemail;
                            //   SqlCommand srEdsco_Email = new SqlCommand("SELECT  tblprojectkey.empemail  FROM tblprojectkey INNER JOIN newproject ON tblprojectkey.projectid = newproject.projectid WHERE (tblprojectkey.empemail <> '') AND (tblprojectkey.empemail IS NOT NULL) AND newproject.status = 'ONGOING' and  tblprojectkey .projectid = '" + projectid_ + "' and newproject.submittalalerts = 1 and tblprojectkey . type = 'EDSCO' and tblprojectkey .submittalfiles = 1 ", conn);
                            SqlCommand srEdsco_Email = new SqlCommand("Select emailid from tblpendingalerts where projectid = '" + projectid_ + "' AND type = 'TO'", conn);
                            SqlDataReader srread_Email = srEdsco_Email.ExecuteReader();
                            while (srread_Email.Read())
                            {
                                srEdscoemail = srread_Email.GetValue(i).ToString();
                                srlist_emails.Add(srEdscoemail); //Add email to a arraylist
                                                                 //    emails = read_Email.GetValue(i).ToString();
                                                                 //list_emails = email.Split(',');
                                                                 //  list_emails = email;

                                i = i + 1 - 1; //increment or ++i
                            }
                            srread_Email.Close();

                            int p = 0;
                            string srClientemail;
                            //   SqlCommand srClient_Email = new SqlCommand("SELECT  tblprojectkey.empemail  FROM tblprojectkey INNER JOIN newproject ON tblprojectkey.projectid = newproject.projectid WHERE (tblprojectkey.empemail <> '') AND (tblprojectkey.empemail IS NOT NULL) AND newproject.status = 'ONGOING' and  tblprojectkey .projectid = '" + projectid_ + "' and newproject.submittalalerts = 1 and tblprojectkey . type = 'CLIENT' and tblprojectkey .submittalfiles = 1", conn);
                            SqlCommand srClient_Email = new SqlCommand("Select emailid from tblpendingalerts where projectid = '" + projectid_ + "' AND type = 'CC'", conn);
                            SqlDataReader srread_Email1 = srClient_Email.ExecuteReader();
                            while (srread_Email1.Read())
                            {
                                srClientemail = srread_Email1.GetValue(p).ToString();
                                srClientlist_emails.Add(srClientemail); //Add email to a arraylist
                                                                        //    emails = read_Email.GetValue(i).ToString();
                                                                        //list_emails = email.Split(',');
                                                                        //  list_emails = email;

                                p = p + 1 - 1; //increment or ++i
                            }
                            srread_Email1.Close();

                            //int q = 0;
                            //string srConsultemail;
                            //SqlCommand srConsultant_Email = new SqlCommand("SELECT  tblprojectkey.empemail  FROM tblprojectkey INNER JOIN newproject ON tblprojectkey.projectid = newproject.projectid WHERE (tblprojectkey.empemail <> '') AND (tblprojectkey.empemail IS NOT NULL) AND newproject.status = 'ONGOING' and  tblprojectkey .projectid = '" + projectid_ + "' and newproject.submittalalerts = 1 and tblprojectkey . type = 'CONSULTANT' and tblprojectkey .submittalfiles = 1 ", conn);
                            //SqlDataReader srread_Email2 = srConsultant_Email.ExecuteReader();
                            //while (srread_Email2.Read())
                            //{
                            //    srConsultemail = srread_Email2.GetValue(q).ToString();
                            //    srConsultantlist_emails.Add(srConsultemail); //Add email to a arraylist
                            //                                                 //    emails = read_Email.GetValue(i).ToString();
                            //                                                 //list_emails = email.Split(',');
                            //                                                 //  list_emails = email;

                            //    q = q + 1 - 1; //increment or ++i
                            //}
                            //srread_Email2.Close();

                            //  body1 += "Dear Eng. " + empname + ",<br /><br /> Kindly find the below ABSENT records for today, Please check the list and add your comments If any, with note that sanction list automatically applied.<br /><br /> NOTE: This email is Auto generated from the system. So Please don't reply to this email. <br /><br />";
                            ssbody1 += "Dear Sir, <br /><br /> Kindly find the below SITE INSPECTION REQUEST (SIR) submittals which has no ACTION and need to take action. <br /><br /> NOTE: This email is Auto generated from the system. So Please don't reply to this email. <br /><br />";

                            ssbody = "<table border=" + 1 + " cellpadding=" + 1 + " cellspacing=" + 1 + " font-family=Segoe UI>";
                            ssbody += "<tr bgcolor='LightGreen' font-family='Century Gothic'>";
                            ssbody += "<td>TRANSID</td>";
                            ssbody += "<td>SUBMITTAL NUMBER</td>";
                            ssbody += "<td>WORK SCOPE</td>";
                            ssbody += "<td>SUBJECT</td>";
                            ssbody += "<td>SUBMISSION DATE</td>";
                            ssbody += "<td>PREPARED BY</td>";
                            ssbody += "<td>DELAY DAYS</td>";

                            ssbody += "</tr>";

                            foreach (DataRow sTitle in sTitles.Tables[0].Rows)
                            {
                                ssbody += "<tr>";
                                // body += "<td>" + Title[0] + "</td>";
                                ssbody += "<td>" + sTitle[0] + "</td>";
                                //body += "<td>" + String.Format("{0:c}", Title[0]) + "</td>";
                                ssbody += "<td>" + String.Format("{0:c}", sTitle[1]) + "</td>";
                                ssbody += "<td>" + String.Format("{0:c}", sTitle[2]) + "</td>";
                                ssbody += "<td>" + String.Format("{0:c}", sTitle[3]) + "</td>";
                                //  ssbody += "<td>" + String.Format("{0:c}", sTitle[4]) + "</td>";
                                ssbody += "<td>" + Convert.ToDateTime(sTitle[4]).ToString("dd-MMM-yyyy") + "</td>";
                                ssbody += "<td>" + String.Format("{0:c}", sTitle[5]) + "</td>";
                                ssbody += "<td>" + sTitle[6] + "</td>";
                                ssbody += "</tr>";
                            }
                            ssbody += "</table>";

                            //now set up the mail settings
                            MailMessage srmessage = new MailMessage();
                            srmessage.IsBodyHtml = true;
                            //  message.From = new MailAddress(NewTextBox222.Text);
                            srmessage.From = new MailAddress("no-reply@alramsat.com");
                            //can add more recipient
                            // message.To.Add(new MailAddress(emailid));                          
                            //  message.To .Add(new MailAddress(ListBox1.SelectedItem.Text ));
                            //   message.To.Add(new MailAddress(email3));

                            //  message.To.Add(new MailAddress(item.Text));

                            // int k = Convert.ToInt32 ( list_emails.Count.ToString ());                          

                            for (int j = 0; j < srlist_emails.Count; j++)
                            {
                                srmessage.To.Add(new MailAddress(srlist_emails[j].ToString()));
                            }

                            for (int m = 0; m < srClientlist_emails.Count; m++)
                            {
                                srmessage.CC.Add(new MailAddress(srClientlist_emails[m].ToString()));
                            }

                            //for (int n = 0; n < srConsultantlist_emails.Count; n++)
                            //{
                            //    srmessage.To.Add(new MailAddress(srConsultantlist_emails[n].ToString()));
                            //}

                            srmessage.Subject = "Outstanding SIR Submittals Of " + projectname_ + " Auto Notification dated " + DateTime.Now;
                            srmessage.Body = ssbody1 + ssbody;

                            //SmtpClient client = new SmtpClient();
                            //client.Send(message);

                            SmtpClient rsmtp = new SmtpClient();
                            rsmtp.Host = "smtp.office365.com";
                            rsmtp.Port = 587;
                            rsmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            rsmtp.EnableSsl = true;
                            System.Net.NetworkCredential srcredential = new System.Net.NetworkCredential("no-reply@alramsat.com", "ksqlmhnxbgywbbqp");
                            rsmtp.UseDefaultCredentials = false;
                            rsmtp.Credentials = srcredential;
                            rsmtp.Send(srmessage);
                            srmessage = null;
                            // lbltechHR1.Text = "VOX REDSEA SUBMITTAL MAIL SENT SUCCESSFULLY...!!!";  

                            SqlConnection con1 = new SqlConnection(ConnStr);
                            con1.Open();
                            string reportstr1 = "INSERT into tblnotify (revno, action, category,username,reportdate,entrydate) values (@revno, @action, @category,@username,@reportdate,@entrydate)";
                            SqlCommand reportstrcmd1 = new SqlCommand(reportstr1, con1);
                            reportstrcmd1.Parameters.AddWithValue("@revno", projectid_);
                            reportstrcmd1.Parameters.AddWithValue("@action", projectname_ + " Auto Notification dated " + DateTime.Now);
                            reportstrcmd1.Parameters.AddWithValue("@category", "Pending SIR Submittal Alerts");
                            reportstrcmd1.Parameters.AddWithValue("@username", "No-Reply");
                            reportstrcmd1.Parameters.AddWithValue("@reportdate", DateTime.Now.ToString("yyyy-MM-dd"));
                            reportstrcmd1.Parameters.AddWithValue("@entrydate", DateTime.Now);
                            reportstrcmd1.ExecuteNonQuery();

                        }

                        // SIR SUBMITTAL EMAIL NOTIFICATION ENDS

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

                String ConnStr1 = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";
                //    String SQL1 = "SELECT ZHR_TB_ATT_A.TDATE, ZHR_TB_ATT_A.PERNR, ZHR_TB_ATT_A.ENAME, ZHR_TB_ATT_A.PROJECTNAME, emp.email, ZHR_TB_ATT_A.PMO_REMARK FROM emp INNER JOIN ZHR_TB_ATT_A ON emp.empid = ZHR_TB_ATT_A.PERNR WHERE (emp.email <> '' and emp.email is not null and emp.email <> 'No Email') and  ZHR_TB_ATT_A.TDATE ='" + lbldate + "' and RECORD = 'Y' and (ZHR_TB_ATT_A.PMO_REMARK <> '' and ZHR_TB_ATT_A.PMO_REMARK <> 'DAY OFF') and ( ZHR_TB_ATT_A.PMO_REMARK <> 'Business Trip' and ZHR_TB_ATT_A.PMO_REMARK <> 'Annual leave' and ZHR_TB_ATT_A.PMO_REMARK <> 'Exceptional Annual Leave' and ZHR_TB_ATT_A.PMO_REMARK <> 'Emergency Unpaid w Deds.' and  ZHR_TB_ATT_A.PMO_REMARK <> 'Unpaid leave' and ZHR_TB_ATT_A.PMO_REMARK <> 'Marriage leave' and ZHR_TB_ATT_A.PMO_REMARK <> 'Maternity leave' and ZHR_TB_ATT_A.PMO_REMARK <> 'Sick leave' and ZHR_TB_ATT_A.PMO_REMARK <> 'Exam Leave' and ZHR_TB_ATT_A.PMO_REMARK <> 'Child Birth leave' and ZHR_TB_ATT_A.PMO_REMARK <> 'Special Need Maternity Le') and ZHR_TB_ATT_A.PROJECTNAME not like '%VACATIONS%' and ZHR_TB_ATT_A.PERNR <> 1136 order by ZHR_TB_ATT_A.PERNR ";
                String SQL1 = "SELECT ZHR_TB_ATT_A.TDATE, ZHR_TB_ATT_A.PERNR, ZHR_TB_ATT_A.ENAME, ZHR_TB_ATT_A.PROJECTNAME, emp.email, ZHR_TB_ATT_A.PMO_REMARK FROM emp INNER JOIN ZHR_TB_ATT_A ON emp.empid = ZHR_TB_ATT_A.PERNR WHERE  ZHR_TB_ATT_A.TDATE ='" + lbldate + "' and RECORD = 'Y' and (ZHR_TB_ATT_A.PMO_REMARK <> 'Initial Record' and ZHR_TB_ATT_A.PMO_REMARK <> 'Day Off') and (emp.email <> '' and emp.email is not null and emp.email <> 'No Email') and ZHR_TB_ATT_A.PROJECTNAME <> 'OUT OF SCOPE'  ORDER BY ZHR_TB_ATT_A.PROJECTNAME ";
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
                    string bodyemp = "";
                    string body = "";
                    string body1 = "";
                    string body2 = "";
                    String ConnStr = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";
                    //String SQL = "SELECT slno, reportname FROM dailyattreport where reportdate='2018-09-02'";
                    //String SQL = "SELECT [TDATE], [PERNR], [ENAME], [CHECKIN], [CHECKOUT], [PROJECTNAME], [PMO_REMARK], [AREA_SP_REMARK],[PMO_REMARK1], [HR_REMARK]FROM [ZHR_TB_ATT_A] WHERE (([TDATE] = '" + HRAttdatetxt.Text + "') AND ([RECORD] = 'Y') AND ([PMO_REMARK] = 'Absent') AND PROJECTNAME='" + projectname + "') ORDER BY PROJECTNAME,PROJECTLOCATION";
                    String SQL = "SELECT [TDATE] AS DATE, [PERNR] AS EMPID, [ENAME] AS 'EMPLOYEE NAME', [CHECKIN], [CHECKOUT], [DELAY_DUR] AS 'DELAY TIME', [PROJECTNAME], [PROJECTLOCATION] AS LOCATION, [PMO_REMARK] as STATUS FROM [ZHR_TB_ATT_A] WHERE ([TDATE] = '" + lbldate + "' AND [RECORD] = 'Y') and pernr='" + empid + "' ";
                    SqlDataAdapter TitlesAdpt = new SqlDataAdapter(SQL, ConnStr);
                    DataSet Titles = new DataSet();
                    // No need to open or close the connection
                    //   since the SqlDataAdapter will do this automatically.
                    TitlesAdpt.Fill(Titles);
                    bodyemp += "Dear " + empname ;
                    string arabicText = @"
                    <html>
                    <body style='direction: rtl;'>
                        إشارة الى المخالفة التي ارتكبتها والموضحة أدناه, التأخر في الحضور للعمل حسب الأوقات الرسمية المقررة بتاريخ
                        يعتبر هذا انذار بالتأخير يرجى استخدام الرابط أدناه لوصول الى نظام ادارة الحضور والتقدم بالطلب للحصول على الأذونات 
                        <a href='https://ams.alramsat.com:8443/LoginPage.aspx'>https://ams.alramsat.com:8443/LoginPage.aspx</a>                     
                    </body>
                    </html>";
                    //   body1 += " <br /><br /> https://ams.alramsat.com:8443/LoginPage.aspx  يعتبر هذا انذار بالتأخير يرجى استخدام الرابط أدناه لوصول الى نظام ادارة الحضور والتقدم بالطلب للحصول على الأذونات " + lbldate + " إشارة الى المخالفة التي ارتكبتها والموضحة أدناه, التأخر في الحضور للعمل حسب الأوقات الرسمية المقررة بتاريخ";
                    body2 += "<br /><br /> This is a notice of your irregular attendance, find below your inconsistence attendance record that has been registered at " + lbldate + ". Kindly use the below link to access to our Attendance Management System (AMS) and apply for permissions.<br /> https://ams.alramsat.com:8443/LoginPage.aspx <br /> <br /><br />";
                    body = "<table border=" + 1 + " cellpadding=" + 1 + " cellspacing=" + 1 + " width=100% font-family=Segoe UI>";
                    body += "<tr bgcolor='#4da6ff' font-family=Segoe UI>";
                    body += "<td>DATE</td>";
                    body += "<td>EMPID</td>";
                    body += "<td>EMPLOYEE NAME</td>";
                    body += "<td>CHECKIN</td>";
                    body += "<td>CHECKOUT</td>";
                    body += "<td>DELAY_DUR</td>";
                    body += "<td>PROJECT NAME</td>";
                    body += "<td>LOCATION</td>";
                    body += "<td>STATUS</td>";
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
                    message.From = new MailAddress("no-reply@alramsat.com");
                    //can add more recipient
                    message.To.Add(new MailAddress(emailid));

                    //add cc
                    message.CC.Add(new MailAddress("kshareef@alramsat.com"));

                    message.Subject = "Attendance Auto Notification dated " + lbldate;

                    message.Body = bodyemp + arabicText + body2 + body;

                    //SmtpClient client = new SmtpClient();
                    //client.Send(message);

                    SmtpClient rsmtp = new SmtpClient();
                    rsmtp.Host = "smtp.office365.com";
                    rsmtp.Port = 587;
                    rsmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    rsmtp.EnableSsl = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    System.Net.NetworkCredential srcredential = new System.Net.NetworkCredential("no-reply@alramsat.com", "ksqlmhnxbgywbbqp");
                    rsmtp.UseDefaultCredentials = false;
                    rsmtp.Credentials = srcredential;
                    rsmtp.Send(message);
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

                String ConnStr1 = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";
                //    String SQL1 = "SELECT ZHR_TB_ATT_A.TDATE, ZHR_TB_ATT_A.PERNR, ZHR_TB_ATT_A.ENAME, ZHR_TB_ATT_A.PROJECTNAME, emp.email, ZHR_TB_ATT_A.PMO_REMARK FROM emp INNER JOIN ZHR_TB_ATT_A ON emp.empid = ZHR_TB_ATT_A.PERNR WHERE (emp.email <> '' and emp.email is not null and emp.email <> 'No Email') and  ZHR_TB_ATT_A.TDATE ='" + lbldate + "' and RECORD = 'Y' and (ZHR_TB_ATT_A.PMO_REMARK <> '' and ZHR_TB_ATT_A.PMO_REMARK <> 'DAY OFF') and ( ZHR_TB_ATT_A.PMO_REMARK <> 'Business Trip' and ZHR_TB_ATT_A.PMO_REMARK <> 'Annual leave' and ZHR_TB_ATT_A.PMO_REMARK <> 'Exceptional Annual Leave' and ZHR_TB_ATT_A.PMO_REMARK <> 'Emergency Unpaid w Deds.' and  ZHR_TB_ATT_A.PMO_REMARK <> 'Unpaid leave' and ZHR_TB_ATT_A.PMO_REMARK <> 'Marriage leave' and ZHR_TB_ATT_A.PMO_REMARK <> 'Maternity leave' and ZHR_TB_ATT_A.PMO_REMARK <> 'Sick leave' and ZHR_TB_ATT_A.PMO_REMARK <> 'Exam Leave' and ZHR_TB_ATT_A.PMO_REMARK <> 'Child Birth leave' and ZHR_TB_ATT_A.PMO_REMARK <> 'Special Need Maternity Le') and ZHR_TB_ATT_A.PROJECTNAME not like '%VACATIONS%' and ZHR_TB_ATT_A.PERNR <> 1136 order by ZHR_TB_ATT_A.PERNR ";
                String SQL1 = "SELECT ZHR_TB_ATT_A.TDATE, ZHR_TB_ATT_A.PERNR, ZHR_TB_ATT_A.ENAME, ZHR_TB_ATT_A.PROJECTNAME, emp.email, ZHR_TB_ATT_A.PMO_REMARK FROM emp INNER JOIN ZHR_TB_ATT_A ON emp.empid = ZHR_TB_ATT_A.PERNR WHERE  ZHR_TB_ATT_A.TDATE ='" + lbldate + "' and RECORD = 'Y' and (ZHR_TB_ATT_A.PMO_REMARK <> 'Initial Record' and ZHR_TB_ATT_A.PMO_REMARK <> 'Day Off') and (emp.email <> '' and emp.email is not null and emp.email <> 'No Email') and ZHR_TB_ATT_A.PROJECTNAME <> 'OUT OF SCOPE'  ORDER BY ZHR_TB_ATT_A.PROJECTNAME ";

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
                    string bodyemp = "";
                    string body = "";
                    string body1 = "";
                    string body2 = "";
                    String ConnStr = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";
                    //String SQL = "SELECT slno, reportname FROM dailyattreport where reportdate='2018-09-02'";
                    //String SQL = "SELECT [TDATE], [PERNR], [ENAME], [CHECKIN], [CHECKOUT], [PROJECTNAME], [PMO_REMARK], [AREA_SP_REMARK],[PMO_REMARK1], [HR_REMARK]FROM [ZHR_TB_ATT_A] WHERE (([TDATE] = '" + HRAttdatetxt.Text + "') AND ([RECORD] = 'Y') AND ([PMO_REMARK] = 'Absent') AND PROJECTNAME='" + projectname + "') ORDER BY PROJECTNAME,PROJECTLOCATION";
                    String SQL = "SELECT [TDATE] AS DATE, [PERNR] AS EMPID, [ENAME] AS 'EMPLOYEE NAME', [CHECKIN], [CHECKOUT], [DELAY_DUR] AS 'DELAY TIME', [PROJECTNAME], [PROJECTLOCATION] AS LOCATION, [PMO_REMARK] as STATUS FROM [ZHR_TB_ATT_A] WHERE ([TDATE] = '" + lbldate + "' AND [RECORD] = 'Y') and pernr='" + empid + "' ";
                    SqlDataAdapter TitlesAdpt = new SqlDataAdapter(SQL, ConnStr);
                    DataSet Titles = new DataSet();
                    // No need to open or close the connection
                    //   since the SqlDataAdapter will do this automatically.
                    TitlesAdpt.Fill(Titles);
                    bodyemp += "Dear " + empname;
                    string arabicText = @"
                    <html>
                    <body style='direction: rtl;'>
                        إشارة الى المخالفة التي ارتكبتها والموضحة أدناه, التأخر في الحضور للعمل حسب الأوقات الرسمية المقررة بتاريخ
                        يعتبر هذا انذار بالتأخير يرجى استخدام الرابط أدناه لوصول الى نظام ادارة الحضور والتقدم بالطلب للحصول على الأذونات 
                        <a href='https://ams.alramsat.com:8443/LoginPage.aspx'>https://ams.alramsat.com:8443/LoginPage.aspx</a>                     
                    </body>
                    </html>";
                 //   body1 += " <br /><br /> https://ams.alramsat.com:8443/LoginPage.aspx  يعتبر هذا انذار بالتأخير يرجى استخدام الرابط أدناه لوصول الى نظام ادارة الحضور والتقدم بالطلب للحصول على الأذونات " + lbldate + " إشارة الى المخالفة التي ارتكبتها والموضحة أدناه, التأخر في الحضور للعمل حسب الأوقات الرسمية المقررة بتاريخ";
                    body2 += "<br /><br /> This is a notice of your irregular attendance, find below your inconsistence attendance record that has been registered at " + lbldate + ". Kindly use the below link to access to our Attendance Management System (AMS) and apply for permissions.<br /> https://ams.alramsat.com:8443/LoginPage.aspx <br /> <br /><br />";
                    body = "<table border=" + 1 + " cellpadding=" + 1 + " cellspacing=" + 1 + " width=100% font-family=Segoe UI>";
                    body += "<tr bgcolor='#4da6ff' font-family=Segoe UI>";
                    body += "<td>DATE</td>";
                    body += "<td>EMPID</td>";
                    body += "<td>EMPLOYEE NAME</td>";
                    body += "<td>CHECKIN</td>";
                    body += "<td>CHECKOUT</td>";
                    body += "<td>DELAY_DUR</td>";
                    body += "<td>PROJECT NAME</td>";
                    body += "<td>LOCATION</td>";
                    body += "<td>STATUS</td>";
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
                    message.From = new MailAddress("no-reply@alramsat.com");
                    //can add more recipient
                 //   message.To.Add(new MailAddress(emailid));
                    message.To.Add(new MailAddress("kshareef@alramsat.com"));
                    //add cc
                    message.CC.Add(new MailAddress("kshareef@alramsat.com"));

                    message.Subject = "Attendance Auto Notification dated " + lbldate;

                    message.Body = bodyemp + arabicText + body2 + body;

                    //SmtpClient client = new SmtpClient();
                    //client.Send(message);

                    SmtpClient rsmtp = new SmtpClient();
                    rsmtp.Host = "smtp.office365.com";
                    rsmtp.Port = 587;
                    rsmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    rsmtp.EnableSsl = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    System.Net.NetworkCredential srcredential = new System.Net.NetworkCredential("no-reply@alramsat.com", "ksqlmhnxbgywbbqp");
                    rsmtp.UseDefaultCredentials = false;
                    rsmtp.Credentials = srcredential;
                    rsmtp.Send(message);
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

        public void AttendanceJobmail1()
        {
            //  String ConnStr1 = "Data Source=172.177.184.39;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sqladmin;Password=Sqlegypt@123456;";
            String ConnStr1 = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";
            using (SqlConnection conn61 = new SqlConnection(ConnStr1))
            {

                conn61.Open();
                string reportstr1 = "INSERT into tblnotify (empid, action, category,username,reportdate,entrydate) values (@empid, @action, @category,@username,@reportdate,@entrydate)";
                SqlCommand reportstrcmd1 = new SqlCommand(reportstr1, conn61);
                reportstrcmd1.Parameters.AddWithValue("@empid", "");
                reportstrcmd1.Parameters.AddWithValue("@action", "Attendance Jobs Auto Notification dated " + DateTime.Now);
                reportstrcmd1.Parameters.AddWithValue("@category", "Attendance Jobs Email");
                reportstrcmd1.Parameters.AddWithValue("@username", "SYSTEM");
                reportstrcmd1.Parameters.AddWithValue("@reportdate", DateTime.Now.ToString("yyyy-MM-dd"));
                reportstrcmd1.Parameters.AddWithValue("@entrydate", DateTime.Now);
                reportstrcmd1.ExecuteNonQuery();


                string mbody = "";
                string mbody1 = "";
                string startdate1 = DateTime.Now.ToString("yyyy-MM-dd");
                //   String ConnStr = "Data Source=fileserver-2a5c147d11735233.elb.me-south-1.amazonaws.com;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sa;Password=Sqladmin@123;";
                //String SQL = "SELECT slno, reportname FROM dailyattreport where reportdate='2018-09-02'";
                //  String SQL = "SELECT [TDATE], [PERNR], [ENAME], [CHECKIN], [CHECKOUT], [NEXTDAYIN], [NEXTDAYOUT], [PROJECTNAME], [PROJECTLOCATION], [PMO_REMARK], [SITE_REMARK], [AREA_SP_REMARK] FROM [ZHR_TB_ATT_A] WHERE (([TDATE] = '" + HRAttdatetxt.Text + "') AND ([RECORD] = 'T') AND ([PMO_REMARK] = 'Absent') AND PROJECTNAME='" + projectname + "') ORDER BY PROJECTNAME,PROJECTLOCATION";
                //  String SQL = "SELECT DISTINCT (t1.tid),t1.revno , t1.submittal, t1.workscope, t1.description, t1.submissiondate, t1.preparedby,t2.Maxactioncode, DATEDIFF(DAY, submissiondate, GETDATE()) - '" + delaydays_ + "' AS TotDays FROM ArchitectDrawings t1 INNER JOIN (SELECT MAX(actioncode) as Maxactioncode, revno FROM ArchitectDrawingItems  GROUP BY revno) t2 ON t1.revno = t2.revno where (t2.Maxactioncode IS NULL or t2.Maxactioncode = '') and t1 .projectid = '" + projectid_ + "' and DATEDIFF(DAY, submissiondate, GETDATE()) > '" + delaydays_ + "' order by t1.workscope, t1.submittal, t1.tid ";
                String MSQL = "SELECT slno, action, category, username, reportdate, entrydate FROM tblnotify WHERE username = 'SYSTEM' and reportdate = '" + startdate1 + "' order by slno";

                SqlDataAdapter mTitlesAdpt = new SqlDataAdapter(MSQL, ConnStr1);
                DataSet mTitles = new DataSet();
                // No need to open or close the connection
                //   since the SqlDataAdapter will do this automatically.
                mTitlesAdpt.Fill(mTitles);
                //  body1 += "Dear Eng. " + empname + ",<br /><br /> Kindly find the below ABSENT records for today, Please check the list and add your comments If any, with note that sanction list automatically applied.<br /><br /> NOTE: This email is Auto generated from the system. So Please don't reply to this email. <br /><br />";

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";

                    conn.Open();

                    mbody1 += "Dear Sir, <br /><br /> Kindly find the Attendance Auto Jobs list as follows. <br /><br /> ";

                    mbody = "<table border=" + 1 + " cellpadding=" + 1 + " cellspacing=" + 1 + " font-family=Segoe UI>";
                    mbody += "<tr bgcolor='LightGreen' font-family='Century Gothic'>";
                    mbody += "<td>SLNO</td>";
                    mbody += "<td>JOB DETAILS</td>";
                    mbody += "<td>CATEGORY</td>";
                    mbody += "<td>USER NAME</td>";
                    mbody += "<td>REPORT DATE</td>";
                    mbody += "<td>JOB TIME</td>";
                    mbody += "</tr>";

                    foreach (DataRow Title in mTitles.Tables[0].Rows)
                    {
                        mbody += "<tr>";
                        // body += "<td>" + Title[0] + "</td>";
                        mbody += "<td>" + Title[0] + "</td>";
                        mbody += "<td>" + String.Format("{0:c}", Title[1]) + "</td>";
                        mbody += "<td>" + String.Format("{0:c}", Title[2]) + "</td>";
                        mbody += "<td>" + String.Format("{0:c}", Title[3]) + "</td>";
                        mbody += "<td>" + Convert.ToDateTime(Title[4]).ToString("dd-MMM-yyyy") + "</td>";
                        mbody += "<td>" + Convert.ToDateTime(Title[5]).ToString("dd-MMM-yyyy hh:mm:ss") + "</td>";
                        //mbody += "<td>" + String.Format("{0:c}", Title[5]) + "</td>";

                        mbody += "</tr>";
                    }
                    mbody += "</table>";

                    //now set up the mail settings
                    MailMessage mmessage = new MailMessage();
                    mmessage.IsBodyHtml = true;
                    //  message.From = new MailAddress(NewTextBox222.Text);
                    mmessage.From = new MailAddress("no-reply@alramsat.com");
                    mmessage.To.Add(new MailAddress("kshareef@alramsat.com"));

                    // int k = Convert.ToInt32 ( list_emails.Count.ToString ());                         


                    mmessage.Subject = "Attendance Jobs Notification dated " + DateTime.Now;
                    mmessage.Body = mbody1 + mbody;

                    //SmtpClient client = new SmtpClient();
                    //client.Send(message);

                    SmtpClient rsmtp = new SmtpClient();
                    rsmtp.Host = "smtp.office365.com";
                    rsmtp.Port = 587;
                    rsmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    rsmtp.EnableSsl = true;
                    System.Net.NetworkCredential srcredential = new System.Net.NetworkCredential("no-reply@alramsat.com", "ksqlmhnxbgywbbqp");
                    rsmtp.UseDefaultCredentials = false;
                    rsmtp.Credentials = srcredential;
                    rsmtp.Send(mmessage);
                    mmessage = null;

                }

            }
        }

        public void AttendanceJobmail()
        {
            //  String ConnStr1 = "Data Source=172.177.184.39;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sqladmin;Password=Sqlegypt@123456;";
            String ConnStr1 = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";
            using (SqlConnection conn61 = new SqlConnection(ConnStr1))
            {
                //String MAINSQL = "select projectid, delaydays, projectname from newproject where submittalalerts =1 and status <> 'COMPLETED'";

                //SqlDataAdapter TitlesAdpt11 = new SqlDataAdapter(MAINSQL, ConnStr1);
                //DataSet Titles11 = new DataSet();
                //// No need to open or close the connection
                ////   since the SqlDataAdapter will do this automatically.
                //TitlesAdpt11.Fill(Titles11);

                conn61.Open();
                string reportstr1 = "INSERT into tblnotify (empid, action, category,username,reportdate,entrydate) values (@empid, @action, @category,@username,@reportdate,@entrydate)";
                SqlCommand reportstrcmd1 = new SqlCommand(reportstr1, conn61);
                reportstrcmd1.Parameters.AddWithValue("@empid", "");
                reportstrcmd1.Parameters.AddWithValue("@action", "Attendance Jobs Auto Notification dated " + DateTime.Now);
                reportstrcmd1.Parameters.AddWithValue("@category", "Attendance Jobs Email");
                reportstrcmd1.Parameters.AddWithValue("@username", "SYSTEM");
                reportstrcmd1.Parameters.AddWithValue("@reportdate", DateTime.Now.ToString("yyyy-MM-dd"));
                reportstrcmd1.Parameters.AddWithValue("@entrydate", DateTime.Now);
                reportstrcmd1.ExecuteNonQuery();


                string mbody = "";
                string mbody1 = "";
                string startdate1 = DateTime.Now.ToString("yyyy-MM-dd");
                //   String ConnStr = "Data Source=fileserver-2a5c147d11735233.elb.me-south-1.amazonaws.com;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sa;Password=Sqladmin@123;";
                //String SQL = "SELECT slno, reportname FROM dailyattreport where reportdate='2018-09-02'";
                //  String SQL = "SELECT [TDATE], [PERNR], [ENAME], [CHECKIN], [CHECKOUT], [NEXTDAYIN], [NEXTDAYOUT], [PROJECTNAME], [PROJECTLOCATION], [PMO_REMARK], [SITE_REMARK], [AREA_SP_REMARK] FROM [ZHR_TB_ATT_A] WHERE (([TDATE] = '" + HRAttdatetxt.Text + "') AND ([RECORD] = 'T') AND ([PMO_REMARK] = 'Absent') AND PROJECTNAME='" + projectname + "') ORDER BY PROJECTNAME,PROJECTLOCATION";
                //  String SQL = "SELECT DISTINCT (t1.tid),t1.revno , t1.submittal, t1.workscope, t1.description, t1.submissiondate, t1.preparedby,t2.Maxactioncode, DATEDIFF(DAY, submissiondate, GETDATE()) - '" + delaydays_ + "' AS TotDays FROM ArchitectDrawings t1 INNER JOIN (SELECT MAX(actioncode) as Maxactioncode, revno FROM ArchitectDrawingItems  GROUP BY revno) t2 ON t1.revno = t2.revno where (t2.Maxactioncode IS NULL or t2.Maxactioncode = '') and t1 .projectid = '" + projectid_ + "' and DATEDIFF(DAY, submissiondate, GETDATE()) > '" + delaydays_ + "' order by t1.workscope, t1.submittal, t1.tid ";
                String MSQL = "SELECT slno, action, category, username, reportdate, entrydate FROM tblnotify WHERE username = 'SYSTEM' and reportdate = '" + startdate1 + "' order by slno";

                SqlDataAdapter mTitlesAdpt = new SqlDataAdapter(MSQL, ConnStr1);
                DataSet mTitles = new DataSet();
                // No need to open or close the connection
                //   since the SqlDataAdapter will do this automatically.
                mTitlesAdpt.Fill(mTitles);
                //  body1 += "Dear Eng. " + empname + ",<br /><br /> Kindly find the below ABSENT records for today, Please check the list and add your comments If any, with note that sanction list automatically applied.<br /><br /> NOTE: This email is Auto generated from the system. So Please don't reply to this email. <br /><br />";

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";

                    conn.Open();

                    mbody1 += "Dear Sir, <br /><br /> Kindly find the Attendance Auto Jobs list as follows. <br /><br /> ";

                    mbody = "<table border=" + 1 + " cellpadding=" + 1 + " cellspacing=" + 1 + " font-family=Segoe UI>";
                    mbody += "<tr bgcolor='LightGreen' font-family='Century Gothic'>";
                    mbody += "<td>SLNO</td>";
                    mbody += "<td>JOB DETAILS</td>";
                    mbody += "<td>CATEGORY</td>";
                    mbody += "<td>USER NAME</td>";
                    mbody += "<td>REPORT DATE</td>";                  
                    mbody += "<td>JOB TIME</td>";
                    mbody += "</tr>";

                    foreach (DataRow Title in mTitles.Tables[0].Rows)
                    {
                        mbody += "<tr>";
                        // body += "<td>" + Title[0] + "</td>";
                        mbody += "<td>" + Title[0] + "</td>";
                        mbody += "<td>" + String.Format("{0:c}", Title[1]) + "</td>";
                        mbody += "<td>" + String.Format("{0:c}", Title[2]) + "</td>";
                        mbody += "<td>" + String.Format("{0:c}", Title[3]) + "</td>";
                        mbody += "<td>" + Convert.ToDateTime(Title[4]).ToString("dd-MMM-yyyy") + "</td>";
                        mbody += "<td>" + Convert.ToDateTime(Title[5]).ToString("dd-MMM-yyyy hh:mm:ss") + "</td>";
                        //mbody += "<td>" + String.Format("{0:c}", Title[5]) + "</td>";

                        mbody += "</tr>";
                    }
                    mbody += "</table>";

                    //now set up the mail settings
                    MailMessage mmessage = new MailMessage();
                    mmessage.IsBodyHtml = true;
                    //  message.From = new MailAddress(NewTextBox222.Text);
                    mmessage.From = new MailAddress("no-reply@alramsat.com");
                    //can add more recipient
                    // message.To.Add(new MailAddress(emailid));                          
                    //  message.To .Add(new MailAddress(ListBox1.SelectedItem.Text ));
                    //   message.To.Add(new MailAddress(email3));
                    mmessage.To.Add(new MailAddress("kshareef@alramsat.com"));
                    //  message.To.Add(new MailAddress(item.Text));

                    // int k = Convert.ToInt32 ( list_emails.Count.ToString ());                         


                    mmessage.Subject = "Attendance Jobs Notification dated " + DateTime.Now;
                    mmessage.Body = mbody1 + mbody;

                    //SmtpClient client = new SmtpClient();
                    //client.Send(message);

                    SmtpClient rsmtp = new SmtpClient();
                    rsmtp.Host = "smtp.office365.com";
                    rsmtp.Port = 587;
                    rsmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    rsmtp.EnableSsl = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    System.Net.NetworkCredential srcredential = new System.Net.NetworkCredential("no-reply@alramsat.com", "ksqlmhnxbgywbbqp");
                    rsmtp.UseDefaultCredentials = false;
                    rsmtp.Credentials = srcredential;
                    rsmtp.Send(mmessage);
                    mmessage = null;
                    // lbltechHR1.Text = "VOX REDSEA SUBMITTAL MAIL SENT SUCCESSFULLY...!!!";  

                    //SqlConnection con1 = new SqlConnection(ConnStr1);
                    //con1.Open();


                }



                // }
                // }
            }
        }
        static void SubmittalEmail_Alerts1()
        {
            //  String ConnStr1 = "Data Source=172.177.184.39;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sqladmin;Password=Sqlegypt@123456;";
            string ConnStr1 = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";
            using (SqlConnection conn61 = new SqlConnection(ConnStr1))
            {
                DateTime todaydate = DateTime.Now;
                string todaydate1 = todaydate.AddDays(-2).ToString("yyyy-MM-dd");
               
                //    string MAINSQL = "SELECT A9.empid, A9.badgenumber, A9.CheckDate, A9.wsid, Ews.empname, Ews.weekday, Ews.wstype, MAX(CASE WHEN IsFirstEntry = 1 THEN Ews.checkin END) AS wsCheckIn, MAX(CASE WHEN IsLastEntry = 1 THEN Ews.checkout END) AS wsCheckOut, MAX(CASE WHEN IsFirstEntry = 1 THEN A9.CheckTime END) AS CheckInTime, MAX(CASE WHEN IsLastEntry = 1 THEN A9.CheckTime END) AS CheckOutTime FROM Attendance9 A9 INNER JOIN (SELECT empid, CheckDate, MIN(CheckTime) AS MinCheckTime, MAX(CheckTime) AS MaxCheckTime FROM Attendance9 WHERE empid = 1136 GROUP BY empid, CheckDate) AS Subquery ON A9.empid = Subquery.empid AND A9.CheckDate = Subquery.CheckDate INNER JOIN Empws Ews ON A9.empid = Ews.empid AND DATENAME(WEEKDAY, A9.CheckDate) = Ews.weekday OUTER APPLY(SELECT CASE WHEN A9.CheckTime = Subquery.MinCheckTime THEN 1 ELSE 0 END AS IsFirstEntry, CASE WHEN A9.CheckTime = Subquery.MaxCheckTime THEN 1 ELSE 0 END AS IsLastEntry) AS EntryInfo WHERE A9.empid = 1136 GROUP BY A9.empid, A9.badgenumber, A9.CheckDate, A9.wsid, Ews.empname, Ews.weekday, Ews.wstype; ";
                //   string MAINSQL = "SELECT A9.empid, A9.badgenumber, A9.CheckDate, A9.wsid, Ews.empname, Ews.weekday, Ews.wstype, MAX(CASE WHEN IsFirstEntry = 1 THEN Ews.checkin END) AS wsCheckIn, MAX(CASE WHEN IsLastEntry = 1 THEN Ews.checkout END) AS wsCheckOut, MAX(CASE WHEN IsFirstEntry = 1 THEN A9.CheckTime END) AS CheckInTime, MAX(CASE WHEN IsLastEntry = 1 THEN A9.CheckTime END) AS CheckOutTime FROM Attendance9 A9 INNER JOIN (SELECT empid, CheckDate, MIN(CheckTime) AS MinCheckTime, MAX(CheckTime) AS MaxCheckTime FROM Attendance9  GROUP BY empid, CheckDate) AS Subquery ON A9.empid = Subquery.empid AND A9.CheckDate = Subquery.CheckDate INNER JOIN Empws Ews ON A9.empid = Ews.empid AND DATENAME(WEEKDAY, A9.CheckDate) = Ews.weekday OUTER APPLY(SELECT CASE WHEN A9.CheckTime = Subquery.MinCheckTime THEN 1 ELSE 0 END AS IsFirstEntry, CASE WHEN A9.CheckTime = Subquery.MaxCheckTime THEN 1 ELSE 0 END AS IsLastEntry) AS EntryInfo GROUP BY A9.empid, A9.badgenumber, A9.CheckDate, A9.wsid, Ews.empname, Ews.weekday, Ews.wstype;";
                string MAINSQL = "SELECT A9.empid, A9.badgenumber, A9.CheckDate, A9.wsid, Ews.empname, Ews.weekday, Ews.wstype, MAX(CASE WHEN IsFirstEntry = 1 THEN Ews.checkin END) AS wsCheckIn, MAX(CASE WHEN IsLastEntry = 1 THEN Ews.checkout END) AS wsCheckOut, MAX(CASE WHEN IsFirstEntry = 1 THEN A9.CheckTime END) AS CheckInTime, MAX(CASE WHEN IsLastEntry = 1 THEN A9.CheckTime END) AS CheckOutTime, Ews.projectname, Ews.projectlocation, Ews.devicename FROM Attendance9 A9 INNER JOIN (SELECT empid, CheckDate, MIN(CheckTime) AS MinCheckTime, MAX(CheckTime) AS MaxCheckTime FROM Attendance9  GROUP BY empid, CheckDate) AS Subquery ON A9.empid = Subquery.empid AND A9.CheckDate = Subquery.CheckDate INNER JOIN Empws Ews ON A9.empid = Ews.empid AND DATENAME(WEEKDAY, A9.CheckDate) = Ews.weekday OUTER APPLY(SELECT CASE WHEN A9.CheckTime = Subquery.MinCheckTime THEN 1 ELSE 0 END AS IsFirstEntry, CASE WHEN A9.CheckTime = Subquery.MaxCheckTime THEN 1 ELSE 0 END AS IsLastEntry) AS EntryInfo where Ews.wstype = 'D' and (Ews.dailydate = '"+ todaydate1 + "' and A9.CheckDate = '"+ todaydate1 + "') GROUP BY A9.empid, A9.badgenumber, A9.CheckDate, A9.wsid, Ews.empname, Ews.weekday, Ews.wstype, Ews.projectname, Ews.projectlocation, Ews.devicename;";

                SqlDataAdapter TitlesAdpt11 = new SqlDataAdapter(MAINSQL, ConnStr1);
                DataSet Titles11 = new DataSet();
                // No need to open or close the connection
                //   since the SqlDataAdapter will do this automatically.
                TitlesAdpt11.Fill(Titles11);

                conn61.Open();

                //  GridView1.DataSource = Titles1.Tables[0];
                //  GridView1.DataBind();

                if (Titles11.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow Title12 in Titles11.Tables[0].Rows)
                    {

                        //   int delaydays_ = Convert.ToInt32(Title12[1]);
                        //   string projectid_ = Title12[0].ToString();
                        //    string projectname_ = Title12[1].ToString();
                        string wstype = Title12["wstype"].ToString();
                        //if (wstype == "D")
                        //{

                        int empid = Convert.ToInt32(Title12["empid"]);
                        TimeSpan wsCheckIn = TimeSpan.Parse(Title12["wsCheckIn"].ToString());
                        TimeSpan wsCheckOut = TimeSpan.Parse(Title12["wsCheckOut"].ToString());
                        TimeSpan checkInTime = TimeSpan.Parse(Title12["CheckInTime"].ToString());
                        TimeSpan checkOutTime = TimeSpan.Parse(Title12["CheckOutTime"].ToString());

                        TimeSpan delayTime = DCalculateDelayTime(checkInTime, wsCheckIn);
                        TimeSpan earlyLeaveTime = DCalculateEarlyLeaveTime(checkOutTime, wsCheckOut);

                        string badgeno = Title12["badgenumber"].ToString();
                        //   string checkDate = reader.GetString(reader.GetOrdinal("CheckDate"));
                        DateTime checkDate = Convert.ToDateTime(Title12["CheckDate"]);
                        int wsid = Convert.ToInt32(Title12["wsid"]);
                        string empname = Title12["empname"].ToString();
                        string weekday = Title12["weekday"].ToString();
                        string projectname_ = Title12["projectname"].ToString();
                        string projectlocation_ = Title12["projectlocation"].ToString();
                        string devicename_ = Title12["devicename"].ToString();

                        bool isOnePunch = DIsOnePunch(checkInTime, checkOutTime);
                        string attendanceStatus = DCalculateAttendanceStatus(delayTime, earlyLeaveTime, isOnePunch);

                        DInsertIntoZHR_TB_ATT_B(empid, empname, badgeno, checkDate, wsCheckIn, wsCheckOut, checkInTime, checkOutTime, delayTime, earlyLeaveTime, wsid, wstype, attendanceStatus, projectname_, projectlocation_, devicename_);



                    }
                }
            }
        }


        static TimeSpan DGetTimeSpan(SqlDataReader reader, string columnName)
        {
            int columnIndex = reader.GetOrdinal(columnName);
            return reader.IsDBNull(columnIndex) ? TimeSpan.Zero : (reader.GetFieldType(columnIndex) == typeof(TimeSpan) ? reader.GetTimeSpan(columnIndex) : TimeSpan.Zero);
        }

        static void DInsertIntoZHR_TB_ATT_B(int empid, string empname, string badgeno, DateTime checkDate, TimeSpan wsCheckIn, TimeSpan wsCheckOut, TimeSpan checkInTime, TimeSpan checkOutTime, TimeSpan delayTime, TimeSpan earlyLeaveTime, int wsid, string wstype, string attendanceStatus, string projectname, string projectlocation, string devicename)
        {
            string ConnStr = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";

            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();

                string attcmd = "INSERT into ZHR_TB_ATT_B (MANDT, tdate, pernr, record, ename, BADGE_NO, checkin, CHECKOUT, NEXTDAYIN, NEXTDAYOUT, DELAY_DUR, EARLY_L_DUR, WSNAME_SAP, WSNAME_PMO, wsid, type, PMO_REMARK, PROJECTNAME, DEVICENAME, PROJECTLOCATION) Values (@MANDT, @tdate, @pernr, @record, @ename, @BADGE_NO, @checkin, @CHECKOUT, @NEXTDAYIN, @NEXTDAYOUT, @DELAY_DUR, @EARLY_L_DUR, @WSNAME_SAP, @WSNAME_PMO, @wsid, @type, @PMO_REMARK, @PROJECTNAME, @DEVICENAME, @PROJECTLOCATION)";

                using (SqlCommand attcmd1 = new SqlCommand(attcmd, conn))
                {
                    attcmd1.Parameters.AddWithValue("@MANDT", "300");
                    attcmd1.Parameters.AddWithValue("@tdate", checkDate.ToString("yyyy-MM-dd"));
                    attcmd1.Parameters.AddWithValue("@pernr", empid);
                    attcmd1.Parameters.AddWithValue("@record", "A");
                    attcmd1.Parameters.AddWithValue("@ename", empname);
                    attcmd1.Parameters.AddWithValue("@BADGE_NO", badgeno);                  

                    delayTime = DCalculateDelayTime(checkInTime, wsCheckIn);
                    earlyLeaveTime = DCalculateEarlyLeaveTime(checkOutTime, wsCheckOut);

                    if ((checkOutTime - checkInTime).TotalMinutes < 30)
                    {
                        // Check if it is near wscheckin or wscheckout
                        if (DIsNearWsCheckin(checkInTime, wsCheckIn))
                        {
                            //attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString(@"hh\:mm\:ss"));
                            //attcmd1.Parameters.AddWithValue("@CHECKOUT", "00:00:00");

                            if (delayTime.TotalMinutes > 15)
                            {
                                attcmd1.Parameters.AddWithValue("@PMO_REMARK", "Delay + One Punch");
                                attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString(@"hh\:mm\:ss"));
                                attcmd1.Parameters.AddWithValue("@CHECKOUT", "00:00:00");
                            }
                            else if (delayTime.TotalMinutes < 15)
                            {
                                attcmd1.Parameters.AddWithValue("@PMO_REMARK", "One Punch");
                                attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString(@"hh\:mm\:ss"));
                                attcmd1.Parameters.AddWithValue("@CHECKOUT", "00:00:00");
                            }
                        }
                        else if (DIsNearWsCheckout(checkOutTime, wsCheckOut))
                        {
                            attcmd1.Parameters.AddWithValue("@checkin", "00:00:00");
                            attcmd1.Parameters.AddWithValue("@CHECKOUT", checkOutTime.ToString(@"hh\:mm\:ss"));

                            if (earlyLeaveTime.TotalMinutes < 180)
                            {
                                attcmd1.Parameters.AddWithValue("@PMO_REMARK", "Early Leave + One Punch");                               
                            }
                            else if (earlyLeaveTime.TotalMinutes > 180)
                            {
                                attcmd1.Parameters.AddWithValue("@PMO_REMARK", "One Punch");                               
                            }
                        }
                        else
                        {
                            attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString(@"hh\:mm\:ss"));
                            attcmd1.Parameters.AddWithValue("@CHECKOUT", checkOutTime.ToString(@"hh\:mm\:ss"));
                            attcmd1.Parameters.AddWithValue("@PMO_REMARK", attendanceStatus);
                        }
                    }
                    else
                    {
                        attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString(@"hh\:mm\:ss"));
                        attcmd1.Parameters.AddWithValue("@CHECKOUT", checkOutTime.ToString(@"hh\:mm\:ss"));
                        attcmd1.Parameters.AddWithValue("@PMO_REMARK", attendanceStatus);
                    }

                    //   attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString(@"hh\:mm\:ss")); // Format TimeSpan as hh:mm:ss
                    //   attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString("HH:mm:ss"));
                    //   attcmd1.Parameters.AddWithValue("@CHECKOUT", checkOutTime.ToString(@"hh\:mm\:ss"));
                    attcmd1.Parameters.AddWithValue("@NEXTDAYIN", "00:00:00");
                    attcmd1.Parameters.AddWithValue("@NEXTDAYOUT", "00:00:00");
                    attcmd1.Parameters.AddWithValue("@DELAY_DUR", delayTime.ToString(@"hh\:mm\:ss"));
                    if(checkInTime == checkOutTime)
                    {
                        attcmd1.Parameters.AddWithValue("@EARLY_L_DUR", "00:00:00");
                    }
                    else
                    {
                        attcmd1.Parameters.AddWithValue("@EARLY_L_DUR", earlyLeaveTime.ToString(@"hh\:mm\:ss"));
                    }
                //    attcmd1.Parameters.AddWithValue("@EARLY_L_DUR", earlyLeaveTime.ToString(@"hh\:mm\:ss"));
                    attcmd1.Parameters.AddWithValue("@WSNAME_SAP", wsCheckIn.ToString(@"hh\:mm\:ss"));
                    attcmd1.Parameters.AddWithValue("@WSNAME_PMO", wsCheckOut.ToString(@"hh\:mm\:ss"));
                    attcmd1.Parameters.AddWithValue("@wsid", wsid);
                    attcmd1.Parameters.AddWithValue("@type", wstype);
                    //  attcmd1.Parameters.AddWithValue("@PMO_REMARK", attendanceStatus);
                    attcmd1.Parameters.AddWithValue("@PROJECTNAME", projectname);
                    attcmd1.Parameters.AddWithValue("@PROJECTLOCATION", projectlocation);
                    attcmd1.Parameters.AddWithValue("@DEVICENAME", devicename);

                    attcmd1.ExecuteNonQuery();
                }
            }
        }

        static TimeSpan DCalculateDelayTime(TimeSpan actualCheckIn, TimeSpan scheduleCheckIn)
        {
            return actualCheckIn > scheduleCheckIn ? actualCheckIn - scheduleCheckIn : TimeSpan.Zero;
        }

        static TimeSpan DCalculateEarlyLeaveTime(TimeSpan actualCheckOut, TimeSpan scheduleCheckOut)
        {
            return actualCheckOut < scheduleCheckOut ? scheduleCheckOut - actualCheckOut : TimeSpan.Zero;
        }

        static bool DIsOnePunch(TimeSpan actualCheckIn, TimeSpan actualCheckOut)
        {
            return actualCheckIn != TimeSpan.Zero && actualCheckOut == TimeSpan.Zero;
        }

        static string DCalculateAttendanceStatus(TimeSpan delay, TimeSpan earlyLeave, bool isOnePunch)
        {
            //if (delay > TimeSpan.Zero && earlyLeave > TimeSpan.Zero)
            //{
            //    return "Delay + Early Leave";
            //}
            //else if (delay > TimeSpan.Zero)
            //{
            //    return "Delay";
            //}
            //else if (earlyLeave > TimeSpan.Zero)
            //{
            //    return "Early Leave";
            //}
            //else if (isOnePunch)
            //{
            //    return "One Punch";
            //}
            //else
            //{
            //    return "Normal";
            //}

            if (delay.TotalMinutes > 15 && earlyLeave.TotalMinutes < 180)
            {
                return "Delay + Early Leave";
            }
            else if (delay.TotalMinutes > 15)
            {
                return "Delay";
            }
            else if (earlyLeave.TotalMinutes < 180)
            {
                return "Early Leave";
            }
            else if (isOnePunch)
            {
                return "One Punch";
            }
            else
            {
                return "Initial Record";
            }
        }

        // Method to check if the time is near wscheckin or wscheckout
        static bool DIsNearWsCheckin(TimeSpan time, TimeSpan wsCheckin)
        {
            // Set the threshold, for example, 5 minutes
            TimeSpan threshold = TimeSpan.FromMinutes(30);
            return Math.Abs(time.TotalMinutes - wsCheckin.TotalMinutes) < threshold.TotalMinutes;
        }

        static bool DIsNearWsCheckout(TimeSpan time, TimeSpan wsCheckout)
        {
            // Set the threshold, for example, 5 minutes
            TimeSpan threshold = TimeSpan.FromMinutes(30);
            return Math.Abs(time.TotalMinutes - wsCheckout.TotalMinutes) < threshold.TotalMinutes;
        }

        static void AttYesterdayUpdate_A()
        {
            string ConnStr = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";

            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                DateTime todaydate = DateTime.Now;
                string todaydate1 = todaydate.AddDays(-2).ToString("yyyy-MM-dd");

                string stringattcmdA = "UPDATE ZHR_TB_ATT_A SET ZHR_TB_ATT_A.RECORD = 'Y',	ZHR_TB_ATT_A.CHECKIN = ZHR_TB_ATT_B.CHECKIN, ZHR_TB_ATT_A.CHECKOUT = ZHR_TB_ATT_B.CHECKOUT, ZHR_TB_ATT_A.NEXTDAYIN = ZHR_TB_ATT_B.NEXTDAYIN, ZHR_TB_ATT_A.NEXTDAYOUT = ZHR_TB_ATT_B.NEXTDAYOUT,	ZHR_TB_ATT_A.DELAY_DUR = ZHR_TB_ATT_B.DELAY_DUR,	ZHR_TB_ATT_A.EARLY_L_DUR = ZHR_TB_ATT_B.EARLY_L_DUR,    ZHR_TB_ATT_A.WSNAME_SAP = ZHR_TB_ATT_B.WSNAME_SAP,	ZHR_TB_ATT_A.WSNAME_PMO = ZHR_TB_ATT_B.WSNAME_PMO,	ZHR_TB_ATT_A.WSID = ZHR_TB_ATT_B.WSID,	ZHR_TB_ATT_A.TYPE = ZHR_TB_ATT_B.TYPE,	ZHR_TB_ATT_A.PROJECTNAME = ZHR_TB_ATT_B.PROJECTNAME,	ZHR_TB_ATT_A.DEVICENAME = ZHR_TB_ATT_B.DEVICENAME,	ZHR_TB_ATT_A.PROJECTLOCATION = ZHR_TB_ATT_B.PROJECTLOCATION,	ZHR_TB_ATT_A.PMO_REMARK = ZHR_TB_ATT_B.PMO_REMARK FROM ZHR_TB_ATT_A INNER JOIN ZHR_TB_ATT_B ON ZHR_TB_ATT_A.PERNR = ZHR_TB_ATT_B.PERNR and ZHR_TB_ATT_A.TDATE = ZHR_TB_ATT_B.TDATE and ZHR_TB_ATT_B.RECORD = 'A' and ZHR_TB_ATT_A.RECORD = 'T' and ZHR_TB_ATT_B.TDATE = '"+ todaydate1 + "'";
                SqlCommand attcmdA = new SqlCommand(stringattcmdA, conn);
                attcmdA.ExecuteNonQuery();
                
            }
        }

    }
}