using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using Bodoconsult.Core.Web.Mail.Model;

namespace Bodoconsult.Core.Web.Mail
{
    /// <summary>
    /// Send a mail to a lot of receivers
    /// </summary>
    public sealed class MassSmtpMailer: BaseMailer
    {


        public MassSmtpMailer(MailAccount currentMailAccount)
        {
            To = new List<MailReceiver>();
            DefaultSalutation = "Sehr geehrte Damen und Herren";
            CurrentMailAccount = currentMailAccount;
        }


        /// <summary>
        /// HTML-Body für eMails
        /// </summary>
        public string Body { get; set; }


        /// <summary>
        /// Subject for the mail
        /// </summary>
        public string Subject { get; set; }


        /// <summary>
        /// Sending mail address
        /// </summary>
        public string From { get; set; }


        /// <summary>
        /// Default salutation tu use: default is "Sehr geehrte Damen und Herren"
        /// </summary>
        public string DefaultSalutation { get; set; }

        /// <summary>
        /// All mail addresses the mail will be sent to
        /// </summary>
        public IList<MailReceiver> To { get; set; }

        /// <summary>
        /// Contains all images in the document as <see cref="LinkedResource"/>
        /// </summary>
        public IList<LinkedResource> LinkedResources { get; set; }

        /// <summary>
        /// Send mail to all mail addresses registered in <see cref="To"/>
        /// </summary>
        public void SendMails()
        {

            var smtpClient = new SmtpClient(CurrentMailAccount.SmtpServer)
            {
                Credentials = new NetworkCredential(CurrentMailAccount.SmtpAccountName, CurrentMailAccount.SmtpPassword),
                EnableSsl = CurrentMailAccount.UseSecureConnection
            };

            // 1. Send emails to receivers with no salutation

            if (To.Any(x => string.IsNullOrEmpty(x.Salutation)))
            {
                // build the message to send
                var msg = new MailMessage { From = new MailAddress(From) };


                foreach (var mailReciever in To.Where(x => string.IsNullOrEmpty(x.Salutation)))
                {
                    msg.Bcc.Add(mailReciever.EmailAddress);
                }

                msg.Subject = Subject;
                msg.IsBodyHtml = true;
                msg.BodyEncoding = Encoding.UTF8;

                //string txtBody = "See this email online here: " + messageURL; 
                //AlternateView plainView = AlternateView.CreateAlternateViewFromString(txtBody, null, "text/plain"); 

                var htmlView = AlternateView.CreateAlternateViewFromString(Body.Replace("??address??", DefaultSalutation), null, "text/html");
                foreach (var linkedResource in LinkedResources) htmlView.LinkedResources.Add(linkedResource);

                msg.AlternateViews.Add(htmlView);


                // Send the mail
                smtpClient.Send(msg);

            }
            


            // 2. Send emails to receivers with salutation

            foreach (var mailReciever1 in To.Where(x => !string.IsNullOrEmpty(x.Salutation)))
            {
                var msg = new MailMessage
                {
                    From = new MailAddress(From),
                    Subject = Subject,
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8
                };

                msg.Bcc.Add(mailReciever1.EmailAddress);

                //string txtBody = "See this email online here: " + messageURL; 
                //AlternateView plainView = AlternateView.CreateAlternateViewFromString(txtBody, null, "text/plain"); 

                var htmlView = AlternateView.CreateAlternateViewFromString(Body.Replace("??address??", mailReciever1.Salutation), null, "text/html");
                foreach (var linkedResource in LinkedResources) htmlView.LinkedResources.Add(linkedResource);

                msg.AlternateViews.Add(htmlView);

                // Send the mail
                smtpClient.Send(msg);
                
            }

            smtpClient.Dispose();
        }
    }
}
