using System;
using System.Runtime.CompilerServices;
using Aero.Azure.Management.Authentication;
using Aero.Infrastructure;
using Microsoft.Rest.ClientRuntime.Azure.TestFramework;
using NSubstitute;

namespace Aero.Azure.Management
{
    public class MockManagementContext : IDisposable
    {
        public MockContext MockContext { get; private set; }
        public static MockManagementContext Start(string className, [CallerMemberName] string methodName = "testframework_failed")
        {
            var mock = new MockManagementContext
            {
                AzureCredentialFactory = new TestAzureCredentialsFactory(),
                Logger = Substitute.For<IAeroLogger>(),
                MockContext = MockContext.Start(className, methodName)
            };

            return mock;
        }

        public void Dispose()
        {
            MockContext.Dispose();
        }

        public IAzureCredentialFactory AzureCredentialFactory { get; private set; }
        public IAeroLogger Logger { get; private set; }
    }
}
