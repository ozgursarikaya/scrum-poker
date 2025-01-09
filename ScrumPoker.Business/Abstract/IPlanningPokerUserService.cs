using ScrumPoker.Dto;

namespace ScrumPoker.Business.Abstract;

public interface IPlanningPokerUserService
{
    Task<Guid> Create(CreatePlanningPokerUserDto request);
}