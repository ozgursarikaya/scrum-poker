using ScrumPoker.Common.Consts;
using ScrumPoker.DataAccess.Abstract;
using ScrumPoker.DataProvider;
using ScrumPoker.Dto;

namespace ScrumPoker.DataAccess.Concrete;

public class PlanningPokerDal(IDataProvider dataProvider) : IPlanningPokerDal
{
    private readonly IDataProvider _dataProvider = dataProvider;

    public async Task<Guid> Create(CreatePlanningPokerDto request)
    {
        return await _dataProvider.Insert<Guid>(StoredProcedureNames.CreatePlanningPoker, request);
    }

    public async Task<PlanningPokerDto> Get(Guid id)
    {
        return await _dataProvider.Get<PlanningPokerDto>(StoredProcedureNames.GetPlanningPoker);
    }

    public async Task<List<GetPlanningPokerListDto>> GetList(GetPlanningPokerListFilterDto filter)
    {
        return await _dataProvider.GetList<GetPlanningPokerListDto>(StoredProcedureNames.GetPlanningPokers, filter);
    }

    public async Task<bool> Update(UpdatePlanningPokerDto request)
    {
        return await _dataProvider.Update(StoredProcedureNames.UpdatePlanningPoker, request);
    }
}