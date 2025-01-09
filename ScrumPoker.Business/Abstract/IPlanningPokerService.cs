using ScrumPoker.Dto;

namespace ScrumPoker.Business.Abstract;

public interface IPlanningPokerService
{
    Task<Guid> Create(CreatePlanningPokerDto request);
    Task<PlanningPokerDto> Get(Guid id);
    Task<bool> Update(UpdatePlanningPokerDto request);
    Task<List<GetPlanningPokerListDto>> GetList(GetPlanningPokerListFilterDto filter);
}