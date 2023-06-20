using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Final_Project.Models;

namespace Final_Project.Controllers
{
    public class Products1Controller : Controller
    {
        private Final_ProductEntities db = new Final_ProductEntities();

        // GET: Products1
        public ActionResult Index()
        {

            //Session["nameid"] = 1;
            if (TempData["cart"] != null)
            {
                float x = 0;
                List<cart> li2 = TempData["cart"] as List<cart>;
                foreach (var item in li2)
                {
                     x += item.bill;
                }
                TempData["total"] = x;
            }
            TempData.Keep();
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }


        //shop page

        public ActionResult Details123(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
           Session["image"]= product.ProductImage.ToString();
            Session["id"] = product.id + 1;
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }


            List<cart> li = new List<cart>();
        [HttpPost]
        public ActionResult Details123(Product pi, string qty,int id)
        {
            Product product = db.Products.Find(id);
            cart c = new cart();
            c.id = product.id;
            c.ProductName = product.ProductName;
            c.ProductPrice = Convert.ToInt32( product.ProductPrice);
            c.qty = Convert.ToInt32(qty);
            c.ProductImage = product.ProductImage;
            c.bill = c.ProductPrice * c.qty;
            if (TempData["cart"] == null)
            {
                li.Add(c);
                TempData["cart"] = li;
            }
            else
            {
                //List<cart> li1 = TempData["cart"] as List<cart>;
                //li1.Add(c);
                //TempData["cart"] = li1;

                //jophle se mojood hai
                    List<cart> li2 = TempData["cart"] as List<cart>;
                    int flag = 0;
                    foreach (var item in li2)
                    {
                        if (item.id==c.id)
                        {
                            item.qty += c.qty;
                            item.bill += c.bill;
                            flag = 1;
                        }
                    }
                    if (flag==0)
                    {
                        li2.Add(c);
                    }

                    TempData["cart"] = li2;
            }
            TempData.Keep();
            return RedirectToAction("Index","Products1");
        }



        [Authorize]
        public ActionResult CheckOut()
        {
            TempData.Keep();
            return View();
        }


        [Authorize]
        public ActionResult finalcheckout()
        {
            TempData.Keep();
            //pata nhi 
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult finalcheckout(Order o)
        {
            List<cart> li = TempData["cart"] as List<cart>;
            Invoice iv = new Invoice();
            iv.UserFK = Convert.ToInt32(Session["nameid"].ToString());
            iv.date = System.DateTime.Now;
            iv.totalbill = (float)TempData["total"];
            db.Invoices.Add(iv);
            db.SaveChanges();
           
                int a = 0;
            foreach (var item in li)
            {
                Order od = new Order();
                od.ProductFK = item.id;
                od.invoiceFK = iv.id;

                od.datetime= System.DateTime.Now; ;
                od.qty = item.qty;
                od.bill = item.bill;
                od.user_id_FK = Convert.ToInt32(Session["nameid"].ToString());
                TempData["hello"] =a +1;
                
                db.Orders.Add(od);
                db.SaveChanges();
            }

            TempData.Remove("total");
            TempData.Remove("cart");
            TempData.Keep();
            return View("Thankyoupage");

        }
            public ActionResult remove(int? id)
            {
            List<cart> li2 = TempData["cart"] as List<cart>;
            cart c = li2.Where(m => m.id == id).SingleOrDefault();
            li2.Remove(c);
            float h = 0;
            foreach (var item in li2)
            {
                h += item.bill;
            }
            TempData["total"] = h;
                return View("CheckOut");
            }


        public ActionResult Thankyoupage()
        {
            return View();
        }



    }
}
