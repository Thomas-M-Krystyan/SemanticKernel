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
            var clientResult = Exercise_1_ChatGpt4o.Run(azureClient);

            // Deployments
            var chatGpt4o = azureClient.GetChatClient("gpt-4o");
            var imageDallE3 = azureClient.GetImageClient("dall-e-3");

            // Example #1:
            // var result = chatGpt4o.CompleteChat("How are you?");

            // Example #2:
            var requestOptions = new ChatCompletionOptions
            {
                MaxOutputTokenCount = 4096,
                Temperature = 1.0f,
                TopP = 1.0f
            };

            var messages = new List<ChatMessage>
            {
                new SystemChatMessage("You are a tourist guide giving a short and concise tips in a bullet-list type of answers"),
                new UserChatMessage("I am going to Madrid for a couple of years. What should I see there?")
            };

            var result = chatGpt4o.CompleteChat(messages, requestOptions);

            // Statistics
            Console.WriteLine($"Input token count: {result.Value.Usage.InputTokenCount}");
            Console.WriteLine($"Output token count: {result.Value.Usage.OutputTokenCount}");
            Console.WriteLine($"Total token count: {result.Value.Usage.TotalTokenCount}");

            // Answer
            Console.WriteLine($"{Environment.NewLine}Answer ({result.Value.Content[0].Kind}):" +
                              $"{Environment.NewLine}{result.Value.Content[0].Text}");
            
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
