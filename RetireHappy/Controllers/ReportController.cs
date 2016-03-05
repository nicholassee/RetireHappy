using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RetireHappy.DAL;
using RetireHappy.Models;

namespace RetireHappy.Controllers
{
    public class ReportController : Controller
    {
        private RetireHappyContext db = new RetireHappyContext();
        private ReportMapper reportMapper = new ReportMapper();

        // GET: Report
        public ActionResult Index()
        {
            return View(reportMapper.ComputeAverage());
        }

        // GET: Report
        public ActionResult Table()
        {
            return View(reportMapper.ComputeAverage());
        }

        public ActionResult Test()
        {
            return View(reportMapper.ComputeAverage());
        }
    }
}