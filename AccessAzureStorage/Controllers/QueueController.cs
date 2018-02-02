using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.ServiceBus.Messaging;

namespace AccessAzureStorage.Controllers
{
    public class QueueController : Controller
    {
        private CloudStorageAccount StorageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings.Get("StorageConnectionString"));

        public ActionResult Index()
        {
            ViewBag.Message = string.Empty;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Message)
        {
            string connectionString = ConfigurationManager.AppSettings.Get("ServiceBusQueue");
            try
            {
                string action = Request.Form.Get("action");
                CloudQueueClient SQueueClient = StorageAccount.CreateCloudQueueClient();
                QueueClient SBQueueClient = QueueClient.CreateFromConnectionString(connectionString);
                CloudQueue Queue = SQueueClient.GetQueueReference("hp532queue");
                Queue.CreateIfNotExists();
                switch (action)
                {
                    case "Send Message":
                        Queue.AddMessage(new CloudQueueMessage(Message));
                        SBQueueClient.Send(new BrokeredMessage(Message));
                        break;
                    case "Receive Message":
                        ViewBag.Message = SBQueueClient.Receive().GetBody<string>();
                        if(string.IsNullOrWhiteSpace(SBQueueClient.Receive().GetBody<string>()))
                            ViewBag.Message = Queue.GetMessage().AsString;
                        break;
                    default:
                        return View("Error");
                }
                return View("Index");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
    }
}