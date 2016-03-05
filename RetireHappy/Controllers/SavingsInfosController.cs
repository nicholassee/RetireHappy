using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RetireHappy.DAL;
using RetireHappy.Models;

namespace RetireHappy.Controllers
{
    public class SavingsInfosController : Controller
    {
        private RetireHappyContext db = new RetireHappyContext();
        private SavingInfosGateway savingInfosGateway = new SavingInfosGateway();

        // GET: SavingsInfos
        public ActionResult Index()
        {
            return View(db.SavingsInfos.ToList());
        }

        // method to receive and compute savingsinfo
        // GET: SavingsInfos/computeSavginsInfo
        public ActionResult ComputeSavingsInfo()
        {
            float avgMonExpenditure = (float)Session["avgMonExpenditure"];
            float monIncome = (float)Session["monIncome"];
            // assuming a fixed rate of 2.4% inflation rate
            float inflationRate = ((float)Session["inflationRate"] / 100) + 1;
            int currentAge = (int)Session["age"];
            int expRetAge = (int)Session["expRetAge"];
            int retDuration = (int)Session["retDuration"];
            int limit = expRetAge + retDuration;
            float curSavingAmt = (float)Session["curSavingAmt"];
            float desiredMonRetInc = (float)Session["desiredMonRetInc"];
            SavingsInfo savingsInfo = new SavingsInfo();

            //float pvAsOfRetAge = computeRiskLevel(expRetAge, currentAge, desiredMonRetInc, inflationRate, retDuration);

            // calculate PV as of current age
            //float pvAsOfCurAge = (float)(pvAsOfRetAge / Math.Pow((1 + 0.01), (expRetAge - currentAge)));
            float calcRetSavings = savingsInfo.calculate(expRetAge, currentAge, desiredMonRetInc, inflationRate, retDuration);
            // calculate monthly savings using PV as of current age
            //float calcRetSavings = pvAsOfCurAge / ((expRetAge - currentAge) * 12);

            // to calculate risk level using inflation adjusted current monthly savings
            float riskLevelDiff = savingsInfo.computeRiskLevel(calcRetSavings, curSavingAmt);
            //float riskLevelDiff = ((calcRetSavings - curSavingAmt) / curSavingAmt) * 100;


            if (riskLevelDiff <= 0)
            {
                savingsInfo.riskLevel = "Low Risk";
                ViewBag.riskClass = "panel-green";
            }
            else if (riskLevelDiff < 20)
            {
                savingsInfo.riskLevel = "Medium Risk";
                ViewBag.riskClass = "panel-yellow";
            }
            else
            {
                savingsInfo.riskLevel = "High Risk";
                ViewBag.riskClass = "panel-red";
            }
            savingsInfo.diffPercent = (float)Math.Round(riskLevelDiff, 2);
            savingsInfo.calcRetSavings = (float)Math.Round(calcRetSavings, 2);
            savingsInfo.Id = (int)Session["Id"];
            savingsInfo.expPercent = (float)Math.Round( (avgMonExpenditure / monIncome) * 100, 2);
            savingInfosGateway.Insert(savingsInfo);
            return View(savingsInfo);
        }

        // GET: SavingsInfos/calculatorStep4/5
        public ActionResult calculatorStep4(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavingsInfo savingsInfo = savingInfosGateway.SelectById(id);
            if (savingsInfo == null)
            {
                return HttpNotFound();
            }
            return View(savingsInfo);
        }

        // GET: SavingsInfos/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavingsInfo savingsInfo = db.SavingsInfos.Find(id);
            if (savingsInfo == null)
            {
                return HttpNotFound();
            }
            return View(savingsInfo);
        }

        // GET: SavingsInfos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SavingsInfos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "uId,calcRetSavings,savePercent,riskLevel,expPercent,diffPercent")] SavingsInfo savingsInfo)
        {
            if (ModelState.IsValid)
            {
                db.SavingsInfos.Add(savingsInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(savingsInfo);
        }

        // GET: SavingsInfos/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavingsInfo savingsInfo = db.SavingsInfos.Find( Convert.ToInt32(id) );
            if (savingsInfo == null)
            {
                return HttpNotFound();
            }
            return View(savingsInfo);
        }

        // POST: SavingsInfos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "uId,calcRetSavings,savePercent,riskLevel,expPercent,diffPercent")] SavingsInfo savingsInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(savingsInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(savingsInfo);
        }

        // GET: SavingsInfos/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavingsInfo savingsInfo = db.SavingsInfos.Find(id);
            if (savingsInfo == null)
            {
                return HttpNotFound();
            }
            return View(savingsInfo);
        }

        // POST: SavingsInfos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SavingsInfo savingsInfo = db.SavingsInfos.Find(id);
            db.SavingsInfos.Remove(savingsInfo);
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
