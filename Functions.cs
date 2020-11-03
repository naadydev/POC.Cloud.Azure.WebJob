using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BIM.Domain.Reports.PDF.WebJob
{
    public class Functions
    {        
        private readonly IConfiguration _configuration;
        public Functions(IConfiguration configuration)
        {
            _configuration = configuration;
           var val = _configuration["ServiceBus:ConnectionString"];
        }
        
        // [Timeout("00:00:15")]
        // [Singleton]
        public async Task ProcessQueueMessage([ServiceBusTrigger(
            queueName: "%ServiceBus:QueueName%", 
            Connection = "ServiceBus:ConnectionString")
            ] Message  queueMessage,
            ExecutionContext executionContext,
            string messageId,
            int deliveryCount,
            ILogger logger)
        {
            logger.LogInformation($"Processing ServiceBus message (Id={messageId}, DeliveryCount={deliveryCount})");
            await Task.Delay(1000);
            logger.LogInformation($"Message complete (Id={messageId})");
        }
        

    }
}

// Notes :
// To use other trigger and binding types, install the NuGet package that contains them and call the Add<binding> extension method implemented in the extension. For example, if you want to use an Azure Cosmos DB binding, install Microsoft.Azure.WebJobs.Extensions.CosmosDB and call AddCosmosDB
// To use the Timer trigger or the Files binding, which are part of core services, call the AddTimers or AddFiles extension methods, respectively.
// https://docs.microsoft.com/en-us/azure/app-service/webjobs-sdk-how-to
