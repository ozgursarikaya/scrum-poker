using ScrumPoker.Dto;

namespace ScrumPoker.DataAccess.Abstract;

public interface IPlanningPokerVotingTypeDal
{
    Task<List<PlanningPokerVotingTypeDto>> GetList();
}