using System;
using System.IO;
using System.Threading.Tasks;
using Aero.Infrastructure;
using Newtonsoft.Json.Linq;

namespace Aero.Azure.ArmTemplate
{
    public interface ITemplateBuilder
    {
        Task<JObject> LoadArmDeployParamatersAsync(string path);
        Task<JObject> LoadArmTemplateAsync(string path);
    }

    public class TemplateBuilder : ITemplateBuilder
    {
        private readonly IAeroLogger _logger;

        public TemplateBuilder(IAeroLogger<TemplateBuilder> logger)
        {
            _logger = logger;
        }

        public Task<JObject> LoadArmDeployParamatersAsync(string path)
        {
            try
            {
                return LoadFileAsync(path);
            }
            catch (Exception ex)
            {
                _logger.Error($"Failed. Path: {path}, ExMessage: {ex.Message}", ex);
                throw;
            }
        }

        public Task<JObject> LoadArmTemplateAsync(string path)
        {
            try
            {
                return LoadFileAsync(path);
            }
            catch (Exception ex)
            {
                _logger.Error($"Failed. Path: {path}, ExMessage: {ex.Message}", ex);
                throw;
            }
        }

        private async Task<JObject> LoadFileAsync(string path)
        {
            using (var reader = new StreamReader(path))
            {
                return JObject.Parse(await reader.ReadToEndAsync());
            }
        }
    }
}
