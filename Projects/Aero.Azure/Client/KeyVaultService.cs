using System.Threading.Tasks;
using Aero.Infrastructure;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Aero.Azure.Client
{
    public class KeyVaultService : AbstractClientService
    {
        private readonly KeyVaultClient _client;
        private string _clientId;
        private string _clientSecret;
        private string _vaultUrl;

        public KeyVaultService(IAeroLogger<KeyVaultService> logger) : base(logger)
        {

            _client = new KeyVaultClient(KeyValueAuthentication);
        }

        public void Initialize(string clientId, string clientSecret, string vaultName)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _vaultUrl = $"https://{vaultName}.vault.azure.net";
        }

        private async Task<string> KeyValueAuthentication(string authority, string resource, string scope)
        {
            Logger.Info($"ClientId: {_clientId}, Authority: {authority}, Resource: {resource}, Scope: {scope}");

            var clientCredential = new ClientCredential(_clientId, _clientSecret);

            var context = new AuthenticationContext(authority, TokenCache.DefaultShared);

            var result = await context.AcquireTokenAsync(resource, clientCredential);

            return result.AccessToken;
        }

        public async Task<CertificateBundle> GetCertificate(string certificateName)
        {
            return await _client.GetCertificateAsync(_vaultUrl, certificateName);
        }

        public async Task<string> GetSecret(string secretName)
        {
            var secret = await _client.GetSecretAsync(_vaultUrl, secretName);
            return secret.Value;
        }

        public async Task<string> SetSecret(string secretName, string value)
        {
            var secret = await _client.SetSecretAsync(_vaultUrl, secretName, value);
            return secret.Value;
        }
    }
}
