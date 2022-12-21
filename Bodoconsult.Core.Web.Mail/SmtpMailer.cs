using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using Bodoconsult.Core.Web.Mail.Model;

namespace Bodoconsult.Core.Web.Mail
{
    /// <summary>
    /// Send an email via SMTP unsecured or via SSL secured
    /// </summary>
    public sealed class SmtpMailer: BaseMailer
    {



        public SmtpMailer(MailAccount currentMailAccount)
        {
            CurrentMailAccount = currentMailAccount;
            if (string.IsNullOrEmpty(CurrentMailAccount.MailAddressSender)) CurrentMailAccount.MailAddressSender = CurrentMailAccount.SmtpAccountName;
        }



        private SmtpClient _smtpClient;

        /// <summary>
        /// Initialize SMTP client before sending a mail
        /// </summary>
        public void Init()
        {
            ServicePointManager.ServerCertificateValidationCallback =
                (sender, certificate, chain, sslPolicyErrors) => true;

            if (string.IsNullOrEmpty(CurrentMailAccount.SmtpAccountName))
            {
                _smtpClient = new SmtpClient(CurrentMailAccount.SmtpServer)
                {
                   Credentials = CredentialCache.DefaultNetworkCredentials,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = CurrentMailAccount.UseSecureConnection,
                    
                };

                
            }
            else
            {
                _smtpClient = new SmtpClient(CurrentMailAccount.SmtpServer)
                {
                    Credentials = new NetworkCredential(CurrentMailAccount.SmtpAccountName, CurrentMailAccount.SmtpPassword),
                    EnableSsl = CurrentMailAccount.UseSecureConnection
                };
            }

        }

        /// <summary>
        /// Send an email based on an HTML body string
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body">HTML encoded text to send as mail</param>
        public void SendMail(string to, string subject, string body)
        {

            try
            {
                var msg = new MailMessage
                    {
                        From = new MailAddress(CurrentMailAccount.MailAddressSender),
                        Subject = subject,
                        BodyEncoding = Encoding.UTF8,
                        IsBodyHtml = true,
                        Body = body
                    };
                msg.To.Add(to);

                _smtpClient.Send(msg);
            }
            catch (Exception ex)
            {
                throw new Exception("Smtp mailing error", ex);
            }

        }

        private static string StripHtml(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }

        /// <summary>
        /// Send mail based on a <see cref="MailMessage"/> object
        /// </summary>
        /// <param name="message"></param>
        public void SendMail(MailMessage message)
        {

           

            try
            {
                _smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                throw new Exception("Smtp mailing error", ex);
            }

        }

        /// <summary>
        /// Dispose smtp client
        /// </summary>
        public override void Dispose()
        {
            try
            {
                _smtpClient.Dispose();
            }
            catch
            {
                // ignored
            }
        }




    }
}