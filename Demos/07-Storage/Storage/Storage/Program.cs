using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Storage
{
    class Program
    {
        private static string _connectionString = "DefaultEndpointsProtocol=https;AccountName=az204ordemo002str;AccountKey=25tKlwRTe3FSZLmfO8H+JSWgkwTnZq5aUSU4GhL0oMEVaHdG4OMjWBC7ykZjnm8BWIkGgEY+Q/8v+AStbP4QRw==;EndpointSuffix=core.windows.net";

        /// <summary>
        /// See https://docs.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-dotnet
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string containerName = "testcontainer";

            BlobContainerClient containerClient = CreateContainer(containerName);

            // create a blob
            UploadNewBlob(containerClient);

            // List all blobs in the container
            var blobs = ListBlobs(containerClient);
            foreach (BlobItem blobItem in blobs)
            {
                Console.WriteLine("blobItem.Name: " + blobItem.Name);
                Console.WriteLine("blobItem.Properties.BlobType: " + blobItem.Properties.BlobType.ToString());
                Console.WriteLine("blobItem.Properties.LastModified: " + blobItem.Properties.LastModified.ToString());
                Console.WriteLine("blobItem.Properties.AccessTier: " + blobItem.Properties.AccessTier.ToString());
            }

            Console.WriteLine("Press a key to continue...");
            Console.ReadLine();
        }

        static BlobContainerClient CreateContainer(string name)
        {
            // Create a BlobServiceClient object which will be used to create a container client
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);

            //Create a unique name for the container
            string containerName = name + "-demo03";

            // Create the container and return a container client object
            BlobContainerClient containerClient =
                blobServiceClient.CreateBlobContainer(containerName);

            return containerClient;
        }

        static void UploadNewBlob(BlobContainerClient client)
        {
            // Get a reference to a blob
            BlobClient blobClient = client.GetBlobClient("myname.txt");

            byte[] byteArray = Encoding.ASCII.GetBytes("Reza Salehi");
            MemoryStream stream = new MemoryStream(byteArray);

            blobClient.Upload(stream);

            stream.Close();
        }

        static List<BlobItem> ListBlobs(BlobContainerClient containerClient)
        {
            List<BlobItem> blobs = new List<BlobItem>();

            // List all blobs in the container
            foreach (BlobItem blobItem in containerClient.GetBlobs())
            {
                blobs.Add(blobItem);
            }

            return blobs;
        }
    }
}