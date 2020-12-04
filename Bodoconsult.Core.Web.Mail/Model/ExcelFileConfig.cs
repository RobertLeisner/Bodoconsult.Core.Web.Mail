namespace Bodoconsult.Core.Web.Mail.Model
{

    /// <summary>
    /// Contains config data for Excel import
    /// </summary>
    public sealed class ExcelFileConfig
    {

        public ExcelFileConfig()
        {
            EmailAddressColumn = 0;
            SalutationAddressColumn = 1;
        }


        /// <summary>
        /// Path to the data file with addresses to send the file to
        /// </summary>
        public string PhysicalPathOfDataSourceFile { get; set; }

        /// <summary>
        /// Sheetname in the Excel file
        /// </summary>
        public string SheetName { get; set; }

        /// <summary>
        /// Column number of the email address column (default: 0 [first column])
        /// </summary>
        public int EmailAddressColumn { get; set; }


        /// <summary>
        /// Column number of the salutation address column (default: 1 [second column])
        /// </summary>
        public int SalutationAddressColumn { get; set; }

    }
}
