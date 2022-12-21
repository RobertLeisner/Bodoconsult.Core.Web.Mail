// Copyright (c) Bodoconsult EDV-Dienstleistungen GmbH. All rights reserved.


namespace Bodoconsult.Core.Web.Mail
{
    /// <summary>
    /// Class with mail receiver data
    /// </summary>
    public sealed class MailReceiver
    {
        /// <summary>
        /// Email address for the mail receiver
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Saluation address for the mail receiver in the mail (place holder ??address??)
        /// </summary>
        public string Salutation  { get; set; }

    }
}
