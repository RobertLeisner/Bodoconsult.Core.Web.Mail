// Copyright (c) Bodoconsult EDV-Dienstleistungen GmbH. All rights reserved.


using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Bodoconsult.Core.Web.Mail.Helpers;
using Bodoconsult.Core.Web.Mail.Model;
using NUnit.Framework;

namespace Bodoconsult.Core.Web.Mail.Test.Helpers
{
    public static class TestHelper
    {
        private static string _testDataPath;

        private static readonly string _secretsPath = "c:\\Daten\\Projekte\\_work\\Data\\";

        public static string TempPath = @"c:\temp\";

        private static PwdKeys pwdKeys;


        static TestHelper()
        {
            var fileName = Path.Combine(_secretsPath, "mail.json");

            pwdKeys = JsonHelper.LoadJsonFile<PwdKeys>(fileName);

            PasswordHandler.Key1 = pwdKeys.Key1;
            PasswordHandler.Key2 = pwdKeys.Key2;
            PasswordHandler.Key3 = pwdKeys.Key3;
            PasswordHandler.Salt = pwdKeys.Salt;
        }


        public static string TestDataPath
        {
            get
            {

                if (!string.IsNullOrEmpty(_testDataPath))
                {
                    return _testDataPath;
                }

                var path = new DirectoryInfo(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName).Parent.Parent.Parent.FullName;

                _testDataPath = Path.Combine(path, "TestData");

                if (!Directory.Exists(_testDataPath))
                {
                    Directory.CreateDirectory(_testDataPath);
                }

                return _testDataPath;
            }
        }

        /// <summary>
        /// Start an app by file name
        /// </summary>
        /// <param name="fileName"></param>
        public static void StartFile(string fileName)
        {

            if (!Debugger.IsAttached)
            {
                return;
            }

            Assert.IsTrue(File.Exists(fileName));

            var p = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    UseShellExecute = true, 
                    FileName = fileName
                }
            };

            p.Start();

        }


        /// <summary>
        /// Get a test mail account. Adjust path to your current situation
        /// </summary>
        /// <returns></returns>
        public static MailAccount GetTestMailAccount()
        {

            var fileName = Path.Combine(_secretsPath, "BodoWebMailer.json");

            var account = JsonHelper.LoadJsonFile<MailAccount>(fileName);

            return account;
        }




        /// <summary>
        /// Get a test mail receiver. Adjust path to your current situation
        /// </summary>
        /// <returns></returns>
        public static string GetTestReceiver()
        {
            var fileName = Path.Combine(_secretsPath, "TestMailReceiver.txt");

            var account = File.ReadAllText(fileName);

            return account;
        }

        public static O365MailAccount GetTestO365Account()
        {
            var fileName = Path.Combine(_secretsPath, "O365Mailer.json");

            var account = JsonHelper.LoadJsonFile<O365MailAccount>(fileName);

            var o365 = new O365MailAccount();

            o365.Tenant = PasswordHandler.Decrypt(account.Tenant);
            o365.Instance = PasswordHandler.Decrypt(account.Instance);
            o365.ClientId = PasswordHandler.Decrypt(account.ClientId);
            o365.ClientSecret = PasswordHandler.Decrypt(account.ClientSecret);
            o365.Scope = PasswordHandler.Decrypt(account.Scope);
            o365.UserName = PasswordHandler.Decrypt(account.UserName);

            return o365;
        }
    }
}
