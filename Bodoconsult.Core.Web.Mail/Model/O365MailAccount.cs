// Copyright (c) Bodoconsult EDV-Dienstleistungen GmbH. All rights reserved.

namespace Bodoconsult.Core.Web.Mail.Model
{
    /// <summary>
    /// Represents an Office 365 app account used for mailing
    /// </summary>
    /// <remarks>The Office 365 app account requires Azure app permission Mail.Send</remarks>
    public class O365MailAccount
    {

        /// <summary>
        /// Current O365 instance
        /// </summary>
        public string Instance { get; set; }

        /// <summary>
        /// Current tenant
        /// </summary>
        public string Tenant { get; set; }

        /// <summary>
        /// Current client ID
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Current client secret
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// User account name: Email address or for Office365 shared mailboxes email addresse slash mailbox name (i.e. test@test/rechnung)
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The current O365 scope to use
        /// </summary>
        public string Scope { get; set; }

    }
}