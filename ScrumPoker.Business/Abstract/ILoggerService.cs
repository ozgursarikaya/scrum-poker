namespace ScrumPoker.Business.Abstract
{
    public interface ILoggerService
    {
        void LogInfo(string message, int? userId = null);
        void LogWarning(string message, int? userId = null);
        void LogError(string message, Exception ex, int? userId = null);
    }
}
