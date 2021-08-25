using System;
using System.Text;

namespace Aero.Cake.Extensions
{
    public static class ExceptionExtensions
    {
        public static string ToLogString(this Exception exception, int level = 1)
        {
            var sb = new StringBuilder($"ExMsg{level}: ");

            sb.Append(string.IsNullOrWhiteSpace(exception.Message) ? "None" : exception.Message);

            var nextLogString = exception.InnerException?.ToLogString(level + 1);
            if (!string.IsNullOrWhiteSpace(nextLogString))
            {
                sb.Append($", {nextLogString}");
            }

            return sb.ToString();
        }
    }
}
