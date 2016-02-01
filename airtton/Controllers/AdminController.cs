using airtton.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using airtton.ViewModel;

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

        #region // News

        public ActionResult News()
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
               
        public ActionResult NewsEdit(int id)
        {
            var news = db.News.SingleOrDefault(r => r.ID == id);

            var image = new UploadImageModel()
            {
                InstanceId = news.ID,
                ParentId = news.ID,
                IsPrimary = false,
                CurrentImage = null,
                Type = "news"
            };

            NewsEditViewModel _news = new NewsEditViewModel
            {
                Image = image,
                Title = news.Title,
                Content = news.Content,
                Id = news.ID,
                ImagePath = news.ImageUrl
            };

            return View(_news);

        }
               
        public ActionResult NewsSubmit(NewsEditViewModel submit_news)
        {
            var news = db.News.SingleOrDefault(r => r.ID == submit_news.Id);

            news.Title = submit_news.Title;
            news.Content = submit_news.Content;

            db.SaveChanges();

            ImageUpload _upload = new ImageUpload(Server);
            _upload.UploadImage(submit_news.Image);

            return RedirectToAction("News");
        }
                
        public ActionResult NewsDeleteConfirm(int id)
        {
            var news = db.News.Find(id);

            NewsEditViewModel _news = new NewsEditViewModel
            {
                Title = news.Title,
                Content = news.Content,
                Id = news.ID,
                ImagePath = news.ImageUrl
            };
            return View(_news);
        }
                
        public ActionResult NewsDelete(NewsEditViewModel delete_news)
        {
            var news = db.News.Find(delete_news.Id);

            db.News.Remove(news);
            db.SaveChanges();

            return RedirectToAction("News");
        }
                
        public ActionResult NewsCreate()
        {            
           return View();
        }
                
        public ActionResult CreateSubmit(NewsEditViewModel create_news)
        {
             
            News CreateNews = new News
            {
            Title = create_news.Title,
            Content = create_news.Content,
            CreateDate = DateTime.Now,
             };

            db.News.Add(CreateNews);
            db.SaveChanges();

            

            ImageUpload _upload = new ImageUpload(Server);

            create_news.Image.Type = "news";
            create_news.Image.InstanceId = CreateNews.ID;
            create_news.Image.ParentId = CreateNews.ID;

            _upload.UploadImage(create_news.Image);

            return RedirectToAction("News");
        }
        #endregion

        #region// Events

        public ActionResult Events()
        {
            var events = db.Events.ToList();

            List<EventsSummaryViewModel> event_sm = new List<EventsSummaryViewModel>();

            foreach (var item in events)
            {
                EventsSummaryViewModel _event = new EventsSummaryViewModel
                {
                    Id = item.ID,
                    Title = item.Title,
                    Content = item.Content,
                    CreateDate = item.CreateDate.ToString(),
                    ImagePath = item.ImageUrl
                };
                event_sm.Add(_event);
            }
            return View(event_sm);
        }
                
        public ActionResult EventsEdit(int id)
        {
            var events = db.Events.Find(id);

            var image = new UploadImageModel()
            {
                InstanceId = events.ID,
                ParentId = events.ID,
                IsPrimary = false,
                CurrentImage = null,
                Type = "events"
            };

            EventsEditViewModel _events = new EventsEditViewModel
            {
                Image = image,
                Title = events.Title,
                Content = events.Content,
                Id = events.ID,
                ImagePath = events.ImageUrl
            };

            return View(_events);
        }
                       
        public ActionResult EventsSubmit(EventsEditViewModel submit_events)
        {
            var events = db.Events.Find(submit_events.Id);
            
            events.Title = submit_events.Title;
            events.Content = submit_events.Content;

            db.SaveChanges();

            ImageUpload _upload = new ImageUpload(Server);
            _upload.UploadImage(submit_events.Image);

            return RedirectToAction("Events");
        }
                
        public ActionResult EventsDeleteConfirm(int id)
        {
            var events = db.Events.Find(id);

            EventsEditViewModel _events = new EventsEditViewModel
            {
                Id = events.ID,
                Title = events.Title,
                Content = events.Content,
                ImagePath = events.ImageUrl
            };
            return View(_events);
        }
              
        public ActionResult EventsDelete(EventsEditViewModel delete_events)
        {
            var events = db.Events.Find(delete_events.Id);
            
            db.Events.Remove(events);
            db.SaveChanges();

            return RedirectToAction("Events");
        }
               
        public ActionResult EventsCreate()
        {
            return View();
        }
              
        public ActionResult EventsCreateSubmit(EventsEditViewModel create_events)
        {
            Events events = new Events 
            {
                Title = create_events.Title,
                Content = create_events.Content,
                CreateDate = DateTime.Now
            };

            db.Events.Add(events);
            db.SaveChanges();

            ImageUpload _upload = new ImageUpload(Server);

            create_events.Image.Type = "events";
            create_events.Image.InstanceId = events.ID;
            create_events.Image.ParentId = events.ID;

            _upload.UploadImage(create_events.Image);

            return RedirectToAction("Events");
        }
        #endregion

        #region// GroupIntro        
        public ActionResult GroupIntro()
        {
            var GroupIntros = db.GroupIntro.First();

            GroupIntroSummaryViewModel _GroupIntro = new GroupIntroSummaryViewModel()
            {
                ID = GroupIntros.ID,
                Title = GroupIntros.Title,
                Description = GroupIntros.Description,
                ImagePath = GroupIntros.ImageUrl
            };

            return View(_GroupIntro);
        }

        public ActionResult EditGroupIntro(string title, string content, int id, int echo)
        {
            try
            {
                GroupIntroSummaryViewModel _GroupIntro = new GroupIntroSummaryViewModel()
                {
                    ID = id,
                    Title = title,
                    Description = content,
                    ImagePath = null
                };

                //prepare the partial view by passing the model, turn the view into a string in order to render the html at the front end in js
                var view_str = RazorViewToString.RenderRazorViewToString(this, "Partial/_GroupIntroEdit", _GroupIntro);

                //return json result
                return new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new { result = "success", echo = echo, html = view_str }
                    
                };
            }

            catch (Exception ex)
            {
                return new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new { result = "fail", message = ex.Message }
                };
            }
        }
        
        public ActionResult EditGroupIntroSubmit(string title, string content, int id, int echo)
        {

           var groupIntros = db.GroupIntro.Find(id);
            
                      
           groupIntros.Title = title;
           groupIntros.Description = content;
           //groupIntros.ID = id;                  

            
           db.SaveChanges();

           GroupIntroSummaryViewModel _GroupIntro = new GroupIntroSummaryViewModel()
           {
               ID = groupIntros.ID,
               Title = groupIntros.Title,
               Description = groupIntros.Description,
               ImagePath = null
           };

           var view_str = RazorViewToString.RenderRazorViewToString(this, "Partial/_GroupIntro", _GroupIntro);

           try
           {
               return new JsonResult()
               {
                   JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                   Data = new { result = "success", echo = echo, html = view_str }

               };
           }
           catch (Exception ex)
           {
               return new JsonResult()
               {
                   JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                   Data = new { result = "fail", message = ex.Message }
               };
           }

           //return RedirectToAction("GroupIntro");
        }
        #endregion

        #region// PresidentDetail
        public ActionResult PresidentDetail()
        {
            var PresidentDetails = db.PresidentDetail.First();

            PresidentDetailSummaryViewModel _PresidentDetail = new PresidentDetailSummaryViewModel()
            {
                ID = PresidentDetails.ID,
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
            return View(_PresidentDetail);
        }

        public ActionResult PresidentDetailCreate()
        {
            return View();
        }

        public ActionResult PresidentDetailCreateSubmit(PresidentDetailEditViewModel create_PresidentDetail)
        {
            PresidentDetail presidentDetail = new PresidentDetail
            {
                Name = create_PresidentDetail.Name,
                Position = create_PresidentDetail.Position,
                Description = create_PresidentDetail.Description,
                Email = create_PresidentDetail.Email,
                Facebook = create_PresidentDetail.Facebook,
                Google = create_PresidentDetail.Google,
                LinkedIn = create_PresidentDetail.LinkedIn,
                Twitter = create_PresidentDetail.Twitter,
                Skype = create_PresidentDetail.Skype
            };

            db.PresidentDetail.Add(presidentDetail);
            db.SaveChanges();

            //ImageUpload _upload = new ImageUpload(Server);

            //create_PresidentDetail.Image.Type = "PresidentDetail";
            //create_PresidentDetail.Image.InstanceId = presidentDetail.ID;
            //create_PresidentDetail.Image.ParentId = presidentDetail.ID;

            //_upload.UploadImage(create_PresidentDetail.Image);

            return RedirectToAction("PresidentDetail");
        }

        public ActionResult PresidentDetailEdit(int id)
        {
            var PresidentDetails = db.PresidentDetail.Find(id);

            //var image = new UploadImageModel()
            //{
            //    InstanceId = PresidentDetails.ID,
            //    ParentId = PresidentDetails.ID,
            //    IsPrimary = false,
            //    CurrentImage = null,
            //    Type = "presidentDetail"
            //};

            PresidentDetailEditViewModel _presidentDetail = new PresidentDetailEditViewModel
            {
                ID = PresidentDetails.ID,
                Name = PresidentDetails.Name,
                Position = PresidentDetails.Position,
                Description = PresidentDetails.Description,
                //Image = image,
                Facebook = PresidentDetails.Facebook,
                Twitter = PresidentDetails.Twitter,
                Google = PresidentDetails.Google,
                LinkedIn = PresidentDetails.LinkedIn,
                Email = PresidentDetails.Email,
                Skype = PresidentDetails.Skype,
                ImagePath = PresidentDetails.ImageUrl
            };
            return View(_presidentDetail);
        }

        public ActionResult PresidentDetailSubmit(PresidentDetailEditViewModel submit_PresidentDetail)
        {
            var presidentDetail = db.PresidentDetail.Find(submit_PresidentDetail.ID);

            presidentDetail.Name = submit_PresidentDetail.Name;
            presidentDetail.Position = submit_PresidentDetail.Position;
            presidentDetail.Description = submit_PresidentDetail.Description;
            presidentDetail.Facebook = submit_PresidentDetail.Facebook;
            presidentDetail.Twitter = submit_PresidentDetail.Twitter;
            presidentDetail.Google = submit_PresidentDetail.Google;
            presidentDetail.LinkedIn = submit_PresidentDetail.LinkedIn;
            presidentDetail.Email = submit_PresidentDetail.Email;
            presidentDetail.Skype = submit_PresidentDetail.Skype;
            //presidentDetail.ImageUrl = submit_PresidentDetail.ImagePath;

            db.SaveChanges();

            return RedirectToAction("PresidentDetail");
        }

        //public ActionResult PresidentDetailDeleteConfirm(int id)
        //{
        //    PresidentDetail presidentDetail = db.PresidentDetail.Find(id);

        //    PresidentDetailEditViewModel _presidentDetail = new PresidentDetailEditViewModel
        //    {
        //        Name = presidentDetail.Name,
        //        Position = presidentDetail.Position,
        //        Description = presidentDetail.Description,
        //        //Image = image,
        //        Facebook = presidentDetail.Facebook,
        //        Twitter = presidentDetail.Twitter,
        //        Google = presidentDetail.Google,
        //        LinkedIn = presidentDetail.LinkedIn,
        //        Email = presidentDetail.Email,
        //        Skype = presidentDetail.Skype,
        //        ImagePath = presidentDetail.ImageUrl
        //    };
        //    return View(_presidentDetail);
        //}

        //public ActionResult PresidentDetailDelete(PresidentDetailEditViewModel delete_PresidentDetail)
        //{
        //    var presidentDetail = db.PresidentDetail.Find(delete_PresidentDetail.ID);

        //    db.PresidentDetail.Remove(presidentDetail);
        //    db.SaveChanges();

        //    return RedirectToAction("PresidentDetail");
        //}


        //Honor

        // Organization
        #endregion

        // Career

    }
}