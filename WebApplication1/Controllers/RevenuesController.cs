using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using PagedList;

namespace WebApplication1.Controllers
{
    public class RevenuesController : Controller
    {
        private Entities2 db = new Entities2();

        // GET: Revenues
        public ActionResult Index(string type, string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TypeNameSortParm = String.IsNullOrEmpty(sortOrder) ? "typeName_desc" : "TypeName";
            ViewBag.RevenueDateSortParm = sortOrder == "RevenueDate" ? "revenueDate_desc" : "RevenueDate";
            ViewBag.CreationDateSortParm = sortOrder == "CreationDate" ? "creationDate_desc" : "CreationDate";
            if (type != null)
            {
                page = 1;
            }
            else
            {
                type = currentFilter;
            }
            ViewBag.CurrentFilter = type;
            var userId = User.Identity.GetUserId();
            var revenues = db.Revenues.Include(r => r.AspNetUser).Where(x => x.UserId == userId);

            if (!String.IsNullOrEmpty(type))
            {
                revenues = revenues.Where(x => x.RevenueType.TypeName.Contains(type));
            }
            switch (sortOrder)
            {
                case "typeName_desc":
                    revenues = revenues.OrderByDescending(s => s.RevenueType.TypeName);
                    break;
                case "RevenueDate":
                    revenues = revenues.OrderBy(s => s.RevenueDate);
                    break;
                case "revenueDate_desc":
                    revenues = revenues.OrderByDescending(s => s.RevenueDate);
                    break;
                case "TypeName":
                    revenues = revenues.OrderBy(s => s.RevenueType.TypeName);
                    break;
                case "creationDate_desc":
                    revenues = revenues.OrderByDescending(s => s.CreationDate);
                    break;
                default:  // Name ascending 
                    revenues = revenues.OrderBy(s => s.CreationDate);
                    break;
            }
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(revenues.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult RevenuesChart()
        {
            var userId = User.Identity.GetUserId();
            var revenues = db.Revenues.Include(b => b.AspNetUser).Where(x => x.UserId == userId);
            //ViewBag.BillTypes = new SelectList(db.BillTypes, "Id", "TypeName");
            //ViewBag.TotalBillsAmount = db.Bills.Where(x => x.UserId == userId).Sum(x => x.Amount);
            //var chartId = "260a0f20 - 64b2 - 4419 - ae35 - 4e7dabc768fa";

            ViewBag.SalaryAmount = revenues.Where(x => x.RevenueTypeId == 1).Sum(x => (Decimal?)(x.Amount));
            ViewBag.ScholarshipAmount = revenues.Where(x => x.RevenueTypeId == 2).Sum(x => (Decimal?)(x.Amount));
            ViewBag.AwardAmount = revenues.Where(x => x.RevenueTypeId == 3).Sum(x => (Decimal?)(x.Amount));
            ViewBag.GiftAmount = revenues.Where(x => x.RevenueTypeId == 4).Sum(x => (Decimal?)(x.Amount));
            ViewBag.OtherAmount = revenues.Where(x => x.RevenueTypeId == 5).Sum(x => (Decimal?)(x.Amount));
            return View();

        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {

            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                file.SaveAs(path);
            }

            return RedirectToAction("Index");
        }

        // GET: Revenues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Revenue revenue = db.Revenues.Find(id);
            if (revenue == null)
            {
                return HttpNotFound();
            }
            return View(revenue);
        }

        // GET: Revenues/Create
        public ActionResult Create()
        {
            ViewBag.RevenueTypes = new SelectList(db.RevenueTypes, "Id", "TypeName");
            return View();
        }

        // POST: Revenues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RevenueDate,Amount,RevenueTypeId,UserId")] Revenue revenue, HttpPostedFileBase Content)
        {
            var file = Request.Files[0];

            MemoryStream target = new MemoryStream();
            file.InputStream.CopyTo(target);
            revenue.Content = target.ToArray();

            revenue.CreationDate = DateTime.Now;
            revenue.UserId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                db.Revenues.Add(revenue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", revenue.UserId);
            return View(revenue);
        }

        // GET: Revenues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Revenue revenue = db.Revenues.Find(id);
            if (revenue == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "FirstName", revenue.UserId);
            return View(revenue);
        }

        // POST: Revenues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RevenueDate,Amount,RevenueTypeId,UserId")] Revenue revenue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(revenue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "FirstName", revenue.UserId);
            return View(revenue);
        }

        // GET: Revenues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Revenue revenue = db.Revenues.Find(id);
            if (revenue == null)
            {
                return HttpNotFound();
            }
            return View(revenue);
        }

        // POST: Revenues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Revenue revenue = db.Revenues.Find(id);
            db.Revenues.Remove(revenue);
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
