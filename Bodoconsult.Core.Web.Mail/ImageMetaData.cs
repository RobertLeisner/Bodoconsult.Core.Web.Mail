// Copyright (c) Bodoconsult EDV-Dienstleistungen GmbH. All rights reserved.


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
            get => _url;
            set
            {
                var f = new FileInfo(value);
                Extension = f.Extension;
                Name = f.Name;
                ContentBytes = File.ReadAllBytes(value);

                switch (Extension.ToLowerInvariant())
                {
                    case ".jpeg":
                    case ".jpg":
                        MimeType = "image/jpeg";
                        break;
                    case ".png":
                        MimeType = "image/png";
                        break;
                    default:
                        MimeType = "";
                        break;
                }

                Length = value.Length;
                _url = value;
            }
        }

        public string Name { get; set; }

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

        /// <summary>
        /// Content as byte array
        /// </summary>
        public byte[] ContentBytes { get; private set; }

        /// <summary>
        /// Mime type of the image
        /// </summary>
        public string MimeType { get; set; }
    }
}
