using SwinSchool.CommonShared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.MsmqIntegration;
using System.Web;

namespace SwinSchool.WebUI.Service
{
    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://SwinSchool.WebUI.Service")]
    public interface IUserAccountQueue
    {
        [OperationContract(IsOneWay = true, Action = "*")]
        void SendPasswordResetMessage(MsmqMessage<ResetPasswordRequestDto> msg);
    }

    public class UserAccountQueueClient : ClientBase<IUserAccountQueue>, IUserAccountQueue
    {
        public UserAccountQueueClient()
        {
        }

        public UserAccountQueueClient(string configurationName)
            :
                base(configurationName)
        {
        }

        public UserAccountQueueClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress address)
            :
                base(binding, address)
        {
        }

        public void SendPasswordResetMessage(MsmqMessage<ResetPasswordRequestDto> msg)
        {
            base.Channel.SendPasswordResetMessage(msg);
        }
    }
}