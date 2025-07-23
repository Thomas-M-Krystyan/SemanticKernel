using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Configuration;
using OpenAI;
using OpenAI.Chat;
using SemanticKernel.Exercises;
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

            // Exercises:
            var result = Exercise_1_ChatGpt4o.Run(azureClient);

            // Deployments
            var imageDallE3 = azureClient.GetImageClient("dall-e-3");

            // Statistics
            Console.WriteLine($"Input token count: {result.Usage.InputTokenCount}");
            Console.WriteLine($"Output token count: {result.Usage.OutputTokenCount}");
            Console.WriteLine($"Total token count: {result.Usage.TotalTokenCount}");

            // Answer
            Console.WriteLine($"{Environment.NewLine}Answer ({result.Content[0].Kind}):" +
                              $"{Environment.NewLine}{result.Content[0].Text}");
            
            Console.ReadKey();
        }

        private static IConfigurationRoot ConfigureApp()
        {
            return new ConfigurationBuilder()
                .AddUserSecrets<Program>() // Loads secrets.json for this project
                .Build();
        }
    }
}
