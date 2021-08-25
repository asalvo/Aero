using System;
using System.Threading.Tasks;
using Aero.Cake.Extensions;
using Cake.Common.Diagnostics;

namespace Aero.Cake.Services
{
    public interface IRetryService
    {
        void Retry(int numberOfRetries, TimeSpan timeBetweenRetries, Action<int> action);

        Task RetryAsync(Func<Task> action, int millisecondsTimeout);
    }

    public class RetryService : AbstractService, IRetryService
    {
        public RetryService(IAeroContext myContext) : base(myContext)
        {

        }

        public void Retry(int numberOfRetries, TimeSpan timeBetweenRetries, Action<int> action)
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
                    AeroContext.Warning($"RetryExecution. Action: Exception, ExecutionCount: {executionCount}, {ex.ToLogString()}");
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
        /// <remarks>
        /// This code was adapted from https://devblogs.microsoft.com/pfxteam/crafting-a-task-timeoutafter-method/. It is
        /// a method of a self-completing action under a given time limit. It is expected that the action will loop
        /// within itself until its goal state is completed. One example is polling Azure for a particular status value.
        /// </remarks>
        public async Task RetryAsync(Func<Task> action, int millisecondsTimeout)
        {
            var task = Task.Run(action);

            if (task == await Task.WhenAny(task, Task.Delay(millisecondsTimeout)).ConfigureAwait(false))
            {
                await task.ConfigureAwait(false);
            }
            else
            {
                throw new TimeoutException();
            }
        }
    }
}
