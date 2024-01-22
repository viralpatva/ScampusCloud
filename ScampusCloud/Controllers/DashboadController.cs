using ScampusCloud.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScampusCloud.Controllers
{
    [SessionTimeoutAttribute]
    public class DashboadController : Controller
    {
        // GET: Dashboad
        public ActionResult Dashboad()
        {
            return View();
        }
    }
}