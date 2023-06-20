using Final_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web.Security;

namespace Final_Project.Controllers
{
    public class loginController : Controller
    {
        Final_ProductEntities db = new Final_ProductEntities();

        public ActionResult loginit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult loginit(User user, string ReturnUrl)
        {

            if (isvalid(user) == true)
            {
                FormsAuthentication.SetAuthCookie(user.name, false);
                Session["username"] = user.name.ToString();
             
                if (ReturnUrl != null)
                {
                    return Redirect(ReturnUrl);
                }

                else
                {
                    return View(user);

                }
            }
            else
            {
                return View();

            }

            //if (isvalid(u) == true)
            //{
            //    FormsAuthentication.SetAuthCookie(u.name, false);
            //    Session["username"] = u.name.ToString();
            //    TempData["hemo"] = "<script>alert('Login Succesfull')</script>";
            //    if (ReturnUrl != null)
            //    {
            //        TempData["hemo"] = "<script>alert('User Inserted')</script>";
            //        return Redirect(ReturnUrl);
            //    }
            //    else
            //    {
            //        return View();
            //    }
            //}
            //else
            //{
            //    TempData["hemo1"] = "<script>alert('Login is not Succesfull')</script>";

            //    return View();

            //}


        }
        public bool isvalid(User u)
        {
            var credetials = db.Users.Where(m => m.name == u.password && m.password == u.password).FirstOrDefault();

            Session["nameid"] = credetials.id;
            return (u.name == credetials.name && u.password == credetials.password);
         
            
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create( User u , string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    FormsAuthentication.SetAuthCookie(u.name, false);

                    Session["username"] = u.name.ToString();
                    Session["nameid"] = u.id.ToString();
                    db.Users.Add(u);
                    db.SaveChanges();

                    TempData["hemo"] = "<script>alert('User Inserted')</script>";
                    if (ReturnUrl != null)
                    {
                        return Redirect(ReturnUrl);
                    }

                }
                catch
                { }


            }

            TempData["hemo"] = "<script>alert('User is Not Inserted')</script>";
            return View();

        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["username"] = null;
            return RedirectToAction("Index", "Products");




        }
        [Authorize]
        public ActionResult dashboard()
        {

            //var data = db.dashboard();
            //return View(data.ToList());
            var std = mydetail();
            var tchr = mydasbhboared();

            merge s = new merge();
            s.user = std;
            s.das = tchr;
            return View(s);

        }


        



        
        public List<User> mydetail()
        {


                int id = Convert.ToInt32(Session["nameid"].ToString());
                var data = db.Users.Where(m => m.id == id).ToList();
                return (data.ToList());
            
          

        }
        public List<dashboard_Result> mydasbhboared()
        {

          return  db.dashboard(Convert.ToInt32(Session["nameid"])).ToList();
            //return View(data.ToList());




       
        }
    }
}