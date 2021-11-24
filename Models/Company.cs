using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerTry.Models
{
    public class Company
    {
        public int Id { get; set; }
        
        //Navigation properties
        public virtual IEnumerable<Project> Projects { get; set; } = new HashSet<Project>();
        public virtual IEnumerable<ProjectUser> ProjectUsers { get; set; } = new HashSet<ProjectUser>();
    }
}
