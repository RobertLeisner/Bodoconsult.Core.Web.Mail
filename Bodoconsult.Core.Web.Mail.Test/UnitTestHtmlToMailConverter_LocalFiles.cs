using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.Mail;
using Bodoconsult.Core.Web.Mail.Model;
using Bodoconsult.Core.Web.Mail.Test.Helpers;
using NUnit.Framework;

namespace Bodoconsult.Core.Web.Mail.Test
{
    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class UnitTestHtmlToMailConverter_LocalFiles
    {


        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        //private readonly string _appPath;

        private readonly string _baseUrl;

        private readonly string _docUrl;

        public UnitTestHtmlToMailConverter_LocalFiles()
        {
            //_appPath = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "");
            _baseUrl = Path.Combine(TestHelper.TestDataPath, @"TestData\HtmlLocalData\");
            _docUrl = Path.Combine(TestHelper.TestDataPath, @"TestData\HtmlLocalData\Sample.txt");
        }


        [Test]
        public void TestLoadDocUrlAndCheckbaseUrlAndLocalFile()
        {
            // Arrange
            // ReSharper disable once UseObjectOrCollectionInitializer
            var c = new HtmlToMailConverter();
            

            // Act
            c.DocUrl = _docUrl;

            // Assert
            Assert.IsTrue(c.BaseUrl==_baseUrl);
            Assert.IsTrue(c.LocalFile);

        }


        [Test]
        public void TestLoadDocument()
        {
            // Arrange
            var c = new HtmlToMailConverter {DocUrl = _docUrl};

            // Act
            c.LoadDocument();

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(c.Content));
            Assert.IsTrue(c.LocalFile);

        }


        [Test]
        public void TestFindImages()
        {
            // Arrange
            var c = new HtmlToMailConverter { DocUrl = _docUrl };
            c.LoadDocument();

            // Act
            c.FindImages();

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(c.Content));
            Assert.IsTrue(c.LocalFile);
            Assert.IsTrue(c.Images.Count>0);
            Assert.IsTrue(c.Images[0].Url == _baseUrl+@"logo.jpg");
        }


        [Test]
        public void TestGetLinkedRessources()
        {
            // Arrange
            var c = new HtmlToMailConverter { DocUrl = _docUrl };
            c.LoadDocument();
            c.FindImages();

            // Act
            
            c.GetLinkedRessources();

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(c.Content));
            Assert.IsTrue(c.LocalFile);
            Assert.IsTrue(c.Images.Count > 0);
            Assert.IsTrue(c.Images[0].Url == _baseUrl + @"logo.jpg");
            Assert.IsTrue(c.LinkedResources.Count>0);
        }

        [Test]
        public void TestProcessContent()
        {
            // Arrange
            var c = new HtmlToMailConverter { DocUrl = _docUrl };
            c.LoadDocument();
            c.FindImages();
            c.GetLinkedRessources();

            // Act
            c.ProcessContent();
            

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(c.Content));
            Assert.IsTrue(c.LocalFile);
            Assert.IsTrue(c.Images.Count > 0);
            Assert.IsTrue(c.Images[0].Url == _baseUrl + @"logo.jpg");
            Assert.IsTrue(c.LinkedResources.Count > 0);
            Assert.IsFalse(c.Content.Contains(".jpg"));
            Assert.IsTrue(c.Content.Contains("cid:"));
        }


        [Test]
        public void TestSaveToMail()
        {
            // Arrange
            var msg = new MailMessage {From = new MailAddress("noreply@bodoconsult.de")};
            msg.To.Add( "robert.leisner@bodoconsult.de");
            msg.Subject = $"Testmail {DateTime.Now:s}";

            var c = new HtmlToMailConverter { DocUrl = _docUrl };
            c.LoadDocument();
            c.FindImages();
            c.GetLinkedRessources();
            c.ProcessContent();

            var account = TestHelper.GetTestMailAccount();

            // Act
            c.SaveToMail(ref msg);


            var smtp = new SmtpMailer(account);

            smtp.Init();
            smtp.SendMail(msg);
            smtp.Dispose();

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(c.Content));
            Assert.IsTrue(c.LocalFile);
            Assert.IsTrue(c.Images.Count > 0);
            Assert.IsTrue(c.Images[0].Url == _baseUrl + @"logo.jpg");
            Assert.IsTrue(c.LinkedResources.Count > 0);
            Assert.IsFalse(c.Content.Contains(".jpg"));
            Assert.IsTrue(c.Content.Contains("cid:"));
            Assert.IsTrue(msg.AlternateViews.Count>0);
            Assert.IsTrue(msg.AlternateViews[0].LinkedResources.Count>0);
        }
       

    }
}
