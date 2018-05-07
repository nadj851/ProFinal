using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Enchere.Controllers;
using System.Web.Mvc;

namespace EnchereTest
{
    [TestClass]
    public class RolesControllerTest
    {
        [TestMethod]
        public void IndexObjets()
        {

            //Assert.IsNotNull(result);
            //Assert.AreEqual(result.RouteName, "DefaultApi");
            //Assert.AreEqual(result.RouteValues["id"], result.Content.Id);
            //Assert.AreEqual(result.Content.Name, item.Name);
            ////return View(db.Categories.ToList());
            //Arrange
            RolesController controller = new RolesController();
            //act
            ViewResult result = controller.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            // Assert.AreEqual("Index", result.ViewName);


        }

        //[TestMethod]
        //public void EditObjets()
        //{

        //    CategoriesController controller = new CategoriesController();
        //    //act
        //    ViewResult result = controller.Edit() as ViewResult;

        //    //Assert
        //    Assert.IsNotNull(result);
        //    // Assert.AreEqual("Index", result.ViewName);


        //}

        //[TestMethod]
        //public void DeleteObjets()
        //{

        //    CategoriesController controller = new CategoriesController();
        //    //act
        //    ViewResult result = controller.Delete() as ViewResult;

        //    //Assert
        //    Assert.IsNotNull(result);
        //    // Assert.AreEqual("Index", result.ViewName);


        //}

        [TestMethod]
        public void CreateObjets()
        {

            RolesController controller = new RolesController();
            //act
            ViewResult result = controller.Create() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            //Assert.AreEqual("Create", result.ViewName);


        }
    }
}
