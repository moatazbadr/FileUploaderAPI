using FileUploader.API.PublicClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileUploader.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly UploadHandler _uploadHandler;

        public MainController(IWebHostEnvironment webHostEnvironment)
        {
            _uploadHandler = new UploadHandler(webHostEnvironment);
        }


        [HttpPost]
        public IActionResult UploadFile(IFormFile formFile)
        {
            return Ok (_uploadHandler.Upload(formFile));
        }
    }
}
