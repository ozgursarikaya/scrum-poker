using ScrumPoker.Dto;

namespace ScrumPoker.DataAccess.Abstract;

public interface IRetroDal
{
    Task<Guid> Create(CreateRetroDto request);
}