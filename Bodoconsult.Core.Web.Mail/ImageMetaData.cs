using System;
using System.IO;

namespace Bodoconsult.Core.Web.Mail
{
    /// <summary>
    /// meta data for an image file in a HTML document
    /// </summary>
    public sealed class ImageMetaData
    {
        private string _url;

        /// <summary>
        /// Length of Url for sorting iusses
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// standard CTOR
        /// </summary>
        public ImageMetaData()
        {
            ContentId = Guid.NewGuid().ToString().Replace("-", string.Empty);
        }

        /// <summary>
        /// Url to the image file in local filesystem or in the web
        /// </summary>
        public string Url
        {
            get { return _url; }
            set
            {
                var f = new FileInfo(value);
                Extension = f.Extension;

                Length = value.Length;
                _url = value;
            }
        }

        /// <summary>
        /// File ia local filesystem file (true/false) or otherwise file in the Internet
        /// </summary>
        public bool LocalFile { get; set; }

        /// <summary>
        /// The original Url in the HTML document
        /// </summary>
        public string OriginalUrl { get; set; }

        /// <summary>
        /// Content-ID for the image. Will be created automatically
        /// </summary>
        public string ContentId { get; private set; }


        /// <summary>
        /// The image file's extension
        /// </summary>
        public string Extension { get; private set; }
    }
}
