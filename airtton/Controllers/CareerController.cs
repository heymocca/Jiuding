using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using airtton.ViewModel;
using airtton.Models;

namespace airtton.Controllers
{
    public class CareerController : Controller
    {
        private NewsDBContext db = new NewsDBContext();
        // GET: Career
        public ActionResult Index()
        {
            var career = db.Career.ToList();

            List<CareerSummaryViewModel> career_sm = new List<CareerSummaryViewModel>();

            foreach (var item in career)
            {
                CareerSummaryViewModel _career = new CareerSummaryViewModel
                {
                    ID = item.ID,
                    JobTitle = item.JobTitle,                   
                    Location = item.Location,
                    Experience = item.Experience,                   
                    VacancyNubmer = item.VacancyNubmer,
                    CreateDate = item.CreateDate.ToString(),                   
                };
                career_sm.Add(_career);
            }
            return View(career_sm);
        }

        public ActionResult Detail(int id)
        {
            var career = db.Career.Find(id);

            CareerSummaryViewModel careerDetail = new CareerSummaryViewModel
            {
                JobTitle = career.JobTitle,
                Location = career.Location,
                Experience = career.Experience,
                Education = career.Education,
                WorkType = career.WorkType,
                VacancyNubmer = career.VacancyNubmer,
                CreateDate = career.CreateDate.ToString(),
                Description = career.Description,
                Qualification = career.Qualification
            };
                        
            return View(careerDetail);
        }
    }
}