using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace airtton.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SheetMetal()
        {
            return View();
        }
        public ActionResult PrecisionStamping()
        {
            return View();
        }
        public ActionResult PrecisionMachinery()
        {
            return View();
        }
        public ActionResult MetalProducts()
        {
            return View();
        }
        public ActionResult AssemblyPlant()
        {
            return View();
        }
        public ActionResult ChemicalProducts()
        {
            return View();
        }
    }
}