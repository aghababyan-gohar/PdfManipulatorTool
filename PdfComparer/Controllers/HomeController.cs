using PdfComparer.Extensions;
using PdfComparer.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace PdfComparer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult UploadPdf()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    file.SaveAs(path);

                    //var fileStream = new FileStream(path,
                    //                 FileMode.Open,
                    //                 FileAccess.Read
                    //               );
                    //var fsResult = new FileStreamResult(fileStream, "application/pdf");
                    //return fsResult;

                    var converter = new PdfToImageConverter();
                    var images = converter.Convert(path, fileName, Server.MapPath("~/Images/"), ImageFormat.Bmp);
                }
            }
            return View();
        }
    }
}