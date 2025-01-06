using ScrumPoker.Dto;

namespace ScrumPoker.DataAccess.Abstract;

public interface IPlanningPokerDal
{
    Task<Guid> Create(CreatePlanningPokerDto request);
    Task<PlanningPokerDto> Get(Guid id);
    Task<bool> Update(UpdatePlanningPokerDto request);
}