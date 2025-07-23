using OpenAI;
using OpenAI.Chat;

namespace SemanticKernel.Exercises
{
    internal static class Exercise_2_ChatGpt4o
    {
        internal static async Task<ChatCompletion> RunAsync(OpenAIClient openAiClient)
        {
            // Resolve deployment
            var chatGpt4o = openAiClient.GetChatClient("gpt-4o");

            // Example #2: Chat completion with options and messages
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

            var result = await chatGpt4o.CompleteChatAsync(messages, requestOptions);

            return result;
        }
    }
}