using System.IO;
using System.Threading.Tasks;

namespace DemoApplication.Service
{
    public interface IAzureBlobStorage
    {
        Task UploadAsync(string blobName, Stream stream);

        Task<MemoryStream> DownloadAsync(string blobName);
    }
}
