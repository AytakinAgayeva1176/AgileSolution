using AgileTask.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgileTask.Domain.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public int? PositionId { get; set; }
        public int? DepartmentId { get; set; }
        public PositionViewModel Position { get; set; }
        public DepartmentViewModel Department { get; set; }
        public IEnumerable<VacationApplicationViewModel> Applications { get; set; }

    }
}
