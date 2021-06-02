using System;
using Azure.Storage.Blobs;
using System.IO;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Blobs.Models;

namespace blobservice
{
    class Program
    {
        private static string _container_name = "data";
        private static string _connection_string = "DefaultEndpointsProtocol=https;AccountName=appstore200089;AccountKey=SGnsTS1c1IT6cYeBWsNu3LYywQ/cYj9WtKWjudpUzRRNjIekbj1gx53thQQHZ6dDryuym2kQd3GKWmHs8rA9Og==;EndpointSuffix=core.windows.net";
        private static string _blob_name = "Program.cs";
        static void Main(string[] args)
        {
            BlobServiceClient _service_client = new BlobServiceClient(_connection_string);
            BlobContainerClient _container_client = _service_client.GetBlobContainerClient(_container_name);
            BlobClient _blob_client = _container_client.GetBlobClient(_blob_name);
            
            MemoryStream _memory = new MemoryStream();

            _blob_client.DownloadTo(_memory);
            _memory.Position = 0;
            StreamReader _reader = new StreamReader(_memory);
            Console.WriteLine(_reader.ReadToEnd());

            BlobLeaseClient _blob_lease_client = _blob_client.GetBlobLeaseClient();
            BlobLease _lease = _blob_lease_client.Acquire(TimeSpan.FromSeconds(30));
            Console.WriteLine($"The lease is {_lease.LeaseId}");


            StreamWriter _writer = new StreamWriter(_memory);
            _writer.Write("This is a change");
            _writer.Flush();

            BlobUploadOptions _blobUploadOptions = new BlobUploadOptions()
            {
                Conditions = new BlobRequestConditions()
                {
                    LeaseId = _lease.LeaseId
                }
            };

            _memory.Position = 0;
            _blob_client.Upload(_memory, _blobUploadOptions);
            _blob_lease_client.Release();

            Console.WriteLine("Change made");
            Console.ReadKey();
        }
    }
}



