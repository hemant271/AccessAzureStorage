using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Configuration;

namespace AccessAzureStorage.Controllers
{
    public class BlobController : Controller
    {
        private static CloudStorageAccount CloudStorageAccount = 
            CloudStorageAccount.Parse(ConfigurationManager.AppSettings.Get("StorageConnectionString"));

        private static CloudBlobClient Client = CloudStorageAccount.CreateCloudBlobClient();
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase File)
        {
            try
            {
                CloudBlobContainer Container = Client.GetContainerReference("file-container");
                Container.CreateIfNotExists();

                CloudBlockBlob Blob = Container.GetBlockBlobReference(File.FileName);
                Blob.UploadFromStream(File.InputStream);
            }
            catch (Exception)
            {
                return View("Error");
            }
            return View("Success");
        }
    }
}