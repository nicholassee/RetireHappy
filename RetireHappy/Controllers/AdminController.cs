using System.Web.Mvc;
using RetireHappy.DAL;

namespace RetireHappy.Controllers
{
    public class AdminController : Controller
    {
        private RetireHappyContext db = new RetireHappyContext();
        private AvgExpenditureGateway avgExpenditureGateway = new AvgExpenditureGateway();

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Verify(string adminCode)
        {
            if(adminCode == "retireHappy123")
            {
                ViewBag.admin = "true";
            }
            else
            {
                ViewBag.admin = "false";
                ViewBag.AdminCodeError = "Admin code is incorrect, please contact RetireHappy for further assistance.";
            }
            return View("Upload");
        }

        [HttpPost]
        public ActionResult Sync()
        {
            bool result = avgExpenditureGateway.SyncDataset();
            if (result) {
                ViewBag.SuccessMsg = "Data has been updated";
            }
            else
            {
                ViewBag.Error = "Download link is down, please contact administrator";
            }
            return View("Upload");
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
