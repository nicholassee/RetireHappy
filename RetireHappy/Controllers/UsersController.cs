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
        public ActionResult CalculatorStep1([Bind(Include = "gender")] User user)
        {
            //If no radio button is selected
            if (user.gender == null)
            {
                //Return the same page with error massage
                return View();
            }
            else
            {
                Session["gender"] = user.gender;
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
        public ActionResult CalculatorStep2([Bind(Include = "age,monIncome,curSavingAmt,avgMonExpenditure,inflationRate")] User user)
        {
            Session["age"] = user.age;
            Session["monIncome"] = user.monIncome;
            Session["curSavingAmt"] = user.curSavingAmt;
            Session["avgMonExpenditure"] = user.avgMonExpenditure;
            Session["inflationRate"] = user.inflationRate;
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
        public ActionResult CalculatorStep3([Bind(Include = "Id, desiredMonRetInc,expRetAge,retDuration")] User user)
        {
            //last step of user inputs, insert into gateway
            Session["desiredMonRetInc"] = user.desiredMonRetInc;
            Session["expRetAge"] = user.expRetAge;
            Session["retDuration"] = user.retDuration;
            user.age = (int)Session["age"];
            user.gender = (string)Session["gender"];
            user.monIncome = (float)Session["monIncome"];
            user.avgMonExpenditure = (float)Session["avgMonExpenditure"];
            user.curSavingAmt = (float)Session["curSavingAmt"];
            user.desiredMonRetInc = (float)Session["desiredMonRetInc"];
            user.inflationRate = (float)Session["inflationRate"];
            user.timestamp = DateTime.Now;

            userGateway.Insert(user);
            Session["Id"] = user.Id;
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
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
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
        public ActionResult Create([Bind(Include = "uId,age,gender,expRetAge,retDuration,monIncome,avgMonExpenditure,calcRetSavings,curSavingAmt,timestamp,password,userName")] User user)
        {
            if (ModelState.IsValid)
            {
                db.User.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
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
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            User user = db.User.Find(id);
            db.User.Remove(user);
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
