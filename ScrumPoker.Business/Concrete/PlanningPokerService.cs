using ScrumPoker.Business.Abstract;
using ScrumPoker.DataAccess.Abstract;
using ScrumPoker.Dto;

namespace ScrumPoker.Business.Concrete;

public class PlanningPokerService(IPlanningPokerDal planningPokerDal) : IPlanningPokerService
{
    private readonly IPlanningPokerDal _planningPokerDal = planningPokerDal;

    public async Task<Guid> Create(CreatePlanningPokerDto request)
    {
        return await _planningPokerDal.Create(request);
    }

    public async Task<PlanningPokerDto> Get(Guid id)
    {
        return await _planningPokerDal.Get(id);
    }
}