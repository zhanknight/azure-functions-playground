using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;

namespace azure_functions_playground
{
    public partial class PlaygroundHttpFunction
    {
        private readonly ILogger<PlaygroundHttpFunction> _logger;

        public PlaygroundHttpFunction(ILogger<PlaygroundHttpFunction> logger)
        {
            _logger = logger;
        }

        [Function(nameof(PlaygroundHttpFunction))]
        [ServiceBusOutput("incomingdataqueue", Connection = "ServiceBusConnection")]
        public string Run([HttpTrigger(AuthorizationLevel.Function, "post")]
        HttpRequest req, [FromBody] TemperatureData data)
        {
            string? OutputMessage = null;
            
            if (!Validator(data))
            {
                return OutputMessage;
            }
            else
            {
                OutputMessage = JsonSerializer.Serialize(data);
                _logger.LogInformation("Sending data to message queue!");
                return OutputMessage;
            }
        }

        private bool Validator(TemperatureData data)
        {
           bool isValid = true;

            if (data.DeviceId < 0 | data.DeviceId > 9)
            {
                isValid = false;
            }
            if (data.TimeStamp > DateTime.Now)
            {
                isValid = false;
            }
            if (data.Temperature < 0.1 | data.Temperature > .90)
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
