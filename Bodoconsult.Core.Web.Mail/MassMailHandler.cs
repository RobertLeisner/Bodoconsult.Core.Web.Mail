using System;
using System.Collections.Generic;
using System.Data;
using Bodoconsult.Core.Web.Mail.Model;

namespace Bodoconsult.Core.Web.Mail
{
    public sealed class MassMailHandler
    {

        public MassMailHandler(MailAccount currentMailAccount)
        {
            MailReceivers = new List<MailReceiver>();
            CurrentMailAccount = currentMailAccount;
        }

        /// <summary>
        /// Contains mail config for sending
        /// </summary>
        public MailAccount CurrentMailAccount { get; }

        /// <summary>
        /// List of all MailReceivers
        /// </summary>
        public IList<MailReceiver> MailReceivers { get; set; }

        /// <summary>
        /// Subject for the mass mail
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Contains the converted mail text as master
        /// </summary>
        public HtmlToMailConverter MasterMailText { get; set; }


        /// <summary>
        /// Load mass mail config from Excel sheet (2 columns, email and saluation, at least)
        /// </summary>
        /// <param name="config"></param>
        public void LoadDataFromExcel(ExcelFileConfig config)
        {

            throw new NotImplementedException("Excel connection still to be done!");

            //var dataSet = new DataSet();
            //var da = new OleDbDataAdapter();

            //var connString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"", config.PhysicalPathOfDataSourceFile);
            //var query = string.Format("SELECT * FROM [{0}]", config.SheetName); // You can use any different queries to get the data from the excel sheet

            //var conn = new OleDbConnection(connString);
            //if (conn.State == ConnectionState.Closed) conn.Open();
            //try
            //{
            //    var cmd = new OleDbCommand(query, conn);
            //    da = new OleDbDataAdapter(cmd);
            //    da.Fill(dataSet);

            //}
            //catch
            //{
            //    // Exception Msg 

            //}
            //finally
            //{
            //    da.Dispose();
            //    conn.Close();
            //}


            //var dt = dataSet.Tables[0];


            //foreach (DataRow row in dt.Rows)
            //{

            //    var email = row[config.EmailAddressColumn].ToString();

            //    if (string.IsNullOrEmpty(email)) continue;

            //    var m = new MailReceiver
            //    {
            //        EmailAddress = email,
            //        Salutation = row[config.SalutationAddressColumn].ToString()
            //    };
            //    MailReceivers.Add(m);
            //}




        }


        /// <summary>
        /// Load a HTML file as mail text
        /// </summary>
        /// <param name="docUrl"></param>
        public void LoadHtmlMailText(string docUrl)
        {
            MasterMailText = new HtmlToMailConverter { DocUrl = docUrl };
            MasterMailText.LoadDocument();
            MasterMailText.FindImages();
            MasterMailText.GetLinkedRessources();
            MasterMailText.ProcessContent();
        }

        /// <summary>
        /// Send the emails out
        /// </summary>
        public void SendMails()
        {

            if (CurrentMailAccount == null) return;

            if (MailReceivers == null || MailReceivers.Count == 0) return;


            var m = new MassSmtpMailer(CurrentMailAccount)
            {
                From = CurrentMailAccount.MailAddressSender,

                Subject = Subject,
                Body = MasterMailText.Content,
                LinkedResources = MasterMailText.LinkedResources
            };

            foreach (var receiver in MailReceivers)
            {
                m.To.Add(receiver);
            }

            m.SendMails();

        }
    }
}