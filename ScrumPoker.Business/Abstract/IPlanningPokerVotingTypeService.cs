using ScrumPoker.Dto;

namespace ScrumPoker.Business.Abstract;

public interface IPlanningPokerVotingTypeService
{
    Task<List<PlanningPokerVotingTypeDto>> GetList();
}