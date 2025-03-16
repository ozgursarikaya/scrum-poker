using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using MimeKit;
using ScrumPoker.Business.Abstract;
using ScrumPoker.Dto;
using ScrumPoker.Localization;

namespace ScrumPoker.Business.Concrete
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettingsModelDto _emailSettings;
        private readonly IConfiguration _configuration;
        private readonly ILoggerService _nLogService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public EmailService(IOptions<EmailSettingsModelDto> emailSettings, IConfiguration configuration, ILoggerService nLogService, IStringLocalizer<SharedResource> localizer)
        {
            _emailSettings = emailSettings.Value;
            _configuration = configuration;
            _nLogService = nLogService;
            _localizer = localizer;
        }

        public async Task<bool> Send(EmailSendModelDto model)
        {
            var result = false;

            try
            {
                string domainUrl = _configuration.GetSection("AppSettings:DomainUrl").Value!;
                string templateBody = _localizer[model.TemplateName];
                string body = templateBody.Replace("{BODY}", model.Body);
                body = body.Replace("{DomainUrl}", domainUrl);

                var builder = new BodyBuilder();
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_emailSettings.SmtpUser));

                InternetAddressList mailAddresList = new InternetAddressList();
                List<string> mails = model.Email.Replace(",", ";").Split(";").ToList();
                foreach (var item in mails)
                {
                    mailAddresList.Add(MailboxAddress.Parse(item));
                }
                email.To.AddRange(mailAddresList);

                if (!string.IsNullOrEmpty(model.BccEmails))
                {
                    InternetAddressList bccMailAddresList = new InternetAddressList();
                    List<string> bccMails = model.BccEmails.Replace(",", ";").Split(";").ToList();
                    foreach (var item in bccMails)
                    {
                        bccMailAddresList.Add(MailboxAddress.Parse(item));
                    }
                    email.Bcc.AddRange(bccMailAddresList);
                }

                email.Subject = model.Subject;
                builder.HtmlBody = body;

                var smtp = new SmtpClient();
                if (_emailSettings.SmtpEnableSSL)
                {
                    smtp.Connect(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.StartTls);
                }
                else
                {
                    smtp.Connect(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.None);
                }

                if (!string.IsNullOrEmpty(_emailSettings.SmtpPass))
                {
                    smtp.Authenticate(_emailSettings.SmtpUser, _emailSettings.SmtpPass);
                }
                else
                {
                    smtp.AuthenticationMechanisms.Remove("XOAUTH2");
                    smtp.AuthenticationMechanisms.Remove("LOGIN");
                }

                foreach (var attachment in model.AttachmentFileUrlList)
                {
                    builder.Attachments.Add(attachment);
                }

                email.Body = builder.ToMessageBody();
                await smtp.SendAsync(email);
                smtp.Disconnect(true);

                result = true;
            }
            catch (Exception ex)
            {
                _nLogService.LogError("Mail gönderilirken bir hata oluştu.", ex);
            }

            return result;
        }
    }
}
