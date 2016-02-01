using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace airtton.ViewModel
{
    public class CareerEditViewModel
    {
        public int ID { get; set; }
        public string JobTitle { get; set; }
        public string CategoryName { get; set; }
        public string Location { get; set; }
        public string Experience { get; set; }
        public string Education { get; set; }
        public string WorkType { get; set; }
        public int VacancyNubmer { get; set; }
        public string Description { get; set; }
        public string Qualification { get; set; }
    }
}