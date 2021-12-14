using BugTrackerTry.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerTry.Models
{
    public class TicketComment
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int TicketHistoryId { get; set; }
        public int ProjectUserId { get; set; }

        public string Body { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Deleted { get; set; }

        //Navigation properties
        public virtual Ticket Ticket { get; set; }
        public virtual TicketHistory TicketHistory { get; set; }
        public virtual ProjectUser ProjectUser { get; set; }
    }
}
