using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DonaStoyanova.Models;

namespace DonaStoyanova.Controllers
{
    public class SliderController : Controller
    {
        [Authorize(Roles ="Admin")]
        // GET: Slider
        public ActionResult Index()
        {
            using (OurDbContext db = new OurDbContext())
            {
                return View(db.gallery.ToList());
            }
            //return View();
        }

        //Add Image in slider
        public ActionResult AddImage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddImage(HttpPostedFileBase ImagePath)
        {
            if (ImagePath != null)
            {
                System.Drawing.Image img = System.Drawing.Image.FromStream(ImagePath.InputStream);
                //if ((img.Width != 800) || (img.Height != 800))
                //{
                //    ModelState.AddModelError("", "Image resolution must be 800 x 800 pixels");
                //    return View();
                //}
                
                //Upload Images
                string pic = System.IO.Path.GetFileName(ImagePath.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Content/images"), pic);
                ImagePath.SaveAs(path);
                using (OurDbContext db = new OurDbContext())
                {
                    Gallery gallery = new Gallery { ImagePath = "~/Content/images/" + pic };
                    db.gallery.Add(gallery);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("index");
        }

        //Delete Images
        public ActionResult DeleteImages()
        {
            using (OurDbContext db = new OurDbContext())
            {
                return View(db.gallery.ToList());
            }
        }

        [HttpPost]
        public ActionResult DeleteImages(IEnumerable<int> ImagesIDs)
        {
            using (OurDbContext db = new OurDbContext())
            {
                foreach (var id  in ImagesIDs)
                {
                    var image = db.gallery.Single(s => s.ID == id);
                    string imgPath = Server.MapPath(image.ImagePath);
                    db.gallery.Remove(image);
                    if (System.IO.File.Exists(imgPath))
                        System.IO.File.Delete(imgPath);
                }
                db.SaveChanges();
            }
            return RedirectToAction("DeleteImages");
        }
    }
}