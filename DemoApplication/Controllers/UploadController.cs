using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DemoApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IConfiguration _config;

        public UploadController(IConfiguration config)
        {
            _config = config;
        }


        public IActionResult UploadFile()
        {
            using (var httpClient = new HttpClient())
            {

            }

            return (IActionResult)Task.FromResult(true);
        }
    }
}
