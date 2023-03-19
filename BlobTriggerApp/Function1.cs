using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace BlobTriggerApp
{
    public class Function1
    {
        [FunctionName("EmailNotifierFunction")]
        public async Task Run(
         [BlobTrigger("maincontainer/{name}", Connection = "AzureWebJobsStorage")] Stream blob,
         string name,
         IDictionary<string, string> metadata,
         ILogger log)
        {
            log.LogInformation($"C# Blob trigger function processed blob\n Email:{name} \n Length: {blob.Length} bytes");

            var apiKey = Environment.GetEnvironmentVariable("SendGridApiKey");
            var client = new SendGridClient(apiKey);

            var from = new EmailAddress("mgavrilyuk122@gmail.com", "Maks Havriliuk");
            var subject = "File uploaded successfully";
            var to = new EmailAddress(metadata["email"]);

            var plainTextContent = $"File {name} was uploaded successfully to the blob storage";
            var htmlContent = $"<strong>File {name} was uploaded successfully to the blob storage</strong>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
