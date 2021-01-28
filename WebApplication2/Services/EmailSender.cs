using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Services
{
    public class EmailSender : IEmailSender
    {
        readonly IConfiguration _configuration;
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string email, string subject, string text)
        {
            var message = new Message
            {
                Subject = subject,
                Body = new ItemBody
                {
                    ContentType = BodyType.Html,
                    Content = text
                },
                ToRecipients = new List<Recipient>()
                {
                    new Recipient{EmailAddress= new EmailAddress{Address = email}}
                }
            };
            string[] scopes = new string[] { "https://graph.microsoft.com/.default" };
            IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
                .Create(_configuration["EmailSender:ClientId"])
                .WithTenantId(_configuration["EmailSender:TenantId"])
                .WithClientSecret(_configuration["EmailSender:ClientSecret"])
                .Build();
            await confidentialClientApplication.AcquireTokenForClient(scopes)
                .ExecuteAsync()
                .ConfigureAwait(false);
            var authProvider = new ClientCredentialProvider(confidentialClientApplication);
            var graphClient = new GraphServiceClient(authProvider);
            await graphClient.Users[_configuration["EmailSender:UserId"]]
                .SendMail(message, false)
                .Request()
                .PostAsync();
        }
    }
}
