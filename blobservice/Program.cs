using System;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;

namespace blobservice
{
    class Program
    {
        private static string _container_name = "data";
        private static string _connection_string = "DefaultEndpointsProtocol=https;AccountName=appstore200089;AccountKey=SGnsTS1c1IT6cYeBWsNu3LYywQ/cYj9WtKWjudpUzRRNjIekbj1gx53thQQHZ6dDryuym2kQd3GKWmHs8rA9Og==;EndpointSuffix=core.windows.net";
        private static string _blob_name = "Program.cs";
        private static string _location = "C:\\tmp\\Program.cs";
        
        static void Main(string[] args)
        {
            GenerateSAS();
            ReadBlob();
            Console.ReadKey();
        }

        public static void  ReadBlob()
        {
            Uri _blob_uri = GenerateSAS();
            BlobClient _client = new BlobClient(_blob_uri);
            _client.DownloadTo(_location);
        }
        public static Uri GenerateSAS()
        {
            BlobServiceClient _service_client = new BlobServiceClient(_connection_string);
            BlobContainerClient _container_client = _service_client.GetBlobContainerClient(_container_name);
            BlobClient _blobclient = _container_client.GetBlobClient(_blob_name);
            BlobSasBuilder _builder = new BlobSasBuilder()
            {
                BlobContainerName = _container_name,
                BlobName = _blob_name,
                Resource = "b"    // we are creating the SAS for Blob
            };

            _builder.SetPermissions(BlobSasPermissions.Read | BlobSasPermissions.List);
            _builder.ExpiresOn = DateTimeOffset.UtcNow.AddHours(1);
            return _blobclient.GenerateSasUri(_builder);

        }
    }
}
