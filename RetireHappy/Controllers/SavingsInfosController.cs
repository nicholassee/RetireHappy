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

        // method to receive and compute savingsinfo
        // GET: SavingsInfos/computeSavginsInfo
        public ActionResult ComputeSavingsInfo()
        {
            double avgMonExpenditure = (double)Session["avgMonExpenditure"];
            double monIncome = (double)Session["monIncome"];
            // assuming a fixed rate of 2.4% inflation rate
            double inflationRate = ((double)Session["inflationRate"] / 100) + 1;
            int currentAge = (int)Session["age"];
            int expRetAge = (int)Session["expRetAge"];
            int retDuration = (int)Session["retDuration"];
            int limit = expRetAge + retDuration;
            double curSavingAmt = (double)Session["curSavingAmt"];
            double desiredMonRetInc = (double)Session["desiredMonRetInc"];
            SavingsInfo savingsInfo = new SavingsInfo();

            //float pvAsOfRetAge = computeRiskLevel(expRetAge, currentAge, desiredMonRetInc, inflationRate, retDuration);

            // calculate PV as of current age
            //float pvAsOfCurAge = (float)(pvAsOfRetAge / Math.Pow((1 + 0.01), (expRetAge - currentAge)));
            double calcRetSavings = savingsInfo.calculate(expRetAge, currentAge, desiredMonRetInc, inflationRate, retDuration);
            // calculate monthly savings using PV as of current age
            //float calcRetSavings = pvAsOfCurAge / ((expRetAge - currentAge) * 12);

            // to calculate risk level using inflation adjusted current monthly savings
            double riskLevelDiff = savingsInfo.computeRiskLevel(calcRetSavings, curSavingAmt);
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
            savingsInfo.diffPercent = Math.Round(riskLevelDiff, 2);
            savingsInfo.calcRetSavings = Math.Round(calcRetSavings, 2);
            savingsInfo.Id = (int)Session["Id"];
            savingsInfo.expPercent = Math.Round( (avgMonExpenditure / monIncome) * 100, 2);
            // if userprofile exist for current session user
            if (!string.IsNullOrEmpty((string)Session["updateExisting"]))
            {
                //update savingsinfo related
                savingInfosGateway.updateSavingInfos(savingsInfo);
            }
            else
            {
                //insert new savingsinfo
                savingInfosGateway.Insert(savingsInfo);
            }
            return View(savingsInfo);
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
