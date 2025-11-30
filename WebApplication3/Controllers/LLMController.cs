using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.AI;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LLMController : ControllerBase
    {
        [HttpGet(Name = "GetRequestLLM")]
        public async Task<ChatResponse> Get()
        {
            RequestLLM llm = new RequestLLM();

            //return await llm.imageRecognition();
            return await llm.simpleAsk("Do I need an umbrella?");
        }

    }
}
