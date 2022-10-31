using AgileTask.Domain.Entities;

namespace AgileTask.Domain.ViewModels
{
    public class VacationDayViewModel
    {
        public int Id { get; set; }
        public int NumberOfDays { get; set; }
        public string Note { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }

    }
}
