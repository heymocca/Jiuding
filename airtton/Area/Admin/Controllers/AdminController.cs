using airtton.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace airtton.Controllers
{
    public class AdminController : Controller
    {
        private NewsDBContext db = new NewsDBContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

    }
}