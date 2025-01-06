using ScrumPoker.Common.Consts;
using ScrumPoker.DataAccess.Abstract;
using ScrumPoker.DataProvider;
using ScrumPoker.Dto;

namespace ScrumPoker.DataAccess.Concrete;

public class PlanningPokerVotingTypeDal(IDataProvider dataProvider) : IPlanningPokerVotingTypeDal
{
    private readonly IDataProvider _dataProvider = dataProvider;

    public async Task<List<PlanningPokerVotingTypeDto>> GetList()
    {
        return await _dataProvider.GetList<PlanningPokerVotingTypeDto>(StoredProcedureNames.GetPlanningPokerVotingTypes);
    }
}