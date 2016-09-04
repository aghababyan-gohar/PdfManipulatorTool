

using Ghostscript.NET;
using Ghostscript.NET.Rasterizer;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;

namespace PdfComparer.Helpers
{
    public class PdfToImageConverter
    {
        private GhostscriptVersionInfo _lastInstalledVersion = null;
        private GhostscriptRasterizer _rasterizer = null;
        /// <summary>
        /// Converts given PDF file to any image format
        /// </summary>
        /// <param name="inputPdfPath">Initial pdf file full path</param>
        /// <param name="fileName">pdf file name</param>
        /// <param name="outputPath">destination path for saving converted image</param>
        /// <param name="imageFormat">image format: bmp, jpg, etc.</param>
        /// <returns></returns>
        public List<string> Convert(string inputPdfPath, string fileName, string outputPath, ImageFormat imageFormat)
        {
            int desired_x_dpi = 96;
            int desired_y_dpi = 96;

            var convertedImages = new List<string>();

            _lastInstalledVersion =
                GhostscriptVersionInfo.GetLastInstalledVersion(
                        GhostscriptLicense.GPL | GhostscriptLicense.AFPL,
                        GhostscriptLicense.GPL);

            _rasterizer = new GhostscriptRasterizer();

            _rasterizer.Open(inputPdfPath, _lastInstalledVersion, false);

            for (int pageNumber = 1; pageNumber <= _rasterizer.PageCount; pageNumber++)
            {
                var pageFileName = string.Format("{0}-Page-{1}.{2}", fileName, pageNumber, imageFormat); //fileName + "-Page-" + pageNumber.ToString() + "." + imageFormat;
                var pageFilePath = Path.Combine(outputPath, pageFileName);

                Image img = _rasterizer.GetPage(desired_x_dpi, desired_y_dpi, pageNumber);
                img.Save(pageFilePath, imageFormat);
                convertedImages.Add(pageFilePath);
            }

            return convertedImages;
        }
       
    }
}