using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure.MobileServices;
using AccessAzureStorage.Models;
using System.Net.Http;

namespace AccessAzureStorage.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
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
        public ActionResult Upload(EmployeeData Record)
        {
            var client = new MobileServiceClient("https://hp-mob-app.azurewebsites.net");

            IMobileServiceTable<EmployeeData> EmployeeTable = client.GetTable<EmployeeData>();

            try
            {
                Record.id = new Random().Next(1, 1000).ToString();
                if(Record !=null && EmployeeTable.InsertAsync(Record)!=null)
                {
                    return View("Success");
                }
            }
            catch (Exception)
            {
                return View("Error");
            }

            return View("Index");
        }
    }
}