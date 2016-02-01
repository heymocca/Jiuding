using airtton.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using airtton.ViewModel;

namespace airtton.Controllers
{
    public class NewsController : Controller
    {
        private NewsDBContext db = new NewsDBContext();
        // GET: News

        public ActionResult Index()
        {
            var news = db.News.ToList();

            List<NewsSummaryViewModel> news_sm = new List<NewsSummaryViewModel>();

            foreach (var item in news)
            {
                NewsSummaryViewModel _news = new NewsSummaryViewModel
                {
                    Title = item.Title,
                    Content = item.Content,
                    ImagePath = item.ImageUrl,
                    CreateDate = item.CreateDate.ToString(),
                    Id = item.ID
                };

                news_sm.Add(_news);
            }

            return View(news_sm);
        }


        public ActionResult Events()
        {
            var events = db.Events.ToList();

            List<EventsSummaryViewModel> events_sm = new List<EventsSummaryViewModel>();

            foreach (var item in events)
            {
                EventsSummaryViewModel _events = new EventsSummaryViewModel
                {
                    Title = item.Title,
                    Content = item.Content,
                    ImagePath = item.ImageUrl,
                    CreateDate = item.CreateDate.ToString(),
                    Id = item.ID
                };
                events_sm.Add(_events);
            }
            return View(events_sm);
        }



    }
}