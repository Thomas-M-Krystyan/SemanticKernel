using OpenAI;
using OpenAI.Chat;

namespace SemanticKernel.Exercises
{
    internal static class Exercise_1_ChatGpt4o
    {
        internal static ChatCompletion Run(OpenAIClient openAiClient)
        {
            // Resolve deployment
            var chatGpt4o = openAiClient.GetChatClient("gpt-4o");

            // Example #1: Simple chat completion
            var result = chatGpt4o.CompleteChat("How are you?");

            return result;
        }
    }
}