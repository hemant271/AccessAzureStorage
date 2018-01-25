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

        public ActionResult Upload()
        {
            try
            {
                CloudBlobContainer Container = Client.GetContainerReference("file-container");
                Container.CreateIfNotExists();
                string FileName = DateTime.Now.ToString() + ".txt";

                CloudBlockBlob Blob = Container.GetBlockBlobReference(FileName);
                Blob.UploadText("File written on " + DateTime.Now.ToString());
            }
            catch (Exception)
            {
                return View("Error");
            }
            return View("Success");
        }

        public ActionResult Download()
        {
            return View("Success");
        }
    }
}