﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class Missingemps_YesterdayAtt1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Delete_Att_B();        
            DayAttYesterday();
            NightAttYesterday();
            AbsentAttYesterday();
            AttYesterdayUpdate_A();
            Delete_Att_A();
            AttYesterday_Insert_Att_A();
            AttYesterdayUpdate_Dayoff();
            AttlogYesterday1();

            //   DateTime todaydate = DateTime.Now;
            //    string todaydate1 = todaydate.AddDays(-1).ToString("yyyy-MM-dd");
            string todaydate1 = "2025-05-22";
            DateTime todaydate1_ = Convert.ToDateTime(todaydate1);
            DayOfWeek dayofweek = todaydate1_.DayOfWeek;

            Label1.Text = "Missed Employees Yesterday Attendance Data Update already having entries in ZHR_TB_ATT_B table with record A successfully dated '" + todaydate1 + "' and weekday '"+ dayofweek;
        }

        // DELETE FROM ATT_B ENTRIES STARTS
        static void Delete_Att_B()
        {
            string ConnStr = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";

            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                DateTime todaydate = DateTime.Now;
                //   string todaydate1 = todaydate.AddDays(-1).ToString("yyyy-MM-dd");
                string todaydate1 = "2025-05-22";
                string stringattcmdA1 = "Delete from ZHR_TB_ATT_B where tdate = '" + todaydate1 + "' and  RECORD = 'A'";
                SqlCommand attcmdA1 = new SqlCommand(stringattcmdA1, conn);
                attcmdA1.ExecuteNonQuery();

            }
        }
        // DELETE FROM ATT_B ENTRIES STARTS

        static void DayAttYesterday()
        {
            //  String ConnStr1 = "Data Source=172.177.184.39;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sqladmin;Password=Sqlegypt@123456;";
            string ConnStr1 = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";
            using (SqlConnection conn61 = new SqlConnection(ConnStr1))
            {
             //   DateTime todaydate = DateTime.Now;
                //    string todaydate1 = todaydate.AddDays(-1).ToString("yyyy-MM-dd");
                string todaydate1 = "2025-05-22";
                DateTime todaydate1_ = Convert.ToDateTime(todaydate1);
                DayOfWeek dayofweek = todaydate1_.DayOfWeek;

             //   string MAINSQL = "SELECT A9.empid, A9.badgenumber, A9.CheckDate, A9.wsid, Ews.empname, Ews.weekday, Ews.wstype, MAX(CASE WHEN IsFirstEntry = 1 THEN Ews.checkin END) AS wsCheckIn, MAX(CASE WHEN IsLastEntry = 1 THEN Ews.checkout END) AS wsCheckOut, MAX(CASE WHEN IsFirstEntry = 1 THEN A9.CheckTime END) AS CheckInTime, MAX(CASE WHEN IsLastEntry = 1 THEN A9.CheckTime END) AS CheckOutTime, Ews.projectname, Ews.projectlocation, Ews.devicename FROM Attendance9 A9 INNER JOIN (SELECT empid, CheckDate, MIN(CheckTime) AS MinCheckTime, MAX(CheckTime) AS MaxCheckTime FROM Attendance9  GROUP BY empid, CheckDate) AS Subquery ON A9.empid = Subquery.empid AND A9.CheckDate = Subquery.CheckDate INNER JOIN Empws Ews ON A9.empid = Ews.empid AND DATENAME(WEEKDAY, A9.CheckDate) = Ews.weekday OUTER APPLY(SELECT CASE WHEN A9.CheckTime = Subquery.MinCheckTime THEN 1 ELSE 0 END AS IsFirstEntry, CASE WHEN A9.CheckTime = Subquery.MaxCheckTime THEN 1 ELSE 0 END AS IsLastEntry) AS EntryInfo where Ews.wstype = 'D' and (Ews.dailydate = '" + todaydate1 + "' and A9.CheckDate = '" + todaydate1 + "') GROUP BY A9.empid, A9.badgenumber, A9.CheckDate, A9.wsid, Ews.empname, Ews.weekday, Ews.wstype, Ews.projectname, Ews.projectlocation, Ews.devicename;";
             //    string MAINSQL = "SELECT  A9.empid, A9.badgenumber, A9.CheckDate, A9.wsid, Ews.empname, Ews.weekday, Ews.wstype, MAX(CASE WHEN IsFirstEntry = 1 THEN Ews.checkin END) AS wsCheckIn, MAX(CASE WHEN IsLastEntry = 1 THEN Ews.checkout END) AS wsCheckOut, MAX(CASE WHEN IsFirstEntry = 1 THEN A9.CheckTime END) AS CheckInTime, MAX(CASE WHEN IsLastEntry = 1 THEN A9.CheckTime END) AS CheckOutTime, Ews.projectname, Ews.projectlocation, Ews.devicename FROM Attendance999 A9 INNER JOIN (SELECT empid, CheckDate, MIN(CheckTime) AS MinCheckTime, MAX(CheckTime) AS MaxCheckTime FROM Attendance999  GROUP BY empid, CheckDate) AS Subquery ON A9.empid = Subquery.empid AND A9.CheckDate = Subquery.CheckDate INNER JOIN Empws Ews ON A9.empid = Ews.empid AND DATENAME(WEEKDAY, A9.CheckDate) = Ews.weekday OUTER APPLY(SELECT CASE WHEN A9.CheckTime = Subquery.MinCheckTime THEN 1 ELSE 0 END AS IsFirstEntry, CASE WHEN A9.CheckTime = Subquery.MaxCheckTime THEN 1 ELSE 0 END AS IsLastEntry) AS EntryInfo where Ews.wstype = 'D' and (Ews.dailydate = '" + todaydate1 + "' and A9.CheckDate = '" + todaydate1 + "') GROUP BY A9.empid, A9.badgenumber, A9.CheckDate, A9.wsid, Ews.empname, Ews.weekday, Ews.wstype, Ews.projectname, Ews.projectlocation, Ews.devicename;";
                string MAINSQL = "SELECT Attendance10.empid, Attendance10.badgenumber, Attendance10.CheckDate, Attendance10.CheckIn as CheckInTime, Attendance10.CheckOut as CheckOutTime, Empws.weekday, Empws.checkin AS wsCheckIn, Empws.checkout AS wsCheckOut, Empws.wstype, Empws.projectname, Empws.empname, Empws.projectlocation, Empws.devicename, Empws.wsid FROM Attendance10 INNER JOIN  Empws ON Attendance10.empid = Empws.empid WHERE(Attendance10.CheckDate = '" + todaydate1 + "' and Empws.dailydate = '"+ todaydate1 + "') and Empws.weekday = '"+ dayofweek + "'";
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

                        if (checkOutTime != null || wsCheckOut != null)
                        {
                            DInsertIntoZHR_TB_ATT_B(empid, empname, badgeno, checkDate, wsCheckIn, wsCheckOut, checkInTime, checkOutTime, delayTime, earlyLeaveTime, wsid, wstype, attendanceStatus, projectname_, projectlocation_, devicename_);
                        }

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
                    if (checkInTime == checkOutTime)
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

            if (delay.TotalMinutes > 15 && earlyLeave.TotalMinutes > 0)
            {
                return "Delay + Early Leave";
            }
            else if (delay.TotalMinutes > 15)
            {
                return "Delay";
            }
            else if (earlyLeave.TotalMinutes > 0)
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

        // DAY SHIFT YESTERDAY ATTENDANCE ENDS

        // NIGHT SHIFT YESTERDAY ATTENDANCE STARTS

        static void NightAttYesterday()
        {
        //    DateTime todaydate = DateTime.Now;
            //   string todaydate1 = todaydate.AddDays(-1).ToString("yyyy-MM-dd");
            string todaydate1 = "2025-05-22";
               //  String ConnStr1 = "Data Source=172.177.184.39;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sqladmin;Password=Sqlegypt@123456;";
               string ConnStr1 = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";
            using (SqlConnection conn61 = new SqlConnection(ConnStr1))
            {
                //    string MAINSQL = "SELECT A9.empid, A9.badgenumber, A9.CheckDate, A9.wsid, Ews.empname, Ews.weekday, Ews.wstype, MAX(CASE WHEN IsFirstEntry = 1 THEN Ews.checkin END) AS wsCheckIn, MAX(CASE WHEN IsLastEntry = 1 THEN Ews.checkout END) AS wsCheckOut, MAX(CASE WHEN IsFirstEntry = 1 THEN A9.CheckTime END) AS CheckInTime, MAX(CASE WHEN IsLastEntry = 1 THEN A9.CheckTime END) AS CheckOutTime FROM Attendance9 A9 INNER JOIN (SELECT empid, CheckDate, MIN(CheckTime) AS MinCheckTime, MAX(CheckTime) AS MaxCheckTime FROM Attendance9 WHERE empid = 1136 GROUP BY empid, CheckDate) AS Subquery ON A9.empid = Subquery.empid AND A9.CheckDate = Subquery.CheckDate INNER JOIN Empws Ews ON A9.empid = Ews.empid AND DATENAME(WEEKDAY, A9.CheckDate) = Ews.weekday OUTER APPLY(SELECT CASE WHEN A9.CheckTime = Subquery.MinCheckTime THEN 1 ELSE 0 END AS IsFirstEntry, CASE WHEN A9.CheckTime = Subquery.MaxCheckTime THEN 1 ELSE 0 END AS IsLastEntry) AS EntryInfo WHERE A9.empid = 1136 GROUP BY A9.empid, A9.badgenumber, A9.CheckDate, A9.wsid, Ews.empname, Ews.weekday, Ews.wstype; ";
                //    string MAINSQL = "WITH NightShiftEntries AS (SELECT A9.empid, A9.badgenumber, A9.CheckDate, A9.CheckTime AS CheckInTime, Ews.checkin AS wsCheckIn, Ews.checkout AS wsCheckOut, Ews.weekday, Ews.wstype, Ews.wsid, Ews.empname, ROW_NUMBER() OVER(PARTITION BY A9.empid, A9.badgenumber, A9.CheckDate ORDER BY A9.CheckTime) AS RowNum FROM Attendance9 A9 INNER JOIN Empws Ews ON A9.wsid = Ews.wsid AND A9.empid = Ews.empid AND DATENAME(WEEKDAY, A9.CheckDate) = Ews.weekday    WHERE Ews.wstype = 'N' AND CONVERT(time, A9.CheckTime) >= Ews.checkin) SELECT empid, badgenumber, empname, CheckDate, CASE WHEN DATEDIFF(MINUTE, wsCheckIn, CheckInTime) <= 180 THEN CheckInTime ELSE '00:00:00' END AS CheckInTime, COALESCE((SELECT MIN(CheckTime) FROM Attendance9 A2 WHERE A2.empid = A1.empid AND A2.CheckDate = DATEADD(DAY, 1, A1.CheckDate)), '00:00:00') AS CheckOutTime, weekday, wsCheckIn, wsCheckOut, wstype, wsid FROM NightShiftEntries A1 WHERE RowNum = 1; ";
                string MAINSQL = "WITH NightShiftEntries AS (SELECT A9.empid, A9.badgenumber, A9.CheckDate, A9.CheckTime AS CheckInTime, Ews.checkin AS wsCheckIn, Ews.checkout AS wsCheckOut, Ews.weekday, Ews.wstype, Ews.wsid, Ews.empname, Ews.projectname, Ews.projectlocation, Ews.devicename, ROW_NUMBER() OVER(PARTITION BY A9.empid, A9.badgenumber, A9.CheckDate ORDER BY A9.CheckTime) AS RowNum FROM Attendance9 A9 INNER JOIN Empws Ews ON A9.wsid = Ews.wsid AND A9.empid = Ews.empid AND DATENAME(WEEKDAY, A9.CheckDate) = Ews.weekday WHERE Ews.wstype = 'N' and (Ews.dailydate = '" + todaydate1 + "' and A9.CheckDate = '" + todaydate1 + "') AND CONVERT(time, A9.CheckTime) >= Ews.checkin) SELECT empid, badgenumber, empname, CheckDate, CASE WHEN DATEDIFF(MINUTE, wsCheckIn, CheckInTime) <= 180 THEN CheckInTime ELSE '00:00:00' END AS CheckInTime, COALESCE((SELECT MIN(CheckTime) FROM Attendance9 A2 WHERE A2.empid = A1.empid AND A2.CheckDate = DATEADD(DAY, 1, A1.CheckDate)), '00:00:00') AS CheckOutTime, weekday, wsCheckIn, wsCheckOut, wstype, wsid, projectname, projectlocation, devicename FROM NightShiftEntries A1 WHERE RowNum = 1;";

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

                        int empid = Convert.ToInt32(Title12["empid"]);
                        TimeSpan wsCheckIn = TimeSpan.Parse(Title12["wsCheckIn"].ToString());
                        TimeSpan wsCheckOut = TimeSpan.Parse(Title12["wsCheckOut"].ToString());
                        TimeSpan checkInTime = TimeSpan.Parse(Title12["CheckInTime"].ToString());
                        TimeSpan checkOutTime = TimeSpan.Parse(Title12["CheckOutTime"].ToString());
                        TimeSpan delayTime = NCalculateDelayTime(checkInTime, wsCheckIn);
                        TimeSpan earlyLeaveTime = NCalculateEarlyLeaveTime(checkOutTime, wsCheckOut);

                        string badgeno = Title12["badgenumber"].ToString();
                        //   string checkDate = reader.GetString(reader.GetOrdinal("CheckDate"));
                        DateTime checkDate = Convert.ToDateTime(Title12["CheckDate"]);
                        int wsid = Convert.ToInt32(Title12["wsid"]);
                        string empname = Title12["empname"].ToString();
                        string weekday = Title12["weekday"].ToString();
                        string projectname_ = Title12["projectname"].ToString();
                        string projectlocation_ = Title12["projectlocation"].ToString();
                        string devicename_ = Title12["devicename"].ToString();

                        bool isOnePunch = NIsOnePunch(checkInTime, checkOutTime);
                        string attendanceStatus = NCalculateAttendanceStatus(delayTime, earlyLeaveTime, isOnePunch);

                        //   InsertIntoZHR_TB_ATT_A(empid, empname, badgeno, checkDate, wsCheckIn, wsCheckOut, checkInTime, checkOutTime, delayTime, earlyLeaveTime, wsid, wstype, attendanceStatus);
                        NInsertIntoZHR_TB_ATT_B(empid, empname, badgeno, checkDate, wsCheckIn, wsCheckOut, checkInTime, checkOutTime, delayTime, earlyLeaveTime, wsid, wstype, attendanceStatus, projectname_, projectlocation_, devicename_);


                    }
                }
            }
        }


        static TimeSpan NGetTimeSpan(SqlDataReader reader, string columnName)
        {
            int columnIndex = reader.GetOrdinal(columnName);
            return reader.IsDBNull(columnIndex) ? TimeSpan.Zero : (reader.GetFieldType(columnIndex) == typeof(TimeSpan) ? reader.GetTimeSpan(columnIndex) : TimeSpan.Zero);
        }


        static void NInsertIntoZHR_TB_ATT_B(int empid, string empname, string badgeno, DateTime checkDate, TimeSpan wsCheckIn, TimeSpan wsCheckOut, TimeSpan checkInTime, TimeSpan checkOutTime, TimeSpan delayTime, TimeSpan earlyLeaveTime, int wsid, string wstype, string attendanceStatus, string projectname, string projectlocation, string devicename)
        {
            string ConnStr = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";

            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();

                //    string attcmd = "INSERT into ZHR_TB_ATT_A (MANDT, tdate, pernr, record, ename, BADGE_NO, checkin, CHECKOUT, NEXTDAYIN, NEXTDAYOUT, DELAY_DUR, EARLY_L_DUR, WSNAME_SAP, WSNAME_PMO, wsid, type, PMO_REMARK) Values (@MANDT, @tdate, @pernr, @record, @ename, @BADGE_NO, @checkin, @CHECKOUT, @NEXTDAYIN, @NEXTDAYOUT, @DELAY_DUR, @EARLY_L_DUR, @WSNAME_SAP, @WSNAME_PMO, @wsid, @type, @PMO_REMARK)";
                string attcmd = "INSERT into ZHR_TB_ATT_B (MANDT, tdate, pernr, record, ename, BADGE_NO, checkin, CHECKOUT, NEXTDAYIN, NEXTDAYOUT, DELAY_DUR, EARLY_L_DUR, WSNAME_SAP, WSNAME_PMO, wsid, type, PMO_REMARK, PROJECTNAME, DEVICENAME, PROJECTLOCATION) Values (@MANDT, @tdate, @pernr, @record, @ename, @BADGE_NO, @checkin, @CHECKOUT, @NEXTDAYIN, @NEXTDAYOUT, @DELAY_DUR, @EARLY_L_DUR, @WSNAME_SAP, @WSNAME_PMO, @wsid, @type, @PMO_REMARK, @PROJECTNAME, @DEVICENAME, @PROJECTLOCATION)";

                using (SqlCommand attcmd1 = new SqlCommand(attcmd, conn))
                {
                    attcmd1.Parameters.AddWithValue("@MANDT", "300");
                    attcmd1.Parameters.AddWithValue("@tdate", checkDate.ToString("yyyy-MM-dd"));
                    attcmd1.Parameters.AddWithValue("@pernr", empid);
                    attcmd1.Parameters.AddWithValue("@record", "A");
                    attcmd1.Parameters.AddWithValue("@ename", empname);
                    attcmd1.Parameters.AddWithValue("@BADGE_NO", badgeno);

                    delayTime = NCalculateDelayTime(checkInTime, wsCheckIn);
                    earlyLeaveTime = NCalculateEarlyLeaveTime(checkOutTime, wsCheckOut);

                    if ((checkInTime - checkOutTime).TotalMinutes < 30)
                    {
                        // Check if it is near wscheckin or wscheckout
                        if (NIsNearWsCheckin(checkInTime, wsCheckIn))
                        {
                            attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString(@"hh\:mm\:ss"));
                            attcmd1.Parameters.AddWithValue("@NEXTDAYOUT", "00:00:00");

                            // if (delayTime.TotalMinutes > 15 && (checkInTime.ToString(@"hh\:mm\:ss") == "00:00:00" || checkOutTime.ToString(@"hh\:mm\:ss") == "00:00:00"))
                            if (delayTime.TotalMinutes > 15)
                            {
                                attcmd1.Parameters.AddWithValue("@PMO_REMARK", "Delay + One Punch");
                            }
                            //else if (checkInTime.ToString(@"hh\:mm\:ss") == "00:00:00" || checkOutTime.ToString(@"hh\:mm\:ss") == "00:00:00")
                            //{
                            //    attcmd1.Parameters.AddWithValue("@PMO_REMARK", "One Punch");
                            //}
                        }
                        else if (NIsNearWsCheckout(checkOutTime, wsCheckOut))
                        {
                            attcmd1.Parameters.AddWithValue("@checkin", "00:00:00");
                            attcmd1.Parameters.AddWithValue("@NEXTDAYOUT", checkOutTime.ToString(@"hh\:mm\:ss"));

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
                            attcmd1.Parameters.AddWithValue("@NEXTDAYOUT", checkOutTime.ToString(@"hh\:mm\:ss"));
                            attcmd1.Parameters.AddWithValue("@PMO_REMARK", attendanceStatus);
                        }
                    }
                    else
                    {
                        attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString(@"hh\:mm\:ss"));
                        attcmd1.Parameters.AddWithValue("@NEXTDAYOUT", checkOutTime.ToString(@"hh\:mm\:ss"));
                        attcmd1.Parameters.AddWithValue("@PMO_REMARK", attendanceStatus);
                    }

                    //   attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString(@"hh\:mm\:ss")); // Format TimeSpan as hh:mm:ss
                    //   attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString("HH:mm:ss"));
                    //   attcmd1.Parameters.AddWithValue("@CHECKOUT", checkOutTime.ToString(@"hh\:mm\:ss"));
                    attcmd1.Parameters.AddWithValue("@NEXTDAYIN", "00:00:00");
                    attcmd1.Parameters.AddWithValue("@CHECKOUT", "00:00:00");
                    attcmd1.Parameters.AddWithValue("@DELAY_DUR", delayTime.ToString(@"hh\:mm\:ss"));
                    attcmd1.Parameters.AddWithValue("@EARLY_L_DUR", earlyLeaveTime.ToString(@"hh\:mm\:ss"));
                    attcmd1.Parameters.AddWithValue("@WSNAME_SAP", wsCheckIn.ToString(@"hh\:mm\:ss"));
                    attcmd1.Parameters.AddWithValue("@WSNAME_PMO", wsCheckOut.ToString(@"hh\:mm\:ss"));
                    attcmd1.Parameters.AddWithValue("@wsid", wsid);
                    attcmd1.Parameters.AddWithValue("@type", wstype);
                    //   attcmd1.Parameters.AddWithValue("@PMO_REMARK", attendanceStatus);
                    attcmd1.Parameters.AddWithValue("@PROJECTNAME", projectname);
                    attcmd1.Parameters.AddWithValue("@PROJECTLOCATION", projectlocation);
                    attcmd1.Parameters.AddWithValue("@DEVICENAME", devicename);

                    attcmd1.ExecuteNonQuery();
                }
            }
        }
        static TimeSpan NCalculateDelayTime(TimeSpan actualCheckIn, TimeSpan scheduleCheckIn)
        {
            return actualCheckIn > scheduleCheckIn ? actualCheckIn - scheduleCheckIn : TimeSpan.Zero;
        }

        static TimeSpan NCalculateEarlyLeaveTime(TimeSpan actualCheckOut, TimeSpan scheduleCheckOut)
        {
            return actualCheckOut < scheduleCheckOut ? scheduleCheckOut - actualCheckOut : TimeSpan.Zero;
            //  return actualCheckOut < scheduleCheckOut ? actualCheckOut - scheduleCheckOut : TimeSpan.Zero;
        }

        static bool NIsOnePunch(TimeSpan actualCheckIn, TimeSpan actualCheckOut)
        {
            return actualCheckIn != TimeSpan.Zero && actualCheckOut == TimeSpan.Zero;
        }

        static string NCalculateAttendanceStatus(TimeSpan delay, TimeSpan earlyLeave, bool isOnePunch)
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

            if (delay.TotalMinutes > 15 && earlyLeave.ToString(@"hh\:mm\:ss") != "00:00:00")
            {
                return "Delay + Early Leave";
            }
            //else if (delay.TotalMinutes > 15 && earlyLeave.TotalMinutes > 0)
            //{
            //    return "Delay";
            //}
            else if (delay.TotalMinutes > 15)
            {
                return "Delay";
            }
            else if (earlyLeave.ToString(@"hh\:mm\:ss") != "00:00:00")
            {
                return "Early Leave";
            }
            //else if (earlyLeave.TotalMinutes < 180)
            //{
            //    return "Early Leave";
            //}
            else if (isOnePunch)
            {
                return "One Punch";
            }
            else if (delay.TotalMinutes > 15 && isOnePunch)
            {
                return "Delay + One Punch";
            }
            else
            {
                return "Initial Record";
            }
        }

        // Method to check if the time is near wscheckin or wscheckout
        static bool NIsNearWsCheckin(TimeSpan time, TimeSpan wsCheckin)
        {
            // Set the threshold, for example, 5 minutes
            TimeSpan threshold = TimeSpan.FromMinutes(60);
            return Math.Abs(time.TotalMinutes - wsCheckin.TotalMinutes) < threshold.TotalMinutes;
        }

        static bool NIsNearWsCheckout(TimeSpan time, TimeSpan wsCheckout)
        {
            // Set the threshold, for example, 5 minutes
            TimeSpan threshold = TimeSpan.FromMinutes(60);
            return Math.Abs(time.TotalMinutes - wsCheckout.TotalMinutes) < threshold.TotalMinutes;
        }
        // NIGHT SHIFT YESTERDAY ATTENDANCE ENDS

        // ABSENT ATTENDANCE YESTERDAY STARTS
        static void AbsentAttYesterday()
        {
         //   DateTime todaydate = DateTime.Now;
            //    string todaydate1 = todaydate.AddDays(-1).ToString("yyyy-MM-dd");
            string todaydate1 = "2025-05-22";
           //  String ConnStr1 = "Data Source=172.177.184.39;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sqladmin;Password=Sqlegypt@123456;";
           string ConnStr1 = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";
            using (SqlConnection conn61 = new SqlConnection(ConnStr1))
            {
                //    string MAINSQL = "SELECT A9.empid, A9.badgenumber, A9.CheckDate, A9.wsid, Ews.empname, Ews.weekday, Ews.wstype, MAX(CASE WHEN IsFirstEntry = 1 THEN Ews.checkin END) AS wsCheckIn, MAX(CASE WHEN IsLastEntry = 1 THEN Ews.checkout END) AS wsCheckOut, MAX(CASE WHEN IsFirstEntry = 1 THEN A9.CheckTime END) AS CheckInTime, MAX(CASE WHEN IsLastEntry = 1 THEN A9.CheckTime END) AS CheckOutTime FROM Attendance9 A9 INNER JOIN (SELECT empid, CheckDate, MIN(CheckTime) AS MinCheckTime, MAX(CheckTime) AS MaxCheckTime FROM Attendance9 WHERE empid = 1136 GROUP BY empid, CheckDate) AS Subquery ON A9.empid = Subquery.empid AND A9.CheckDate = Subquery.CheckDate INNER JOIN Empws Ews ON A9.empid = Ews.empid AND DATENAME(WEEKDAY, A9.CheckDate) = Ews.weekday OUTER APPLY(SELECT CASE WHEN A9.CheckTime = Subquery.MinCheckTime THEN 1 ELSE 0 END AS IsFirstEntry, CASE WHEN A9.CheckTime = Subquery.MaxCheckTime THEN 1 ELSE 0 END AS IsLastEntry) AS EntryInfo WHERE A9.empid = 1136 GROUP BY A9.empid, A9.badgenumber, A9.CheckDate, A9.wsid, Ews.empname, Ews.weekday, Ews.wstype; ";
                //    string MAINSQL = "WITH NightShiftEntries AS (SELECT A9.empid, A9.badgenumber, A9.CheckDate, A9.CheckTime AS CheckInTime, Ews.checkin AS wsCheckIn, Ews.checkout AS wsCheckOut, Ews.weekday, Ews.wstype, Ews.wsid, Ews.empname, ROW_NUMBER() OVER(PARTITION BY A9.empid, A9.badgenumber, A9.CheckDate ORDER BY A9.CheckTime) AS RowNum FROM Attendance9 A9 INNER JOIN Empws Ews ON A9.wsid = Ews.wsid AND A9.empid = Ews.empid AND DATENAME(WEEKDAY, A9.CheckDate) = Ews.weekday    WHERE Ews.wstype = 'N' AND CONVERT(time, A9.CheckTime) >= Ews.checkin) SELECT empid, badgenumber, empname, CheckDate, CASE WHEN DATEDIFF(MINUTE, wsCheckIn, CheckInTime) <= 180 THEN CheckInTime ELSE '00:00:00' END AS CheckInTime, COALESCE((SELECT MIN(CheckTime) FROM Attendance9 A2 WHERE A2.empid = A1.empid AND A2.CheckDate = DATEADD(DAY, 1, A1.CheckDate)), '00:00:00') AS CheckOutTime, weekday, wsCheckIn, wsCheckOut, wstype, wsid FROM NightShiftEntries A1 WHERE RowNum = 1; ";
                string MAINSQL = "select weekday, checkin as wscheckin, checkout as wscheckout, wstype, projectname, empid, RIGHT('000000000'+CONVERT(VARCHAR,empid),9) as badgenumber,  empname, projectlocation, devicename, wsid, workschedule, dailydate from Empws where DATENAME(WEEKDAY, dailydate) = weekday and dailydate = '" + todaydate1 + "' and empid not in (select distinct empid from Attendance9 where CheckDate = '" + todaydate1 + "')";

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

                        int empid = Convert.ToInt32(Title12["empid"]);
                        TimeSpan wsCheckIn = TimeSpan.Parse(Title12["wsCheckIn"].ToString());
                        TimeSpan wsCheckOut = TimeSpan.Parse(Title12["wsCheckOut"].ToString());
                        TimeSpan checkInTime = TimeSpan.Parse("00:00:00");
                        TimeSpan checkOutTime = TimeSpan.Parse("00:00:00");
                        TimeSpan delayTime = TimeSpan.Parse("00:00:00");
                        TimeSpan earlyLeaveTime = TimeSpan.Parse("00:00:00");

                        string badgeno = Title12["badgenumber"].ToString();
                        //   string checkDate = reader.GetString(reader.GetOrdinal("CheckDate"));
                        DateTime checkDate = Convert.ToDateTime(Title12["dailydate"]);
                        int wsid = Convert.ToInt32(Title12["wsid"]);
                        string empname = Title12["empname"].ToString();
                        string weekday = Title12["weekday"].ToString();
                        string projectname_ = Title12["projectname"].ToString();
                        string projectlocation_ = Title12["projectlocation"].ToString();
                        string devicename_ = Title12["devicename"].ToString();

                        //    bool isOnePunch = IsOnePunch(checkInTime, checkOutTime);
                        string attendanceStatus = "Absent";

                        //   InsertIntoZHR_TB_ATT_A(empid, empname, badgeno, checkDate, wsCheckIn, wsCheckOut, checkInTime, checkOutTime, delayTime, earlyLeaveTime, wsid, wstype, attendanceStatus);
                        AInsertIntoZHR_TB_ATT_B(empid, empname, badgeno, checkDate, wsCheckIn, wsCheckOut, checkInTime, checkOutTime, delayTime, earlyLeaveTime, wsid, wstype, attendanceStatus, projectname_, projectlocation_, devicename_);


                    }
                }
            }
        }


        static TimeSpan AGetTimeSpan(SqlDataReader reader, string columnName)
        {
            int columnIndex = reader.GetOrdinal(columnName);
            return reader.IsDBNull(columnIndex) ? TimeSpan.Zero : (reader.GetFieldType(columnIndex) == typeof(TimeSpan) ? reader.GetTimeSpan(columnIndex) : TimeSpan.Zero);
        }


        static void AInsertIntoZHR_TB_ATT_B(int empid, string empname, string badgeno, DateTime checkDate, TimeSpan wsCheckIn, TimeSpan wsCheckOut, TimeSpan checkInTime, TimeSpan checkOutTime, TimeSpan delayTime, TimeSpan earlyLeaveTime, int wsid, string wstype, string attendanceStatus, string projectname, string projectlocation, string devicename)
        {
            string ConnStr = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";

            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();

                //    string attcmd = "INSERT into ZHR_TB_ATT_A (MANDT, tdate, pernr, record, ename, BADGE_NO, checkin, CHECKOUT, NEXTDAYIN, NEXTDAYOUT, DELAY_DUR, EARLY_L_DUR, WSNAME_SAP, WSNAME_PMO, wsid, type, PMO_REMARK) Values (@MANDT, @tdate, @pernr, @record, @ename, @BADGE_NO, @checkin, @CHECKOUT, @NEXTDAYIN, @NEXTDAYOUT, @DELAY_DUR, @EARLY_L_DUR, @WSNAME_SAP, @WSNAME_PMO, @wsid, @type, @PMO_REMARK)";
                string attcmd = "INSERT into ZHR_TB_ATT_B (MANDT, tdate, pernr, record, ename, BADGE_NO, checkin, CHECKOUT, NEXTDAYIN, NEXTDAYOUT, DELAY_DUR, EARLY_L_DUR, WSNAME_SAP, WSNAME_PMO, wsid, type, PMO_REMARK, PROJECTNAME, DEVICENAME, PROJECTLOCATION) Values (@MANDT, @tdate, @pernr, @record, @ename, @BADGE_NO, @checkin, @CHECKOUT, @NEXTDAYIN, @NEXTDAYOUT, @DELAY_DUR, @EARLY_L_DUR, @WSNAME_SAP, @WSNAME_PMO, @wsid, @type, @PMO_REMARK, @PROJECTNAME, @DEVICENAME, @PROJECTLOCATION)";

                using (SqlCommand attcmd1 = new SqlCommand(attcmd, conn))
                {
                    attcmd1.Parameters.AddWithValue("@MANDT", "300");
                    attcmd1.Parameters.AddWithValue("@tdate", checkDate.ToString("yyyy-MM-dd"));
                    attcmd1.Parameters.AddWithValue("@pernr", empid);
                    attcmd1.Parameters.AddWithValue("@record", "A");
                    attcmd1.Parameters.AddWithValue("@ename", empname);
                    attcmd1.Parameters.AddWithValue("@BADGE_NO", badgeno);

                    //delayTime = CalculateDelayTime(checkInTime, wsCheckIn);
                    //earlyLeaveTime = CalculateEarlyLeaveTime(checkOutTime, wsCheckOut);

                    //if ((checkInTime - checkOutTime).TotalMinutes < 30)
                    //{
                    //    // Check if it is near wscheckin or wscheckout
                    //    if (IsNearWsCheckin(checkInTime, wsCheckIn))
                    //    {
                    //        attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString(@"hh\:mm\:ss"));
                    //        attcmd1.Parameters.AddWithValue("@NEXTDAYOUT", "00:00:00");

                    //       // if (delayTime.TotalMinutes > 15 && (checkInTime.ToString(@"hh\:mm\:ss") == "00:00:00" || checkOutTime.ToString(@"hh\:mm\:ss") == "00:00:00"))
                    //       if(delayTime.TotalMinutes > 15)
                    //        {
                    //            attcmd1.Parameters.AddWithValue("@PMO_REMARK", "Delay + One Punch");
                    //        }
                    //        //else if (checkInTime.ToString(@"hh\:mm\:ss") == "00:00:00" || checkOutTime.ToString(@"hh\:mm\:ss") == "00:00:00")
                    //        //{
                    //        //    attcmd1.Parameters.AddWithValue("@PMO_REMARK", "One Punch");
                    //        //}
                    //    }
                    //    else if (IsNearWsCheckout(checkOutTime, wsCheckOut))
                    //    {
                    //        attcmd1.Parameters.AddWithValue("@checkin", "00:00:00");
                    //        attcmd1.Parameters.AddWithValue("@NEXTDAYOUT", checkOutTime.ToString(@"hh\:mm\:ss"));

                    //        if (earlyLeaveTime.TotalMinutes < 180)
                    //        {
                    //            attcmd1.Parameters.AddWithValue("@PMO_REMARK", "Early Leave + One Punch");
                    //        }
                    //        else if (earlyLeaveTime.TotalMinutes > 180)
                    //        {
                    //            attcmd1.Parameters.AddWithValue("@PMO_REMARK", "One Punch");
                    //        }
                    //    }
                    //    else
                    //    {
                    //        attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString(@"hh\:mm\:ss"));
                    //        attcmd1.Parameters.AddWithValue("@NEXTDAYOUT", checkOutTime.ToString(@"hh\:mm\:ss"));
                    //        attcmd1.Parameters.AddWithValue("@PMO_REMARK", attendanceStatus);
                    //    }
                    //}
                    //else
                    //{
                    //    attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString(@"hh\:mm\:ss"));
                    //    attcmd1.Parameters.AddWithValue("@NEXTDAYOUT", checkOutTime.ToString(@"hh\:mm\:ss"));
                    //    attcmd1.Parameters.AddWithValue("@PMO_REMARK", attendanceStatus);
                    //}

                    //   attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString(@"hh\:mm\:ss")); // Format TimeSpan as hh:mm:ss
                    //   attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString("HH:mm:ss"));
                    //   attcmd1.Parameters.AddWithValue("@CHECKOUT", checkOutTime.ToString(@"hh\:mm\:ss"));

                    attcmd1.Parameters.AddWithValue("@CHECKIN", "00:00:00");
                    attcmd1.Parameters.AddWithValue("@CHECKOUT", "00:00:00");
                    attcmd1.Parameters.AddWithValue("@NEXTDAYIN", "00:00:00");
                    attcmd1.Parameters.AddWithValue("@NEXTDAYOUT", "00:00:00");
                    attcmd1.Parameters.AddWithValue("@DELAY_DUR", "00:00:00");
                    attcmd1.Parameters.AddWithValue("@EARLY_L_DUR", "00:00:00");
                    attcmd1.Parameters.AddWithValue("@WSNAME_SAP", wsCheckIn.ToString(@"hh\:mm\:ss"));
                    attcmd1.Parameters.AddWithValue("@WSNAME_PMO", wsCheckOut.ToString(@"hh\:mm\:ss"));
                    attcmd1.Parameters.AddWithValue("@wsid", wsid);
                    attcmd1.Parameters.AddWithValue("@type", wstype);
                    attcmd1.Parameters.AddWithValue("@PMO_REMARK", attendanceStatus);
                    attcmd1.Parameters.AddWithValue("@PROJECTNAME", projectname);
                    attcmd1.Parameters.AddWithValue("@PROJECTLOCATION", projectlocation);
                    attcmd1.Parameters.AddWithValue("@DEVICENAME", devicename);
                    attcmd1.ExecuteNonQuery();
                }
            }
        }

        // ABSENT ATTENDANCE YESTERDAY ENDS

        // DAYOFF ATTENDANCE YESTERDAY STARTS
        static void AttYesterdayUpdate_Dayoff()
        {
            string ConnStr = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";

            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
             //   DateTime todaydate = DateTime.Now;
             //   string todaydate1 = todaydate.AddDays(-1).ToString("yyyy-MM-dd");
                string todaydate1 = "2025-05-22";
                string stringattcmdA = "UPDATE ZHR_TB_ATT_A SET PMO_REMARK = 'Day Off' where RECORD = 'Y' AND (WSNAME_PMO = '00:00:00' and WSNAME_SAP = '00:00:00') and TDATE = '" + todaydate1 + "'";
                SqlCommand attcmdA = new SqlCommand(stringattcmdA, conn);
                attcmdA.ExecuteNonQuery();

            }
        }
        // DAYOFF ATTENDANCE YESTERDAY ENDS

        // UPDATE YESTERDAY DATA WITH RECORD A AND SITE REMARKS STARTS
        static void AttYesterdayUpdate_A()
        {
            string ConnStr = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";

            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
            //    DateTime todaydate = DateTime.Now;
             //   string todaydate1 = todaydate.AddDays(-1).ToString("yyyy-MM-dd");
                string todaydate1 = "2025-05-22";
                string stringattcmdA = "UPDATE ZHR_TB_ATT_B SET ZHR_TB_ATT_B.SITE_REMARK = ZHR_TB_ATT_A.SITE_REMARK, ZHR_TB_ATT_B.PMO_REMARK1 = ZHR_TB_ATT_A.PMO_REMARK1 FROM ZHR_TB_ATT_A INNER JOIN ZHR_TB_ATT_B ON ZHR_TB_ATT_A.PERNR = ZHR_TB_ATT_B.PERNR and ZHR_TB_ATT_A.TDATE = ZHR_TB_ATT_B.TDATE and ZHR_TB_ATT_B.RECORD = 'A' and ZHR_TB_ATT_A.RECORD = 'Y' and ZHR_TB_ATT_B.TDATE = '"+todaydate1+"' ";
                SqlCommand attcmdA = new SqlCommand(stringattcmdA, conn);
                attcmdA.ExecuteNonQuery();

            }
        }

        // UPDATE YESTERDAY DATA WITH RECORD A AND SITE REMARKS ENDS


        // DELETE FROM ATT_A ENTRIES STARTS
        static void Delete_Att_A()
        {
            string ConnStr = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";

            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
            //    DateTime todaydate = DateTime.Now;
                //   string todaydate1 = todaydate.AddDays(-1).ToString("yyyy-MM-dd");
                string todaydate1 = "2025-05-22";
                string stringattcmdA1 = "Delete from ZHR_TB_ATT_A where tdate = '" + todaydate1 + "' and  RECORD = 'Y'";
                SqlCommand attcmdA1 = new SqlCommand(stringattcmdA1, conn);
                attcmdA1.ExecuteNonQuery();

            }
        }
        // DELETE FROM ATT_A ENTRIES ENDS

        // INSERT ATT_A TABLE FROM ATT_B WITH SITE REMARKS STARTS
        static void AttYesterday_Insert_Att_A()
        {
            string ConnStr = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";

            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
            //    DateTime todaydate = DateTime.Now;
             //   string todaydate1 = todaydate.ToString("yyyy-MM-dd");
                string todaydate1 = "2025-05-22";
                string stringattcmdA = "INSERT INTO ZHR_TB_ATT_A (MANDT, tdate, pernr, record, ename, BADGE_NO, checkin, CHECKOUT, NEXTDAYIN, NEXTDAYOUT, DELAY_DUR, EARLY_L_DUR, WSNAME_SAP, WSNAME_PMO, wsid, type, PMO_REMARK, PROJECTNAME, DEVICENAME, PROJECTLOCATION, SITE_REMARK, PMO_REMARK1) SELECT MANDT, tdate, pernr, 'Y', ename, BADGE_NO, checkin, CHECKOUT, NEXTDAYIN, NEXTDAYOUT, DELAY_DUR, EARLY_L_DUR, WSNAME_SAP, WSNAME_PMO, wsid, type, PMO_REMARK, PROJECTNAME, DEVICENAME, PROJECTLOCATION, SITE_REMARK, PMO_REMARK1 FROM ZHR_TB_ATT_B WHERE TDATE = '" + todaydate1 + "' AND RECORD = 'A'; ";
                SqlCommand attcmdA = new SqlCommand(stringattcmdA, conn);
                attcmdA.ExecuteNonQuery();

            }
        }

        // INSERT ATT_A TABLE FROM ATT_B WITH SITE REMARKS ENDS



        // Att logs start
        static void AttlogYesterday1()
        {
         //   DateTime todaydate = DateTime.Now;
         //   string todaydate1 = todaydate.ToString("yyyy-MM-dd");
            string todaydate1 = "2025-05-22";
            //  String ConnStr1 = "Data Source=172.177.184.39;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sqladmin;Password=Sqlegypt@123456;";
            string ConnStr1 = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";
            using (SqlConnection conn611 = new SqlConnection(ConnStr1))
            {
                conn611.Open();
                string reportstr15 = "INSERT into tblnotify (empid, action, category,username,reportdate,entrydate) values (@empid, @action, @category,@username,@reportdate,@entrydate)";
                SqlCommand reportstrcmd15 = new SqlCommand(reportstr15, conn611);
                reportstrcmd15.Parameters.AddWithValue("@empid", "");
                reportstrcmd15.Parameters.AddWithValue("@action", "Attendance Yesterday Data First Updated Successfully dated " + todaydate1 + " ");
                reportstrcmd15.Parameters.AddWithValue("@category", "Att Yesterday Data01");
                reportstrcmd15.Parameters.AddWithValue("@username", "SYSTEM");
                reportstrcmd15.Parameters.AddWithValue("@reportdate", todaydate1);
                reportstrcmd15.Parameters.AddWithValue("@entrydate", DateTime.Now);
                reportstrcmd15.ExecuteNonQuery();

            }
        }
        // Att logs ends

    }
}