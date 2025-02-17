using FileUploader.API.PublicClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileUploader.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        [HttpPost]
        public IActionResult UploadFile(IFormFile formFile)
        {
            return Ok (new uploadHandler().Upload(formFile));
        }
    }
}
