using ScrumPoker.Business.Abstract;
using ScrumPoker.Dto;

namespace ScrumPoker.Business.Concrete;

public class PlanningPokerUserService : IPlanningPokerUserService
{
    private readonly IPlanningPokerUserService _planningPokerUserService;

    public PlanningPokerUserService(IPlanningPokerUserService planningPokerUserService)
    {
        _planningPokerUserService = planningPokerUserService;
    }

    public async Task<Guid> Create(CreatePlanningPokerUserDto request)
    {
        return await _planningPokerUserService.Create(request);
    }

    public async Task<bool> Delete(Guid planningPokerId, Guid userId)
    {
        return await _planningPokerUserService.Delete(planningPokerId, userId);
    }

    public async Task<List<PlanningPokerUserDto>> GetList(Guid planningPokerId)
    {
        return await _planningPokerUserService.GetList(planningPokerId);
    }

    public async Task<bool> Update(UpdatePlanningPokerUserDto request)
    {
        return await _planningPokerUserService.Update(request);
    }
}