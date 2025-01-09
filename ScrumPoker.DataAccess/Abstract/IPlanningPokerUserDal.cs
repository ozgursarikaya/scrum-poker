using ScrumPoker.Dto;

namespace ScrumPoker.DataAccess.Abstract;

public interface IPlanningPokerUserDal
{
    Task<Guid> Create(CreatePlanningPokerUserDto request);
}