using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AgileTask.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? PositionId { get; set; }
        public int? DepartmentId { get; set; }
        public Position Position { get; set; }
        public Department Department { get; set; }
        public List<VacationApplication> VacationApplications { get; set; }
    }
}
