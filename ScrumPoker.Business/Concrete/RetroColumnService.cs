using ScrumPoker.Business.Abstract;
using ScrumPoker.DataAccess.Abstract;
using ScrumPoker.Dto;

namespace ScrumPoker.Business.Concrete;

public class RetroColumnService(IRetroColumnDal retroColumnDal) : IRetroColumnService
{
    private readonly IRetroColumnDal _retroColumnDal = retroColumnDal;

    public async Task<Guid> Create(CreateRetroColumnDto request)
    {
        return await _retroColumnDal.Create(request);
    }

    public async Task<bool> Delete(Guid id, Guid retroId)
    {
        return await _retroColumnDal.Delete(id, retroId);
    }

    public async Task<List<RetroColumnDto>> GetList(Guid retroId)
    {
        return await _retroColumnDal.GetList(retroId);
    }
}