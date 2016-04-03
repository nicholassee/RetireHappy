using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RetireHappy.Controllers
{
    public class MemberCheckingController : Controller
    {
        // GET: MemberChecking
        // GET: Base
        public Boolean checkIfMember()
        {
            if (string.IsNullOrEmpty((string)Session["userType"]))
            {
                return false;
            }
            else
                return true;
        }
    }
}