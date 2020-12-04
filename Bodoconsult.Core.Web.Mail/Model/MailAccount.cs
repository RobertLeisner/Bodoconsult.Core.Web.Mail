namespace Bodoconsult.Core.Web.Mail.Model
{
    /// <summary>
    /// Contains mail config for sending
    /// </summary>
    public sealed class MailAccount
    {
        private string _accountName;

        /// <summary>
        /// SMTP server (IP or fullyqualified DNS name) to use for sending mails.
        /// </summary>
        public string SmtpServer { get; set; }

        /// <summary>
        /// Account name to use, if not set, <see cref="MailAddressSender"/> is used for login
        /// </summary>
        public string SmtpAccountName
        {
            get { return string.IsNullOrEmpty(_accountName)? MailAddressSender: _accountName; }
            set { _accountName = value; }
        }

        /// <summary>
        /// Password to login on SMTP server with account <see cref="SmtpAccountName"/>
        /// </summary>
        public string SmtpPassword { get; set; }


        /// <summary>
        /// Mail address to use for sending 
        /// </summary>
        public string MailAddressSender { get; set; }


        /// <summary>
        /// Use secured connection via SSL
        /// </summary>
        public bool UseSecureConnection { get; set; }


    }
}
