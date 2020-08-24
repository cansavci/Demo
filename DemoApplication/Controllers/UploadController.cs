using DemoApplication.Domain;
using DemoApplication.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DemoApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAzureBlobStorage _blobStorage;

        public UploadController(IConfiguration config, IAzureBlobStorage blobStorage)
        {
            _config = config;
            _blobStorage = blobStorage;
        }


        public async Task<string> UploadFile(Asset assset)
        {
            var blobName = Guid.NewGuid().ToString() + Path.GetExtension(assset.Data.FileName);
            var fileStream = GetFileStream(assset.Data);

            await _blobStorage.UploadAsync(blobName, fileStream);

            return blobName;
        }

        #region Helper
        private MemoryStream GetFileStream(IFormFile file)
        {
            MemoryStream filestream = new MemoryStream();
            file.CopyTo(filestream);
            return filestream;
        }
        #endregion
    }
}
