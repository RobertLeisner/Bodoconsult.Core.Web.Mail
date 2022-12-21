using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Bodoconsult.Core.Web.Mail.Model;
using NUnit.Framework;

namespace Bodoconsult.Core.Web.Mail.Test.Helpers
{
    public static class TestHelper
    {
        private static string _testDataPath;

        public static string TempPath = @"d:\temp\";

        public static string TestDataPath
        {
            get
            {

                if (!string.IsNullOrEmpty(_testDataPath)) return _testDataPath;

                var path = new DirectoryInfo(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName).Parent.Parent.Parent.FullName;

                _testDataPath = Path.Combine(path, "TestData");

                if (!Directory.Exists(_testDataPath)) Directory.CreateDirectory(_testDataPath);

                return _testDataPath;
            }
        }

        /// <summary>
        /// Start an app by file name
        /// </summary>
        /// <param name="fileName"></param>
        public static void StartFile(string fileName)
        {

            if (!Debugger.IsAttached) return;

            Assert.IsTrue(File.Exists(fileName));

            var p = new Process { StartInfo = new ProcessStartInfo { UseShellExecute = true, FileName = fileName } };

            p.Start();

        }


        /// <summary>
        /// Get a test mail account. Adjust path to your current situation
        /// </summary>
        /// <returns></returns>
        public static MailAccount GetTestMailAccount()
        {

            const string fileName = @"D:\Daten\Projekte\_work\TestMailAccount.json";

            var account = JsonHelper.LoadJsonFile<MailAccount>(fileName);

            return account;
        }

        /// <summary>
        /// Get a test mail receiver. Adjust path to your current situation
        /// </summary>
        /// <returns></returns>
        public static string GetTestReceiver()
        {
            const string fileName = @"D:\Daten\Projekte\_work\TestMailReceiver.txt";

            var account = File.ReadAllText(fileName);

            return account;
        }
    }
}
