using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace airtton.ViewModel
{
    public class ContactSummaryViewModel
    {
        public int ID { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Link { get; set; }
        public string Email { get; set; }
    }
}