using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace airtton.ViewModel
{
    public class NewsEditViewModel
    {
        [Required(ErrorMessage="请输入标题")]
        public string Title { get; set; }

        [Required(ErrorMessage="请输入内容")]
        public string Content { get; set; }

        public string ImagePath { get; set; }

        public int Id { get; set; }

        public UploadImageModel Image { get; set; }

    }
}