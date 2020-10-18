using System;
using TF.Basis.Logger;

namespace Corporate.Plataforms.Settings.Client
{
    public class LoggerManager : IDisposable
    {
        private static LoggerManager _instance;

        /// <summary>
        /// Singleton
        /// </summary>
        internal static LoggerManager Instance
        {
            get
            {
                return LoggerManager._instance ?? (LoggerManager._instance = new LoggerManager());
            }
        }


        public LoggerManager()
        {
           // MyLogger.Inicializar();
        }

        public void LogEvent(string message)
        {
           // MyLogger.WriteText("Hub", new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, message);
        }

        public void LogError(string message)
        {
            //MyLogger.WriteTextError("HubError", new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, message);
        }

        public void LogError(Exception ex)
        {
           // MyLogger.WriteTextException("HubError", new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, ex);
        }

        public void Dispose()
        {
           // MyLogger.Finalizar();
        }
    }
}
