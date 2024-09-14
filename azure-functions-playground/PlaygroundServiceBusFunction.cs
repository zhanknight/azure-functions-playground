using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace azure_functions_playground;

public partial class PlaygroundServiceBusFunction
{

    private readonly ILogger<PlaygroundServiceBusFunction> _logger;

    public PlaygroundServiceBusFunction(ILogger<PlaygroundServiceBusFunction> logger)
    {
        _logger = logger;
    }

    [Function(nameof(PlaygroundServiceBusFunction))]
    public async Task Run([ServiceBusTrigger("incomingdataqueue", Connection = "ServiceBusConnection", AutoCompleteMessages = false)] 
    ServiceBusReceivedMessage msg, ServiceBusMessageActions actns) 
    {
        try 
        {
            TemperatureData data = msg.Body.ToObjectFromJson<TemperatureData>();
            _logger.LogInformation($"Triggered by a message from the queue! Temperature data is: {data.Temperature}");

            // get average temp from previous readings and add new data, report average?

            await actns.CompleteMessageAsync(msg);           
        }
        catch 
        {
            _logger.LogError($"Message {msg.SequenceNumber} is not in the correct format, discarding!");
            await actns.DeferMessageAsync(msg);
        }
    }

}
