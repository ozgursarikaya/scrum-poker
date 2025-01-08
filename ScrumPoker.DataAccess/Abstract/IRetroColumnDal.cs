using ScrumPoker.Dto;

namespace ScrumPoker.DataAccess.Abstract;

public interface IRetroColumnDal
{
    Task<Guid> Create(CreateRetroColumnDto request);
    Task<List<RetroColumnDto>> GetList(Guid retroId);
    Task<bool> Delete(Guid id, Guid retroId);
}