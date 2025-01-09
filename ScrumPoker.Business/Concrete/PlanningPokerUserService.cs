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
}