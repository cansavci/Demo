using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace DemoApplication.Service
{
    public class AzureBlobStorage : IAzureBlobStorage
    {
        private readonly AzureBlobSettings _settings;

        public AzureBlobStorage(AzureBlobSettings settings)
        {
            _settings = settings;
        }

        public async Task UploadAsync(string blobName, Stream stream)
        {
            //Blob
            string sasKey = await GetSasKey(blobName, BlobType.BlockBlob);
            CloudBlockBlob blockBlob = GetBlockBlob(new Uri(sasKey));

            //Upload
            stream.Position = 0;
            await blockBlob.UploadFromStreamAsync(stream);
        }

        public async Task<MemoryStream> DownloadAsync(string blobName)
        {
            //Blob
            CloudBlockBlob blockBlob = await GetBlockBlobAsync(blobName);

            //Download
            var stream = new MemoryStream();
            await blockBlob.DownloadToStreamAsync(stream);
            return stream;
        }

        #region Helper Methods
        private CloudBlockBlob GetBlockBlob(Uri uri)
        {
            var blob = new CloudBlockBlob(uri);
            return blob;
        }

        public async Task<CloudBlockBlob> GetBlockBlobAsync(string blobName)
        {
            //Container
            CloudBlobContainer blobContainer = await GetContainerAsync();

            //Blob
            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(blobName);

            return blockBlob;
        }

        private async Task<CloudBlobContainer> GetContainerAsync()
        {
            //Account
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_settings.ConnectionString);

            //Client
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            var options = new BlobRequestOptions { ParallelOperationThreadCount = 1, SingleBlobUploadThresholdInBytes = 1024 * 1024 * 4 };
            blobClient.DefaultRequestOptions = options;
            //Container
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(_settings.ContainerName);
            await blobContainer.CreateIfNotExistsAsync();

            return blobContainer;
        }

        private async Task<string> GetSasKey(string blobName, BlobType type)
        {
            if (type == BlobType.AppendBlob)
            {
                var targetBlob = await GetAppendBlobAsync(blobName);

                //Create a Shared Access Signature for the blob
                var SaS = targetBlob.GetSharedAccessSignature(
                   new SharedAccessBlobPolicy()
                   {
                       Permissions = SharedAccessBlobPermissions.Write | SharedAccessBlobPermissions.List | SharedAccessBlobPermissions.Read,
                       SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(_settings.SharedAccessExpiryTime),
                   });

                return string.Format(CultureInfo.InvariantCulture, "{0}{1}", targetBlob.Uri, SaS);
            }
            else if (type == BlobType.BlockBlob)
            {
                var targetBlob = await GetBlockBlobAsync(blobName);

                //Create a Shared Access Signature for the blob
                var SaS = targetBlob.GetSharedAccessSignature(
                   new SharedAccessBlobPolicy()
                   {
                       Permissions = SharedAccessBlobPermissions.Write | SharedAccessBlobPermissions.List | SharedAccessBlobPermissions.Read,
                       SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(_settings.SharedAccessExpiryTime),
                   });

                return string.Format(CultureInfo.InvariantCulture, "{0}{1}", targetBlob.Uri, SaS);
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<CloudAppendBlob> GetAppendBlobAsync(string blobName)
        {
            //Container
            CloudBlobContainer blobContainer = await GetContainerAsync();

            //Blob
            var blob = blobContainer.GetAppendBlobReference(blobName);

            return blob;
        }
        #endregion


    }
}
