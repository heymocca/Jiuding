using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace airtton.ViewModel
{
    public class UploadImageModel
    {
        public int InstanceId { get; set; } // add to be used for instancesId: newsId,.. etc

        public int ParentId { get; set; }

        [Display(Name = "Local file")]
        public HttpPostedFileBase File { get; set; } 

        public bool IsFile { get; set; }

        public bool IsPrimary { get; set; } 

        public string CurrentImage { get; set; }

        [Range(0, int.MaxValue)]
        public int X { get; set; }

        [Range(0, int.MaxValue)]
        public int Y { get; set; }

        [Range(1, int.MaxValue)]
        public int Width { get; set; }

        [Range(1, int.MaxValue)]
        public int Height { get; set; }

        public string Type { get; set; } 
    }
}