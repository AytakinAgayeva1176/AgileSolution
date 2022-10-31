using AgileTask.Domain.Entities;
using AgileTask.Domain.Enums;
using System;

namespace AgileTask.Domain.ViewModels
{
    public class VacationApplicationViewModel
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public DateTime StartDate { get; set; }
        public int Days { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
