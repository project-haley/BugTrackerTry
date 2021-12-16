using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerTry.Models
{
    public class TicketHistory
    {
        public int Id { get; set; }
        public int TicketId { get; set; }

        public TicketHistory(int ticketId)
        {
            TicketId = ticketId;
        }



        //Navigation properties
        public virtual Ticket Ticket { get; set; }
        //public virtual IEnumerable<TicketComment> TicketComments { get; set; } = new HashSet<TicketComment>();
        //public virtual IEnumerable<TicketAttachment> TicketAttachments { get; set; } = new HashSet<TicketAttachment>();
    }
}
