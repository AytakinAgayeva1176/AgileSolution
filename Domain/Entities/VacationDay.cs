

namespace AgileTask.Domain.Entities
{
    public class VacationDay : BaseEntity
    {
        public int NumberOfDays { get; set; }
        public string Note { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
