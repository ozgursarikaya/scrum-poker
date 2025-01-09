using ScrumPoker.Common.Consts;
using ScrumPoker.DataAccess.Abstract;
using ScrumPoker.DataProvider;
using ScrumPoker.Dto;

namespace ScrumPoker.DataAccess.Concrete;

public class PlanningPokerUserDal : IPlanningPokerUserDal
{
    private readonly IDataProvider _dataProvider;

    public PlanningPokerUserDal(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public async Task<Guid> Create(CreatePlanningPokerUserDto request)
    {
        return await _dataProvider.Insert<Guid>(StoredProcedureNames.CreatePlanningPokerUser, request);
    }
}