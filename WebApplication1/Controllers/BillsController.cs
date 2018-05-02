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
using Microsoft.AspNet.Identity;
using PagedList;




namespace WebApplication1.Controllers
{
    public class BillsController : Controller
    {
        private Entities2 db = new Entities2();

        // GET: Bills
        public ActionResult Index(string type, string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TypeNameSortParm = String.IsNullOrEmpty(sortOrder) ? "typeName_desc" : "TypeName";
            ViewBag.BillDateSortParm = sortOrder == "BillDate" ? "billDate_desc" : "BillDate";
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
            var bills = db.Bills.Include(b => b.AspNetUser).Where(x => x.UserId == userId);
            if (!String.IsNullOrEmpty(type))
            {
                bills = bills.Where(x => x.BillType.TypeName.Contains(type));
            }
            switch (sortOrder)
            {
                case "typeName_desc":
                    bills = bills.OrderByDescending(s => s.BillType.TypeName);
                    break;
                
                case "billDate_desc":
                    bills = bills.OrderByDescending(s => s.BillDate);
                    break;
                case "TypeName":
                    bills = bills.OrderBy(s => s.BillType.TypeName);
                    break;
                case "CreationDate":
                    bills = bills.OrderBy(s => s.CreationDate);
                    break;
                case "creationDate_desc":
                    bills = bills.OrderByDescending(s => s.CreationDate);
                    break;
                default:   
                    bills = bills.OrderBy(s => s.BillDate);
                    break;
            }

            //ViewBag.BillTypes = new SelectList(db.BillTypes, "Id", "TypeName");
            //ViewBag.TotalBillsAmount = db.Bills.Where(x => x.UserId == userId).Sum(x => x.Amount);

            int pageSize = 8;
            int pageNumber = (page ?? 1);
            //if (type == null)
            //{
            //    return View(bills.OrderBy(x => x.Amount >= 0).ToPagedList(pageNumber, pageSize));
            //}
            //else
            //{
            return View(bills.ToPagedList(pageNumber, pageSize));
            //}

        }
        public ActionResult BillsChart()
        {
            var userId = User.Identity.GetUserId();
            var bills = db.Bills.Include(b => b.AspNetUser).Where(x => x.UserId == userId);
            ViewBag.TotalBillsAmount = db.Bills.Where(x => x.UserId == userId).Sum(x => x.Amount);
            ViewBag.TotalRevenuesAmount = db.Revenues.Where(x => x.UserId == userId).Sum(x => x.Amount);
            ViewBag.ElectricityAmount = bills.Where(x => x.BillTypeId == 1).Sum(x => (Decimal?)(x.Amount));
            ViewBag.WaterAmount = bills.Where(x => x.BillTypeId == 2).Sum(x => (Decimal?)(x.Amount));
            ViewBag.PropertyTaxAmount = bills.Where(x => x.BillTypeId == 3).Sum(x => (Decimal?)(x.Amount));
            ViewBag.RentAmount = bills.Where(x => x.BillTypeId == 4).Sum(x => (Decimal?)(x.Amount));
            ViewBag.CarInsuranceAmount = bills.Where(x => x.BillTypeId == 5).Sum(x => (Decimal?)(x.Amount));
            ViewBag.HomeInsuranceAmount = bills.Where(x => x.BillTypeId == 6).Sum(x => (Decimal?)(x.Amount));
            ViewBag.OtherAmount = bills.Where(x => x.BillTypeId == 7).Sum(x => (Decimal?)(x.Amount));
            ViewBag.VacationAmount = bills.Where(x => x.BillTypeId == 8).Sum(x => (Decimal?)(x.Amount));



            return View();

        }
        public ActionResult BalanceChart()
        {
            var userId = User.Identity.GetUserId();
            var bills = db.Bills.Include(b => b.AspNetUser).Where(x => x.UserId == userId);
            ViewBag.ElectricityAmount0616 = bills.Where(x => x.BillTypeId == 1 && x.BillDate.Year == 2016 && x.BillDate.Month==06).Sum(x => (Decimal?)(x.Amount));
            ViewBag.ElectricityAmount0716 = bills.Where(x => x.BillTypeId == 1 && x.BillDate.Year == 2016 && x.BillDate.Month == 07).Sum(x => (Decimal?)(x.Amount));
            ViewBag.ElectricityAmount0816 = bills.Where(x => x.BillTypeId == 1 && x.BillDate.Year == 2016 && x.BillDate.Month == 08).Sum(x => (Decimal?)(x.Amount));
            ViewBag.ElectricityAmount0916 = bills.Where(x => x.BillTypeId == 1 && x.BillDate.Year == 2016 && x.BillDate.Month == 09).Sum(x => (Decimal?)(x.Amount));
            ViewBag.ElectricityAmount1016 = bills.Where(x => x.BillTypeId == 1 && x.BillDate.Year == 2016 && x.BillDate.Month == 10).Sum(x => (Decimal?)(x.Amount));
            ViewBag.ElectricityAmount1116 = bills.Where(x => x.BillTypeId == 1 && x.BillDate.Year == 2016 && x.BillDate.Month == 11).Sum(x => (Decimal?)(x.Amount));
            ViewBag.ElectricityAmount1216 = bills.Where(x => x.BillTypeId == 1 && x.BillDate.Year == 2016 && x.BillDate.Month == 12).Sum(x => (Decimal?)(x.Amount));
            ViewBag.ElectricityAmount0117 = bills.Where(x => x.BillTypeId == 1 && x.BillDate.Year == 2017 && x.BillDate.Month == 01).Sum(x => (Decimal?)(x.Amount));
            ViewBag.ElectricityAmount0217 = bills.Where(x => x.BillTypeId == 1 && x.BillDate.Year == 2017 && x.BillDate.Month == 02).Sum(x => (Decimal?)(x.Amount));
            ViewBag.ElectricityAmount0317 = bills.Where(x => x.BillTypeId == 1 && x.BillDate.Year == 2017 && x.BillDate.Month == 03).Sum(x => (Decimal?)(x.Amount));
            ViewBag.ElectricityAmount0417 = bills.Where(x => x.BillTypeId == 1 && x.BillDate.Year == 2017 && x.BillDate.Month == 04).Sum(x => (Decimal?)(x.Amount));
            ViewBag.ElectricityAmount0517 = bills.Where(x => x.BillTypeId == 1 && x.BillDate.Year == 2017 && x.BillDate.Month == 05).Sum(x => (Decimal?)(x.Amount));
            ViewBag.ElectricityAmount0617 = bills.Where(x => x.BillTypeId == 1 && x.BillDate.Year == 2017 && x.BillDate.Month == 06).Sum(x => (Decimal?)(x.Amount));
            ViewBag.ElectricityAmount0717 = bills.Where(x => x.BillTypeId == 1 && x.BillDate.Year == 2017 && x.BillDate.Month == 07).Sum(x => (Decimal?)(x.Amount));
            ViewBag.WaterAmount0616 = bills.Where(x => x.BillTypeId == 2 && x.BillDate.Year == 2016 && x.BillDate.Month == 06).Sum(x => (Decimal?)(x.Amount));
            ViewBag.WaterAmount0716 = bills.Where(x => x.BillTypeId == 2 && x.BillDate.Year == 2016 && x.BillDate.Month == 07).Sum(x => (Decimal?)(x.Amount));
            ViewBag.WaterAmount0816 = bills.Where(x => x.BillTypeId == 2 && x.BillDate.Year == 2016 && x.BillDate.Month == 08).Sum(x => (Decimal?)(x.Amount));
            ViewBag.WaterAmount0916 = bills.Where(x => x.BillTypeId == 2 && x.BillDate.Year == 2016 && x.BillDate.Month == 09).Sum(x => (Decimal?)(x.Amount));
            ViewBag.WaterAmount1016 = bills.Where(x => x.BillTypeId == 2 && x.BillDate.Year == 2016 && x.BillDate.Month == 10).Sum(x => (Decimal?)(x.Amount));
            ViewBag.WaterAmount1116 = bills.Where(x => x.BillTypeId == 2 && x.BillDate.Year == 2016 && x.BillDate.Month == 11).Sum(x => (Decimal?)(x.Amount));
            ViewBag.WaterAmount1216 = bills.Where(x => x.BillTypeId == 2 && x.BillDate.Year == 2016 && x.BillDate.Month == 12).Sum(x => (Decimal?)(x.Amount));
            ViewBag.WaterAmount0117 = bills.Where(x => x.BillTypeId == 2 && x.BillDate.Year == 2017 && x.BillDate.Month == 01).Sum(x => (Decimal?)(x.Amount));
            ViewBag.WaterAmount0217 = bills.Where(x => x.BillTypeId == 2 && x.BillDate.Year == 2017 && x.BillDate.Month == 02).Sum(x => (Decimal?)(x.Amount));
            ViewBag.WaterAmount0317 = bills.Where(x => x.BillTypeId == 2 && x.BillDate.Year == 2017 && x.BillDate.Month == 03).Sum(x => (Decimal?)(x.Amount));
            ViewBag.WaterAmount0417 = bills.Where(x => x.BillTypeId == 2 && x.BillDate.Year == 2017 && x.BillDate.Month == 04).Sum(x => (Decimal?)(x.Amount));
            ViewBag.WaterAmount0517 = bills.Where(x => x.BillTypeId == 2 && x.BillDate.Year == 2017 && x.BillDate.Month == 05).Sum(x => (Decimal?)(x.Amount));
            ViewBag.WaterAmount0617 = bills.Where(x => x.BillTypeId == 2 && x.BillDate.Year == 2017 && x.BillDate.Month == 06).Sum(x => (Decimal?)(x.Amount));
            ViewBag.WaterAmount0717 = bills.Where(x => x.BillTypeId == 2 && x.BillDate.Year == 2017 && x.BillDate.Month == 07).Sum(x => (Decimal?)(x.Amount));
            ViewBag.PropertyTaxAmount0616 = bills.Where(x => x.BillTypeId == 3 && x.BillDate.Year == 2016 && x.BillDate.Month == 06).Sum(x => (Decimal?)(x.Amount));
            ViewBag.PropertyTaxAmount0716 = bills.Where(x => x.BillTypeId == 3 && x.BillDate.Year == 2016 && x.BillDate.Month == 07).Sum(x => (Decimal?)(x.Amount));
            ViewBag.PropertyTaxAmount0816 = bills.Where(x => x.BillTypeId == 3 && x.BillDate.Year == 2016 && x.BillDate.Month == 08).Sum(x => (Decimal?)(x.Amount));
            ViewBag.PropertyTaxAmount0916 = bills.Where(x => x.BillTypeId == 3 && x.BillDate.Year == 2016 && x.BillDate.Month == 09).Sum(x => (Decimal?)(x.Amount));
            ViewBag.PropertyTaxAmount1016 = bills.Where(x => x.BillTypeId == 3 && x.BillDate.Year == 2016 && x.BillDate.Month == 10).Sum(x => (Decimal?)(x.Amount));
            ViewBag.PropertyTaxAmount1116 = bills.Where(x => x.BillTypeId == 3 && x.BillDate.Year == 2016 && x.BillDate.Month == 11).Sum(x => (Decimal?)(x.Amount));
            ViewBag.PropertyTaxAmount1216 = bills.Where(x => x.BillTypeId == 3 && x.BillDate.Year == 2016 && x.BillDate.Month == 12).Sum(x => (Decimal?)(x.Amount));
            ViewBag.PropertyTaxAmount0117 = bills.Where(x => x.BillTypeId == 3 && x.BillDate.Year == 2017 && x.BillDate.Month == 01).Sum(x => (Decimal?)(x.Amount));
            ViewBag.PropertyTaxAmount0217 = bills.Where(x => x.BillTypeId == 3 && x.BillDate.Year == 2017 && x.BillDate.Month == 02).Sum(x => (Decimal?)(x.Amount));
            ViewBag.PropertyTaxAmount0317 = bills.Where(x => x.BillTypeId == 3 && x.BillDate.Year == 2017 && x.BillDate.Month == 03).Sum(x => (Decimal?)(x.Amount));
            ViewBag.PropertyTaxAmount0417 = bills.Where(x => x.BillTypeId == 3 && x.BillDate.Year == 2017 && x.BillDate.Month == 04).Sum(x => (Decimal?)(x.Amount));
            ViewBag.PropertyTaxAmount0517 = bills.Where(x => x.BillTypeId == 3 && x.BillDate.Year == 2017 && x.BillDate.Month == 05).Sum(x => (Decimal?)(x.Amount));
            ViewBag.PropertyTaxAmount0617 = bills.Where(x => x.BillTypeId == 3 && x.BillDate.Year == 2017 && x.BillDate.Month == 06).Sum(x => (Decimal?)(x.Amount));
            ViewBag.PropertyTaxAmount0717 = bills.Where(x => x.BillTypeId == 3 && x.BillDate.Year == 2017 && x.BillDate.Month == 07).Sum(x => (Decimal?)(x.Amount));
            ViewBag.RentAmount0616 = bills.Where(x => x.BillTypeId == 4 && x.BillDate.Year == 2016 && x.BillDate.Month == 06).Sum(x => (Decimal?)(x.Amount));
            ViewBag.RentAmount0716 = bills.Where(x => x.BillTypeId == 4 && x.BillDate.Year == 2016 && x.BillDate.Month == 07).Sum(x => (Decimal?)(x.Amount));
            ViewBag.RentAmount0816 = bills.Where(x => x.BillTypeId == 4 && x.BillDate.Year == 2016 && x.BillDate.Month == 08).Sum(x => (Decimal?)(x.Amount));
            ViewBag.RentAmount0916 = bills.Where(x => x.BillTypeId == 4 && x.BillDate.Year == 2016 && x.BillDate.Month == 09).Sum(x => (Decimal?)(x.Amount));
            ViewBag.RentAmount1016 = bills.Where(x => x.BillTypeId == 4 && x.BillDate.Year == 2016 && x.BillDate.Month == 10).Sum(x => (Decimal?)(x.Amount));
            ViewBag.RentAmount1116 = bills.Where(x => x.BillTypeId == 4 && x.BillDate.Year == 2016 && x.BillDate.Month == 11).Sum(x => (Decimal?)(x.Amount));
            ViewBag.RentAmount1216 = bills.Where(x => x.BillTypeId == 4 && x.BillDate.Year == 2016 && x.BillDate.Month == 12).Sum(x => (Decimal?)(x.Amount));
            ViewBag.RentAmount0117 = bills.Where(x => x.BillTypeId == 4 && x.BillDate.Year == 2017 && x.BillDate.Month == 01).Sum(x => (Decimal?)(x.Amount));
            ViewBag.RentAmount0217 = bills.Where(x => x.BillTypeId == 4 && x.BillDate.Year == 2017 && x.BillDate.Month == 02).Sum(x => (Decimal?)(x.Amount));
            ViewBag.RentAmount0317 = bills.Where(x => x.BillTypeId == 4 && x.BillDate.Year == 2017 && x.BillDate.Month == 03).Sum(x => (Decimal?)(x.Amount));
            ViewBag.RentAmount0417 = bills.Where(x => x.BillTypeId == 4 && x.BillDate.Year == 2017 && x.BillDate.Month == 04).Sum(x => (Decimal?)(x.Amount));
            ViewBag.RentAmount0517 = bills.Where(x => x.BillTypeId == 4 && x.BillDate.Year == 2017 && x.BillDate.Month == 05).Sum(x => (Decimal?)(x.Amount));
            ViewBag.RentAmount0617 = bills.Where(x => x.BillTypeId == 4 && x.BillDate.Year == 2017 && x.BillDate.Month == 06).Sum(x => (Decimal?)(x.Amount));
            ViewBag.RentAmount0717 = bills.Where(x => x.BillTypeId == 4 && x.BillDate.Year == 2017 && x.BillDate.Month == 07).Sum(x => (Decimal?)(x.Amount));
            ViewBag.CarInsuranceAmount0616 = bills.Where(x => x.BillTypeId == 5 && x.BillDate.Year == 2016 && x.BillDate.Month == 06).Sum(x => (Decimal?)(x.Amount));
            ViewBag.CarInsuranceAmount0716 = bills.Where(x => x.BillTypeId == 5 && x.BillDate.Year == 2016 && x.BillDate.Month == 07).Sum(x => (Decimal?)(x.Amount));
            ViewBag.CarInsuranceAmount0816 = bills.Where(x => x.BillTypeId == 5 && x.BillDate.Year == 2016 && x.BillDate.Month == 08).Sum(x => (Decimal?)(x.Amount));
            ViewBag.CarInsuranceAmount0916 = bills.Where(x => x.BillTypeId == 5 && x.BillDate.Year == 2016 && x.BillDate.Month == 09).Sum(x => (Decimal?)(x.Amount));
            ViewBag.CarInsuranceAmount1016 = bills.Where(x => x.BillTypeId == 5 && x.BillDate.Year == 2016 && x.BillDate.Month == 10).Sum(x => (Decimal?)(x.Amount));
            ViewBag.CarInsuranceAmount1116 = bills.Where(x => x.BillTypeId == 5 && x.BillDate.Year == 2016 && x.BillDate.Month == 11).Sum(x => (Decimal?)(x.Amount));
            ViewBag.CarInsuranceAmount1216 = bills.Where(x => x.BillTypeId == 5 && x.BillDate.Year == 2016 && x.BillDate.Month == 12).Sum(x => (Decimal?)(x.Amount));
            ViewBag.CarInsuranceAmount0117 = bills.Where(x => x.BillTypeId == 5 && x.BillDate.Year == 2017 && x.BillDate.Month == 01).Sum(x => (Decimal?)(x.Amount));
            ViewBag.CarInsuranceAmount0217 = bills.Where(x => x.BillTypeId == 5 && x.BillDate.Year == 2017 && x.BillDate.Month == 02).Sum(x => (Decimal?)(x.Amount));
            ViewBag.CarInsuranceAmount0317 = bills.Where(x => x.BillTypeId == 5 && x.BillDate.Year == 2017 && x.BillDate.Month == 03).Sum(x => (Decimal?)(x.Amount));
            ViewBag.CarInsuranceAmount0417 = bills.Where(x => x.BillTypeId == 5 && x.BillDate.Year == 2017 && x.BillDate.Month == 04).Sum(x => (Decimal?)(x.Amount));
            ViewBag.CarInsuranceAmount0517 = bills.Where(x => x.BillTypeId == 5 && x.BillDate.Year == 2017 && x.BillDate.Month == 05).Sum(x => (Decimal?)(x.Amount));
            ViewBag.CarInsuranceAmount0617 = bills.Where(x => x.BillTypeId == 5 && x.BillDate.Year == 2017 && x.BillDate.Month == 06).Sum(x => (Decimal?)(x.Amount));
            ViewBag.CarInsuranceAmount0717 = bills.Where(x => x.BillTypeId == 5 && x.BillDate.Year == 2017 && x.BillDate.Month == 07).Sum(x => (Decimal?)(x.Amount));
            ViewBag.HomeInsuranceAmount0616 = bills.Where(x => x.BillTypeId == 6 && x.BillDate.Year == 2016 && x.BillDate.Month == 06).Sum(x => (Decimal?)(x.Amount));
            ViewBag.HomeInsuranceAmount0716 = bills.Where(x => x.BillTypeId == 6 && x.BillDate.Year == 2016 && x.BillDate.Month == 07).Sum(x => (Decimal?)(x.Amount));
            ViewBag.HomeInsuranceAmount0816 = bills.Where(x => x.BillTypeId == 6 && x.BillDate.Year == 2016 && x.BillDate.Month == 08).Sum(x => (Decimal?)(x.Amount));
            ViewBag.HomeInsuranceAmount0916 = bills.Where(x => x.BillTypeId == 6 && x.BillDate.Year == 2016 && x.BillDate.Month == 09).Sum(x => (Decimal?)(x.Amount));
            ViewBag.HomeInsuranceAmount1016 = bills.Where(x => x.BillTypeId == 6 && x.BillDate.Year == 2016 && x.BillDate.Month == 10).Sum(x => (Decimal?)(x.Amount));
            ViewBag.HomeInsuranceAmount1116 = bills.Where(x => x.BillTypeId == 6 && x.BillDate.Year == 2016 && x.BillDate.Month == 11).Sum(x => (Decimal?)(x.Amount));
            ViewBag.HomeInsuranceAmount1216 = bills.Where(x => x.BillTypeId == 6 && x.BillDate.Year == 2016 && x.BillDate.Month == 12).Sum(x => (Decimal?)(x.Amount));
            ViewBag.HomeInsuranceAmount0117 = bills.Where(x => x.BillTypeId == 6 && x.BillDate.Year == 2017 && x.BillDate.Month == 01).Sum(x => (Decimal?)(x.Amount));
            ViewBag.HomeInsuranceAmount0217 = bills.Where(x => x.BillTypeId == 6 && x.BillDate.Year == 2017 && x.BillDate.Month == 02).Sum(x => (Decimal?)(x.Amount));
            ViewBag.HomeInsuranceAmount0317 = bills.Where(x => x.BillTypeId == 6 && x.BillDate.Year == 2017 && x.BillDate.Month == 03).Sum(x => (Decimal?)(x.Amount));
            ViewBag.HomeInsuranceAmount0417 = bills.Where(x => x.BillTypeId == 6 && x.BillDate.Year == 2017 && x.BillDate.Month == 04).Sum(x => (Decimal?)(x.Amount));
            ViewBag.HomeInsuranceAmount0517 = bills.Where(x => x.BillTypeId == 6 && x.BillDate.Year == 2017 && x.BillDate.Month == 05).Sum(x => (Decimal?)(x.Amount));
            ViewBag.HomeInsuranceAmount0617 = bills.Where(x => x.BillTypeId == 6 && x.BillDate.Year == 2017 && x.BillDate.Month == 06).Sum(x => (Decimal?)(x.Amount));
            ViewBag.HomeInsuranceAmount0717 = bills.Where(x => x.BillTypeId == 6 && x.BillDate.Year == 2017 && x.BillDate.Month == 07).Sum(x => (Decimal?)(x.Amount));
            ViewBag.OtherAmount0616 = bills.Where(x => x.BillTypeId == 7 && x.BillDate.Year == 2016 && x.BillDate.Month == 06).Sum(x => (Decimal?)(x.Amount));
            ViewBag.OtherAmount0716 = bills.Where(x => x.BillTypeId == 7 && x.BillDate.Year == 2016 && x.BillDate.Month == 07).Sum(x => (Decimal?)(x.Amount));
            ViewBag.OtherAmount0816 = bills.Where(x => x.BillTypeId == 7 && x.BillDate.Year == 2016 && x.BillDate.Month == 08).Sum(x => (Decimal?)(x.Amount));
            ViewBag.OtherAmount0916 = bills.Where(x => x.BillTypeId == 7 && x.BillDate.Year == 2016 && x.BillDate.Month == 09).Sum(x => (Decimal?)(x.Amount));
            ViewBag.OtherAmount1016 = bills.Where(x => x.BillTypeId == 7 && x.BillDate.Year == 2016 && x.BillDate.Month == 10).Sum(x => (Decimal?)(x.Amount));
            ViewBag.OtherAmount1116 = bills.Where(x => x.BillTypeId == 7 && x.BillDate.Year == 2016 && x.BillDate.Month == 11).Sum(x => (Decimal?)(x.Amount));
            ViewBag.OtherAmount1216 = bills.Where(x => x.BillTypeId == 7 && x.BillDate.Year == 2016 && x.BillDate.Month == 12).Sum(x => (Decimal?)(x.Amount));
            ViewBag.OtherAmount0117 = bills.Where(x => x.BillTypeId == 7 && x.BillDate.Year == 2017 && x.BillDate.Month == 01).Sum(x => (Decimal?)(x.Amount));
            ViewBag.OtherAmount0217 = bills.Where(x => x.BillTypeId == 7 && x.BillDate.Year == 2017 && x.BillDate.Month == 02).Sum(x => (Decimal?)(x.Amount));
            ViewBag.OtherAmount0317 = bills.Where(x => x.BillTypeId == 7 && x.BillDate.Year == 2017 && x.BillDate.Month == 03).Sum(x => (Decimal?)(x.Amount));
            ViewBag.OtherAmount0417 = bills.Where(x => x.BillTypeId == 7 && x.BillDate.Year == 2017 && x.BillDate.Month == 04).Sum(x => (Decimal?)(x.Amount));
            ViewBag.OtherAmount0517 = bills.Where(x => x.BillTypeId == 7 && x.BillDate.Year == 2017 && x.BillDate.Month == 05).Sum(x => (Decimal?)(x.Amount));
            ViewBag.OtherAmount0617 = bills.Where(x => x.BillTypeId == 7 && x.BillDate.Year == 2017 && x.BillDate.Month == 06).Sum(x => (Decimal?)(x.Amount));
            ViewBag.OtherAmount0717 = bills.Where(x => x.BillTypeId == 7 && x.BillDate.Year == 2017 && x.BillDate.Month == 07).Sum(x => (Decimal?)(x.Amount));

            ViewBag.VacationAmount0616 = bills.Where(x => x.BillTypeId == 8 && x.BillDate.Year == 2016 && x.BillDate.Month == 06).Sum(x => (Decimal?)(x.Amount));
            ViewBag.VacationAmount0716 = bills.Where(x => x.BillTypeId == 8 && x.BillDate.Year == 2016 && x.BillDate.Month == 07).Sum(x => (Decimal?)(x.Amount));
            ViewBag.VacationAmount0816 = bills.Where(x => x.BillTypeId == 8 && x.BillDate.Year == 2016 && x.BillDate.Month == 08).Sum(x => (Decimal?)(x.Amount));
            ViewBag.VacationAmount0916 = bills.Where(x => x.BillTypeId == 8 && x.BillDate.Year == 2016 && x.BillDate.Month == 09).Sum(x => (Decimal?)(x.Amount));
            ViewBag.VacationAmount1016 = bills.Where(x => x.BillTypeId == 8 && x.BillDate.Year == 2016 && x.BillDate.Month == 10).Sum(x => (Decimal?)(x.Amount));
            ViewBag.VacationAmount1116 = bills.Where(x => x.BillTypeId == 8 && x.BillDate.Year == 2016 && x.BillDate.Month == 11).Sum(x => (Decimal?)(x.Amount));
            ViewBag.VacationAmount1216 = bills.Where(x => x.BillTypeId == 8 && x.BillDate.Year == 2016 && x.BillDate.Month == 12).Sum(x => (Decimal?)(x.Amount));
            ViewBag.VacationAmount0117 = bills.Where(x => x.BillTypeId == 8 && x.BillDate.Year == 2017 && x.BillDate.Month == 01).Sum(x => (Decimal?)(x.Amount));
            ViewBag.VacationAmount0217 = bills.Where(x => x.BillTypeId == 8 && x.BillDate.Year == 2017 && x.BillDate.Month == 02).Sum(x => (Decimal?)(x.Amount));
            ViewBag.VacationAmount0317 = bills.Where(x => x.BillTypeId == 8 && x.BillDate.Year == 2017 && x.BillDate.Month == 03).Sum(x => (Decimal?)(x.Amount));
            ViewBag.VacationAmount0417 = bills.Where(x => x.BillTypeId == 8 && x.BillDate.Year == 2017 && x.BillDate.Month == 04).Sum(x => (Decimal?)(x.Amount));
            ViewBag.VacationAmount0517 = bills.Where(x => x.BillTypeId == 8 && x.BillDate.Year == 2017 && x.BillDate.Month == 05).Sum(x => (Decimal?)(x.Amount));
            ViewBag.VacationAmount0617 = bills.Where(x => x.BillTypeId == 8 && x.BillDate.Year == 2017 && x.BillDate.Month == 06).Sum(x => (Decimal?)(x.Amount));
            ViewBag.VacationAmount0717 = bills.Where(x => x.BillTypeId == 8 && x.BillDate.Year == 2017 && x.BillDate.Month == 07).Sum(x => (Decimal?)(x.Amount));
            
            DateTime date = DateTime.Now;
            decimal? amount;
            decimal newAverage, oldAverage, beta;
            List<decimal> predictions1 = new List<decimal>();
            List<decimal> predictions2 = new List<decimal>();
            int i = 1;

            for (; i < 9; i++)
            {
                newAverage = averageOfYear(date.Month, date.Year, i);
                oldAverage = averageOfYear(date.Month, date.Year - 1, i);
                amount = bills.Where(b => b.BillDate.Month == date.Month && b.BillDate.Year == date.Year - 1 && b.BillTypeId == i).Sum(b => (decimal?)(b.Amount));
                beta = 0;

                if (amount != null && oldAverage != 0)
                    beta = ((decimal)amount) / oldAverage;

                predictions1.Add(beta * newAverage);
            }
            ViewBag.Predictions1 = predictions1;

            for (i = 1; i < 9; i++)
            {
                newAverage = averageOfYear2(date.Month + 1, date.Year, i, predictions1[i - 1]);
                oldAverage = averageOfYear(date.Month + 1, date.Year - 1, i);
                if ((date.Month + 1) == 13)
                    amount = bills.Where(b => b.BillDate.Month == 1 && b.BillDate.Year == date.Year - 1 && b.BillTypeId == i).Sum(b => (decimal?)(b.Amount));
                else amount = bills.Where(b => b.BillDate.Month == date.Month + 1 && b.BillDate.Year == date.Year - 1 && b.BillTypeId == i).Sum(b => (decimal?)(b.Amount));
                beta = 0;

                if (amount != null && oldAverage != 0)
                    beta = ((decimal)amount) / oldAverage;

                predictions2.Add(beta * newAverage);
            }

            ViewBag.Predictions2 = predictions2;


            return View();

        }

        public decimal averageOfYear(int month, int year, int billTypeID)
        {
            var userId = User.Identity.GetUserId();
            var bills = db.Bills.Include(b => b.AspNetUser).Where(a => a.UserId == userId);
            decimal? amount;
            decimal sum = 0;
            decimal counter = 0;
            int tempMonth = month;
            if (tempMonth == 13)
                tempMonth = 1;
            month--;
            if (month < 1)
            {
                month = 12;
                year--;
            }

            while (month != tempMonth)
            {
                amount = bills.Where(b => b.BillDate.Month == month && b.BillDate.Year == year && b.BillTypeId == billTypeID).Sum(b => (decimal?)(b.Amount));

                if (amount != null)
                {
                    sum = sum + (decimal)amount;
                    counter++;
                }

                month--;

                if (month < 1)
                {
                    month = 12;
                    year--;
                }
            }
            if (counter != 0)
                return sum / counter;
            else return 0;

        }
        public decimal averageOfYear2(int month, int year, int billTypeID, decimal predictions1)
        {
            var userId = User.Identity.GetUserId();
            var bills = db.Bills.Include(b => b.AspNetUser).Where(a => a.UserId == userId);
            decimal? amount;
            decimal sum = 0;
            decimal counter = 0;
            int tempMonth = month + 1;

            if (tempMonth == 13)
                tempMonth = 1;
            if (tempMonth == 14)
                tempMonth = 2;
            if (predictions1 != 0)
            {
                sum = predictions1;
                counter++;
            }

            month = month - 2;
            if (month < 1)
            {
                month = 12;
                year--;
            }

            while (month != tempMonth)
            {
                amount = bills.Where(b => b.BillDate.Month == month && b.BillDate.Year == year && b.BillTypeId == billTypeID).Sum(b => (decimal?)(b.Amount));

                if (amount != null)
                {
                    sum = sum + (decimal)amount;
                    counter++;
                }

                month--;

                if (month < 1)
                {
                    month = 12;
                    year--;
                }
            }
            if (counter != 0)
                return sum / counter;
            else return 0;
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

        // GET: Bills/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        // GET: Bills/Create
        public ActionResult Create()
        {
            ViewBag.BillTypes = new SelectList(db.BillTypes, "Id", "TypeName");
            return View();
        }

        // POST: Bills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BillDate,Amount,BillTypeId,UserId")] Bill bill, HttpPostedFileBase Content)
        {
            var file = Request.Files[0];

            MemoryStream target = new MemoryStream();
            file.InputStream.CopyTo(target);
            bill.Content = target.ToArray();

            bill.CreationDate = DateTime.Now;
            bill.UserId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                db.Bills.Add(bill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", bill.UserId);
            return View(bill);
        }

        // GET: Bills/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", bill.UserId);
            return View(bill);
        }

        // POST: Bills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BillDate,Amount,BillTypeId,UserId")] Bill bill, HttpPostedFileBase Content)
        {
            MemoryStream target = new MemoryStream();
            Content.InputStream.CopyTo(target);
            bill.Content = target.ToArray();

            if (ModelState.IsValid)
            {
                db.Entry(bill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", bill.UserId);
            return View(bill);
        }

        // GET: Bills/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }

            return View(bill);
        }

        // POST: Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bill bill = db.Bills.Find(id);
            db.Bills.Remove(bill);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Bills/Average/5
        public ActionResult Average()
        {
            var userId = User.Identity.GetUserId();
            ViewBag.Total = db.Bills.Where(x => x.UserId == userId).Sum(x => x.Amount);
            return View();
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
