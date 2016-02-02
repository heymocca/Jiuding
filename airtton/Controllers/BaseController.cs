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

        // 钣金加工
        public ActionResult SheetMetal()
        {
            var sheetMetal = db.SheetMetal.First();

            BaseSheetMetalSummaryViewModel sheetMetal_sm = new BaseSheetMetalSummaryViewModel
            {
                Id = sheetMetal.ID,
                Title = sheetMetal.Title,
                Content = sheetMetal.Content
            };

            return View(sheetMetal_sm);
        }

        // 冲压车间
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

        // 精密机械加工
        public ActionResult PrecisionMachinery()
        {
            var precisionMachinery = db.PrecisionMachinery.First();

            BasePrecisionMachinerySummaryViewModel precisionMachinery_sm = new BasePrecisionMachinerySummaryViewModel
            {
                Id = precisionMachinery.ID,
                Title = precisionMachinery.Title,
                Content = precisionMachinery.Content
            };

            return View(precisionMachinery_sm);
        }

        // 金属制品
        public ActionResult MetalProducts()
        {
            var metalProducts = db.MetalProducts.First();

            BaseMetalProductsSummaryViewModel metalProducts_sm = new BaseMetalProductsSummaryViewModel
            {
                Id = metalProducts.ID,
                Title = metalProducts.Title,
                Content = metalProducts.Content
            };

            return View(metalProducts_sm);
        }

        // 组装车间
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

        // 研发大楼
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