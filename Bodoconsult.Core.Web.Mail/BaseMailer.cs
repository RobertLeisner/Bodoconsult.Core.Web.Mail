using System;
using Bodoconsult.Core.Web.Mail.Model;

namespace Bodoconsult.Core.Web.Mail
{
    /// <summary>
    /// Base class for simple and mass mailers <see cref="MassSmtpMailer" /> and <see cref="SmtpMailer"/>/>
    /// </summary>
    public abstract class BaseMailer: IDisposable
    {

        /// <summary>
        /// Current mail account to use
        /// </summary>
        public MailAccount CurrentMailAccount { get; set; }


        /// <summary>
        /// Dispos all needed objects
        /// </summary>
        public virtual void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
