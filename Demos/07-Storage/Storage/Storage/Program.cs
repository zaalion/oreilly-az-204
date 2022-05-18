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
        private static string _connectionString = "BlobEndpoint=https://storageaccordemo01.blob.core.windows.net/;QueueEndpoint=https://storageaccordemo01.queue.core.windows.net/;FileEndpoint=https://storageaccordemo01.file.core.windows.net/;TableEndpoint=https://storageaccordemo01.table.core.windows.net/;SharedAccessSignature=sv=2020-08-04&ss=b&srt=co&sp=rwlactfx&se=2022-05-20T01:28:31Z&st=2022-05-18T17:28:31Z&spr=https&sig=mI%2B3JhJQaL7v8X5PDYl1rmjqfFWsd9qkkENLhpBwVcM%3D";

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