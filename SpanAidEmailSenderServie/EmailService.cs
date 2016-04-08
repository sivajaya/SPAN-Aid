using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Timers;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SpanAidEmailSenderServie
{
    public partial class EmailService : ServiceBase
    {
        #region Declarations
        private Timer timer = new Timer();
        public bool isServiceInProcess = false; //to avoid multiple calls at the same time
        public bool isTimerDisabled = false;
        public bool isExceptionOccur = false;
        #endregion

        #region Constructor
        public EmailService()
        {
            InitializeComponent();
            //StartEmailService();
            timer.Enabled = true;
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Interval = 1500;

        }
        #endregion

        #region Timer Elapsed Event
        public void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //disabe the timer to avoid unwanted call before completion of process.
            timer.Enabled = false;

            //Start the Service for the Time Interval given
            StartEmailService();

            //enable the timer, if the process completes.
            if (!isTimerDisabled)
            {
                timer.Enabled = true;
            }
        }
        #endregion

        #region StartService Function
        public void StartEmailService()
        {
            try
            {
                if (!isServiceInProcess)
                {
                    isServiceInProcess = true;
                    isExceptionOccur = false;
                    using (Span_AidEntities entities = new Span_AidEntities())
                    {
                        List<HealingRadius_Ticket> dbHRTicketList = entities.HealingRadius_Ticket.Where(a => !a.HR_Is_Deleted && !a.HR_Is_Email_Send).ToList();
                        if (dbHRTicketList != null && dbHRTicketList.Count > 0)
                        {
                            foreach (HealingRadius_Ticket HRTicket in dbHRTicketList)
                            {
                                if (HRTicket != null && !string.IsNullOrWhiteSpace(HRTicket.HR_Email_Address))
                                {
                                    bool isEmailSend = SendHREmail(HRTicket);
                                    if (isEmailSend)
                                    {
                                        HRTicket.HR_Is_Email_Send = true;                                        
                                    }
                                }
                            }
                            entities.SaveChanges();
                        }

                        List<TruckLogics_Ticket> dbTLTicketList = entities.TruckLogics_Ticket.Where(a => !a.TL_Is_Deleted && !a.Tl_Is_Email_Send).ToList();
                        if (dbTLTicketList != null && dbTLTicketList.Count > 0)
                        {
                            foreach (TruckLogics_Ticket TLTicket in dbTLTicketList)
                            {
                                if (TLTicket != null && !string.IsNullOrWhiteSpace(TLTicket.Tl_Email_Address))
                                {
                                    bool isEmailSend = SendTLEmail(TLTicket);
                                    if (isEmailSend)
                                    {
                                        TLTicket.Tl_Is_Email_Send = true;
                                    }
                                }
                            }
                            entities.SaveChanges();
                        }
                    }



                    isServiceInProcess = false;
                }
            }
            catch (Exception ex)
            {
                isServiceInProcess = true;
                isExceptionOccur = true;
                //Stop the timer
                timer.Stop();
                System.Diagnostics.EventLog _eventLog = new System.Diagnostics.EventLog("Application", Environment.MachineName, " HR & TL Email Service");
                _eventLog.WriteEntry("Email Service: Exception occured." + ex, System.Diagnostics.EventLogEntryType.Error);
                this.Stop();

            }

        }
        #endregion

        protected override void OnStart(string[] args)
        {
            //start the timer
            timer.Start();
        }

        protected override void OnStop()
        {
        }

        #region Send HR Email

        public bool SendHREmail(HealingRadius_Ticket HRTicket)
        {
            bool isEmailSend = false;
            if (HRTicket != null && !string.IsNullOrWhiteSpace(HRTicket.HR_Ticket_Number)
                && !string.IsNullOrWhiteSpace(HRTicket.HR_Ticket_Description) && !string.IsNullOrWhiteSpace(HRTicket.HR_Ticket_Status))
            {
                SmtpClient smtpClient = new SmtpClient();
                MailMessage mailmsg = new MailMessage();
                mailmsg = new MailMessage();
                mailmsg.IsBodyHtml = true;
                mailmsg.To.Add(HRTicket.HR_Email_Address);
                mailmsg.Bcc.Add("sivakumar.kumarasamy@w3magix.com");
                mailmsg.From = new MailAddress("jagadeesh.gnanasekaran@spanllc.com", "Jagadeesh G");
                mailmsg.Subject = "Updates from Healing Radius Ticket - " + HRTicket.HR_Ticket_Number;
                mailmsg.Body = "Hi " + HRTicket.HR_User_Name + ", <br/> Your requst for Healing radius regarging (" + HRTicket.HR_Ticket_Description + ") has been " + HRTicket.HR_Ticket_Status.ToLower();
                smtpClient.Credentials = new NetworkCredential("vistaemails@gmail.com", "poiuyt15");
                smtpClient.EnableSsl = true;
                smtpClient.Port = 587;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Send(mailmsg);
                isEmailSend = true;
            }
            return isEmailSend;
        }

        #endregion

        #region Send TL Email

        public bool SendTLEmail(TruckLogics_Ticket TLTicket)
        {
            bool isEmailSend = false;
            if (TLTicket != null && !string.IsNullOrWhiteSpace(TLTicket.TL_Ticket_Number)
                && !string.IsNullOrWhiteSpace(TLTicket.TL_Ticket_Description) && !string.IsNullOrWhiteSpace(TLTicket.TL_Ticket_Status))
            {
                SmtpClient smtpClient = new SmtpClient();
                MailMessage mailmsg = new MailMessage();
                mailmsg = new MailMessage();
                mailmsg.IsBodyHtml = true;
                mailmsg.To.Add(TLTicket.Tl_Email_Address);
                mailmsg.Bcc.Add("sivakumar.kumarasamy@w3magix.com");
                mailmsg.From = new MailAddress("jagadeesh.gnanasekaran@spanllc.com", "Jagadeesh G");
                mailmsg.Subject = "Updates from TruckLogics Ticket - " + TLTicket.TL_Ticket_Number;
                mailmsg.Body = "Hi " + TLTicket.TL_User_Name + ", <br/> Your requst for TruckLogics regarging (" + TLTicket.TL_Ticket_Description + ") has been " + TLTicket.TL_Ticket_Status.ToLower();
                smtpClient.Credentials = new NetworkCredential("vistaemails@gmail.com", "poiuyt15");
                smtpClient.EnableSsl = true;
                smtpClient.Port = 587;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Send(mailmsg);
                isEmailSend = true;
            }
            return isEmailSend;
        }

        #endregion
    }
}
