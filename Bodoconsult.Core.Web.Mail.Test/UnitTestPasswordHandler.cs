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
                Tenant = "F/9NSz5BmzPjSUpRrLM9wcKu1NmwWSdR8SuVPyIfrk38E2BdLVgDv+28Z8tWJcRnSIRnMP8TAeDuZddJS+N0pnqc01s7L4PzpjOZ4LbKMII=",
                Instance = "mZqrt2SMI5qC5SHZgN4H8H1SC4w6ZJ5IvRbtXbif1wHvv/TsHGfwED0xyZL3JfRzpANnV2Og8WDKHPmwIONr9zjIrNqrEKh+E5ryrFlWCiM=",
                Scope = "mZqrt2SMI5qC5SHZgN4H8Pl5xFwFcdDz0SNOQapGrOEZhuEBuVhsem/GOpp4iQ774y+cHye7RHysJ1aZfLhrOo033UG0KRVtEPpx/6VfVsQ=",
                ClientId = "EuXih/XyadFXuM7Sy64/c0VMF+iNEYIiMQD3AErwDWya5RVmQiKVXdPYR8XcHFW1N2cx3I/Ew8UthbHnUrBGki7QdF5Pu21yGjenRhU9+7A=",
                ClientSecret = "Q+x1I8jq3Yd6n/mANjOQ+VGBSRwKbpFttHVfjQA/Wt85ku4tgBM5FSIW9r5MoRBwXVNXwmthOdtzlJlZ8oms68Fzdau952LuZrYxeKj/uJDrc5uBrjZBwdSfOnKdJnbM",
                UserName = "1dHEZnNSGo+o3gWlMT1ygybJrVIwzWEGlvAbQldjcRHuCNcOa73i+6o0nEO1Y2lYPRYhdGkjqU3sICcgnv0HRLfUFxFPvawFqwgikw7qT9I=",

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