using System;
using System.Configuration;
using System.Web;

using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Text;

using System.Net.Mail;
using System.Xml;
using System.Diagnostics;
using System.Web.UI.HtmlControls;
using System.Web.UI;


namespace TX_ESC_04SessionRememberV2
{
    /// <summary>
    /// Summary description for DataProcess.
    /// </summary>
    public class DataProcess
    {
        protected static string Constring;
        public DataProcess()
        {
            //
            // TODO: Add constructor logic here
            //


        }

        public static void SendEmail(Utility.MessageType msgType)
        {
            Constring = string.Empty;


            //tx_esc_04SessionReminder.ServerConnection.ConnectionService ConObject = new tx_esc_04SessionReminder.ServerConnection.ConnectionService();
           // Constring = ConObject.ConnectionString("tx_esc_04", ConfigurationManager.AppSettings["database.activeserver"]);

            //Constring = ConObject.ConnectionString("tx_esc_04", "dev");
            //XmlDocument DocElement = new XmlDocument();
            //DocElement.Load(@"C:\tx_esc_04\tx_esc_04EventCreation.xml");
            Constring = ConfigurationManager.AppSettings["DBconnection_DEV"];
            
			switch(msgType)
			{
                case Utility.MessageType.SessionReminder:
                    using (SqlCommand SQLCommand = new SqlCommand("[/sysmail/tx_esc_04Notification/sessionreminderv2]", new SqlConnection(Constring)))
                    {

                        SQLCommand.CommandType = CommandType.StoredProcedure;

                        try
                        {

                            SQLCommand.Connection.Open();
                            SqlDataReader dr = SQLCommand.ExecuteReader(CommandBehavior.CloseConnection);
                            while (dr.Read())
                            {
                                MailMessage Mmsg = new MailMessage();

                                //MailMessage Mmsg = new MailMessage();
                                Mmsg.Subject = "Notification: Region 4 Confidential Survey";
                                Mmsg.IsBodyHtml = true;


                                //Render HtmlTable
                                HtmlTable table = new HtmlTable();
                                table.Width = "75%";
                                
                                HtmlTableRow row;
                                row = new HtmlTableRow();
                                row.Cells.Add(new HtmlTableCell());
                                row.Cells[0].ColSpan = 2;
                                row.Cells[0].Align = "center";
                                row.Cells[0].Width = "100%";

                                row.Cells[0].InnerHtml = "<img src=\"cid:customerLogo\" alt= \"Region4 Logo\" />";
                              
                                table.Rows.Add(row);

                                row = new HtmlTableRow();
                                row.Cells.Add(new HtmlTableCell());

                                StringBuilder builder =  new StringBuilder();
                                builder.AppendFormat(" Dear {0} {1}, <br><br>", dr["fname"].ToString(), dr["lname"].ToString());

                                //if (Convert.ToInt32(dr["eventtype_id"].ToString()) == 5017) //Job fair
                                //    builder.AppendFormat("<br />Thank you for your registration. This confirms your registration for the {0}. Your receipt is included in the Payments Received section below.<br/><br/>", dr["title"].ToString());
                                //else
                                //    builder.AppendFormat("<br />Thank you for your registration to attend the following Region 4 session:<br/>");
                                ////Title
                                //builder.AppendFormat("<span style=\"font-weight: bold;\">Session: </span>{0}-{1}<br />", dr["obj_id"].ToString(), dr["title"].ToString());
                                ////subtitle
                                //builder.AppendFormat("<span style=\"font-weight: bold;\">Subtitle:</span>{0}<br />", dr["subtitle"].ToString());
                                ////description
                                //builder.AppendFormat("<strong>Description:</strong>{0}<br /><br />", dr["description"].ToString());

                                row.Cells[0].InnerHtml = builder.ToString();

                                table.Rows.Add(row);


                                row = new HtmlTableRow();
                                row.Cells.Add(new HtmlTableCell());
                                row.Cells[0].ColSpan = 2;
                                row.Cells[0].Width = "100%";
                                row.Cells[0].InnerHtml = "";

 row.Cells[0].InnerHtml += "<a href=\"https://www.esc4.net\">Region 4 Education Service Center, Houston, TX</a> is grateful for the opportunity to partner with you to serve your needs.<br><br>";

 

row.Cells[0].InnerHtml +="You will be receiving an email from <a mailto:info@ccubesstrategy.com>info@ccubesstrategy.com</a> requesting you to complete a confidential, 20-minute assessment about Region 4. I would be sincerely grateful if you would fully complete the assessment all the way to the end. <b>Please complete the survey by June 12, 2023 by 5:00pm.</b><br><br>";

 

row.Cells[0].InnerHtml +="<b>All individual responses are completely confidential, and our partner will not share them with anyone.</b><br><br>";

 

row.Cells[0].InnerHtml +="Please let me know if you do not receive the survey link or have any difficulty accessing the survey. We sincerely value your time and thank you in advance for contributing your critical input.<br><br>" ;


row.Cells[0].InnerHtml += "With gratitude,<br>Rodney Watson, Ph.D.<br>Executive Director<br>";
row.Cells[0].InnerHtml += "<img src=\"cid:customerLogo2\" alt= \"Region4 Logo\" />";

                                row.Cells[0].InnerHtml +="<br>Region 4 Education Service Center<br>7145 West Tidwell Road<br>Houston, TX 77092<br>Email <a mailto:rodney.watson@esc4.net>rodney.watson@esc4.net</a><br><a href=http://www.esc4.net>http://www.esc4.net</a><br><br>";

                                row.Cells[0].InnerHtml += " NOTICE: <font size=2>This electronic transmission, including any attachments, is intended only for the use of the individual or entity to which it is addressed. This transmission may contain information that is confidential, privileged and exempt from disclosure under applicable law. If you are not the intended recipient, you are hereby notified that any disclosure, copying, distribution or use of any of the information contained in this transmission is strictly PROHIBITED. If you have received this communication in error, please notify the sender immediately by replying to this message and delete this e-mail message from your computer.<font>";
                                table.Rows.Add(row);


                                //string timeDateSummary = "";
                                //string locationSummary = "";
                                //using (SqlCommand SQLCommand_Reminder = new SqlCommand("[/sysmail/tx_esc_04Notification/sessionreminder/schedule]", new SqlConnection(Constring)))
                                //{
                                //    SQLCommand_Reminder.CommandType = CommandType.StoredProcedure;
                                //    SQLCommand_Reminder.Parameters.AddWithValue("@session_id", Convert.ToInt32(dr["obj_id"].ToString()));

                                //    try
                                //    {
                                //        SQLCommand_Reminder.Connection.Open();
                                //        SqlDataReader dr_reminder = SQLCommand_Reminder.ExecuteReader(CommandBehavior.CloseConnection);

                                //        while (dr_reminder.Read())
                                //        {
                                           
                                //            if (dr["eventtype_id"].ToString() == "2237")
                                //            {

                                //                row = new HtmlTableRow(); //Location
                                //                row.Cells.Add(new HtmlTableCell());
                                //                row.Cells.Add(new HtmlTableCell());

                                //                row.Cells[0].InnerHtml = "<b>Location:<b>";
                                //                row.Cells[1].InnerHtml = "Online";
                                //                table.Rows.Add(row);

                                //                if (Convert.ToInt32(dr_reminder["Online_Type_ID"].ToString()) == -62)
                                //                {
                                //                    row = new HtmlTableRow(); //Dates
                                //                    row.Cells.Add(new HtmlTableCell());
                                //                    row.Cells.Add(new HtmlTableCell());

                                //                    row.Cells[0].InnerHtml = "<b>Dates:<b>";
                                //                    row.Cells[1].InnerHtml = Convert.ToDateTime(dr_reminder["startDate"]).ToShortDateString() + " - " + Convert.ToDateTime(dr_reminder["endDate"]);
                                //                    table.Rows.Add(row);

                                //                }
                                                
                                //            }
                                //            else
                                //            {
                                //                row = new HtmlTableRow();
                                //                row.Cells.Add(new HtmlTableCell());
                                //                row.Cells[0].ColSpan = 2;

                                //                DateTime currentStart, currentEnd;
                                //                currentStart = currentEnd = new DateTime(1919, 10, 9);

                                //                if (Convert.ToDateTime(dr_reminder["startDate"].ToString()) == currentStart && Convert.ToDateTime(dr_reminder["endDate"].ToString()) == currentEnd)
                                //                    continue;
                                //                timeDateSummary += String.Format("{0:d} {0:t} - {1:t}<br /><br />", Convert.ToDateTime(dr_reminder["startDate"].ToString()), Convert.ToDateTime(dr_reminder["endDate"].ToString()));
                                //                currentStart = Convert.ToDateTime(dr_reminder["startDate"].ToString());
                                //                currentEnd = Convert.ToDateTime(dr_reminder["endDate"].ToString()); ;

                                //                locationSummary += string.Format("{0}:{1} " + (((dr_reminder["siteAddress"].ToString()=="7200 Northwest 100 Drive") && (dr_reminder["Room"].ToString().Contains("TETN") || dr_reminder["Room"].ToString().Contains("MCC ")) ? "<a href=\"https://www.google.com/maps/place/7200+Northwest+100+Drive+Houston+TX+77092\">{2} {3}, {4} {5}</a><br /><br />" :"{2} {3}, {4} {5} <br /><br />")), dr_reminder["siteName"].ToString(), dr_reminder["Room"].ToString(),
                                //                    dr_reminder["isServiceCenter"].ToString() == "1" ? dr_reminder["siteAddress"].ToString() : dr_reminder["roomAddress"].ToString(),
                                //                    dr_reminder["isServiceCenter"].ToString() == "1" ? dr_reminder["siteCity"].ToString() : dr_reminder["roomCity"].ToString(),
                                //                    dr_reminder["isServiceCenter"].ToString() == "1" ? dr_reminder["siteState"].ToString() : dr_reminder["roomState"].ToString(),
                                //                    dr_reminder["isServiceCenter"].ToString() == "1" ? dr_reminder["siteZip"].ToString() : dr_reminder["roomZip"].ToString());

                                //            }
                                //        }
                                //         row.Cells[0].InnerHtml = String.Format(
                                //                        "<table> <tr><td valign=\"top\"><b>Dates/Times <br/></b>{0}</td><td valign=\"top\"><div style=\"width: auto; float: left; margin-left: 50px;\"><b>Location </b><br />{1}</div></td></tr></table>"
                                //                        , timeDateSummary, locationSummary);

                                //                table.Rows.Add(row);
                                //    }
                                //    catch (Exception Ex)
                                //    {

                                //        System.Diagnostics.EventLog.WriteEntry("Session Reminder Service Error tx_esc_04:", Ex.Message.ToString(), System.Diagnostics.EventLogEntryType.Error);
                                //    }

                                //    finally
                                //    {
                                //        if (SQLCommand_Reminder.Connection.State != ConnectionState.Closed)
                                //            SQLCommand_Reminder.Connection.Close();
                                //    }




                                //    row = new HtmlTableRow();
                                //    row.Cells.Add(new HtmlTableCell());
                                //    row.Cells[0].ColSpan = 2;
                                //    row.Cells[0].InnerHtml = "<font color=red>The impact of the spread of the novel coronavirus (COVID-19) is evolving rapidly. We continue to monitor the situation and are committed to taking the most appropriate steps necessary, in the best interest of our team members and customers, while following the guidance of  public health officials.  We are focused on the prevention of the spread of influenza and COVID-19.  If you are experiencing any respiratory symptoms, as suggested by the CDC, please stay at home and do not attend the scheduled training.  Please contact register@esc4.net to reschedule your training when you are well. We are happy to assist you with transferring into the same or a similar session at a future date.<br/><br></font><b>Please be aware that traffic conditions vary greatly and may increase expected travel times to our facility. </b><br/>";

                                //    table.Rows.Add(row);


                                //    //row = new HtmlTableRow();
                                //    //row.Cells.Add(new HtmlTableCell());
                                //    //row.Cells[0].ColSpan = 2;
                                //    //row.Cells[0].Align = "left";
                                //    //row.Cells[0].InnerHtml = "<br/><br/><b>Special Accommodations:</b><br/>Region 4 Education Service Center (Region 4) is accessible to individuals with disabilities. If you have special needs or require special arrangements in order to participate in a session, please contact Region 4 Registration Services five working days prior to the training program date at 713.744.6326 or <a href=\"mailto:register@esc4.net\">register@esc4.net</a>.<br/>";
                                //    //table.Rows.Add(row);


                                //    //if (Convert.ToInt32(dr["eventtype_id"].ToString()) != Convert.ToInt32(ConfigurationManager.AppSettings["jobfairEventTypeID"].ToString()))****
                                //    if (Convert.ToInt32(dr["eventtype_id"].ToString()) != 5017)
                                //    {
                                //        row = new HtmlTableRow();
                                //        row.Cells.Add(new HtmlTableCell());
                                //        row.Cells[0].ColSpan = 2;
                                //        row.Cells[0].Align = "left";
                                //        row.Cells[0].InnerHtml = "<br/><b>Cancellation and Refund Policy:</b><br/>Cancellations <b>must</b> be completed online or sent to <a href=\"mailto:cancellations@esc4.net\">cancellations@esc4.net</a> no later than seven (7) calendar days prior to event for a refund. We are not able to accept phone cancellations. Registrations are transferrable or substitutions may be allowed up to, but not after the session date.<br/>";
                                //        table.Rows.Add(row);

                                //        row = new HtmlTableRow();
                                //        row.Cells.Add(new HtmlTableCell());
                                //        row.Cells[0].ColSpan = 2;
                                //        row.Cells[0].Align = "left";
                                //        row.Cells[0].InnerHtml = "<br/>A processing fee of 25% of the registration cost with a minimum fee of $35 will be applied to the refund if the cancellation is submitted before seven (7) calendar days prior to the event. Refunds are not issued for online courses, nonattendance, or cancellations submitted one to six days prior to the event. Participants will receive a full refund for events cancelled by Region 4. This only applies to sessions with a fee.<br/>";
                                //        table.Rows.Add(row);

                                //        row = new HtmlTableRow();
                                //        row.Cells.Add(new HtmlTableCell());
                                //        row.Cells[0].ColSpan = 2;
                                //        row.Cells[0].Align = "left";
                                //        row.Cells[0].InnerHtml = "<b>To ensure the safety of children under the age of 18, minors are not allowed in professional development sessions or to be left unattended on the premises. Region 4 Education Service Center does not provide child care accommodations. Participants who bring children will be asked to leave.</b><br/>";
                                //        table.Rows.Add(row);

                                //        row = new HtmlTableRow();
                                //        row.Cells.Add(new HtmlTableCell());
                                //        row.Cells[0].ColSpan = 2;
                                //        row.Cells[0].Align = "left";
                                //        row.Cells[0].InnerHtml = "<br/><b>Session Cancellation:</b></br>In the event of a class cancellation you will be contacted by email.<br/><br/>";
                                //        table.Rows.Add(row);

                                //        row = new HtmlTableRow();
                                //        row.Cells.Add(new HtmlTableCell());
                                //        row.Cells[0].ColSpan = 2;
                                //        row.Cells[0].Align = "left";
                                //        row.Cells[0].InnerHtml = String.Format("<br/><b>Questions?</b></br>Visit our <a href=\"{0}\">Help & How-To</a> page, manage your <a href=\"{1}\">registrations online</a> or contact Registration Services at <a href=\"mailto:register@esc4.net\">register@esc4.net<a/> or 713-744-6326<br/><br/>",
                                //            (ConfigurationManager.AppSettings["database.activeserver"] == "production") ? "www.escweb.net/tx_esc_04/help.aspx" : ConfigurationManager.AppSettings["database.activeserver"] + "-www.escweb.net/tx_esc_04/help.aspx",
                                //            (ConfigurationManager.AppSettings["database.activeserver"] == "production") ? "www.escweb.net/tx_esc_04/shoebox/registration/default.aspx" : ConfigurationManager.AppSettings["database.activeserver"] + "-www.escweb.net/tx_esc_04/shoebox/registration/default.aspx");
                                //        table.Rows.Add(row);

                                //        row = new HtmlTableRow();
                                //        row.Cells.Add(new HtmlTableCell());
                                //        row.Cells[0].ColSpan = 2;
                                //        row.Cells[0].Align = "left";
                                //        row.Cells[0].InnerHtml = "<br/><hr><br/>This email is generated by the system. Please do not reply to this email. <br/><br/>";
                                //        table.Rows.Add(row);
                                //    }

                                StringWriter sw = new StringWriter();
                                table.RenderControl(new HtmlTextWriter(sw));
                                string MsgBody = sw.ToString();
                                Mmsg.Body = MsgBody;

                                AlternateView htmlview = default(AlternateView);
                                htmlview = AlternateView.CreateAlternateViewFromString(MsgBody, null, "text/html");

                                LinkedResource CustomerLogo = new LinkedResource(@"c:\tx_esc_04\r4_logo.png");
                                CustomerLogo.ContentId = "customerLogo";

                                LinkedResource CustomerLogo2 = new LinkedResource(@"c:\tx_esc_04\r4_logosmall.png");
                                CustomerLogo2.ContentId = "customerLogo2";


                                CustomerLogo.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                                htmlview.LinkedResources.Add(CustomerLogo);
                                Mmsg.AlternateViews.Add(htmlview);

                                CustomerLogo2.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                                htmlview.LinkedResources.Add(CustomerLogo2);
                                Mmsg.AlternateViews.Add(htmlview);


                                string[] sessioncreate_recipient = dr["email"].ToString().Split(';');
                                StringBuilder ss = new StringBuilder();
                                foreach (string eachrecipient in sessioncreate_recipient)
                                {
                                    //checking for format of email
                                    try
                                    {
                                        if (new System.Text.RegularExpressions.Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*").IsMatch(eachrecipient))
                                            Mmsg.To.Add(eachrecipient);
                                    }
                                    catch (Exception e)
                                    {
                                        continue;
                                    }


                                        
                                        

                                }

                                //Mmsg.To = dr["email"].ToString();

                                Mmsg.From = new MailAddress("messenger@esclive.net", "Messenger");


                                try
                                {
                                    //checking for invalid domain
                                    SmtpClient SmtpMail = new SmtpClient(ConfigurationManager.AppSettings["smtp_server"]);
                                    if (Mmsg.To.Count > 0)
                                    {
                                        SmtpMail.Send(Mmsg);
                                        Mmsg.Dispose();
                                    }
                                    Updatenotifiedfieldforsessionreminder(dr["email"].ToString());
                                }

                                catch (Exception Ex)
                                {
                                    string innerError = Ex.Message;

                                    if (Ex.InnerException != null)
                                    {
                                        innerError += "An error has been identified:\n" + Ex.InnerException + "\n" + Mmsg.To;
                                    }

                                    System.Diagnostics.EventLog.WriteEntry("Session Reminder tx_esc_04:", innerError, System.Diagnostics.EventLogEntryType.Error);
                                    continue; 

                                }

                                //}
                            }
                        }

                        catch (Exception Ex)
                        {
                            System.Diagnostics.EventLog.WriteEntry("Session Reminder Service Error tx_esc_04:",  Ex.Message.ToString(), System.Diagnostics.EventLogEntryType.Error);

                            break;
                        }

                        finally
                        {
                            if (SQLCommand.Connection.State != ConnectionState.Closed)
                                SQLCommand.Connection.Close();
                        }
                    }


                    break;

                //case Utility.Utility.MessageType.SessionremiderToFaci:
                //    using (SqlCommand SQLCommand = new SqlCommand("[/sysmail/tx_esc_04Notification/Registrationconfirmation/faci]", new SqlConnection(Constring)))
                //    {

                //        SQLCommand.CommandType = CommandType.StoredProcedure;

                //        try
                //        {

                //            SQLCommand.Connection.Open();
                //            SqlDataReader dr = SQLCommand.ExecuteReader(CommandBehavior.CloseConnection);
                //            while (dr.Read())
                //            {
                //                MailMessage Mmsg = new MailMessage();

                //                //MailMessage Mmsg = new MailMessage();
                //                Mmsg.Subject = "Reminder of Scheduled Event";
                //                Mmsg.IsBodyHtml = true;

                //                Mmsg.Body = string.Format(DocElement.DocumentElement.ChildNodes[9].InnerText, dr[0].ToString(), dr[1].ToString(), Convert.ToDateTime(dr[2].ToString()).ToShortDateString(), Convert.ToDateTime(dr[2].ToString()).ToShortTimeString(), Convert.ToDateTime(dr[3].ToString()).ToShortTimeString(), dr[4].ToString(),dr[5].ToString(),dr[6].ToString());

                //                string[] sessioncreate_recipient = dr[7].ToString().Split(';');
                //                StringBuilder ss = new StringBuilder();
                //                foreach (string eachrecipient in sessioncreate_recipient)
                //                {
                //                    //checking for format of email
                //                    if (new System.Text.RegularExpressions.Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*").IsMatch(eachrecipient))
                //                    {

                //                        Mmsg.To.Add(eachrecipient);


                //                    }

                //                }
                //                //Mmsg.To = ss.ToString();

                //                Mmsg.From = new MailAddress("messenger@esclive.net", "Messenger");


                //                try
                //                {
                //                    //checking for invalid domain
                //                    SmtpClient SmtpMail = new SmtpClient(ConfigurationManager.AppSettings["smtp_server"]);
                //                    if (Mmsg.To.Count > 0)
                //                    {
                //                        SmtpMail.Send(Mmsg);
                //                        Mmsg.Dispose();
                //                    }
                //                    Updatenotifiedfieldforsessionremindertofaci(Convert.ToDateTime(dr[2].ToString()), Convert.ToDateTime(dr[3].ToString()));// dr[6].ToString()
                //                }

                //                catch (Exception Ex)
                //                {
                //                    string innerError = Ex.Message;

                //                    if (Ex.InnerException != null)
                //                    {
                //                        innerError += "An error has been identified:\n" + Ex.InnerException + "\n" + Mmsg.To;
                //                    }

                //                    System.Diagnostics.EventLog.WriteEntry("Session Reminder to faci tx_esc_04:", innerError, System.Diagnostics.EventLogEntryType.Error);

                //                }

                //            }
                //        }

                //        catch (Exception Ex)
                //        {
                //            System.Diagnostics.EventLog.WriteEntry("Session Reminder to faci Service Error tx_esc_04:", Constring.ToString() + Ex.Message.ToString(), System.Diagnostics.EventLogEntryType.Error);

                //        }

                //        finally
                //        {
                //            if (SQLCommand.Connection.State != ConnectionState.Closed)
                //                SQLCommand.Connection.Close();
                //        }
                //    }



                    //break;
                
                default:
                    break;                  

            }
              
        }



        public
        static void Updatenotifiedfieldforsessionreminder(string email)
        {
            // System.Diagnostics.EventLog.WriteEntry("Session Reminder Service Error wa_esd113:", dt1.ToString() +','+ dt2.ToString(), System.Diagnostics.EventLogEntryType.Error);
            using (SqlCommand SQLCommand1 = new SqlCommand("[/sysmail/tx_esc_04Notification/sessionreminder/update]", new SqlConnection(Constring)))
            {


                SQLCommand1.Connection.Open();
                SQLCommand1.CommandType = CommandType.StoredProcedure;
                SQLCommand1.Parameters.Add("@Email", SqlDbType.NVarChar,500).Value = email;
                try
                {
                    SQLCommand1.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    System.Web.HttpContext.Current.Response.Write(ex.Message);
                }

                finally
                {

                    if (SQLCommand1.Connection.State != ConnectionState.Closed)
                        SQLCommand1.Connection.Close();

                }

            }


        }


        public static void Updatenotifiedfieldforsessionremindertofaci(DateTime dt1, DateTime dt2)
        {
            // System.Diagnostics.EventLog.WriteEntry("Session Reminder Service Error wa_esd113:", dt1.ToString() +','+ dt2.ToString(), System.Diagnostics.EventLogEntryType.Error);
            using (SqlCommand SQLCommand1 = new SqlCommand("[/sysmail/tx_esc_04Notification/registrationconfirmation/faci/update]", new SqlConnection(Constring)))
            {


                SQLCommand1.Connection.Open();
                SQLCommand1.CommandType = CommandType.StoredProcedure;
                SQLCommand1.Parameters.Add("@dt1", SqlDbType.DateTime, 100).Value = dt1;
                SQLCommand1.Parameters.Add("@dt2", SqlDbType.DateTime, 100).Value = dt2;
                try
                {
                    SQLCommand1.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    System.Web.HttpContext.Current.Response.Write(ex.Message);
                }

                finally
                {

                    if (SQLCommand1.Connection.State != ConnectionState.Closed)
                        SQLCommand1.Connection.Close();

                }

            }


        }


    }
}