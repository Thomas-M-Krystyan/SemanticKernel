using OpenAI;
using OpenAI.Chat;

namespace SemanticKernel.Exercises
{
    internal static class Exercise_1_ChatGpt4o
    {
        internal static async Task<ChatCompletion> RunAsync(OpenAIClient openAiClient)
        {
            // Resolve deployment
            var chatGpt4o = openAiClient.GetChatClient("gpt-4o");

            // Example #1: Simple chat completion
            var result = await chatGpt4o.CompleteChatAsync("How are you?");

            return result;
        }
    }
}