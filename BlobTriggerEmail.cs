using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace BlobTriggerEmail
{
    public class BlobTriggerEmail
    {
        [FunctionName("BlobTriggerEmail")]
        public void Run([BlobTrigger("maincontainer/{name}", Connection = "fileblobstorageapp_STORAGE")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        }
    }
}
