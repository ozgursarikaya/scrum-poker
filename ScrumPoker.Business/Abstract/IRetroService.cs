using ScrumPoker.Dto;

namespace ScrumPoker.Business.Abstract;

public interface IRetroService
{
    Task<Guid> Create(CreateRetroDto request);
}