using ScrumPoker.Business.Abstract;
using ScrumPoker.DataAccess.Abstract;
using ScrumPoker.Dto;

namespace ScrumPoker.Business.Concrete;

public class PlanningPokerUserService : IPlanningPokerUserService
{
    private readonly IPlanningPokerUserDal _planningPokerUserDal;

    public PlanningPokerUserService(IPlanningPokerUserDal planningPokerUserDal)
    {
        _planningPokerUserDal = planningPokerUserDal;
    }

    public async Task<Guid> Create(CreatePlanningPokerUserDto request)
    {
        return await _planningPokerUserDal.Create(request);
    }

    public async Task<bool> Delete(Guid planningPokerId, Guid userId)
    {
        return await _planningPokerUserDal.Delete(planningPokerId, userId);
    }

    public async Task<List<PlanningPokerUserDto>> GetList(Guid planningPokerId)
    {
        return await _planningPokerUserDal.GetList(planningPokerId);
    }

    public async Task<bool> Update(UpdatePlanningPokerUserDto request)
    {
        return await _planningPokerUserDal.Update(request);
    }
}