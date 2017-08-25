using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DonaStoyanova.Models;

namespace DonaStoyanova.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (OurDbContext db = new OurDbContext())
            {
                return View(db.gallery.ToList());
            }
            //return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}