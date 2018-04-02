using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compareitor.CommonModel;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;

namespace Compareitor.AzureBlobStorage
{
    public class BlobStorageComparer : PerformanceComparerBase
    {
        CloudStorageAccount storageAccount;
        CloudBlobClient blobClient;
        CloudBlobContainer container;

        public override void Setup()
        {
            storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["blobConnection"]);

            blobClient = storageAccount.CreateCloudBlobClient();

            container = blobClient.GetContainerReference("invoices");
            container.CreateIfNotExists();
        }

        protected override void ExecuteWrite(List<Invoice> invoices)
        {
            foreach (var invoice in invoices)
            {
                var blob = container.GetBlockBlobReference(invoice.Id.ToString());
                blob.UploadText(JsonConvert.SerializeObject(invoice));
            }
        }

        protected override void ExecuteRead(List<Invoice> invoices)
        {
            foreach (var invoice in invoices)
            {
                var blob = container.GetBlockBlobReference(invoice.Id.ToString());
                JsonConvert.DeserializeObject<Invoice>(blob.DownloadText());
            }
        }
    }
}
