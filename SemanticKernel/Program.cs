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

            // Deployments
            var (deploymentName, endpoint, apiKey) = GetDeployment_Gpt4o(configuration);

            // Azure OpenAI Client
            var azureClient = new AzureOpenAIClient(endpoint, new AzureKeyCredential(apiKey));

            // Chat Client
            var chatClient = azureClient.GetChatClient(deploymentName);
        }

        private static IConfigurationRoot ConfigureApp()
        {
            return new ConfigurationBuilder()
                .AddUserSecrets<Program>() // Loads secrets.json for this project
                .Build();
        }

        private static (string Name, Uri Endpoint, string ApiKey) GetDeployment_Gpt4o(IConfigurationRoot configuration)
        {
            var gpt4o = configuration.GetSection("AzureOpenAI:Deployments:GPT-4o");

            var deploymentName = gpt4o["DeploymentName"]!;
            var endpoint = new Uri(gpt4o["Endpoint"]!);
            var apiKey = gpt4o["ApiKey"]!;

            return (deploymentName, endpoint, apiKey);
        }
    }
}
