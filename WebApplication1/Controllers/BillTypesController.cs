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
    public class BillTypesController : Controller
    {
        private Entities2 db = new Entities2();

        // GET: BillTypes
        public ActionResult Index()
        {
            return View(db.BillTypes.ToList());
        }

        // GET: BillTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillType billType = db.BillTypes.Find(id);
            if (billType == null)
            {
                return HttpNotFound();
            }
            return View(billType);
        }

        // GET: BillTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BillTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TypeName")] BillType billType)
        {
            if (ModelState.IsValid)
            {
                db.BillTypes.Add(billType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(billType);
        }

        // GET: BillTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillType billType = db.BillTypes.Find(id);
            if (billType == null)
            {
                return HttpNotFound();
            }
            return View(billType);
        }

        // POST: BillTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TypeName")] BillType billType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(billType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(billType);
        }

        // GET: BillTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillType billType = db.BillTypes.Find(id);
            if (billType == null)
            {
                return HttpNotFound();
            }
            return View(billType);
        }

        // POST: BillTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BillType billType = db.BillTypes.Find(id);
            db.BillTypes.Remove(billType);
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
