using System;
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
    public partial class Iqamaemail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            QiwaEmail_Saudis();
            QiwaEmail_NonSaudis();
            IqamaEmail_NonSaudis();
            attendancemail();
            DateTime todaydate = DateTime.Now;
            //    string todaydate1 = todaydate.AddDays(-1).ToString("yyyy-MM-dd");
         //   string todaydate1 = "2024-12-10";

            Label1.Text = "Iqama and Qiwa contract details email sent successfully dated   ... '" + todaydate;

        }
        public void QiwaEmail_Saudis()
        {           
            String ConnStr1 = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";
            using (SqlConnection conn61 = new SqlConnection(ConnStr1))
            {              

                conn61.Open(); 
                string mbody = "";
                string mbody1 = "";                           
                String SQL = "SELECT empid, empname, empjob, empnation, empmobile,  iqama,  Qiwacontract, DATEDIFF(DAY, GETDATE(), Qiwacontract) as ExpiryDays, status from emp where status = 'Active' and Qiwacontract <> '1900-01-01' and Qiwacontract is not null and empnation = 'Saudi' and DATEDIFF(DAY, GETDATE(), Qiwacontract) <= 45 order by ExpiryDays";

                SqlDataAdapter mTitlesAdpt = new SqlDataAdapter(SQL, ConnStr1);
                DataSet mTitles = new DataSet();
                // No need to open or close the connection
                //   since the SqlDataAdapter will do this automatically.
                mTitlesAdpt.Fill(mTitles);
                //  body1 += "Dear Eng. " + empname + ",<br /><br /> Kindly find the below ABSENT records for today, Please check the list and add your comments If any, with note that sanction list automatically applied.<br /><br /> NOTE: This email is Auto generated from the system. So Please don't reply to this email. <br /><br />";
              
             //       conn.ConnectionString = "Data Source=192.168.1.85;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sqladmin;Password=Sqlpms@123456789;";

            //        conn.Open();                   

                    mbody1 += "Dear Sir, <br /><br /> Kindly find the Auto Notification of Saudi Employee Qiwa Contract Expire details. <br /><br /> NOTE: This email is Auto generated from the system. So Please don't reply to this email. <br /><br />";

                    mbody = "<table border=" + 5 + " cellpadding=" + 5 + " cellspacing=" + 5 + " font-family=Segoe UI>";
                    mbody += "<tr bgcolor='LightGreen' font-family='Century Gothic'>";
                    mbody += "<td>EMPID</td>";
                    mbody += "<td>EMPLOYEE NAME</td>";
                    mbody += "<td>PROFESSION</td>";
                    mbody += "<td>NATIONALITY</td>";
                    mbody += "<td>MOBILE</td>";
                    mbody += "<td>IQAMA NUMBER</td>";
                    //mbody += "<td>IQAMA EXPIRY</td>";                 
                    mbody += "<td>QIWA EXPIRY</td>";
                    mbody += "<td>EXPIRY DAYS</td>";
                    mbody += "<td>STATUS</td>";

                    mbody += "</tr>";

                    foreach (DataRow mTitle in mTitles.Tables[0].Rows)
                    {
                        mbody += "<tr>";
                        // body += "<td>" + Title[0] + "</td>";
                        mbody += "<td>" + mTitle[0] + "</td>";
                        //body += "<td>" + String.Format("{0:c}", Title[0]) + "</td>";
                        mbody += "<td>" + String.Format("{0:c}", mTitle[1]) + "</td>";
                        mbody += "<td>" + String.Format("{0:c}", mTitle[2]) + "</td>";
                        mbody += "<td>" + String.Format("{0:c}", mTitle[3]) + "</td>";
                        mbody += "<td>" + String.Format("{0:c}", mTitle[4]) + "</td>";
                        mbody += "<td>" + String.Format("{0:c}", mTitle[5]) + "</td>";
                        //mbody += "<td>" + String.Format("{0:c}", mTitle[6]) + "</td>";
                        //mbody += "<td>" + String.Format("{0:c}", mTitle[7]) + "</td>";
                        mbody += "<td>" + Convert.ToDateTime(mTitle[6]).ToString("dd-MMM-yyyy") + "</td>";
                    //    mbody += "<td>" + Convert.ToDateTime(mTitle[7]).ToString("dd-MMM-yyyy") + "</td>";
                        mbody += "<td>" + mTitle[7] + "</td>";
                        mbody += "<td>" + String.Format("{0:c}", mTitle[8]) + "</td>";
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
                mmessage.To.Add(new MailAddress("sayed@alramsat.com"));
                mmessage.CC.Add(new MailAddress("kshareef@alramsat.com"));

                //  message.To.Add(new MailAddress(item.Text));

                // int k = Convert.ToInt32 ( list_emails.Count.ToString ());                        



                mmessage.Subject = "Auto Notification of Saudi Employee Qiwa Contract dated  " + DateTime.Now;
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


            }
        }

        public void QiwaEmail_NonSaudis()
        {
            String ConnStr1 = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";
            using (SqlConnection conn61 = new SqlConnection(ConnStr1))
            {

                conn61.Open();
                string mbody = "";
                string mbody1 = "";
                String SQL = "SELECT empid, empname, empjob, empnation, empmobile,  iqama,  Qiwacontract, DATEDIFF(DAY, GETDATE(), Qiwacontract) as ExpiryDays, status from emp where status = 'Active' and Qiwacontract <> '1900-01-01' and Qiwacontract is not null and empnation <> 'Saudi' and DATEDIFF(DAY, GETDATE(), Qiwacontract) <= 100 order by ExpiryDays";

                SqlDataAdapter mTitlesAdpt = new SqlDataAdapter(SQL, ConnStr1);
                DataSet mTitles = new DataSet();
                // No need to open or close the connection
                //   since the SqlDataAdapter will do this automatically.
                mTitlesAdpt.Fill(mTitles);
                //  body1 += "Dear Eng. " + empname + ",<br /><br /> Kindly find the below ABSENT records for today, Please check the list and add your comments If any, with note that sanction list automatically applied.<br /><br /> NOTE: This email is Auto generated from the system. So Please don't reply to this email. <br /><br />";

                //       conn.ConnectionString = "Data Source=192.168.1.85;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sqladmin;Password=Sqlpms@123456789;";

                //        conn.Open();                   

                mbody1 += "Dear Sir, <br /><br /> Kindly find the Auto Notification of Non Saudi Employee Qiwa Contract Expire details. <br /><br /> NOTE: This email is Auto generated from the system. So Please don't reply to this email. <br /><br />";

                mbody = "<table border=" + 1 + " cellpadding=" + 1 + " cellspacing=" + 1 + " font-family=Segoe UI>";
                mbody += "<tr bgcolor='LightGreen' font-family='Century Gothic'>";
                mbody += "<td>EMPID</td>";
                mbody += "<td>EMPLOYEE NAME</td>";
                mbody += "<td>PROFESSION</td>";
                mbody += "<td>NATIONALITY</td>";
                mbody += "<td>MOBILE</td>";
                mbody += "<td>IQAMA NUMBER</td>";
                //mbody += "<td>IQAMA EXPIRY</td>";                 
                mbody += "<td>QIWA EXPIRY</td>";
                mbody += "<td>EXPIRY DAYS</td>";
                mbody += "<td>STATUS</td>";

                mbody += "</tr>";

                foreach (DataRow mTitle in mTitles.Tables[0].Rows)
                {
                    mbody += "<tr>";
                    // body += "<td>" + Title[0] + "</td>";
                    mbody += "<td>" + mTitle[0] + "</td>";
                    //body += "<td>" + String.Format("{0:c}", Title[0]) + "</td>";
                    mbody += "<td>" + String.Format("{0:c}", mTitle[1]) + "</td>";
                    mbody += "<td>" + String.Format("{0:c}", mTitle[2]) + "</td>";
                    mbody += "<td>" + String.Format("{0:c}", mTitle[3]) + "</td>";
                    mbody += "<td>" + String.Format("{0:c}", mTitle[4]) + "</td>";
                    mbody += "<td>" + String.Format("{0:c}", mTitle[5]) + "</td>";
                    //mbody += "<td>" + String.Format("{0:c}", mTitle[6]) + "</td>";
                    //mbody += "<td>" + String.Format("{0:c}", mTitle[7]) + "</td>";
                    mbody += "<td>" + Convert.ToDateTime(mTitle[6]).ToString("dd-MMM-yyyy") + "</td>";
                    //    mbody += "<td>" + Convert.ToDateTime(mTitle[7]).ToString("dd-MMM-yyyy") + "</td>";
                    mbody += "<td>" + mTitle[7] + "</td>";
                    mbody += "<td>" + String.Format("{0:c}", mTitle[8]) + "</td>";
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
                mmessage.To.Add(new MailAddress("sayed@alramsat.com"));
                mmessage.CC.Add(new MailAddress("kshareef@alramsat.com"));

                //  message.To.Add(new MailAddress(item.Text));

                // int k = Convert.ToInt32 ( list_emails.Count.ToString ());                        



                mmessage.Subject = "Auto Notification of Non-Saudi Employee Qiwa Contract dated " + DateTime.Now;
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


            }
        }

        public void IqamaEmail_NonSaudis()
        {
            String ConnStr1 = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";
            using (SqlConnection conn61 = new SqlConnection(ConnStr1))
            {

                conn61.Open();
                string mbody = "";
                string mbody1 = "";
                String SQL = "SELECT empid, empname, empjob, empnation, empmobile,  iqama, iqamaexpiry,  DATEDIFF(DAY, GETDATE(), iqamaexpiry) as ExpiryDays, status from emp where status = 'Active' and iqamaexpiry <> '1900-01-01' and iqamaexpiry is not null and empnation <> 'Saudi' and DATEDIFF(DAY, GETDATE(), iqamaexpiry) <= 30 order by ExpiryDays";

                SqlDataAdapter mTitlesAdpt = new SqlDataAdapter(SQL, ConnStr1);
                DataSet mTitles = new DataSet();
                // No need to open or close the connection
                //   since the SqlDataAdapter will do this automatically.
                mTitlesAdpt.Fill(mTitles);
                //  body1 += "Dear Eng. " + empname + ",<br /><br /> Kindly find the below ABSENT records for today, Please check the list and add your comments If any, with note that sanction list automatically applied.<br /><br /> NOTE: This email is Auto generated from the system. So Please don't reply to this email. <br /><br />";

                //       conn.ConnectionString = "Data Source=192.168.1.85;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sqladmin;Password=Sqlpms@123456789;";

                //        conn.Open();                   

                mbody1 += "Dear Sir, <br /><br /> Kindly find the Auto Notification of Non Saudi Employee Iqama Expire details. <br /><br /> NOTE: This email is Auto generated from the system. So Please don't reply to this email. <br /><br />";

                mbody = "<table border=" + 1 + " cellpadding=" + 1 + " cellspacing=" + 1 + " font-family=Segoe UI>";
                mbody += "<tr bgcolor='LightGreen' font-family='Century Gothic'>";
                mbody += "<td>EMPID</td>";
                mbody += "<td>EMPLOYEE NAME</td>";
                mbody += "<td>PROFESSION</td>";
                mbody += "<td>NATIONALITY</td>";
                mbody += "<td>MOBILE</td>";
                mbody += "<td>IQAMA NUMBER</td>";
                mbody += "<td>IQAMA EXPIRY</td>";                 
                //mbody += "<td>QIWA EXPIRY</td>";
                mbody += "<td>EXPIRY DAYS</td>";
                mbody += "<td>STATUS</td>";

                mbody += "</tr>";

                foreach (DataRow mTitle in mTitles.Tables[0].Rows)
                {
                    mbody += "<tr>";
                    // body += "<td>" + Title[0] + "</td>";
                    mbody += "<td>" + mTitle[0] + "</td>";
                    //body += "<td>" + String.Format("{0:c}", Title[0]) + "</td>";
                    mbody += "<td>" + String.Format("{0:c}", mTitle[1]) + "</td>";
                    mbody += "<td>" + String.Format("{0:c}", mTitle[2]) + "</td>";
                    mbody += "<td>" + String.Format("{0:c}", mTitle[3]) + "</td>";
                    mbody += "<td>" + String.Format("{0:c}", mTitle[4]) + "</td>";
                    mbody += "<td>" + String.Format("{0:c}", mTitle[5]) + "</td>";
                    //mbody += "<td>" + String.Format("{0:c}", mTitle[6]) + "</td>";
                    //mbody += "<td>" + String.Format("{0:c}", mTitle[7]) + "</td>";
                    mbody += "<td>" + Convert.ToDateTime(mTitle[6]).ToString("dd-MMM-yyyy") + "</td>";
                    //    mbody += "<td>" + Convert.ToDateTime(mTitle[7]).ToString("dd-MMM-yyyy") + "</td>";
                    mbody += "<td>" + mTitle[7] + "</td>";
                    mbody += "<td>" + String.Format("{0:c}", mTitle[8]) + "</td>";
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
                mmessage.To.Add(new MailAddress("sayed@alramsat.com"));
                mmessage.CC.Add(new MailAddress("kshareef@alramsat.com"));

                //  message.To.Add(new MailAddress(item.Text));

                // int k = Convert.ToInt32 ( list_emails.Count.ToString ());                        



                mmessage.Subject = "Auto Notification of Non-Saudi Employee Iqama Expiry dated " + DateTime.Now;
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


            }
        }
        public void attendancemail()
        {
            string lblvacdate = DateTime.Now.ToString("yyyy-MM-dd");

            //  string lblvacdate = "2019-08-24";

            //  string connstring1 = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            String ConnStr1 = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";
            using (SqlConnection conn61 = new SqlConnection(ConnStr1))
            {
                string checkcat = "select distinct projectname from ZHR_TB_ATT_A where tdate='" + lblvacdate + "' and record = 'T' ";

                conn61.Open();

                SqlDataAdapter TitlesAdpt22 = new SqlDataAdapter(checkcat, ConnStr1);
                DataSet Titles22 = new DataSet();
                DataTable dt = new DataTable();
                TitlesAdpt22.Fill(Titles22);

                foreach (DataRow Title in Titles22.Tables[0].Rows)
                {
                    string projectname = Title[0].ToString();

                    String Sqlattabsent = "select count(pernr) from ZHR_TB_ATT_A where tdate='" + lblvacdate + "' and record='T' and PMO_REMARK = 'Absent' and ( type = 'D' or type = 'M') and projectname = '" + projectname + "' ";
                    String Sqlattpresent = "select count(pernr) from ZHR_TB_ATT_A where tdate='" + lblvacdate + "' and record='T' and PMO_REMARK = 'Initial Record' and ( type = 'D' or type = 'M') and projectname = '" + projectname + "' ";
                    String Sqlattothers = "select count(pernr) from ZHR_TB_ATT_A where tdate='" + lblvacdate + "' and record='T' and PMO_REMARK = 'Delay' and ( type = 'D' or type = 'M') and projectname = '" + projectname + "' ";
                    String Sqlattnight = "select count(pernr) from ZHR_TB_ATT_A where tdate='" + lblvacdate + "' and record='T'  and (type = 'N') and projectname = '" + projectname + "' ";


                    SqlCommand Sqlattabsentcmd = new SqlCommand(Sqlattabsent, conn61);
                    int countabsent = Convert.ToInt32(Sqlattabsentcmd.ExecuteScalar());

                    SqlCommand Sqlattpresentcmd = new SqlCommand(Sqlattpresent, conn61);
                    int countpresent = Convert.ToInt32(Sqlattpresentcmd.ExecuteScalar());

                    SqlCommand Sqlattotherscmd = new SqlCommand(Sqlattothers, conn61);
                    int countothers = Convert.ToInt32(Sqlattotherscmd.ExecuteScalar());

                    SqlCommand Sqlattnightcmd = new SqlCommand(Sqlattnight, conn61);
                    int countnight = Convert.ToInt32(Sqlattnightcmd.ExecuteScalar());

                    String Sqlattcount = "insert into tblattcount( projectname,date,projectlocation,absentcount,presentcount,othercount,nightshift) select projectname,tdate, projectlocation, '" + countabsent + "', '" + countpresent + "', '" + countothers + "', '" + countnight + "' from ZHR_TB_ATT_A where tdate='" + lblvacdate + "' and RECORD = 'T' and projectname = '" + projectname + "' group by projectname, tdate, projectlocation  ";
                    SqlCommand Sqlattcountcmd = new SqlCommand(Sqlattcount, conn61);
                    Sqlattcountcmd.ExecuteNonQuery();

                }

                // GridView1.DataSource = dt;
                //  GridView1.DataBind();

                string checkcatt = "select count(category) from tblnotify where category = 'Project Attendance Email' and reportdate = '" + lblvacdate + "' ";

                // conn61.Open();

                SqlCommand zhrcheckcatt = new SqlCommand(checkcatt, conn61);
                int countcheckcatt = Convert.ToInt32(zhrcheckcatt.ExecuteScalar());

                if (countcheckcatt == 0)
                {

                    string body = "";
                    string body1 = "";
                    String ConnStr = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";
                    //String SQL = "SELECT slno, reportname FROM dailyattreport where reportdate='2018-09-02'";
                    //  String SQL = "SELECT [TDATE], [PERNR], [ENAME], [CHECKIN], [CHECKOUT], [NEXTDAYIN], [NEXTDAYOUT], [PROJECTNAME], [PROJECTLOCATION], [PMO_REMARK], [SITE_REMARK], [AREA_SP_REMARK] FROM [ZHR_TB_ATT_A] WHERE (([TDATE] = '" + HRAttdatetxt.Text + "') AND ([RECORD] = 'T') AND ([PMO_REMARK] = 'Absent') AND PROJECTNAME='" + projectname + "') ORDER BY PROJECTNAME,PROJECTLOCATION";
                    String SQL2 = "select date, projectname, projectlocation, absentcount, presentcount, othercount, nightshift, (absentcount+ presentcount+ othercount+nightshift) as Total from tblattcount where date = '" + lblvacdate + "' order by projectlocation";

                    SqlDataAdapter TitlesAdpt2 = new SqlDataAdapter(SQL2, ConnStr);
                    DataSet Titles2 = new DataSet();

                    // No need to open or close the connection
                    //   since the SqlDataAdapter will do this automatically.
                    TitlesAdpt2.Fill(Titles2);
                    TitlesAdpt2.Fill(dt);
                    //  body1 += "Dear Eng. " + empname + ",<br /><br /> Kindly find the below ABSENT records for today, Please check the list and add your comments If any, with note that sanction list automatically applied.<br /><br /> NOTE: This email is Auto generated from the system. So Please don't reply to this email. <br /><br />";
                    body1 += "Dear Sir, <br /><br /> This is an auto-generated email notification of Project Attendance Summary Report.  <br /><br />";

                    body = "<table border=" + 1 + " cellpadding=" + 1 + " cellspacing=" + 1 + " font-family=Segoe UI>";
                    body += "<tr bgcolor='Orange' font-family='Century Gothic'>";
                    body += "<td>ATTENDANCE DATE</td>";
                    body += "<td>PROJECT NAME</td>";
                    body += "<td>LOCATION</td>";
                    body += "<td>ABSENT COUNT</td>";
                    body += "<td>PRESENT COUNT</td>";
                    body += "<td>DELAY COUNT</td>";
                    body += "<td>NIGHT SHIFT</td>";
                    body += "<td>TOTAL COUNT</td>";
                    body += "</tr>";


                    foreach (DataRow Title in Titles2.Tables[0].Rows)
                    {
                        // string dateformat = Convert.ToDateTime(Title [2]).ToString("dd-MM-yyyy");                       

                        body += "<tr>";
                        body += "<td>" + String.Format("{0:c}", Convert.ToDateTime(Title[0]).ToString("dd-MM-yyyy")) + "</td>";
                        body += "<td>" + String.Format("{0:c}", Title[1]) + "</td>";
                        body += "<td>" + String.Format("{0:c}", Title[2]) + "</td>";
                        body += "<td>" + Title[3] + "</td>";
                        body += "<td>" + Title[4] + "</td>";
                        body += "<td>" + Title[5] + "</td>";
                        body += "<td>" + Title[6] + "</td>";
                        body += "<td>" + Title[7] + "</td>";

                        body += "</tr>";
                    }


                    int absentsum = dt.AsEnumerable().Sum(row => row.Field<int>("absentcount"));
                    int presentsum = dt.AsEnumerable().Sum(row => row.Field<int>("presentcount"));
                    int othersum = dt.AsEnumerable().Sum(row => row.Field<int>("othercount"));
                    int totalsum = dt.AsEnumerable().Sum(row => row.Field<int>("Total"));
                    int nightsum = dt.AsEnumerable().Sum(row => row.Field<int>("nightshift"));

                    body += "<tr>";
                    body += "<td>";
                    body += "<td>";
                    body += "<td>TOTAL RECORDS</td>";

                    body += "<td>" + absentsum + "</td>";
                    body += "<td>" + presentsum + "</td>";
                    body += "<td>" + othersum + "</td>";
                    body += "<td>" + nightsum + "</td>";
                    body += "<td>" + totalsum + "</td>";
                    body += "</table>";

                    //now set up the mail settings
                    MailMessage message = new MailMessage();
                    message.IsBodyHtml = true;
                    //  message.From = new MailAddress(NewTextBox222.Text);
                    message.From = new MailAddress("no-reply@alramsat.com");
                    //can add more recipient
                    // message.To.Add(new MailAddress(emailid));                  

                    message.To.Add(new MailAddress("kshareef@alramsat.com"));
                    message.To.Add(new MailAddress("mshari@alramsat.com"));
                    //add cc         

                    //    message.CC.Add(new MailAddress("kshareef"));


                    message.Subject = "Attendance Daily Summary Report dated " + DateTime.Now;
                    message.Body = body1 + body;

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
                    // lbltechHR1.Text = "VOX REDSEA SUBMITTAL MAIL SENT SUCCESSFULLY...!!!";

                    //  string constring11 = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                    using (SqlConnection conn1 = new SqlConnection(ConnStr))
                    {

                        string reportstr1 = "INSERT into tblnotify (empid, action, category,username,reportdate,entrydate) values (@empid, @action, @category,@username,@reportdate,@entrydate)";

                        conn1.Open();

                        SqlCommand reportstrcmd1 = new SqlCommand(reportstr1, conn1);

                        reportstrcmd1.Parameters.AddWithValue("@empid", "");
                        reportstrcmd1.Parameters.AddWithValue("@action", "Successfully Sent Email Project Employees Att Report dated From " + lblvacdate + " ");
                        reportstrcmd1.Parameters.AddWithValue("@category", "Project Attendance Email");
                        reportstrcmd1.Parameters.AddWithValue("@username", "SYSTEM");
                        reportstrcmd1.Parameters.AddWithValue("@reportdate", lblvacdate);
                        reportstrcmd1.Parameters.AddWithValue("@entrydate", DateTime.Now);
                        reportstrcmd1.ExecuteNonQuery();
                    }
                    //vacation email ends                                
                    // }
                }
            }
        }
    }
}