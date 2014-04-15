using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace TimeServiceClient
{
    [ServiceContract(Namespace = "urn:ws:orchestration")]
    public interface IOrchestration
    {
        [OperationContract(AsyncPattern = false, Action = "*", ReplyAction = "*")]
        Message BeginProcessRequest(Message requestMessage);
    }
}
