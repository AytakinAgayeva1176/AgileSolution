using AgileTask.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace AgileTask.Domain.Entities
{
    public class VacationApplication : BaseEntity
    {
        public Status Status { get; set; }
        public DateTime StartDate { get; set; }
        public int Days { get; set; }
        [Required]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
