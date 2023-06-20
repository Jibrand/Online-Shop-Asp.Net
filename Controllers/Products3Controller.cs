using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Final_Project.Models;

namespace Final_Project.Controllers
{
    [Authorize]

    public class Products3Controller : Controller
    {
        private Final_ProductEntities db = new Final_ProductEntities();

        // GET: Products3
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            Session["imagea"] = product.ProductImage.ToString();
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products3/Create
        public ActionResult Create()
        {
            ViewBag.ProductCategories = new SelectList(db.Categories, "Id", "Category1");
            return View();
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product s)
        {
            string filename = Path.GetFileNameWithoutExtension(s.ImageFile.FileName);
            string extension = Path.GetExtension(s.ImageFile.FileName);

            HttpPostedFileBase postedFileBase = s.ImageFile;
            int length = postedFileBase.ContentLength;
            if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg")
            {
                if (length <= 1000000)
                {
                    filename = filename + extension;
                    s.ProductImage = "~/images/" + filename;
                    filename = Path.Combine(Server.MapPath("~/images/"), filename);
                    s.ImageFile.SaveAs(filename);
                    db.Products.Add(s);
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        ViewBag.message = "<script>alert('Image Inserted')</script>";
                        ModelState.Clear();
                        return RedirectToAction("Index", "products3");
                    }
                    else
                    {
                        ViewBag.message = "<script>alert('Image Is Not Inserted')</script>";
                    }
                }
                else
                {
                    ViewBag.message = "<script>alert('Image Should be less than 1mb')</script>";
                }
            }
            else
            {
                ViewBag.message = "<script>alert('Image Is Not Supported')</script>";
            }
            return View();
        }






























        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            Session["image"] = product.ProductImage;

            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCategories = new SelectList(db.Categories, "Id", "Category1", product.ProductCategories);
            return View(product);
        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Product s)
        {
            if (ModelState.IsValid == true)
            {
                if (s.ImageFile != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(s.ImageFile.FileName);
                    string extension = Path.GetExtension(s.ImageFile.FileName);

                    HttpPostedFileBase postedFileBase = s.ImageFile;
                    int length = postedFileBase.ContentLength;
                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg")
                    {
                        if (length <= 1000000)
                        {
                            filename = filename + extension;
                            s.ProductImage = "~/images/" + filename;
                            filename = Path.Combine(Server.MapPath("~/images/"), filename);
                            s.ImageFile.SaveAs(filename);
                            db.Entry(s).State = EntityState.Modified;
                            int a = db.SaveChanges();
                            if (a > 0)
                            {
                                ViewBag.message = "<script>alert('Image Inserted')</script>";
                                string imagepath = Request.MapPath(Session["image"].ToString());
                                if (System.IO.File.Exists(imagepath))
                                {
                                    System.IO.File.Delete(imagepath);
                                }
                                ModelState.Clear();
                                return RedirectToAction("Index", "Products3");
                            }
                            else
                            {
                                ViewBag.message = "<script>alert('Image Is Not Inserted')</script>";
                            }
                        }
                        else
                        {
                            ViewBag.message = "<script>alert('Image Should be less than 1mb')</script>";
                        }
                    }
                    else
                    {
                        ViewBag.message = "<script>alert('Image Is Not Supported')</script>";
                    }
                }
                else
                {
                    s.ProductImage = Session["image"].ToString();
                    db.Entry(s).State = EntityState.Modified;
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        ViewBag.message = "<script>alert('Image Inserted')</script>";
                        ModelState.Clear();
                        return RedirectToAction("List", "Home");
                    }
                    else
                    {
                        ViewBag.message = "<script>alert('Image Is Not Inserted')</script>";
                    }
                }
            }
            return View();
        }

   
        public ActionResult Delete(int? id)
        {
            var data = db.Products.Where(model => model.id == id).FirstOrDefault();
            if (data != null)
            {
                db.Entry(data).State = EntityState.Deleted;
                int a = db.SaveChanges();
                if (a > 0)
                {
                    ViewBag.message = "<script>alert('Image Deleted')</script>";
                    //dta tu delete hojaye ga lekin image nhitu?????code????????
                    string imagepath = Request.MapPath(data.ProductImage.ToString());
                    if (System.IO.File.Exists(imagepath))
                    {
                        System.IO.File.Delete(imagepath);
                    }

                    //
                    ModelState.Clear();
                    return RedirectToAction("Index", "Products3");
                }
                else
                {
                    ViewBag.message = "<script>alert('Image Is Not Deleted')</script>";
                }
            }
            return RedirectToAction("Index", "Products3");

        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
