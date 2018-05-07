using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Enchere.Controllers;
using System.Web.Mvc;

namespace EnchereTest
{
    [TestClass]
    public class ObjetsControllerTest
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
            ObjetsController controller = new ObjetsController();
            //act
            ViewResult result = controller.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            // Assert.AreEqual("Index", result.ViewName);


        }

        //[TestMethod]
        //public void EditObjets()
        //{

        //    ObjetsController controller = new ObjetsController();
        //    //act
        //    ViewResult result = controller.Edit() as ViewResult;

        //    //Assert
        //    Assert.IsNotNull(result);
        //    // Assert.AreEqual("Index", result.ViewName);


        //}

        //[TestMethod]
        //public void DeleteObjets()
        //{

        //    ObjetsController controller = new ObjetsController();
        //    //act
        //    ViewResult result = controller.Delete() as ViewResult;

        //    //Assert
        //    Assert.IsNotNull(result);
        //    // Assert.AreEqual("Index", result.ViewName);


        //}

        [TestMethod]
        public void CreateObjets()
        {

            ObjetsController controller = new ObjetsController();
            //act
            ViewResult result = controller.Create() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            //Assert.AreEqual("Create", result.ViewName);


        }
    
    }
}
