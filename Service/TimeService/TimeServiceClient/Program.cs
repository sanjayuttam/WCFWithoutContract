using System;
using System.Runtime.Serialization.Json;
using System.ServiceModel.Channels;
namespace TimeServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //Message message = Message.CreateMessage(
            //     MessageVersion.None,                                                       // No SOAP message version
            //                "*",                                                            // SOAP action, ignored since this is JSON
            //                null,                                                           // Message body
            //                new DataContractJsonSerializer(typeof(int)));                   // Specify DataContractJsonSerializer
            //message.Properties.Add(WebBodyFormatMessageProperty.Name,
            //new WebBodyFormatMessageProperty(WebContentFormat.Json));                       // Use JSON format

            //var response = ChannelFactoryHelper.ExecuteService(message, "service1-webhttp", "GetData");

            var message = Message.CreateMessage(MessageVersion.None, "*", "\"SomeData\" : {\"Data\" : \"stuff\" ");
            var response = ChannelFactoryHelper.ExecuteService(message, "service1-webhttp", "GetMoreData");

            Console.WriteLine("Response from web-service: {0}", response);
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}
