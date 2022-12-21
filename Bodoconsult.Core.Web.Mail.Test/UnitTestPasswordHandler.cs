// Copyright (c) Bodoconsult EDV-Dienstleistungen GmbH. All rights reserved.


using Bodoconsult.Core.Web.Mail.Helpers;
using NUnit.Framework;
using System.Diagnostics;
using Bodoconsult.Core.Web.Mail.Model;
using Bodoconsult.Core.Web.Mail.Test.Helpers;

namespace Bodoconsult.Core.Web.Mail.Test
{
    public class UnitTestPasswordHandler
    {

        [Test]
        [Explicit]
        public void Test3()
        {
            var pwdKey = new O365MailAccount()
            {
                Tenant = "",
                Instance = "",
                Scope = "",
                ClientId = "",
                ClientSecret = "",
                UserName = "",

            };

            JsonHelper.SaveAsFile(@"C:\temp\O365Mailer.json", pwdKey);

            Assert.IsTrue(true);
        }


        [Test]
        [Explicit]
        public void Test1()
        {
            var pwdKey = new PwdKeys
            {
                Key1 = "abc",
                Key2 = "def",
                Key3 = "ghi",
                Salt = new byte[] { 0x46, 0x76, 0x62, 0x6e, 0x21, 0x4d, 0x66, 0x63, 0x76, 0x65, 0x64, 0x65, 0x76 }

            };

            JsonHelper.SaveAsFile(@"C:\temp\pwd.json", pwdKey);

            Assert.IsTrue(true);
        }

        [Test]
        public void TestEncrypt()
        {
            // Arrange 
            var s = "TestBahnhof123";

            // Act  
            var result1 = PasswordHandler.Encrypt(s);

            // Assert
            Assert.IsNotNull(result1);
            Assert.AreNotEqual(s, result1);

        }


        [Test]
        public void TestDecrypt()
        {
            // Arrange 
            var s = "TestBahnhof123";

            // Act  
            var result1 = PasswordHandler.Encrypt(s);

            // Assert
            Assert.IsNotNull(result1);
            Assert.AreNotEqual(s, result1);

            // Act
            var result2 = PasswordHandler.Decrypt(result1);

            Assert.That(result2, Is.EqualTo(s));

        }


        [Explicit]
        [Test]
        public void Test2()
        {

            var s = "ff6d0f41-99fe-4597-8c32-3be31580e954";

            var x = TestHelper.TempPath;
            Assert.IsNotNull(s);

            s = PasswordHandler.Encrypt(s);

            Assert.IsFalse(string.IsNullOrEmpty(s));

            Debug.Print(s);

            //var s = new AppSettings
            //{
            //    ConnectionString =
            //        "Data Source=192.168.10.125;Initial Catalog=BodoFileTransfer;Integrated Security=SSPI;",
            //    ErrorMailer = "test@bodoconsult.de",
            //};

            //JsonHelper.SaveAsFile(@"D:\temp\appSettings.json", s);

            //Assert.IsTrue(true);
        }
    }
}