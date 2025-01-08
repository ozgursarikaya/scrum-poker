using ScrumPoker.Common.Consts;
using ScrumPoker.DataAccess.Abstract;
using ScrumPoker.DataProvider;
using ScrumPoker.Dto;

namespace ScrumPoker.DataAccess.Concrete;

public class RetroColumnDal(IDataProvider dataProvider) : IRetroColumnDal
{
    private readonly IDataProvider _dataProvider = dataProvider;

    public async Task<Guid> Create(CreateRetroColumnDto request)
    {
        return await _dataProvider.Insert<Guid>(StoredProcedureNames.CreateRetroColumn, request);
    }

    public async Task<bool> Delete(Guid id, Guid retroId)
    {
        return await _dataProvider.Delete(StoredProcedureNames.DeleteRetroColumn, new { Id = id, RetroId = retroId });
    }

    public async Task<List<RetroColumnDto>> GetList(Guid retroId)
    {
        return await _dataProvider.GetList<RetroColumnDto>(StoredProcedureNames.GetRetroColumns, new { RetroId = retroId });
    }
}