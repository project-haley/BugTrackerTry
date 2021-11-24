using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerTry.Models
{
    public class Project
    {
        public int Id { get; set; }
        public int ProjectUserId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public byte[] ImageData { get; set; }
        public string ContentType { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        //Navigation properties
        //public virtual Company Company { get; set; }
        public virtual ProjectUser ProjectUser { get; set; }
        public virtual Company Company { get; set; }
        public virtual IEnumerable<Ticket> Tickets { get; set; } = new HashSet<Ticket>();

        //How to create a nagivation property of all users associated with a project?
        //Should this even be a navigation property?
    }
}
