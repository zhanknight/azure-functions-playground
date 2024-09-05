using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;

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
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "post")]
        HttpRequest req, [FromBody] TemperatureData data)
        {
            if (!Validator(data))
            {
                return new BadRequestObjectResult("Invalid data");
            }
            else
            {
                return new OkObjectResult("Data validated and accepted for processing!");
            }
        }

        private bool Validator(TemperatureData data)
        {
           bool isValid = true;

            if (data.DeviceId < 0 | data.DeviceId > 9)
            {
                isValid = false;
            }
            if (data.Time > DateTime.Now)
            {
                isValid = false;
            }
            if (data.Temperature < 0.0 | data.Temperature > .90)
            {
                isValid = false;
            }

            return isValid;
        }

        public class TemperatureData
        {
            public int DeviceId { get; set; }
            public DateTime Time { get; set; }
            public double Temperature { get; set; }
        }
    }
}
