using BookSale.Managament.Domain.Setting;
using Microsoft.Extensions.Configuration;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _senderEmail;
        private readonly string _senderName;
        private readonly string _key;

        public EmailService(IConfiguration configuration)
        {
            var config = configuration.GetSection("EmailBrevo");

            _senderEmail = config["Sender:Email"] ?? string.Empty;
            _senderName = config["Sender:Name"] ?? string.Empty;
            _key = config["Key"] ?? string.Empty;

            if (!Configuration.Default.ApiKey.ContainsKey("api-key"))
            {
                Configuration.Default.ApiKey.Add("api-key", _key);
            }

        }

        public async Task<bool> Send(EmailSetting emailSetting)
        {
            try
            {
                var transaction = new TransactionalEmailsApi();

                var sender = new SendSmtpEmailSender(_senderName, _senderEmail);
                var to = new List<SendSmtpEmailTo> {
                    new SendSmtpEmailTo(emailSetting.To, emailSetting.Name)
                };

                string body = emailSetting.Content;

                List<SendSmtpEmailCc> lsCC = null;

                if (emailSetting.CC.Any())
                {
                    lsCC = new();
                    foreach (var cc in emailSetting.CC)
                    {
                        lsCC.Add(new SendSmtpEmailCc { Email = cc });
                    }
                }

                List<SendSmtpEmailAttachment> lsAttachment = null;

                if (emailSetting.AttachmentFiles.Any())
                {
                    lsAttachment = new();

                    foreach (var pathFile in emailSetting.AttachmentFiles)
                    {
                        lsAttachment.Add(new SendSmtpEmailAttachment { Url = pathFile });
                    }
                }

                var sendEmail = new SendSmtpEmail(sender, to, null, lsCC, body, null, emailSetting.Subject, null, lsAttachment);

                CreateSmtpEmail result = await transaction.SendTransacEmailAsync(sendEmail);

                return result is not null ? true : false;

            }
            catch
            {
                return false;
            }




        }
    }
}
