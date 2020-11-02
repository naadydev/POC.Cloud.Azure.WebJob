using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace BIM.Domain.Reports.PDF.WebJob
{
    class Program
    {
        
        static async Task Main()
        {
            // ---------------------------
            //IConfiguration configuration = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json", true, true)
            //    .Build();
            //var serviceBusConnectionStr = configuration["ServiceBus:ConnectionString"];
            // --------------------------
            var builder = new HostBuilder();
            // builder.UseEnvironment("development");
            builder.ConfigureLogging((context, b) => { b.AddConsole(); });

            builder.ConfigureWebJobs(b =>
            {
                b.AddExecutionContextBinding();

                #region ServiceBus
                b.AddServiceBus();
                // OR with Some Configuration >>>>> b.AddServiceBus(sb =>
                //{
                //    //sb.ConnectionString = "";
                //   // sb.MessageHandlerOptions.AutoComplete = true;
                //    //sb.MessageHandlerOptions.MaxConcurrentCalls = 16;
                //}); 
                #endregion

                #region Storage Service
                //b.AddAzureStorageCoreServices();
                //b.AddAzureStorage(); 
                #endregion

                #region SendGrid
                //b.AddSendGrid(a =>
                //{
                //    a.FromAddress.Email = "samples@functions.com";
                //    a.FromAddress.Name = "Azure Functions";
                //}); 
                #endregion

            });

            #region DI
            // builder
            //   .ConfigureServices(services =>
            // {
            //     // add some sample services to demonstrate job class DI
            //     services.AddSingleton<ISampleServiceA, SampleServiceA>();
            //     services.AddSingleton<ISampleServiceB, SampleServiceB>();
            // })
            #endregion

            #region Host
            var host = builder.Build();
            using (host)
            {
                #region Manually triggering the function
                //var jobHost = host.Services.GetService(typeof(IJobHost)) as JobHost;
                //var inputs = new Dictionary<string, object> { { "value", "Hello world!" } };
                //await host.StartAsync();
                //await jobHost.CallAsync("CreateQueueMessage", inputs);
                //await host.StopAsync(); 
                #endregion
                // ---------
                await host.RunAsync();
            }
            #endregion
        }

    }


}
