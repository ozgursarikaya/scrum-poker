using ScrumPoker.Common.Consts;
using ScrumPoker.DataAccess.Abstract;
using ScrumPoker.DataProvider;
using ScrumPoker.Dto;

namespace ScrumPoker.DataAccess.Concrete;

public class RetroDal(IDataProvider dataProvider) : IRetroDal
{
    private readonly IDataProvider _dataProvider = dataProvider;

    public async Task<Guid> Create(CreateRetroDto request)
    {
        return await _dataProvider.Insert<Guid>(StoredProcedureNames.CreateRetro, request);
    }
}