using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionApp1
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-storage-blob-trigger?tabs=csharp
    /// https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-storage-blob-input?tabs=csharp
    /// https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-storage-blob-output?tabs=csharp
    /// </summary>
    public static class BlobTriggered
    {
        [FunctionName("BlobTriggered")]
        public static void Run([BlobTrigger("input/{name}",
            Connection = "blobConnection")]Stream myBlob,
            [Blob("output/out.txt", FileAccess.Write, Connection = "blobConnection")] Stream outputFile, 
            string name, ILogger log)
        {
            var message = $"C# Blob trigger function Processed blob\n Name:" +
                $"{name} \n Size: {myBlob.Length} Bytes";

            log.LogInformation(message);

            var writer = new StreamWriter(outputFile);
            writer.WriteLine(message);
            writer.Flush();
            writer.Close();
        }
    }
}
