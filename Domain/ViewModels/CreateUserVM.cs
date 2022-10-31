using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgileTask.Domain.ViewModels
{
    public class CreateUserVM
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<string> RoleIds { get; set; }
        public int? PositionId { get; set; }
        public int? DepartmentId { get; set; }
        public string UserName { get; set; }
    }
}
