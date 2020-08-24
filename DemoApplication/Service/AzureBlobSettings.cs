using System;

namespace DemoApplication.Service
{
    public class AzureBlobSettings
    {
        public AzureBlobSettings(string connectionString, string containerName, int sharedAccessExpiryTime)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("connectionString");

            if (string.IsNullOrEmpty(containerName))
            {
                throw new ArgumentNullException("containerName");
            }

            ConnectionString = connectionString;
            ContainerName = containerName;

            if (sharedAccessExpiryTime == 0)
            {
                SharedAccessExpiryTime = 5; // Default 5 minutes
            }
            else
            {
                SharedAccessExpiryTime = sharedAccessExpiryTime;
            }
        }

        public string ConnectionString { get; }
        public string ContainerName { get; }
        public int SharedAccessExpiryTime { get; }
    }
}
