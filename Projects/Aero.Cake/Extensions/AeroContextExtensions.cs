using Cake.Common.Diagnostics;
using System;
using System.Threading.Tasks;

namespace Aero.Cake.Extensions
{
    public static class AeroContextExtensions
    {
        public static void Retry(this AeroContext aeroContext, int numberOfRetries, TimeSpan timeBetweenRetries, Action<int> action)
        {
            var executionCount = 0;
            bool hasError;

            do
            {
                hasError = false;

                try
                {
                    executionCount++;
                    action.Invoke(executionCount);
                }
                catch (Exception ex)
                {
                    hasError = true;
                    aeroContext.Warning($"RetryExecution. Action: Exception, ExecutionCount: {executionCount}, ExMsg: {ex.Message}");
                    if (executionCount > numberOfRetries)
                    {
                        //Could be logged by the caller, otherwise it's output as an uncaught exception
                        throw;
                    }
                    System.Threading.Thread.Sleep(timeBetweenRetries);
                }

            } while (executionCount <= numberOfRetries && hasError);
        }

        /// <summary>
        /// Execute a task which must complete in under a specified timeout period
        /// </summary>
        /// <param name="aeroContext"></param>
        /// <param name="action"></param>
        /// <param name="millisecondsTimeout"></param>
        /// <remarks>
        /// This code was adapted from https://devblogs.microsoft.com/pfxteam/crafting-a-task-timeoutafter-method/. It is
        /// a method of a self-completing action under a given time limit. It is expected that the action will loop
        /// within itself until its goal state is completed. One example is polling Azure for a particular status value.
        /// </remarks>
        public static async Task RetryUntilSuccessOrTimeout(this AeroContext aeroContext, Action action, int millisecondsTimeout)
        {
            var task = Task.Run(action);

            if (task == await Task.WhenAny(task, Task.Delay(millisecondsTimeout)))
                await task;
            else
                throw new TimeoutException();
        }
    }
}
