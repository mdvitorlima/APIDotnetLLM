using Microsoft.Extensions.AI;
using System.ComponentModel;
using System.Diagnostics;

namespace WebApplication3
{
    public class RequestLLM
    {

        public async Task<ChatResponse> imageRecognition()
        {
            [Description("Gets the weather")]
            string GetWeather() => Random.Shared.NextDouble() > 0.5 ? "It's sunny" : "It's raining";

            var chatOptions = new ChatOptions
            {
                Tools = [AIFunctionFactory.Create(GetWeather)]
            };

            var endpoint = "http://localhost:11434/";
            var modelId = "llava:7b";

            IChatClient client = new OllamaChatClient(endpoint, modelId: modelId)
                .AsBuilder()
                .UseFunctionInvocation()
                .Build();

            var message = new ChatMessage(ChatRole.User, "What´s in this image?");
            message.Contents.Add(new DataContent(System.IO.File.ReadAllBytes("images/image2.jpg"), "image/jpg"));

            Stopwatch timePerParse;
            long ticksThisTime = 0;

            timePerParse = Stopwatch.StartNew();

            var response = await client.GetResponseAsync(message);

            timePerParse.Stop();
            ticksThisTime = timePerParse.ElapsedTicks;

            return response;

        }

        public async Task<ChatResponse> simpleAsk(String question)
        {
            //[Description("Gets the weather")]
            //string GetWeather() => Random.Shared.NextDouble() > 0.5 ? "It's sunny" : "It's raining";

            //var chatOptions = new ChatOptions
            //{
            //    Tools = [AIFunctionFactory.Create(GetWeather)]
            //};

            var endpoint = "http://localhost:11434/";
            var modelId = "llama3.1";

            IChatClient client = new OllamaChatClient(endpoint, modelId: modelId)
                .AsBuilder()
                .UseFunctionInvocation()
                .Build();

            string userPrompt = question;

            return await client.GetResponseAsync(userPrompt);

        }


    }
}
