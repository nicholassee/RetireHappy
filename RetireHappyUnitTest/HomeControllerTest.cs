using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RetireHappy.Controllers;

namespace RetireHappyUnitTest
{
    [TestClass]
    public class HomeControllerTest : Controller
    {
        [TestMethod]
        public void ReturnsIndexView()
        {
            HomeController controllerUnderTest = new HomeController();
            var result = controllerUnderTest.Index() as ViewResult;
            Assert.AreEqual("", result.ViewName);
            
        }
    }
}
