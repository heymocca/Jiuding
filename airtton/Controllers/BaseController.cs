using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using airtton.ViewModel;
using airtton.Models;

namespace airtton.Controllers
{
    public class BaseController : Controller
    {
        private NewsDBContext db = new NewsDBContext();
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
            var precisionStamping = db.PrecisionStamping.First();

            BasePrecisionStampingSummaryViewModel precisionStamping_sm = new BasePrecisionStampingSummaryViewModel
            {
                Id = precisionStamping.ID,
                Title = precisionStamping.Title,
                Content = precisionStamping.Content
            };

            return View(precisionStamping_sm);
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
            var assemblyPlant = db.AssemblyPlant.First();

            BaseAssemblyPlantSummaryViewModel assemblyPlant_sm = new BaseAssemblyPlantSummaryViewModel
            {
                Id = assemblyPlant.ID,
                Title = assemblyPlant.Title,
                Content = assemblyPlant.Content
            };

            return View(assemblyPlant_sm);
            
        }

        public ActionResult ChemicalProducts()
        {
            var chemicalProducts = db.ChemicalProducts.First();

            BaseChemicalProductsSummaryViewModel chemicalProducts_sm = new BaseChemicalProductsSummaryViewModel
            {
                Id = chemicalProducts.ID,
                Title = chemicalProducts.Title,
                Content = chemicalProducts.Content
            };

            return View(chemicalProducts_sm);
        }
    }
}