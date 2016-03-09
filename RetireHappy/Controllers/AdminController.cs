﻿using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RetireHappy.Models;
using RetireHappy.DAL;
using Excel = Microsoft.Office.Interop.Excel;

namespace RetireHappy.Controllers
{
    public class AdminController : Controller
    {
        private RetireHappyDBEntities db = new RetireHappyDBEntities();
        private AvgExpenditureGateway avgExpenditureGateway = new AvgExpenditureGateway();

        // GET: Admin
        public ActionResult Index()
        {
            return View(db.AvgExpenditures.ToList());
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase uploadFile)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            string path = Server.MapPath("~/Content/" + uploadFile.FileName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            uploadFile.SaveAs(path);
            // Read data from excel file
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(path);
            xlWorkSheet = xlWorkBook.Sheets[4];
            range = xlWorkSheet.UsedRange;
            for(int row = 5; row <= range.Rows.Count; row++)
            {
                if(((Excel.Range)range.Cells[row, 1]).Text == "FOOD AND NON-ALCOHOLIC BEVERAGES")
                {
                    AvgExpenditure avgExpenditure = new AvgExpenditure();
                    avgExpenditure.category = ((Excel.Range)range.Cells[row, 1]).Text;
                    avgExpenditure.avgAmount = double.Parse(((Excel.Range)range.Cells[row, 2]).Text);
                    avgExpenditure.count = 0;
                    avgExpenditure.recordYear = System.DateTime.Now;
                    avgExpenditureGateway.Insert(avgExpenditure);
                }
            }
            xlWorkBook.Close(true, null, null);
            xlApp.Quit();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
            return View();
        }

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AvgExpenditure avgExpenditure = db.AvgExpenditures.Find(id);
            if (avgExpenditure == null)
            {
                return HttpNotFound();
            }
            return View(avgExpenditure);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "eId,category,recordYear,avgAmount,count")] AvgExpenditure avgExpenditure)
        {
            if (ModelState.IsValid)
            {
                db.AvgExpenditures.Add(avgExpenditure);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(avgExpenditure);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AvgExpenditure avgExpenditure = db.AvgExpenditures.Find(id);
            if (avgExpenditure == null)
            {
                return HttpNotFound();
            }
            return View(avgExpenditure);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "eId,category,recordYear,avgAmount,count")] AvgExpenditure avgExpenditure)
        {
            if (ModelState.IsValid)
            {
                db.Entry(avgExpenditure).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(avgExpenditure);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AvgExpenditure avgExpenditure = db.AvgExpenditures.Find(id);
            if (avgExpenditure == null)
            {
                return HttpNotFound();
            }
            return View(avgExpenditure);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AvgExpenditure avgExpenditure = db.AvgExpenditures.Find(id);
            db.AvgExpenditures.Remove(avgExpenditure);
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