using ScrumPoker.Dto;

namespace ScrumPoker.Business.Abstract
{
    public interface IEmailService
    {
        Task<bool> Send(EmailSendModelDto model);
    }
}
