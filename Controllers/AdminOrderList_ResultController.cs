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
    public class AdminOrderList_ResultController : Controller
    {
        private Final_ProductEntities db = new Final_ProductEntities();

        // GET: AdminOrderList_Result
        public ActionResult Index()
        {
            var data = db.AdminOrderList();
            return View(data.ToList());
        }

        // GET: AdminOrderList_Result/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminOrderList_Result adminOrderList_Result = db.AdminOrderList_Result.Find(id);
            if (adminOrderList_Result == null)
            {
                return HttpNotFound();
            }
            return View(adminOrderList_Result);
        }

        // GET: AdminOrderList_Result/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminOrderList_Result/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "invoiceFK,ProductName,ProductPrice,name,contact,Category,qty,bill,totalbill,datetime")] AdminOrderList_Result adminOrderList_Result)
        {
            if (ModelState.IsValid)
            {
                db.AdminOrderList_Result.Add(adminOrderList_Result);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adminOrderList_Result);
        }

        // GET: AdminOrderList_Result/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminOrderList_Result adminOrderList_Result = db.AdminOrderList_Result.Find(id);
            if (adminOrderList_Result == null)
            {
                return HttpNotFound();
            }
            return View(adminOrderList_Result);
        }

        // POST: AdminOrderList_Result/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "invoiceFK,ProductName,ProductPrice,name,contact,Category,qty,bill,totalbill,datetime")] AdminOrderList_Result adminOrderList_Result)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adminOrderList_Result).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adminOrderList_Result);
        }

        // GET: AdminOrderList_Result/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminOrderList_Result adminOrderList_Result = db.AdminOrderList_Result.Find(id);
            if (adminOrderList_Result == null)
            {
                return HttpNotFound();
            }
            return View(adminOrderList_Result);
        }

        // POST: AdminOrderList_Result/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AdminOrderList_Result adminOrderList_Result = db.AdminOrderList_Result.Find(id);
            db.AdminOrderList_Result.Remove(adminOrderList_Result);
            db.SaveChanges();
            return RedirectToAction("Index");
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
