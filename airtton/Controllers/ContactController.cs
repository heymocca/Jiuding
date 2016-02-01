using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using airtton.Models;
using airtton.ViewModel;

namespace airtton.Controllers
{
    public class ContactController : Controller
    {
        private NewsDBContext db = new NewsDBContext();

        // GET: Contact
        public ActionResult Index()
        {
            var Contacts = db.Contact.First();

            ContactSummaryViewModel _Contact = new ContactSummaryViewModel()
            {
                Address = Contacts.Address,
                Email = Contacts.Email,
                Phone = Contacts.Phone,
                Fax = Contacts.Fax,
                Link = Contacts.Link
            };

            return View(_Contact);
        }

        public ActionResult MessageSubmit(MessageInfoSummaryViewModel messageInfo)
        {
            MessageInfo messages = new MessageInfo
            {
                Name = messageInfo.Name,
                Email = messageInfo.Email,
                Content = messageInfo.Content,
                CreateDate = DateTime.Now
            };

            db.MessageInfo.Add(messages);
            db.SaveChanges();

            return RedirectToAction("Contact");
        }

    }
}