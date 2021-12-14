using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerTry.Models
{
    public class TicketAttachment
    {

        public int Id { get; set; }
        public int TicketId { get; set; }

        public TicketAttachment(int ticketId)
        {
            TicketId = ticketId;
        }

        public int TicketHistoryId { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Deleted { get; set; }

        [NotMapped]
        public IFormFile AttachmentFile { get; set; }

        public byte[] ImageData { get; set; }
        public string ContentType { get; set; }

        //Navigation properties
        public virtual Ticket Ticket { get; set; }
    }
}
