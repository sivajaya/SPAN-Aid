using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Span_Aid.Controllers
{
    public class SpanAidController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        public List<Span_Aid_TL> GetTLTicketDetailsListByEmailAddress(Span_Aid spanAid)
        {
            List<Span_Aid_TL> TLTicketDetailsList = null;
            if (!string.IsNullOrWhiteSpace(spanAid.EmailAddress))
            {
                using (Span_AidEntities entities = new Span_AidEntities())
                {
                    TLTicketDetailsList = entities.TruckLogics_Ticket.Where(a => a.Tl_Email_Address == spanAid.EmailAddress && !a.TL_Is_Deleted).Select(a => new Span_Aid_TL()
                    { 
                        TLTicketId = a.TL_Ticket_Id,
                        TLTicketNumber = a.TL_Ticket_Number,
                        TLEmailAddress = a.Tl_Email_Address,
                        TLUserName = a.TL_User_Name,
                        TLModuleName = a.TL_Module_Name,
                        TLTicketDescription = a.TL_Ticket_Description,
                        TLTicketStatus = a.TL_Ticket_Status,
                    }).ToList();                   
                }
            }
            return TLTicketDetailsList;
        }

         [HttpPost]
        public List<Span_Aid_HR> GetHRTicketDetailsListByEmailAddress(Span_Aid spanAid)
        {
            List<Span_Aid_HR> HRTicketDetailsList = null;
            if (!string.IsNullOrWhiteSpace(spanAid.EmailAddress))
            {
                using (Span_AidEntities entities = new Span_AidEntities())
                {
                    HRTicketDetailsList = entities.HealingRadius_Ticket.Where(a => a.HR_Email_Address == spanAid.EmailAddress && !a.HR_Is_Deleted).Select(a => new Span_Aid_HR()
                    {
                        HRTicketId = a.HR_Ticket_Id,
                        HRTicketNumber = a.HR_Ticket_Number,
                        HREmailAddress = a.HR_Email_Address,
                        HRUserName = a.HR_User_Name,
                        HRAppointmentNumber = a.HR_Appoinment_Number,
                        HRTicketDescription = a.HR_Ticket_Description,
                        HRTicketStatus = a.HR_Ticket_Status,
                    }).ToList();
                }
            }
            return HRTicketDetailsList;
        }

         [HttpPost]
         public Span_Aid_TL GetTLTicketDetailsByTLTicketId(Span_Aid spanAid)
        {
            Span_Aid_TL TLTicketDetails = null;
            if (spanAid.TLTicketId > 0)
            {
                using (Span_AidEntities entities = new Span_AidEntities())
                {
                    TruckLogics_Ticket dbTLTicketDetails = entities.TruckLogics_Ticket.SingleOrDefault(a => a.TL_Ticket_Id == spanAid.TLTicketId && !a.TL_Is_Deleted);
                    if (dbTLTicketDetails != null)
                    {
                        TLTicketDetails = new Span_Aid_TL();
                        TLTicketDetails.TLTicketId = dbTLTicketDetails.TL_Ticket_Id;
                        TLTicketDetails.TLTicketNumber = dbTLTicketDetails.TL_Ticket_Number;
                        TLTicketDetails.TLEmailAddress = dbTLTicketDetails.Tl_Email_Address;
                        TLTicketDetails.TLUserName = dbTLTicketDetails.TL_User_Name;
                        TLTicketDetails.TLModuleName = dbTLTicketDetails.TL_Module_Name;
                        TLTicketDetails.TLTicketDescription = dbTLTicketDetails.TL_Ticket_Description;
                        TLTicketDetails.TLTicketStatus = dbTLTicketDetails.TL_Ticket_Status;
                    }
                }
            }
            return TLTicketDetails;
        }

         [HttpPost]
         public Span_Aid_HR GetHRTicketDetailsByHRTicketId(Span_Aid spanAid)
        {
            Span_Aid_HR HRTicketDetails = null;
            if (spanAid.HRTicketId > 0)
            {
                using (Span_AidEntities entities = new Span_AidEntities())
                {
                    HealingRadius_Ticket dbHRTicketDetails = entities.HealingRadius_Ticket.SingleOrDefault(a => a.HR_Ticket_Id == spanAid.HRTicketId && !a.HR_Is_Deleted);
                    if (dbHRTicketDetails != null)
                    {
                        HRTicketDetails = new Span_Aid_HR();
                        HRTicketDetails.HRTicketId = dbHRTicketDetails.HR_Ticket_Id;
                        HRTicketDetails.HRTicketNumber = dbHRTicketDetails.HR_Ticket_Number;
                        HRTicketDetails.HRUserName = dbHRTicketDetails.HR_User_Name;
                        HRTicketDetails.HREmailAddress = dbHRTicketDetails.HR_Email_Address;
                        HRTicketDetails.HRAppointmentNumber = dbHRTicketDetails.HR_Appoinment_Number;
                        HRTicketDetails.HRTicketDescription = dbHRTicketDetails.HR_Ticket_Description;
                        HRTicketDetails.HRTicketStatus = dbHRTicketDetails.HR_Ticket_Status;
                    }
                }
            }
            return HRTicketDetails;
        }

         [HttpPost]
        public Span_Aid_HR SaveHRTicketDetails(Span_Aid_HR HRTicketDetails)
        {
            if (HRTicketDetails != null && !string.IsNullOrWhiteSpace(HRTicketDetails.HREmailAddress) && !string.IsNullOrWhiteSpace(HRTicketDetails.HRTicketNumber)
                 && !string.IsNullOrWhiteSpace(HRTicketDetails.HRTicketStatus) && !string.IsNullOrWhiteSpace(HRTicketDetails.HRAppointmentNumber))
            {
                using (Span_AidEntities entities = new Span_AidEntities())
                {
                    HealingRadius_Ticket dbHRTicketDetails = new HealingRadius_Ticket();

                    dbHRTicketDetails.HR_Ticket_Number = "HRT-" + (10000 + (entities.HealingRadius_Ticket.Count() + 1));
                    dbHRTicketDetails.HR_Email_Address = HRTicketDetails.HREmailAddress;
                    dbHRTicketDetails.HR_User_Name = HRTicketDetails.HRUserName;
                    dbHRTicketDetails.HR_Appoinment_Number = HRTicketDetails.HRAppointmentNumber;
                    if (!string.IsNullOrWhiteSpace(HRTicketDetails.HRTicketDescription) && HRTicketDetails.HRTicketDescription.Length > 500)
                    {
                        HRTicketDetails.HRTicketDescription = HRTicketDetails.HRTicketDescription.Substring(0, 500);
                    }
                    dbHRTicketDetails.HR_Ticket_Description = HRTicketDetails.HRTicketDescription;
                    dbHRTicketDetails.HR_Ticket_Status = HRTicketDetails.HRTicketStatus;
                    dbHRTicketDetails.HR_Ticket_Create_Date = DateTime.Now;
                    dbHRTicketDetails.HR_Ticket_Update_Date = DateTime.Now;
                    dbHRTicketDetails.HR_Is_Deleted = false;
                    entities.HealingRadius_Ticket.Add(dbHRTicketDetails);
                    entities.SaveChanges();
                }
            }
            return HRTicketDetails;
        }

         [HttpPost]
        public Span_Aid_TL SaveTLTicketDetails(Span_Aid_TL TLTicketDetails)
        {
            if (TLTicketDetails != null && !string.IsNullOrWhiteSpace(TLTicketDetails.TLEmailAddress) && !string.IsNullOrWhiteSpace(TLTicketDetails.TLTicketNumber)
                 && !string.IsNullOrWhiteSpace(TLTicketDetails.TLTicketStatus) && !string.IsNullOrWhiteSpace(TLTicketDetails.TLModuleName))
            {
                using (Span_AidEntities entities = new Span_AidEntities())
                {
                    TruckLogics_Ticket dbTLTicketDetails = new TruckLogics_Ticket();

                    dbTLTicketDetails.TL_Ticket_Number = "TLT-" + (10000 + (entities.TruckLogics_Ticket.Count() + 1)); ;
                    dbTLTicketDetails.Tl_Email_Address = TLTicketDetails.TLEmailAddress;
                    dbTLTicketDetails.TL_User_Name = TLTicketDetails.TLUserName;
                    dbTLTicketDetails.TL_Module_Name = TLTicketDetails.TLModuleName;
                    if (!string.IsNullOrWhiteSpace(TLTicketDetails.TLTicketDescription) && TLTicketDetails.TLTicketDescription.Length > 500)
                    {
                        TLTicketDetails.TLTicketDescription = TLTicketDetails.TLTicketDescription.Substring(0, 500);
                    }
                    dbTLTicketDetails.TL_Ticket_Description = TLTicketDetails.TLTicketDescription;
                    dbTLTicketDetails.TL_Ticket_Status = TLTicketDetails.TLTicketStatus;
                    dbTLTicketDetails.TL_Ticket_Create_Date = DateTime.Now;
                    dbTLTicketDetails.TL_Ticket_Update_Date = DateTime.Now;
                    dbTLTicketDetails.TL_Is_Deleted = false;
                    entities.TruckLogics_Ticket.Add(dbTLTicketDetails);
                    entities.SaveChanges();
                }
            }
            return TLTicketDetails;
        }

         [HttpPost]
         public bool DeleteTLTicketDetailsByTicketId(Span_Aid_TL TLTicketDetails)
         {
             bool isDeleted = false;
             if (TLTicketDetails != null && TLTicketDetails.TLTicketId > 0)
             {
                 using (Span_AidEntities entities = new Span_AidEntities())
                 {
                     TruckLogics_Ticket dbTLTicketDetails = entities.TruckLogics_Ticket.SingleOrDefault(a => a.TL_Ticket_Id == TLTicketDetails.TLTicketId && !a.TL_Is_Deleted);
                     if (dbTLTicketDetails != null)
                     {
                         dbTLTicketDetails.TL_Is_Deleted = true;
                         dbTLTicketDetails.TL_Ticket_Update_Date = DateTime.Now;                         
                         entities.SaveChanges();
                         isDeleted = true;
                     }
                 }
             }
             return isDeleted;
         }

         [HttpPost]
         public bool DeleteHRTicketDetailsByTicketId(Span_Aid_HR HRTicketDetails)
         {
             bool isUpdated = false;
             if (HRTicketDetails != null && HRTicketDetails.HRTicketId > 0)
             {
                 using (Span_AidEntities entities = new Span_AidEntities())
                 {
                     HealingRadius_Ticket dbHRTicketDetails = entities.HealingRadius_Ticket.SingleOrDefault(a => a.HR_Ticket_Id == HRTicketDetails.HRTicketId && !a.HR_Is_Deleted);
                     if (dbHRTicketDetails != null)
                     {
                         dbHRTicketDetails.HR_Is_Deleted = true;
                         dbHRTicketDetails.HR_Ticket_Update_Date = DateTime.Now;
                         entities.SaveChanges();
                         isUpdated = true;
                     }
                 }
             }
             return isUpdated;
         }

         [HttpPost]
         public bool DeleteHRTicketDetailsByTicketId(Span_Aid_HR HRTicketDetails)
         {
             bool isDeleted = false;
             if (HRTicketDetails != null && HRTicketDetails.HRTicketId > 0 && !string.IsNullOrWhiteSpace(HRTicketDetails.HRTicketStatus))
             {
                 using (Span_AidEntities entities = new Span_AidEntities())
                 {
                     HealingRadius_Ticket dbHRTicketDetails = entities.HealingRadius_Ticket.SingleOrDefault(a => a.HR_Ticket_Id == HRTicketDetails.HRTicketId && !a.HR_Is_Deleted);
                     if (dbHRTicketDetails != null)
                     {
                         dbHRTicketDetails.HR_Ticket_Status = HRTicketDetails.HRTicketStatus;
                         dbHRTicketDetails.HR_Is_Email_Send = false;
                         dbHRTicketDetails.HR_Is_PushNotification = false;
                         dbHRTicketDetails.HR_Ticket_Update_Date = DateTime.Now;
                         entities.SaveChanges();
                         isDeleted = true;
                     }
                 }
             }
             return isDeleted;
         }

         [HttpPost]
         public bool UpdateTLTicketStausByTicketId(Span_Aid_TL TLTicketDetails)
         {
             bool isUpdated = false;
             if (TLTicketDetails != null && TLTicketDetails.TLTicketId > 0 && !string.IsNullOrWhiteSpace(TLTicketDetails.TLTicketStatus))
             {
                 using (Span_AidEntities entities = new Span_AidEntities())
                 {
                     TruckLogics_Ticket dbTLTicketDetails = entities.TruckLogics_Ticket.SingleOrDefault(a => a.TL_Ticket_Id == TLTicketDetails.TLTicketId && !a.TL_Is_Deleted);
                     if (dbTLTicketDetails != null)
                     {
                         dbTLTicketDetails.TL_Ticket_Status = TLTicketDetails.TLTicketStatus;
                         dbTLTicketDetails.Tl_Is_Email_Send = false;
                         dbTLTicketDetails.TL_Is_PushNotification = false;
                         dbTLTicketDetails.TL_Ticket_Update_Date = DateTime.Now;
                         entities.SaveChanges();
                         isUpdated = true;
                     }
                 }
             }
             return isUpdated;
         }

       
    }
}