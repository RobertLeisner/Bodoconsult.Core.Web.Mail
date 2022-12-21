using System.Net.Mail;
using Bodoconsult.Core.Web.Mail.Test.Helpers;
using NUnit.Framework;

namespace Bodoconsult.Core.Web.Mail.Test
{
    [TestFixture]
    public class UnitTestSmtpMailer
    {
        [Test]
        public void TestSendMailPlainMail()
        {

            var account = TestHelper.GetTestMailAccount();

            var smtp = new SmtpMailer(account);
             smtp.Init();

            smtp.SendMail(TestHelper.GetTestReceiver(), "Testmail", "dgdgdgdgdgs sfsgdgs sshshsh");

            Assert.IsTrue(true);

        }


        [Test]
        public void TestSendMailMessage()
        {

            var account = TestHelper.GetTestMailAccount();


            // New message as HTML mail
            var msg = new MailMessage {IsBodyHtml = true};

            // From
            var add = new MailAddress(account.MailAddressSender);
            msg.From = add;

            // To
            add = new MailAddress(TestHelper.GetTestReceiver());
            msg.To.Add(add);

            // Subject and mail body
            msg.Subject = "Bodoconsult.Core.Web.Mail: test mail";
            msg.Body = "<p>ajHA SADad asd AS Ddad</p>";

            // Send the mail
            var smtp = new SmtpMailer(account);
            smtp.Init();

            smtp.SendMail(msg);

            Assert.IsTrue(true);

        }
    }
}