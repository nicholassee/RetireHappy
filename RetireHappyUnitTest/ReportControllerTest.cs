using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using RetireHappy.Controllers;

namespace RetireHappyUnitTest
{
    [TestClass]
    public class ReportControllerTest
    {
        [TestMethod]
        public void ReturnsTableView()
        {
            ReportController controllerUnderTest = new ReportController();
            var result = controllerUnderTest.Table() as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }
    }
}
