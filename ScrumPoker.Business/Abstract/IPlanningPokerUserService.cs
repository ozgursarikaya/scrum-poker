using ScrumPoker.Dto;

namespace ScrumPoker.Business.Abstract;

public interface IPlanningPokerUserService
{
    Task<Guid> Create(CreatePlanningPokerUserDto request);
    Task<bool> Delete(Guid planningPokerId, Guid userId);
    Task<bool> Update(UpdatePlanningPokerUserDto request);
    Task<List<PlanningPokerUserDto>> GetList(Guid planningPokerId);
}