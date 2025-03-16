using Microsoft.AspNetCore.Http;
using NLog;
using ScrumPoker.Business.Abstract;

namespace ScrumPoker.Logger.Concrete
{
    public class NLogService : ILoggerService
    {
        private static readonly NLog.Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NLogService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void LogInfo(string message, int? userId = null)
        {
            using (SetLogContext(userId))
            {
                _logger.Info(message);
            }
        }

        public void LogWarning(string message, int? userId = null)
        {
            using (SetLogContext(userId))
            {
                _logger.Warn(message);
            }
        }

        public void LogError(string message, Exception ex, int? userId = null)
        {
            using (SetLogContext(userId))
            {
                _logger.Error(ex, message);
            }
        }

        private IDisposable? SetLogContext(int? userId)
        {
            var scopeProperties = new List<IDisposable>();

            if (userId.HasValue)
            {
                scopeProperties.Add(ScopeContext.PushProperty("UserId", userId.Value.ToString()));
            }

            string? ipAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
            if (!string.IsNullOrEmpty(ipAddress))
            {
                scopeProperties.Add(ScopeContext.PushProperty("IpAddress", ipAddress));
            }

            return new ScopeContextDisposable(scopeProperties);
        }

        private class ScopeContextDisposable : IDisposable
        {
            private readonly List<IDisposable> _disposables;

            public ScopeContextDisposable(List<IDisposable> disposables)
            {
                _disposables = disposables;
            }

            public void Dispose()
            {
                _disposables.ForEach(d => d.Dispose());
            }
        }
    }

}
