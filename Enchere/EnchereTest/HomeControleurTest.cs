using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Enchere;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using WebApplication1.Controllers; 

namespace EnchereTest
{
    [TestClass]
    public class HomeControleurTest
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
           Assert.AreEqual("bon",result.ViewBag.message);

            //Assert.IsNotNull(result);

        }



    }
}
