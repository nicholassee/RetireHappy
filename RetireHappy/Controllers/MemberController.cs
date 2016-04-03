using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RetireHappy.Models;
using RetireHappy.DAL;

namespace RetireHappy.Controllers
{
    public class MemberController : Controller
    {
        private RetireHappyContext db = new RetireHappyContext();
        private MemberGateway memberGateway = new MemberGateway();
        private ExpenditureGateway expenditureGateway = new ExpenditureGateway();
        private AvgExpenditureGateway avgExpenditureGateway = new AvgExpenditureGateway();

        // GET: Member/Register
        public ActionResult Register()
        {
            ViewBag.mId = new SelectList(db.ExpenditureLists, "mId", "item1");
            return View();
        }

        // POST: Member/Register
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "mId,userName,password")] Member member)
        {
            if (ModelState.IsValid)
            {
                Member tempMember = ((MemberGateway)memberGateway).SearchByUsername(member.userName);
                // default values if no result found
                if (tempMember.mId == 0 && tempMember.userName == null && tempMember.password == null)
                {
                    memberGateway.Insert(member);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Username taken!";
                    return View("Register");
                }
                
            }

            ViewBag.mId = new SelectList(db.ExpenditureLists, "mId", "item1", member.mId);
            return View(member);
        }

        // GET: Member/Login
        public ActionResult Login()
        {
            ViewBag.mId = new SelectList(db.ExpenditureLists, "mId", "item1");
            return View();
        }

        // POST: Member/Login
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "mId,userName,password")] Member member)
        {
            if (ModelState.IsValid)
            {
                Member tempMember = ((MemberGateway)memberGateway).verifyCredential(member.userName, member.password);
                if (tempMember.mId == 0 && tempMember.userName == null && tempMember.password == null)
                {
                    ViewBag.Error = "Incorrect username or password!";
                    return View("Login");
                }
                else
                {
                    Session["userType"] = "Member";
                    Session["memberId"] = tempMember.mId;
                    Session["username"] = tempMember.userName;
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.mId = new SelectList(db.ExpenditureLists, "mId", "item1", member.mId);
            return View(member);
        }

        // GET: Member/Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }


        public ActionResult MemberExpenditureList()
        {

            ExpenditureList expenditureList = ((ExpenditureGateway)expenditureGateway).checkExistingExpList((int)Session["memberId"]);
            if (expenditureList.mId != 0)
            {
                IEnumerable<AvgExpenditure> avgExpenditure = ((AvgExpenditureGateway)avgExpenditureGateway).MatchExpenditureList(expenditureList.mId);
                ViewBag.total = expenditureList.calcTotalExpenditure(avgExpenditure);
                return View(avgExpenditure.ToList());
            }
            ViewBag.EmptyList = "true";
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
