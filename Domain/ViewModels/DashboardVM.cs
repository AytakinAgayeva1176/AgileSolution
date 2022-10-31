using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgileTask.Domain.ViewModels
{
    public class DashboardVM
    {
        public int UsersCount { get; set; }
        public int DepartmentsCount { get; set; }
        public int PositionsCount { get; set; }
        public int ApplicationsCount { get; set; }
    }
}
