using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RetireHappy.Models;
using RetireHappy.DAL;
using Excel = Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using System.IO;
using System;
using NPOI.SS.UserModel;

namespace RetireHappy.Controllers
{
    public class AdminController : Controller
    {
        private RetireHappyContext db = new RetireHappyContext();
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
            if(uploadFile == null || uploadFile.ContentLength == 0)
            {
                ViewBag.Error = "Please select a excel file";
                return View("Upload");
            }
            else
            {
                if(uploadFile.FileName.EndsWith("xls") || uploadFile.FileName.EndsWith("xlsx"))
                {
                    string path = Server.MapPath("~/Content/" + uploadFile.FileName);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    uploadFile.SaveAs(path);
                    IWorkbook wb = WorkbookFactory.Create(new FileStream(
                       Path.GetFullPath(path),
                       FileMode.Open, FileAccess.Read,
                       FileShare.ReadWrite));
                    ISheet ws = wb.GetSheetAt(3);
                    string tempCategory = "";
                    avgExpenditureGateway.deleteAll();
                    // assuming format is fixed, row starts from 9 and column for category and type is in 0 and figure is in column 1
                    for (int row = 8; row <= ws.LastRowNum; row++)
                    {
                        string temp = ws.GetRow(row).GetCell(0).ToString();
                        if (string.IsNullOrEmpty(temp))
                        {

                        }
                        else
                        {
                            //According to format of dataset
                            if (!char.IsWhiteSpace(temp[3]))
                            {
                                tempCategory = temp;
                            }
                            //According to format of dataset
                            else if (temp.Length > 4)
                            {
                                if (char.IsWhiteSpace(temp[3]) == true && char.IsWhiteSpace(temp[4]) == false && ws.GetRow(row).GetCell(1).ToString() != "-")
                                {
                                    AvgExpenditure avgExpenditure = new AvgExpenditure();
                                    avgExpenditure.type = temp.Trim();
                                    avgExpenditure.category = tempCategory.Trim();
                                    avgExpenditure.recordYear = System.DateTime.Now;
                                    avgExpenditure.avgAmount = double.Parse(ws.GetRow(row).GetCell(1).ToString());
                                    avgExpenditureGateway.Insert(avgExpenditure);
                                }
                            }
                        }

                    }
                    ViewBag.SuccessMsg = "Data has been updated";
                }
                else
                {
                    ViewBag.Error = "File type is unsupported";
                    return View("Upload");
                }
            }
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
