using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using airtton.Models;



namespace airtton.ViewModel
{
    public class ImageUpload
    {

        private NewsDBContext db = new NewsDBContext();

        public static readonly string ItemUploadFolderPath = "~/uploads/"; 
        private HttpServerUtilityBase ThisServer; 

        private static readonly List<int> image_sizes = new List<int>() { 64, 128, 256, 480, 640, 1024 };

        public string ImageUrl { get; set; }

        public ImageUpload(HttpServerUtilityBase server)
        {
            ThisServer = server;
        }



        //public void DeleteImage(int imageId, string type)
        //{
        //    try
        //    {

        //        string image_path = GetImageUploadPatch(type);
        //        switch (type)
        //        {
        //            case "news":
        //                {
        //                    var image = db.News.Find(imageId);
        //                    foreach (int size in image_sizes)
        //                    {
        //                        RemoveImage(image.ImagePath, image_path, size);
        //                    }

        //                    db.PlaceImages.Remove(image);
        //                    db.SaveChanges();
        //                }
        //                break;
        //            case "products":
        //                {
        //                    var image = db.ProductImages.Find(imageId);
        //                    foreach (int size in image_sizes)
        //                    {
        //                        RemoveImage(image.ImagePath, image_path, size);
        //                    }

        //                    db.ProductImages.Remove(image);
        //                    db.SaveChanges();
        //                }
        //                break;
        //        }

        //    }
        //    catch (Exception e)
        //    {

        //    }
        //}




        public bool UploadImage(UploadImageModel model)
        {
            try
            {
                string currentImage = model.CurrentImage;
                List<string> images = new List<string>();
                images.Add(currentImage);
                //remove related image

                if (model.IsPrimary == true) 
                {
                    //if (model.Type == "website_logo")
                    //{
                    //    //remove previous logos related to the store model.id
                    //    var site = db.WebSites.SingleOrDefault(c => c.PlaceId == model.ParentId);

                    //    if (site.SiteLogo != null)
                    //    {
                    //        var image = site.SiteLogo;
                    //        string image_path = string.Format("websites/stores/{0}", model.ParentId);

                    //        foreach (int size in image_sizes)
                    //        {
                    //            RemoveImage(image, image_path, size);
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    RemoveImages(images.ToArray(), model.Type);
                    //}
                }

                var save_url = SaveImage(model, model.Type);

                ImageUrl = save_url;

                //If we had success so far
                if (save_url == null)
                    throw new Exception("There no image to save!");

                SaveImagePath(model, save_url);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region Helpers

        private void RemoveImage(string image_system_path, string image_path, int size)
        {
            var fileName = Path.GetFileName(image_system_path);

            string fn = string.Empty;

            if (size == 0)
            {
                fn = String.Format("_{0}x{1}", 0, fileName);
            }
            else
            {
                fn = String.Format("_{0}x{1}", size.ToString(), fileName);
            }

            var physicalPath = Path.Combine(ThisServer.MapPath(ItemUploadFolderPath + image_path), fn);

            if (System.IO.File.Exists(physicalPath))
            {
                System.IO.File.Delete(physicalPath);
            }
        }

        private void SaveImagePath(UploadImageModel model, string save_url)
        {
            try
            {
                switch (model.Type)
                {
                    case "news":
                        {
                            
                            var news = db.News.SingleOrDefault(c => c.ID == model.ParentId);

                            if (news != null)
                            {
                                news.ImageUrl = save_url;
                                db.Entry(news).State = EntityState.Modified;
                            }
                            
                        }
                        break;

                    case "events":
                        {
                            var events = db.Events.SingleOrDefault(c => c.ID == model.ParentId);

                            if (events != null)
                            {
                                events.ImageUrl = save_url;
                                db.Entry(events).State = EntityState.Modified;
                            }

                        }
                        break;

                    case "presidentDetail":
                        {
                            var presidentDetails = db.PresidentDetail.SingleOrDefault(c => c.ID == model.ParentId);

                            if (presidentDetails != null)
                            {
                                presidentDetails.ImageUrl = save_url;
                                db.Entry(presidentDetails).State = EntityState.Modified;
                            }
                        }
                        break;
                }

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                throw new Exception("fail to save image path to data base", ex);
            }
        }

        private void RemoveImages(string[] fileNames, string type)
        {
            try
            {
                string image_path = GetImagePath(type);

                if (fileNames != null)
                {
                    foreach (var fullName in fileNames)
                    {
                        foreach (int size in image_sizes)
                        {
                            var fileName = Path.GetFileName(fullName);

                            var fn = String.Format("_{0}x{1}", size.ToString(), fileName);

                            DeleteImageMethod(fn, image_path);
                        }
                        //var fileName = Path.GetFileName(fullName);
                        //var thumbFileName = "Thumb_" + fileName;
                        //var thumb128FileName = "Thumb128_" + fileName;
                        //DeleteImageMethod(fileName, image_path);
                        //DeleteImageMethod(thumbFileName, image_path);
                        //DeleteImageMethod(thumb128FileName, image_path);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                throw new Exception("fail to remove prmary image", ex);
            }
        }

        private void DeleteImageMethod(string fileName, string path)
        {
            if (fileName != null)
            {
                var physicalPath = Path.Combine(ThisServer.MapPath(path), fileName);

                // TODO: Verify user permissions

                if (System.IO.File.Exists(physicalPath))
                {
                    // The files are not actually removed in this demo
                    System.IO.File.Delete(physicalPath);
                }
            }
        }

        private string SaveImage(UploadImageModel model, string type)
        {
            //Prepare the needed variables
            Bitmap original = null;
            var name = "newimagefile";
            var errorField = string.Empty;

            string image_path = GetImagePath(type);

            //if (type == "website_logo" || type == "slides" || type == "second_slides")
            //    image_path = image_path + model.ParentId + "\\";

            string save_path = null;

            try
            {
                if (model.File != null) // model.IsFile
                {
                    errorField = "File";
                    name = Path.GetFileNameWithoutExtension(model.File.FileName);
                    original = Bitmap.FromStream(model.File.InputStream) as Bitmap;

                    //If we had success so far
                    if (original != null)
                    {
                        var fileName = Path.GetFileName(model.File.FileName);
                        string append = String.Format("{0}_{1}_{2}", model.ParentId, model.IsPrimary, airtton.Helpers.IdFromInt.Base64Hash(8));
                        string finalFileName = String.Format("{0}{1}", append, ".jpg");

                        
                        save_path = String.Format("{0}_{1}x{2}", image_path, 0, finalFileName);
                        SaveImageMethod(model, original, save_path, 0);

                        
                        var path = Path.Combine(ThisServer.MapPath(image_path), finalFileName);

                        save_path = string.Format("{0}", finalFileName);
                    }

                    return save_path;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return save_path;
            }

            return save_path;
        }

        private void SaveImageMethod(UploadImageModel model, Bitmap original, string save_path, int size)
        {
            var img = CreateImage(original, model.X, model.Y, model.Width, model.Height);

            var fn = ThisServer.MapPath(save_path);
            if (size != 0)
            {
                Image imgActual = ScaleBySize(img, size);
                imgActual.Save(fn, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else
            {
                img.Save(fn, System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            //if (img.Size.Width > size)
            //{
            //    Image imgActual = ScaleBySize(img, size);
            //    var fn = Server.MapPath(save_path);
            //    imgActual.Save(fn, System.Drawing.Imaging.ImageFormat.Png);
            //}
            //else
            //{
            //    var fn = Server.MapPath(save_path);
            //    img.Save(fn, System.Drawing.Imaging.ImageFormat.Png);
            //}

        }

        private static string GetImagePath(string type)
        {
            string image_path = null;

            if (type == "news")
                image_path = ItemUploadFolderPath + "news/";

            else if (type == "events")
            image_path = ItemUploadFolderPath + "events/";

            //else if (type == "presidentDetail")
            //    image_path = ItemUploadFolderPath + "presidentDetail/";

            else
            {
                image_path = ItemUploadFolderPath + "else/";
            }

            return image_path;
        }

        /// <summary>
        /// Creates a small image out of a larger image.
        /// </summary>
        /// <param name="original">The original image which should be cropped (will remain untouched).</param>
        /// <param name="x">The value where to start on the x axis.</param>
        /// <param name="y">The value where to start on the y axis.</param>
        /// <param name="width">The width of the final image.</param>
        /// <param name="height">The height of the final image.</param>
        /// <returns>The cropped image.</returns>
        Bitmap CreateImage(Bitmap original, int x, int y, int width, int height)
        {
            var img = new Bitmap(width, height);

            using (var g = Graphics.FromImage(img))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(original, new Rectangle(0, 0, width, height), x, y, width, height, GraphicsUnit.Pixel);
            }

            return img;
        }

        private bool validateExtension(string extension)
        {
            extension = extension.ToLower();
            switch (extension)
            {
                case ".jpg":
                    return true;
                case ".png":
                    return true;
                case ".gif":
                    return true;
                case ".jpeg":
                    return true;
                default:
                    return false;
            }
        }

        private static string GetImageUploadPatch(string type)
        {
            string image_path = null;

            switch (type)
            {
                case "places":
                    image_path = "places/";
                    break;
                case "brands":
                    image_path = "brands/";
                    break;
                case "products":
                    image_path = "products/";
                    break;
                case "user":
                    image_path = "users/";
                    break;
                case "catalogs":
                    image_path = "catalogs/";
                    break;
                case "avatars":
                    image_path = "users/";
                    break;
                case "slides":
                    image_path = "slides/";
                    break;
            }
            return image_path;
        }

        private Image ScaleBySize(Image imgPhoto, int size)
        {
            int logoSize = size;

            float sourceWidth = imgPhoto.Width;
            float sourceHeight = imgPhoto.Height;
            float destHeight = 0;
            float destWidth = 0;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            // Resize Image to have the height = logoSize/2 or width = logoSize.
            // Height is greater than width, set Height = logoSize and resize width accordingly
            if (sourceWidth > (2 * sourceHeight))
            {
                destWidth = logoSize;
                destHeight = (float)(sourceHeight * logoSize / sourceWidth);
            }
            else
            {
                int h = logoSize / 2;
                destHeight = h;
                destWidth = (float)(sourceWidth * h / sourceHeight);
            }
            // Width is greater than height, set Width = logoSize and resize height accordingly

            Bitmap bmPhoto = new Bitmap((int)destWidth, (int)destHeight,
                                        PixelFormat.Format64bppPArgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBilinear; //.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, (int)destWidth, (int)destHeight),
                new Rectangle(sourceX, sourceY, (int)sourceWidth, (int)sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();

            return bmPhoto;
        }

        #endregion


    }
}