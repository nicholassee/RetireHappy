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
        private RetireHappyDBEntities db = new RetireHappyDBEntities();
        private UserGateway userGateway = new UserGateway();

        // GET: Users
        public ActionResult Index()
        {
            return View(userGateway.SelectAll());
        }

        // GET: Users/selectGender
        public ActionResult selectGender()
        {
            return View();
        }

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
        public ActionResult CalculatorStep2([Bind(Include = "age,monIncome,curSavingAmt,avgMonExpenditure,inflationRate")] UserProfile userProfile)
        {
            Session["age"] = userProfile.age;
            Session["monIncome"] = userProfile.monIncome;
            Session["curSavingAmt"] = userProfile.curSavingAmt;
            Session["avgMonExpenditure"] = userProfile.avgMonExpenditure;
            Session["inflationRate"] = userProfile.inflationRate;
            return RedirectToAction("CalculatorStep3");
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
            // To fix entity framework bug of being unable to cater to multiple temp instance of userProfile model
            UserProfile newUserProfile = new UserProfile();
            newUserProfile.desiredMonRetInc = userProfile.desiredMonRetInc;
            newUserProfile.expRetAge = userProfile.expRetAge;
            newUserProfile.retDuration = userProfile.retDuration;
            newUserProfile.age = (int)Session["age"];
            newUserProfile.gender = (string)Session["gender"];
            newUserProfile.monIncome = (double)Session["monIncome"];
            newUserProfile.avgMonExpenditure = (double)Session["avgMonExpenditure"];
            newUserProfile.curSavingAmt = (double)Session["curSavingAmt"];
            newUserProfile.desiredMonRetInc = (double)Session["desiredMonRetInc"];
            newUserProfile.inflationRate = (double)Session["inflationRate"];
            newUserProfile.timestamp = DateTime.Now;

            userGateway.Insert(newUserProfile);
            Session["Id"] = newUserProfile.Id;
            //return RedirectToAction("Index");
            return RedirectToAction("ComputeSavingsInfo", "SavingsInfos");
        }

        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userProfile = db.UserProfiles.Find(id);
            if (userProfile == null)
            {
                return HttpNotFound();
            }
            return View(userProfile);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "uId,age,gender,expRetAge,retDuration,monIncome,avgMonExpenditure,calcRetSavings,curSavingAmt,timestamp,password,userName")] UserProfile userProfile)
        {
            if (ModelState.IsValid)
            {
                db.UserProfiles.Add(userProfile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userProfile);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userProfiles = db.UserProfiles.Find(id);
            if (userProfiles == null)
            {
                return HttpNotFound();
            }
            return View(userProfiles);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "uId,age,gender,expRetAge,retDuration,monIncome,avgMonExpenditure,calcRetSavings,curSavingAmt,timestamp,password,userName")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userProfile = db.UserProfiles.Find(id);
            if (userProfile == null)
            {
                return HttpNotFound();
            }
            return View(userProfile);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserProfile userProfile = db.UserProfiles.Find(id);
            db.UserProfiles.Remove(userProfile);
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
