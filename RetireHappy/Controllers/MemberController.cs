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

        // GET: Member
        public ActionResult Index()
        {
            var members = db.Members.Include(m => m.ExpenditureList);
            return View(members.ToList());
        }

        // GET: Member/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

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
                Member tempMember = memberGateway.SearchByUsername(member.userName);
                // default values if no result found
                if (tempMember.mId == 0 && tempMember.userName == null && tempMember.password == null)
                {
                    memberGateway.Insert(member);
                    return RedirectToAction("Index");
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

        // GET: Member/Create
        public ActionResult Login()
        {
            ViewBag.mId = new SelectList(db.ExpenditureLists, "mId", "item1");
            return View();
        }

        // POST: Member/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "mId,userName,password")] Member member)
        {
            if (ModelState.IsValid)
            {
                Member tempMember = memberGateway.verifyCredential(member.userName, member.password);
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
                    return RedirectToAction("Index");
                }
            }

            ViewBag.mId = new SelectList(db.ExpenditureLists, "mId", "item1", member.mId);
            return View(member);
        }

        // GET: Member/Create
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        // GET: Member/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            ViewBag.mId = new SelectList(db.ExpenditureLists, "mId", "item1", member.mId);
            return View(member);
        }

        // POST: Member/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mId,password,userName")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.mId = new SelectList(db.ExpenditureLists, "mId", "item1", member.mId);
            return View(member);
        }

        // GET: Member/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Member/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
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
