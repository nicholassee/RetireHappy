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
    public class UsersController : Controller
    {
        private RetireHappyContext db = new RetireHappyContext();
        private UserGateway userGateway = new UserGateway();

        // GET: Users/calculatorStep1
        public ActionResult CalculatorStep1()
        {
            return View();
        }

        // POST: Users/calculatorStep1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CalculatorStep1([Bind(Include = "gender")] UserProfile userProfile)
        {
            //If no radio button is selected
            if (userProfile.gender == null)
            {
                //Return the same page with error massage
                return View();
            }
            else
            {
                Session["gender"] = userProfile.gender;
            }

            return RedirectToAction("CalculatorStep2");
        }

        // GET: Users/CalculatorStep2
        public ActionResult CalculatorStep2()
        {
            return View();
        }

        // POST: Users/CalculatorStep2
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CalculatorStep2([Bind(Include = "age,monIncome,curSavingAmt,avgMonExpenditure,inflationRate, ifUseAvgExp")] UserProfile userProfile)
        {
            if (userProfile.age == null)
            {
                //Return the same page with error massage
                return View();
            }
            else
            {
                Session["age"] = userProfile.age;
                Session["monIncome"] = userProfile.monIncome;
                Session["curSavingAmt"] = userProfile.curSavingAmt;
                Session["avgMonExpenditure"] = userProfile.avgMonExpenditure;
                Session["inflationRate"] = userProfile.inflationRate;
                Session["ifUseAvgExp"] = userProfile.ifUseAvgExp;
                if (Convert.ToBoolean(userProfile.ifUseAvgExp) == false)
                {
                    return RedirectToAction("CalculatorStep3");
                }
                else
                {
                    return RedirectToAction("Index", "Expenditure");
                }
            }
        }

        // GET: Users/CalculatorStep3
        public ActionResult CalculatorStep3()
        {
            return View();
        }

        // POST: Users/CalculatorStep3
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CalculatorStep3([Bind(Include = "Id, desiredMonRetInc,expRetAge,retDuration")] UserProfile userProfile)
        {
            //last step of user inputs, insert into gateway
            Session["desiredMonRetInc"] = userProfile.desiredMonRetInc;
            Session["expRetAge"] = userProfile.expRetAge;
            Session["retDuration"] = userProfile.retDuration;
            userProfile.age = (int)Session["age"];
            userProfile.gender = (string)Session["gender"];
            userProfile.monIncome = (double)Session["monIncome"];
            userProfile.avgMonExpenditure = (double)Session["avgMonExpenditure"];
            userProfile.curSavingAmt = (double)Session["curSavingAmt"];
            userProfile.desiredMonRetInc = (double)Session["desiredMonRetInc"];
            userProfile.inflationRate = (double)Session["inflationRate"];
            userProfile.timestamp = DateTime.Now;
            userProfile.ifUseAvgExp = (string)Session["ifUseAvgExp"];
            // ** ATTN JERLYN ** this check is not needed as both methods of expenditure stores in the same session variable hence
            // it will contain values
            //if (Convert.ToBoolean(userProfile.ifUseAvgExp) == true)
            //{
            // userProfile.avgMonExpenditure = (float)Session["avgMonExpenditure"];
            //}
            if(!string.IsNullOrEmpty((string)Session["userType"]))
            {
                userProfile.mId = (int)Session["memberId"];
                UserProfile tempUserProfile = userGateway.checkIfExistUserProfile((int)userProfile.mId);
                // default value for null object
                if (tempUserProfile.Id == 0)
                {
                    // if no existing user profile for a member, insert new user profile
                    userGateway.Insert(userProfile);
                }
                else
                {
                    // if exist user profile, update.
                    // have to set back to the same Id before update
                    Session["updateExisting"] = "true";
                    userProfile.Id = tempUserProfile.Id;
                    userGateway.updateUserProfile(userProfile);
                }
            }
            else
            {
                userGateway.Insert(userProfile);
            }
            
            
            Session["Id"] = userProfile.Id;
            return RedirectToAction("ComputeSavingsInfo", "SavingsInfos");
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
