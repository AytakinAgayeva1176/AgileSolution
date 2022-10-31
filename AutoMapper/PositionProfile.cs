using AgileTask.Domain.Entities;
using AgileTask.Domain.ViewModels;
using AutoMapper;

namespace AgileTask.AutoMapper
{
    public class PositionProfile: Profile
    {
        public PositionProfile()
        {
            CreateMap<Position, PositionViewModel>();
            CreateMap<PositionViewModel, Position>();
        }
    }
}
