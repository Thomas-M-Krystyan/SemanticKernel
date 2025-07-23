using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Configuration;

namespace SemanticKernel
{
    internal class Program
    {
        private static void Main(string[] _)
        {
            // TODO: Differentiate between environments to use local settings or Azure Key Vault

            // Load "secrets.json" configuration
            var configuration = ConfigureApp();

            // Azure OpenAI Client
            var azureOpenAI = GetSecret_AzureOpenAI(configuration);

            var azureClient = new AzureOpenAIClient(
                azureOpenAI.Endpoint,
                new AzureKeyCredential(azureOpenAI.ApiKey));

            // Deployments
            var chatGpt4o = azureClient.GetChatClient("gpt-4o");
        }

        private static IConfigurationRoot ConfigureApp()
        {
            return new ConfigurationBuilder()
                .AddUserSecrets<Program>() // Loads secrets.json for this project
                .Build();
        }

        private static (Uri Endpoint, string ApiKey) GetSecret_AzureOpenAI(IConfigurationRoot configuration)
        {
            var gpt4o = configuration.GetSection("AzureOpenAI");

            var endpoint = new Uri(gpt4o["Endpoint"]!);
            var apiKey = gpt4o["ApiKey"]!;

            return (endpoint, apiKey);
        }
    }
}
