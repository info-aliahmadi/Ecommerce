using Hydra.Infrastructure.Notification.Sms.Interface;
using Hydra.Infrastructure.Notification.Sms.Models;
using Hydra.Infrastructure.Notification.Sms.Setting;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Hydra.Infrastructure.Notification.Sms.Service
{
    public class SmsService : ISmsService
    {
        private readonly ISmsSetting _smsSetting;

        public SmsService(ISmsSetting smsSetting)
        {
            _smsSetting = smsSetting;
        }

        public bool IsEnabled()
        {
            return _smsSetting.IsEnabled;
        }

        public List<SmsMessage> ReceiveSms()
        {
            throw new NotImplementedException();
        }

        public void Send(SmsMessage smsMessage)
        {
            TwilioClient.Init(_smsSetting.AccountSid, _smsSetting.AuthToken);
            foreach (var number in smsMessage.ToNumbers)
            {
                MessageResource.Create(
                    body: smsMessage.Text,
                    from: new PhoneNumber(_smsSetting.FromNumber),
                    to: new PhoneNumber(number)
                );
            }
        }
    }
}
