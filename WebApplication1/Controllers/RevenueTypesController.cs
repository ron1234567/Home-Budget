using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class RevenueTypesController : Controller
    {
        private Entities2 db = new Entities2();

        // GET: RevenueTypes
        public ActionResult Index()
        {
            return View(db.RevenueTypes.ToList());
        }

        // GET: RevenueTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RevenueType revenueType = db.RevenueTypes.Find(id);
            if (revenueType == null)
            {
                return HttpNotFound();
            }
            return View(revenueType);
        }

        // GET: RevenueTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RevenueTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TypeName")] RevenueType revenueType)
        {
            if (ModelState.IsValid)
            {
                db.RevenueTypes.Add(revenueType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(revenueType);
        }

        // GET: RevenueTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RevenueType revenueType = db.RevenueTypes.Find(id);
            if (revenueType == null)
            {
                return HttpNotFound();
            }
            return View(revenueType);
        }

        // POST: RevenueTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TypeName")] RevenueType revenueType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(revenueType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(revenueType);
        }

        // GET: RevenueTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RevenueType revenueType = db.RevenueTypes.Find(id);
            if (revenueType == null)
            {
                return HttpNotFound();
            }
            return View(revenueType);
        }

        // POST: RevenueTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RevenueType revenueType = db.RevenueTypes.Find(id);
            db.RevenueTypes.Remove(revenueType);
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
