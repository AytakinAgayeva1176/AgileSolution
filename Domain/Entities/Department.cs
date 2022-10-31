using System.Collections.Generic;

namespace AgileTask.Domain.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public string AbbrName { get; set; }
        public string Note { get; set; }
        public List<ApplicationUser> Users { get; set; }
    }
}
