using ScrumPoker.Business.Abstract;
using ScrumPoker.DataAccess.Abstract;
using ScrumPoker.Dto;

namespace ScrumPoker.Business.Concrete;

public class PlanningPokerVotingTypeService(IPlanningPokerVotingTypeDal planningPokerVotingTypeDal) : IPlanningPokerVotingTypeService
{
    private readonly IPlanningPokerVotingTypeDal _planningPokerVotingTypeDal = planningPokerVotingTypeDal;

    public async Task<List<PlanningPokerVotingTypeDto>> GetList()
    {
        return await _planningPokerVotingTypeDal.GetList();
    }
}