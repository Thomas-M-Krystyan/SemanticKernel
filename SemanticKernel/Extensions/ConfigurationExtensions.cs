using Microsoft.Extensions.Configuration;

namespace SemanticKernel.Extensions
{
    internal static class ConfigurationExtensions
    {
        internal static (Uri Endpoint, string ApiKey) GetSecret_AzureOpenAI(this IConfigurationRoot configuration)
        {
            var gpt4o = configuration.GetSection("AzureOpenAI");

            var endpoint = new Uri(gpt4o["Endpoint"]!);
            var apiKey = gpt4o["ApiKey"]!;

            return (endpoint, apiKey);
        }
    }
}