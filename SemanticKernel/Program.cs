using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Configuration;
using OpenAI;
using SemanticKernel.Extensions;

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
            var azureOpenAI = configuration.GetSecret_AzureOpenAI();

            OpenAIClient azureClient = new AzureOpenAIClient(
                azureOpenAI.Endpoint,
                new AzureKeyCredential(azureOpenAI.ApiKey));

            // Deployments
            var chatGpt4o = azureClient.GetChatClient("gpt-4o");
            var imageDallE3 = azureClient.GetImageClient("dall-e-3");
        }

        private static IConfigurationRoot ConfigureApp()
        {
            return new ConfigurationBuilder()
                .AddUserSecrets<Program>() // Loads secrets.json for this project
                .Build();
        }
    }
}
