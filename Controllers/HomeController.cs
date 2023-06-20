using Final_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Final_Project.Controllers
{
    public class HomeController : Controller
    {
        Final_ProductEntities db = new Final_ProductEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {

            return View();
        }
   
        public ActionResult Contact()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Contact(Contact e)
        {
            if (ModelState.IsValid == true)
            {
                db.Contacts.Add(e);
                int a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["datainsert"] = "<script>alert('Thankyou For Your Message')</script>";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    TempData["datainsert"] = "<script>alert('Thankyou For Your Message')</script>";
                }
            }
            return View();
        }
    }
}