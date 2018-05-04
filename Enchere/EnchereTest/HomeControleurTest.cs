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
using Enchere.Models;

namespace EnchereTest
{
    [TestClass]
    public class HomeControleurTest
    {
        

        [TestMethod]
        public void Index()
        {

            //Assert.IsNotNull(result);
            //Assert.AreEqual(result.RouteName, "DefaultApi");
            //Assert.AreEqual(result.RouteValues["id"], result.Content.Id);
            //Assert.AreEqual(result.Content.Name, item.Name);
            ////return View(db.Categories.ToList());
            //Arrange
            HomeController controller = new HomeController();
             //act
            ViewResult result = controller.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
           // Assert.AreEqual("Index", result.ViewName);


        }

        //public void Details(int objetId)
        //public void Details()

        //{

        //    HomeController controller = new HomeController();
        //    //act
        //    ViewResult result = controller.Details() as ViewResult;

        //    //Assert
        //    Assert.IsNotNull(result);

        //}

        [TestMethod]
        public void Appliquer()
        {
            HomeController controller = new HomeController();
            //act
            ViewResult result = controller.Appliquer() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }


        //public void DetailEnchere()
        //{
        //    HomeController controller = new HomeController();
        //    //act
        //    ViewResult result = controller.DetailEnchere() as ViewResult;

        //    //Assert
        //    Assert.IsNotNull(result);
        //}



        //[TestMethod]
        //// GET: Roles/Edit/5
        //public void Edit()
        //{
        //    HomeController controller = new HomeController();
        //    //act
        //    ViewResult result = controller.Edit() as ViewResult;

        //    //Assert
        //    Assert.IsNotNull(result);
        //}



        [TestMethod]
        public void Delete()
        {
            HomeController controller = new HomeController();
            //act
            ViewResult result = controller.Appliquer() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }





        [TestMethod]
        public void Contact()
        {
            HomeController controller = new HomeController();
            //act
            ViewResult result = controller.Contact() as ViewResult;

            //Assert
            Assert.IsNotNull(result);


        }



        [TestMethod]
        public void Recherche()
        {
            HomeController controller = new HomeController();
            //act
            ViewResult result = controller.Recherche() as ViewResult;

            //Assert
            Assert.IsNotNull(result);

        }

       



    }
}
