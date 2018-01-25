using AccessAzureStorage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Configuration;

namespace AccessAzureStorage.Controllers
{
    public class TableController : Controller
    {
        private CloudStorageAccount StorageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings.Get("StorageConnectionString"));
        
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
        public ActionResult Upload(EmployeeData Records)
        {
            try
            {
                CloudTableClient TableClient = StorageAccount.CreateCloudTableClient();
                CloudTable DataEntity = TableClient.GetTableReference("Employees");
                DataEntity.CreateIfNotExists();

                if (Records.EmployeeId != null && Records.Name != null)
                {
                    Records.PartitionKey = Records.EmployeeId;
                    Records.RowKey = Records.Name;
                    Records.Timestamp = DateTime.Now;
                    TableOperation Insert = TableOperation.InsertOrReplace(Records);
                    DataEntity.Execute(Insert);
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
            return View("Success");
        }
    }
}