using System;
using System.Runtime.CompilerServices;
using Aero.Infrastructure;
using Cake.Core;
using Cake.Core.Diagnostics;

namespace Aero.Cake
{
    public class AeroCakeLogger<T> : AeroCakeLogger, IAeroLogger<T>
    {
        public AeroCakeLogger(ICakeContext cakeContext) : base(cakeContext, typeof(T))
        {

        }
    }

    public class AeroCakeLogger : IAeroLogger
    {
        private readonly ICakeContext _cakeContext;
        //private readonly ILogger _log;
        private readonly Type _type;
        private readonly string _typeName;

        public AeroCakeLogger(ICakeContext cakeContext, Type type)
        {
            _cakeContext = cakeContext;
            _type = type;
            _typeName = _type.Name;
        }



        private string CreateMessage(string message, Exception ex = null, string memberName = "X")
        {
            //TODO: Use CakeContext to figure out if we are running on VSTS or Team City
            var addTimeStamp = true;

            var exMessage = string.Empty;
            if (ex != null)
            {
                exMessage = $"ExMessage: {ex.Message}";
            }

            if (addTimeStamp)
            {
                return $"[{DateTime.UtcNow:HH:mm:ss}] [{_typeName}.{memberName}] {message} {exMessage}";
            }
            else
            {
                return $"[{_typeName}.{memberName}] {message} {exMessage}";
            }
        }

        public void Debug(string message, Exception ex = null, [CallerMemberName] string memberName = "X")
        {
            var fullMessage = CreateMessage(message, ex, memberName);
            _cakeContext.Log.Debug(fullMessage);
            //_log.LogDebug(ex, fullMessage);
        }

        public void Error(string message, Exception ex = null, [CallerMemberName] string memberName = "X")
        {
            var fullMessage = CreateMessage(message, ex, memberName);
            _cakeContext.Log.Error(fullMessage);
            // _log.LogError(ex, fullMessage);
        }

        public void Fatal(string message, Exception ex = null, [CallerMemberName] string memberName = "X")
        {
            var fullMessage = CreateMessage(message, ex, memberName);
            _cakeContext.Log.Error(fullMessage);
            // _log.LogCritical(ex, fullMessage);
        }

        public void Info(string message, Exception ex = null, [CallerMemberName] string memberName = "X")
        {
            var fullMessage = CreateMessage(message, ex, memberName);
            _cakeContext.Log.Information(fullMessage);
            // _log.LogInformation(ex, fullMessage);
        }

        public void Warn(string message, Exception ex = null, [CallerMemberName] string memberName = "X")
        {
            var fullMessage = CreateMessage(message, ex, memberName);
            _cakeContext.Log.Warning(fullMessage);
            // _log.LogWarning(new EventId(), ex, fullMessage);
        }
    }
}
