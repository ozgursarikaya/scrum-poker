using ScrumPoker.Dto;

namespace ScrumPoker.DataAccess.Abstract;

public interface IPlanningPokerUserDal
{
    Task<Guid> Create(CreatePlanningPokerUserDto request);
    Task<bool> Delete(Guid planningPokerId, Guid userId);
    Task<bool> Update(UpdatePlanningPokerUserDto request);
    Task<List<PlanningPokerUserDto>> GetList(Guid planningPokerId);
}