using ScrumPoker.Dto;

namespace ScrumPoker.Business.Abstract;

public interface IRetroColumnService
{
    Task<Guid> Create(CreateRetroColumnDto request);
    Task<List<RetroColumnDto>> GetList(Guid retroId);
}