using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Span_Aid
{
    public class Span_Aid_HR
    {
        public long HRTicketId { get; set; }
        public string HRTicketNumber { get; set; }
        public string HREmailAddress { get; set; }
        public string HRUserName { get; set; }
        public string HRAppointmentNumber { get; set; }
        public string HRTicketDescription { get; set; }
        public string HRTicketStatus { get; set; }
        public DateTime HRTicketCreateDate { get; set; }
        public DateTime HRTicketUpdateDate { get; set; }
    }
}