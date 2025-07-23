using Azure;
using Azure.AI.OpenAI;
using OpenAI.Chat;

namespace SemanticKernel
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var endpoint = new Uri("https://thoma-mdepts9j-swedencentral.cognitiveservices.azure.com/");
            var model = "gpt-4o";
            var deploymentName = "gpt-4o";
            var apiKey = "<your-api-key>";

            AzureOpenAIClient azureClient = new(
                endpoint,
                new AzureKeyCredential(apiKey));
            ChatClient chatClient = azureClient.GetChatClient(deploymentName);
        }
    }
}
