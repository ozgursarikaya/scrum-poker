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

    public async Task<bool> Delete(Guid planningPokerId, Guid userId)
    {
        return await _dataProvider.Delete(StoredProcedureNames.CreatePlanningPoker, new
        {
            PlanningPokerId = planningPokerId,
            UserId = userId
        });
    }

    public async Task<List<PlanningPokerUserDto>> GetList(Guid planningPokerId)
    {
        return await _dataProvider.GetList<PlanningPokerUserDto>(StoredProcedureNames.GetPlanningPoker, new { PlanningPokerId = planningPokerId });
    }

    public async Task<bool> Update(UpdatePlanningPokerUserDto request)
    {
        return await _dataProvider.Update(StoredProcedureNames.UpdatePlanningPokerUser, request);
    }
}