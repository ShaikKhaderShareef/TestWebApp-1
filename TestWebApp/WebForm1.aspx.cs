using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestWebApp
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            foreach (HttpPostedFile postedFile in FileUpload1.PostedFiles)
            {
                string filename = Path.GetFileName(postedFile.FileName);
                string contentType = postedFile.ContentType;
                int fileSize = postedFile.ContentLength;
                int jobId = 32;
                using (Stream fs = postedFile.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        string constr = ConfigurationManager.ConnectionStrings["PeekterConnectionString"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            string query = "INSERT INTO JobImages (JobId, ImageName, ImageData, ImageSize, ImageType) VALUES (@JobId, @ImageName, @ImageData, @ImageSize, @ImageType)";
                            using (SqlCommand cmd = new SqlCommand(query))
                            {
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@JobId", jobId);
                                cmd.Parameters.AddWithValue("@ImageName", filename);
                                cmd.Parameters.AddWithValue("@ImageData", bytes);
                                cmd.Parameters.AddWithValue("@ImageSize", fileSize);
                                cmd.Parameters.AddWithValue("@ImageType", contentType);
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                }
            }
          //  Response.Redirect(Request.Url.AbsoluteUri);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //string targetPath = Path.Combine(@"\\46.105.37.107\edc database\SUBMITTAL FILES\", "PROJ-118", "CVI", "Submitted"); //with complete path
            //                                                                                                                   // string targetPath1 = Path.Combine(@"\\46.105.37.107\edc database\SUBMITTAL FILES\", txtproject.Text, "CVI", txtsubmittal.Text, cmbworkscope.SelectedItem.Text, txtrevisedprojectid.Text, "Submitted", "P"); //with complete path                

            //System.IO.Directory.CreateDirectory(targetPath);

            //if (!System.IO.Directory.Exists(targetPath))
            //{
            //    System.IO.Directory.CreateDirectory(targetPath);
            //   // ExportReportToPDF(targetPath1, fname);
            //}
            //else
            //{
            //  //  ExportReportToPDF(targetPath1, fname);
            //}

            //  SubmittalEmail_Alerts();
            Response.Redirect("~/WebForm2.aspx");
        }

        public void SubmittalEmail_Alerts()
        {
            //  String ConnStr1 = "Data Source=192.168.1.85;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sqladmin;Password=Sqlegypt@123456;";
            String ConnStr1 = "Data Source=192.168.1.85;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sqladmin;Password=Sqlpms@123456789;";
            using (SqlConnection conn61 = new SqlConnection(ConnStr1))
            {
                String MAINSQL = "select projectid, delaydays, projectname from newproject where submittalalerts =1 and status <> 'COMPLETED' and projectid = 'PROJ-1344'";

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
                        String ConnStr = "Data Source=192.168.1.85;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sqladmin;Password=Sqlpms@123456789;";
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
                            conn.ConnectionString = "Data Source=192.168.1.85;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sqladmin;Password=Sqlpms@123456789;";

                            conn.Open();

                            ArrayList mlist_emails = new ArrayList();
                            ArrayList mClientlist_emails = new ArrayList();
                            ArrayList mConsultantlist_emails = new ArrayList();

                            //  string[] list_emails;
                            int i = 0;
                            string mEdscoemail;
                            // SqlCommand mEdsco_Email = new SqlCommand("SELECT  tblprojectkey.empemail  FROM tblprojectkey INNER JOIN newproject ON tblprojectkey.projectid = newproject.projectid WHERE (tblprojectkey.empemail <> '') AND (tblprojectkey.empemail IS NOT NULL) AND newproject.status = 'ONGOING' and  tblprojectkey .projectid = '" + projectid_ + "' and newproject.submittalalerts = 1 and tblprojectkey . type = 'EDSCO' and tblprojectkey .submittalfiles = 1 ", conn);
                            SqlCommand mEdsco_Email = new SqlCommand("Select emailid from tblpendingalerts where projectid = '" + projectid_ + "' AND type = 'TO' ", conn);
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
                            SqlCommand mClient_Email = new SqlCommand("Select emailid from tblpendingalerts where projectid = '" + projectid_ + "' AND type = 'CC' ", conn);
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
                            mmessage.From = new MailAddress("no-reply@eastdeltasa.com");
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
                            rsmtp.Host = "mail.eastdeltasa.com";
                            rsmtp.Port = 587;
                            System.Net.NetworkCredential srcredential = new System.Net.NetworkCredential("khadershareef@eastdeltasa.com", "admin@123");
                            rsmtp.UseDefaultCredentials = false;
                            rsmtp.Credentials = srcredential;
                            rsmtp.Send(mmessage);
                            mmessage = null;
                            // lbltechHR1.Text = "VOX REDSEA SUBMITTAL MAIL SENT SUCCESSFULLY...!!!";  

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
                            conn.ConnectionString = "Data Source=192.168.1.85;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sqladmin;Password=Sqlpms@123456789;";

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
                            rmessage.From = new MailAddress("no-reply@eastdeltasa.com");
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
                            rsmtp.Host = "mail.eastdeltasa.com";
                            rsmtp.Port = 587;
                            System.Net.NetworkCredential rcredential = new System.Net.NetworkCredential("khadershareef@eastdeltasa.com", "admin@123");
                            rsmtp.UseDefaultCredentials = false;
                            rsmtp.Credentials = rcredential;
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
                            conn.ConnectionString = "Data Source=192.168.1.85;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sqladmin;Password=Sqlpms@123456789;";

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
                            srmessage.From = new MailAddress("no-reply@eastdeltasa.com");
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
                            rsmtp.Host = "mail.eastdeltasa.com";
                            rsmtp.Port = 587;
                            System.Net.NetworkCredential srcredential = new System.Net.NetworkCredential("khadershareef@eastdeltasa.com", "admin@123");
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

        protected void ASPxButton1_Click(object sender, EventArgs e)
        {
            string Plants = "Black Ash.";
            string ip = @"\\192.168.1.25\ERP Data\Cloud\";
            //  ASPxTextBox1.Text = ip + Plants;
            string targetPath = Path.Combine(@"\\pms-sql.eastasia.cloudapp.azure.com\edsco database1\SUBMITTAL FILES\PROJ-1247\", "Drawing"); //with complete path
          //  string targetPath = Path.Combine(@"\\pms-sql.eastasia.cloudapp.azure.com\testshare\PROJ-1247\", "Drawing"); //with complete path
            ASPxTextBox1.Text = targetPath;
            if (!System.IO.Directory.Exists(targetPath))
            {
                filesError.Visible = true;
                ASPxFileManager1.Visible = false;
            }
            else
            {
                ASPxFileManager1.Visible = true;
                ASPxFileManager1.Settings.RootFolder = targetPath;
            }
        }
    }
}