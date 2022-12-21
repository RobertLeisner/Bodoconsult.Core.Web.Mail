// Copyright (c) Bodoconsult EDV-Dienstleistungen GmbH. All rights reserved.

using Bodoconsult.Core.Web.Mail.Model;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using System.Linq;
using System.Net.Http.Headers;

namespace Bodoconsult.Core.Web.Mail
{
    /// <summary>
    /// Implementation of an Office365 based mailer
    /// </summary>
    public class O365Mailer
    {

        // Even if this is a console application here, a daemon application is a confidential client application
        GraphServiceClient _app;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="mailAccount"></param>
        public O365Mailer(O365MailAccount mailAccount)
        {
            MailAccount = mailAccount;
        }

        /// <summary>
        /// Current O365 mail account
        /// </summary>
        public O365MailAccount MailAccount  { get;  }


        /// <summary>
        /// Login to O365 Graph API
        /// </summary>
        /// <returns>Awaitable task</returns>
        public void Login()
        {

            try
            {
                var scopes = new[] { "https://graph.microsoft.com/.default" };
                var tenantId = MailAccount.Tenant;

                // Configure the MSAL client as a confidential client
                var confidentialClient = ConfidentialClientApplicationBuilder
                    .Create(MailAccount.ClientId)
                    .WithAuthority($"https://login.microsoftonline.com/{tenantId}/v2.0")
                    .WithClientSecret(MailAccount.ClientSecret)
                    .Build();

                // Build the Microsoft Graph client. As the authentication provider, set an async lambda
                // which uses the MSAL client to obtain an app-only access token to Microsoft Graph,
                // and inserts this access token in the Authorization header of each API request. 

                _app = new GraphServiceClient(new DelegateAuthenticationProvider(async (requestMessage) =>
                {

                    // Retrieve an access token for Microsoft Graph (gets a fresh token if needed).
                    var authResult = await confidentialClient
                        .AcquireTokenForClient(scopes)
                        .ExecuteAsync();

                    // Add the access token in the Authorization header of the API request.
                    requestMessage.Headers.Authorization =
                        new AuthenticationHeaderValue("Bearer", authResult.AccessToken);
                }));

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Send an email over an O365 account
        /// </summary>
        /// <param name="to">Mail receiver separated by semmicolon</param>
        /// <param name="subject">Mail subject</param>
        /// <param name="content">Mail content with full HTML markup for a webpage</param>
        public void SendMail(string to, string subject, string content)
        {

            // Define a simple e-mail message.
            var message = new Message
            {
                Subject = subject,
                Body = new ItemBody
                {
                    ContentType = BodyType.Html,
                    Content = content
                },
            };

            var receips = to.Split(new[] { ';' }).Select(receiver => new Recipient { EmailAddress = new EmailAddress { Address = receiver } }).ToList();

            message.ToRecipients = receips;

            // Send mail as the given user. 
            SendMail(message);

        }

        /// <summary>
        /// Send a mail message via O365
        /// </summary>
        /// <param name="message">Message to be sent</param>
        public void SendMail(Message message)
        {
            // Send mail as the given user. 
            _app.Users[MailAccount.UserName]
                .SendMail(message, true)
                .Request()
                .PostAsync().Wait();
        }

    }
}