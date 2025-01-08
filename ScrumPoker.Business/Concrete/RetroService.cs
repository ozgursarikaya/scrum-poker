using ScrumPoker.Business.Abstract;
using ScrumPoker.DataAccess.Abstract;
using ScrumPoker.Dto;

namespace ScrumPoker.Business.Concrete;

public class RetroService(IRetroDal retroDal) : IRetroService
{
    private readonly IRetroDal _retroDal = retroDal;

    public async Task<Guid> Create(CreateRetroDto request)
    {
        return await _retroDal.Create(request);
    }
}