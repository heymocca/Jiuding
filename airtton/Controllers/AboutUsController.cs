using airtton.Models;
using airtton.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace airtton.Controllers
{
    public class AboutUsController : Controller
    {
        private NewsDBContext db = new NewsDBContext(); 

        // GET: AboutUs
        public ActionResult GroupIntro()
        {
            var GroupIntros = db.GroupIntro.First();
         
                GroupIntroSummaryViewModel _GroupIntro = new GroupIntroSummaryViewModel
                {
                    Title = GroupIntros.Title,
                    Description = GroupIntros.Description
                };            
                return View(_GroupIntro);
        }


        // PresidentDetail
        public ActionResult PresidentDetail()
        {
            var PresidentDetails = db.PresidentDetail.First();
            
            PresidentDetailSummaryViewModel _PresidentDetails = new PresidentDetailSummaryViewModel
                {
                    Name = PresidentDetails.Name,
                    Position = PresidentDetails.Position,
                    Description = PresidentDetails.Description,
                    Facebook = PresidentDetails.Facebook,
                    Twitter = PresidentDetails.Twitter,
                    Google = PresidentDetails.Google,
                    LinkedIn = PresidentDetails.LinkedIn,
                    Email = PresidentDetails.Email,
                    Skype = PresidentDetails.Skype,
                    ImagePath = PresidentDetails.ImageUrl
                };
            return View(_PresidentDetails);
            }

        // Honor
        public ActionResult Honor()
        {
            var Honors = db.Honor.ToList();

            List<HonorSummaryViewModel> Honor_sm = new List<HonorSummaryViewModel>();

            foreach (var item in Honors)
            {
                HonorSummaryViewModel _Honor = new HonorSummaryViewModel 
                { 
                    Title = item.Title,
                    Description = item.Description,
                    ImageUrl = item.ImageUrl
                };

                Honor_sm.Add(_Honor);
            }
            return View(Honor_sm);
        }

        // Organization
        public ActionResult Organization()
        {
            var Organizations = db.Organization.ToList();

            List<OrganizationSummaryViewModel> Organization_sm = new List<OrganizationSummaryViewModel>();

            foreach (var item in Organizations)
            {
                OrganizationSummaryViewModel _Organization = new OrganizationSummaryViewModel
                {
                    Title = item.Title,
                    Description = item.Description,
                    ImageUrl = item.ImageUrl
                };
                Organization_sm.Add(_Organization);
            }
            return View(Organization_sm);
        }
    }
}