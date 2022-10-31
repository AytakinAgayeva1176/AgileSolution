using System.Collections.Generic;

namespace AgileTask.Domain.Entities
{
    public class Position : BaseEntity
    {
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public List<VacationDay> VacationDays { get; set; }
        public List<ApplicationUser> Users { get; set; }
    }
}
