using BugTrackerTry.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerTry.Models
{
    public class TicketSnapshot
    {
        public int Id { get; set; }
        public int TicketHistoryId { get; set; }

        public string Title { get; set; }
        public string Body { get; set; }

        public TicketStatus TicketStatus { get; set; }
        public TicketType TicketType { get; set; }
        public DateTime Resolved { get; set; }

        public DateTime Created { get; set; }

        public byte[] ImageData { get; set; }
        public string ContentType { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }

        public virtual TicketHistory TicketHistory { get; set; }
    }
}
