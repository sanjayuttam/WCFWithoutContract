using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace TimeServiceClient
{
    /// <summary>
    /// Enables contractless calls to services
    /// </summary>
    public class ChannelFactoryHelper
    {
        /// <summary>
        /// Executes an asynchronous service call without requiring a contract reference
        /// </summary>
        /// <param name="requestMessage">The System.ServiceMode.Channels.Message request object to pass to the service being called</param>
        /// <param name="endpointConfigurationName">The WCF client endpoint name, from the config file</param>
        /// <param name="method">The client service method to call</param>
        /// <returns></returns>
        public static async Task<Message> ExecuteServiceAsync(Message requestMessage, string endpointConfigurationName, string method)
        {
            var task = Task.Factory.StartNew(() => ExecuteService(requestMessage, endpointConfigurationName, method));
            var result = await task;
            return result;
        }

        /// <summary>
        /// Executes a synchronous service call without requiring a contract reference
        /// </summary>
        /// <param name="requestMessage">The System.ServiceMode.Channels.Message request object to pass to the service being called</param>
        /// <param name="endpointConfigurationName">The WCF client endpoint name, from the config file</param>
        /// <param name="method">The client service method to call</param>
        /// <returns></returns>
        public static Message ExecuteService(Message requestMessage, string endpointConfigurationName, string method)
        {
            var factory = new ChannelFactory<IOrchestration>(endpointConfigurationName);
            var defaultServiceAddress = factory.Endpoint.Address.ToString();

            if (Equals(requestMessage.Version, MessageVersion.None))
            {
                defaultServiceAddress = string.Concat(defaultServiceAddress, "/", method);
            }

            factory.Endpoint.Address = new EndpointAddress(defaultServiceAddress);
            requestMessage.Headers.To = new Uri(defaultServiceAddress);

            IOrchestration _client = null;
            Message response = null;

            try
            {
                _client = factory.CreateChannel();
                response = _client.BeginProcessRequest(requestMessage);
            }
            finally
            {
                if (_client != null)
                {
                    var channel = (IClientChannel)_client;
                    if (channel.State != CommunicationState.Closed)
                    {
                        try
                        {
                            channel.Close();
                        }
                        catch
                        {
                            channel.Abort();
                        }
                    }
                }
            }

            return response;
        }
    }
}
