using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace airtton.ViewModel
{
    public class PresidentDetailEditViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public string Facebook { get; set; }

        public string Twitter { get; set; }

        public string Google { get; set; }

        public string LinkedIn { get; set; }

        public string Email { get; set; }

        public string Skype { get; set; }        

        //public UploadImageModel Image { get; set; }
       
 
    }
}