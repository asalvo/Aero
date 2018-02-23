using System.Data.SqlClient;
using System.Reflection;
using Aero.Infrastructure;
using Cake.Core;
using Cake.Core.Diagnostics;
using DbUp;
using DbUp.Engine.Output;

namespace Aero.Cake.Services
{
    public interface IDbUpService
    {
        void  Upgrade(string server, string database, string username, string password);
    }

    public class DbUpService : AbstractService, IDbUpService
    {
        public DbUpService(ICakeContext cakeContext, IAeroLogger<DbUpService> logger) : base(cakeContext, logger)
        {
        }

        public void Upgrade(string server, string database, string username, string password)
        {
            var csb = CreateSqlConnectionStringBuilder(server, database, username, password);
            
            var usernameAudit = string.IsNullOrWhiteSpace(username) ? "IntegratedAuth" : username;
            CakeContext.Log.Information($"Server: {csb.DataSource}, Database: {csb.InitialCatalog}, Username: {usernameAudit}");

            var upgrader = DeployChanges.To
                .SqlDatabase(csb.ConnectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogTo(new CakeDbUpLogger(CakeContext))
                .Build();

            upgrader.PerformUpgrade();
        }

        private SqlConnectionStringBuilder CreateSqlConnectionStringBuilder(string server, string database, string username, string password)
        {
            

            var csb = new SqlConnectionStringBuilder
            {
                DataSource = server,
                InitialCatalog = database,
                ApplicationName = "StormEx.Backend.Build"
            };

            if (string.IsNullOrWhiteSpace(username))
            {
                csb.IntegratedSecurity = true;
            }
            else
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
