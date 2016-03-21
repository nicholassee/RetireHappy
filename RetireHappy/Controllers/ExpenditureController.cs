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
        private RetireHappyContext db = new RetireHappyContext();

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
            double totalAmt = newExpObj.calcTotalExpenditure();
            //store in session var
            Session["avgMonExpenditure"] = 0;
            Session["avgMonExpenditure"] = totalAmt;

            // only save expenditure list if member
            if (!string.IsNullOrEmpty((string)Session["userType"]))
            {
                //push to db
                newExpObj.mId = (int)Session["memberId"];
                ExpenditureList tempExpList = expGW.checkExistingExpList(newExpObj.mId);
                //default value for null obj
                if(tempExpList.mId == 0)
                {
                    // if no existing expenditure list, insert new
                    expGW.Insert(newExpObj);
                }
                else
                {
                    // else update old list
                    expGW.updateExpenditureList(newExpObj);
                }
                
            }
            // if not a member, not required to save expenditure list
            
            return RedirectToAction("CalculatorStep3", "Users");
        }
    }
}
