using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace azure_functions_playground
{
    public class PlaygroundHttpFunction
    {
        private readonly ILogger<PlaygroundHttpFunction> _logger;

        public PlaygroundHttpFunction(ILogger<PlaygroundHttpFunction> logger)
        {
            _logger = logger;
        }

        [Function("PlaygroundHttpFunction")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
