using BugTrackerTry.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerTry.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int ProjectUserId { get; set; }

        public string Title { get; set; }
        public string Body { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public DateTime Resolved { get; set; }

        public TicketStatus TicketStatus { get; set; }
        public TicketType TicketType { get; set; }

        public byte[] ImageData { get; set; }
        public string ContentType { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }

        //Navigation properties
        public virtual Project Project { get; set; }
        public virtual ProjectUser ProjectUser { get; set; }
        public virtual TicketHistory TicketHistory { get; set; }
        public virtual IEnumerable<TicketAttachment> TicketAttachments { get; set; } = new HashSet<TicketAttachment>();
        public virtual IEnumerable<TicketComment> TicketComments { get; set; } = new HashSet<TicketComment>();
    }
}
