using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using examplemvc.Filters;


namespace examplemvc.Controllers
{
    [TypeFilter(typeof(CustomAuthorizeFilter))]
    public class ExcelController : Controller
    {
        [HttpGet("Upload/UploadExcel")]
        public IActionResult UploadForm()
        {
            return View("/Views/Upload/Upload.cshtml");
        }
        
        [HttpPost("UploadExcel")]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> UploadExcel(IFormFile excelFile)
        {
            if (excelFile != null && excelFile.Length > 0)
            {
                try
                {
                    var fileExtension = Path.GetExtension(excelFile.FileName);
                    if (fileExtension != ".xlsx")
                    {
                        return RedirectToAction("UploadError", "Excel");
                    }

                    var uploads = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                    if (!Directory.Exists(uploads))
                    {
                        Directory.CreateDirectory(uploads);
                    }

                    var filePath = Path.Combine(uploads, excelFile.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await excelFile.CopyToAsync(fileStream);
                    }

                    return RedirectToAction("UploadSuccess", "Excel");
                }
                catch (Exception ex)
                {
                    return RedirectToAction("UploadError", "Excel");
                }
            }

            return RedirectToAction("UploadForm", "Excel");
        }


        public IActionResult UploadSuccess()
        {
            return View("/Views/Upload/UploadSuccess.cshtml");
        }

        public IActionResult UploadError()
        {
            return View("/Views/Upload/Error.cshtml");
        }
    }
}
