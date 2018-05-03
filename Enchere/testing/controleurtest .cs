using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication1.Controllers;
using System.Web.Mvc;
using Enchere;


namespace testing
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Essai()
        {


            //Arrange
            HomeController controller = new HomeController();

            //act
            ViewResult result = controller.Essai() as ViewResult;


            //Assert
            // Assert.IsNotNull(result);
            Assert.AreEqual("bon", result.ViewBag.message);

            //Assert.IsNotNull(result);

        }
    }
}
