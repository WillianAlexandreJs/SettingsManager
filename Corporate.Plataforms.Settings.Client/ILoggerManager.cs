using System;

namespace Corporate.Plataforms.Settings.Client
{
    public interface ILoggerManager
    {
        void LogEvent(string message);

        void LogError(string message);

        void LogError(string message, Exception ex);
        
    }
}