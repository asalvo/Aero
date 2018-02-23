using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aero.Azure.Management.Authentication;
using Aero.Infrastructure;
using Microsoft.Rest.Azure;

namespace Aero.Azure.Management
{
    public interface IManagementService
    {
        void Initialize(AzureCredentials credentials);
    }

    public abstract class AbstractManagementService<TClient> : IManagementService
    {
        protected AbstractManagementService(IAeroLogger logger)
        {
            Logger = logger;
        }


        protected TClient Client { get; set; }

        protected IAeroLogger Logger { get; }

        public async Task<T[]> GetPagedData<T>(Func<Task<IPage<T>>> firstCall, Func<string, Task<IPage<T>>> nextCall)
        {
            var list = new List<T>();
            var pageCount = 0;

            //First Call
            Logger.Debug($"Type: {typeof(T).Name}, Page: {pageCount}");
            var pageResult = await firstCall();
            list.AddRange(pageResult);
            
            //Loop while we have paging links
            while (!string.IsNullOrWhiteSpace(pageResult.NextPageLink))
            {
                Logger.Debug($"Type: {typeof(T).Name}, Page: {pageCount}");
                pageResult = await nextCall(pageResult.NextPageLink);
                list.AddRange(pageResult);
            }

            return list.ToArray();
        }

        public abstract void Initialize(AzureCredentials credentials);
    }
}
