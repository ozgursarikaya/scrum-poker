using ScrumPoker.Business.Abstract;
using ScrumPoker.DataAccess.Concrete;
using ScrumPoker.Dto;

namespace ScrumPoker.Business.Concrete;

public class RetroColumnService(RetroColumnDal retroColumnDal) : IRetroColumnService
{
    private readonly RetroColumnDal _retroColumnDal = retroColumnDal;

    public async Task<Guid> Create(CreateRetroColumnDto request)
    {
        return await _retroColumnDal.Create(request);
    }
}
