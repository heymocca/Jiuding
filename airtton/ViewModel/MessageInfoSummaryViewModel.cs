using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace airtton.ViewModel
{
    public class MessageInfoSummaryViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public string CreateDate { get; set; }
        public int MessageNumber { get; set; }
    }
}