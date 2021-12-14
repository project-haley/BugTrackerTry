using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerTry.Models
{
    public class ProjectUser : IdentityUser
    {
        

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string DisplayName { get; set; }

        [Display(Name = "User Image")]
        public byte[] ImageData { get; set; }

        public byte[] Image { get; set; }
        public string ContentType { get; set; }


        [StringLength(100, ErrorMessage = "{0} must be no more than {1} characters.")]
        public string LinkedInUrl { get; set; }
        [StringLength(100, ErrorMessage = "{0} must be no more than {1} characters.")]
        public string TwitterUrl { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        //Navigation properties
        public virtual ICollection<Project> Projects { get; set; } = new HashSet<Project>();
        public virtual ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
        public virtual ICollection<TicketComment> TicketComments { get; set; } = new HashSet<TicketComment>();
    }
}
