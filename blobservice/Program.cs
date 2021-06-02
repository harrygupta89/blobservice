using System;
using Azure.Storage.Blobs;
using System.IO;

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
            BlobClient _blobclient = _container_client.GetBlobClient(_blob_name);

            MemoryStream _memory = new MemoryStream();
            _blobclient.DownloadTo(_memory);
            _memory.Position = 0;

            StreamReader _reader = new StreamReader(_memory);
            Console.WriteLine(_reader.ReadToEnd());
            
            StreamWriter _writer = new StreamWriter(_memory);
            _writer.Write("This is a change version1");
            _writer.Flush();
            _memory.Position = 0;
            _blobclient.Upload(_memory,true);
            Console.WriteLine("change made");
            Console.ReadKey();
        }
    }
}



