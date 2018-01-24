using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccessAzureStorage.Controllers
{
    public class BlobController : Controller
    {
        // GET: Blob
        public ActionResult Index()
        {
            return View();
        }
    }
}