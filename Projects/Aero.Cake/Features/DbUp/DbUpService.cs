using System;
using System.Data.SqlClient;
using System.Reflection;
using Aero.Cake.Services;
using Cake.Common.Diagnostics;
using Cake.Core;
using Cake.Core.Diagnostics;
using DbUp;
using DbUp.Engine.Output;

namespace Aero.Cake.Features.DbUp
{
    public interface IDbUpService
    {
        void Upgrade(string server, string database, string username, string password, string tenantId = null);
    }

    public class DbUpService : AbstractService, IDbUpService
    {
        public DbUpService(AeroContext aeroContext) : base(aeroContext)
        {
        }

        public void Upgrade(string server, string database, string username, string password, string tenantId = null)
        {
            var csb = CreateSqlConnectionStringBuilder(server, database, username, password, tenantId);
            var connectionString = csb.ConnectionString;

            var usernameAudit = string.IsNullOrWhiteSpace(username) ? "IntegratedAuth" : username;
            AeroContext.Information($"Server: {csb.DataSource}, Database: {csb.InitialCatalog}, Username: {usernameAudit}");

            var useAzureSecurity = false;
            if (!string.IsNullOrWhiteSpace(tenantId))
            {
                //https://docs.microsoft.com/en-us/azure/key-vault/service-to-service-authentication#running-the-application-using-managed-identity
                useAzureSecurity = true;
                Environment.SetEnvironmentVariable("AzureServicesAuthConnectionString", $"RunAs=App;AppId={username};TenantId={tenantId};AppKey={password}");
            }

            var upgrade = DeployChanges.To
                .SqlDatabase(connectionString, null, useAzureSecurity)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogTo(new CakeDbUpLogger(AeroContext))
                .Build();

            var result = upgrade.PerformUpgrade();
            if (!result.Successful)
            {
                throw new Exception("Failed to updated database");
            }
        }

        private SqlConnectionStringBuilder CreateSqlConnectionStringBuilder(string server, string database, string username, string password, string tenantId)
        {
            var csb = new SqlConnectionStringBuilder
            {
                DataSource = server,
                InitialCatalog = database,
            };

            //If no username then assume integrated security
            if (string.IsNullOrWhiteSpace(username))
            {
                csb.IntegratedSecurity = true;
            }
            //If there is a username but no tenantId, then assume SQL authentication
            else if (string.IsNullOrWhiteSpace(tenantId))
            {
                csb.UserID = username;
                csb.Password = password;
            }

            return csb;
        }

        private class CakeDbUpLogger : IUpgradeLog
        {
            private readonly ICakeContext _cakeContext;

            public CakeDbUpLogger(ICakeContext cakeContext)
            {
                _cakeContext = cakeContext;
            }

            public void WriteError(string format, params object[] args)
            {
                _cakeContext.Log.Error(format, args);
            }

            public void WriteInformation(string format, params object[] args)
            {
                _cakeContext.Log.Information(format, args);
            }

            public void WriteWarning(string format, params object[] args)
            {
                _cakeContext.Log.Warning(format, args);
            }
        }
    }
}
