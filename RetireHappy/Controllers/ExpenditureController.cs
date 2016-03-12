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
    public class ExpenditureController : Controller
    {
        ExpenditureGateway expGW = new ExpenditureGateway();
        MemberGateway memberGateway = new MemberGateway();
        private RetireHappyDBEntities db = new RetireHappyDBEntities();

        // GET: Expenditure
        public ActionResult Index(String searchString)
        {
            var avgExpItemList = from item in db.AvgExpenditures select item;

            if (!String.IsNullOrEmpty(searchString))
            {
                avgExpItemList = avgExpItemList.Where(i => i.category.Contains(searchString)
                || i.type.Contains(searchString));

            }
            return View(avgExpItemList.ToList());
        }
        public ActionResult Tabulate(String idArr)
        {


            ExpenditureList newExpObj = new ExpenditureList();
            newExpObj.updateList(idArr);
            newExpObj.Id = 1;
            double totalAmt = newExpObj.calcTotalExpenditure();
            //store in session var
            Session["avgMonExpenditure"] = 0;
            Session["avgMonExpenditure"] = totalAmt;
            // check user type. Save obj in DB if user is member
            //for testing
            Session["userType"] = "Member";
            //Session["memberID"] = "100";

            if (Session["userType"].Equals("Member"))
            {
                //Member demoMember = new Member();
                //demoMember.Id = 1;
                //demoMember.password = "demo";
                //demoMember.userName = "demo";
                //memberGateway.Insert(demoMember);
                //update memberID col
                //newExpObj.updateMemberID(Session["memberID"].ToString());
                //push to db

                expGW.Insert(newExpObj);
                expGW.Save();
            }

            return RedirectToAction("CalculatorStep3", "Users");
        }
    }
}
