using Azure;
using Azure.AI.Inference;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
    .AddUserSecrets<Program>();

var configuration = builder.Build();


var endpoint = "https://models.inference.ai.azure.com";

// If running locally, make sure to add GITHUB_TOKEN value to the user secrets.
// If you're in codespaces, it'll be taken care of for you.


string token = configuration["GITHUB_TOKEN"] ??
    Environment.GetEnvironmentVariable("GITHUB_TOKEN") ??
    throw new InvalidOperationException("Make sure to add GITHUB_TOKEN value to the user secrets or environment variables."); 

/*var client = new ChatCompletionsClient(new Uri(endpoint), new AzureKeyCredential(token));
var chatHistory = new List<ChatRequestMessage>
{
    new ChatRequestSystemMessage("You are a helpful assistant that knows about AI")
};

while (true)
{
    Console.Write("You: ");
    var userMessage = Console.ReadLine();

    // Exit loop
    if (userMessage.StartsWith("/q"))
    {
        break;
    }

    chatHistory.Add(new ChatRequestUserMessage(userMessage));
    var options = new ChatCompletionsOptions(chatHistory)
    {
        Model = "Phi-3-medium-4k-instruct"
    };

    ChatCompletions? response = await client.CompleteAsync(options);
    //ChatResponseMessage? assistantMessage = response.Choices.First().Message;
    var content = response.Content;

    chatHistory.Add(new ChatRequestAssistantMessage(content));

    Console.WriteLine($"Assistant: {content}");
}*/


// this works!!

// These variables are needed to access the GitHub Models
AzureKeyCredential credential = new(token);
Uri modelEndpoint = new("https://models.inference.ai.azure.com");
string modelName = "DeepSeek-R1";

IChatClient chatClient = new ChatCompletionsClient(modelEndpoint, credential)
    .AsChatClient(modelName);

string question = "If I have 3 apples and eat 2, how many bananas do I have?";

Console.WriteLine($">>> User: {question}");
Console.Write(">>>");
Console.WriteLine(">>> DeepSeek (might be a while): ");


ChatCompletion? response = await chatClient.CompleteAsync(question);

Console.WriteLine(response.Message);
//await foreach (var item in response)
//{
//    Console.Write(item);
//}
