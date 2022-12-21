using System;
using System.IO;
using Bodoconsult.Core.Web.Mail.Test.Helpers;
using NUnit.Framework;

namespace Bodoconsult.Core.Web.Mail.Test
{
    [TestFixture]
    public class UnitTestMassSmtpMailer
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        //private readonly string _appPath;

        private string _baseUrl;

        private readonly string _docUrl;

        public UnitTestMassSmtpMailer()
        {
            
            _baseUrl = Path.Combine(TestHelper.TestDataPath, @"TestData\HtmlLocalData\");
            _docUrl = Path.Combine(TestHelper.TestDataPath, @"TestData\HtmlLocalData\Sample.txt");
        }






        [Test]
        public void TestMassSmtpMailer()
        {

            var c = new HtmlToMailConverter { DocUrl = _docUrl };
            c.LoadDocument();
            c.FindImages();
            c.GetLinkedRessources();
            c.ProcessContent();


            var account = TestHelper.GetTestMailAccount();


            var m = new MassSmtpMailer(account)
            {
                From = "noreply@bodoconsult.de",
                Subject = $"Testmail {DateTime.Now:s}",
                Body = c.Content,
                LinkedResources = c.LinkedResources
            };


            m.To.Add(new MailReceiver { EmailAddress = "robert.leisner@bodoconsult.de" });
            m.To.Add(new MailReceiver { EmailAddress = "info@bodoconsult.de" });
            m.To.Add(new MailReceiver { EmailAddress = "support@bodoconsult.de" });

            m.SendMails();
        }


        //[Test]
        //public void TestMassSmtpMailerIqplus()
        //{

        //    var c = new HtmlToMailConverter { DocUrl = _docUrl };
        //    c.LoadDocument();
        //    c.FindImages();
        //    c.GetLinkedRessources();
        //    c.ProcessContent();


        //    var m = new MassSmtpMailer
        //    {
        //        SmtpServer = "smtp.strato.de",
        //        SmtpPassword = "Bahnhof_2016!",
        //        SmtpAccount = "office@iqplus.international",
        //        From = "office@iqplus.international",
        //        UseSecureConnection = true,
        //        Subject = string.Format("Testmail {0:s}", DateTime.Now),
        //        Body = c.Content,
        //        LinkedResources = c.LinkedResources
        //    };


        //    m.To.Add(new MailReceiver {EmailAddress = "robert.leisner@bodoconsult.de"});
        //    m.To.Add(new MailReceiver { EmailAddress = "info@bodoconsult.de" });
        //    m.To.Add(new MailReceiver { EmailAddress = "rleisner@iqplus.international" });

        //    m.SendMails();
        //}

    }
}