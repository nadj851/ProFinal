using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Enchere.Controllers;
using System.Web.Mvc;

namespace EnchereTest
{
    [TestClass]
    public class ApplicationUsersControllerTest
    {
        [TestMethod]
        public void IndexUser()
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
        //public void EditUser()
        //{

        //    ApplicationUsersController controller = new ApplicationUsersController();
        //    //act
        //    ViewResult result = controller.Edit() as ViewResult;

        //    //Assert
        //    Assert.IsNotNull(result);
        //    // Assert.AreEqual("Index", result.ViewName);


        //}

        //[TestMethod]
        //public void DeleteUser()
        //{

        //    ApplicationUsersController  controller = new ApplicationUsersController();
        //    //act
        //    ViewResult result = controller.Delete() as ViewResult;

        //    //Assert
        //    Assert.IsNotNull(result);
        //    // Assert.AreEqual("Index", result.ViewName);


        //}

        [TestMethod]
        public void CreateUser()
        {

            ApplicationUsersController controller = new ApplicationUsersController();
            //act
            ViewResult result = controller.Create() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            //Assert.AreEqual("Create", result.ViewName);


        }

    }
}
