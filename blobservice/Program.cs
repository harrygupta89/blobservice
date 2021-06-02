using System;
using Azure.Storage.Blobs;

namespace blobservice
{
    class Program
    {
        private static string _connection_string = "DefaultEndpointsProtocol=https;AccountName=appstore200089;AccountKey=SGnsTS1c1IT6cYeBWsNu3LYywQ/cYj9WtKWjudpUzRRNjIekbj1gx53thQQHZ6dDryuym2kQd3GKWmHs8rA9Og==;EndpointSuffix=core.windows.net";
        private static string _container_name = "data"; 
        private static string _blob_name = "Courses.json";
        private static string _local_blob = "\\app\\Courses.json";
        static void Main(string[] args)
        {
            BlobServiceClient _service_client = new BlobServiceClient(_connection_string);
            BlobContainerClient _container_client = _service_client.GetBlobContainerClient(_container_name);
            BlobClient _blob_client = _container_client.GetBlobClient(_blob_name);
            _blob_client.DownloadTo(_local_blob);
            Console.WriteLine("BLOB Downloded");
            Console.ReadKey();
        }
    }
}


/*
 
You can see the volumes in your system from below path.
\\wsl$\docker-desktop-data\version-pack-data\community\docker\volumes\blobvolume\_data

*/



