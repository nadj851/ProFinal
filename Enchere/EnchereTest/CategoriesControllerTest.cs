using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using Enchere.Controllers;

namespace EnchereTest
{
    [TestClass]
    public class CategoriesControllerTest
    {
        [TestMethod]        
        public void IndexCategorie()
        {

            //Assert.IsNotNull(result);
            //Assert.AreEqual(result.RouteName, "DefaultApi");
            //Assert.AreEqual(result.RouteValues["id"], result.Content.Id);
            //Assert.AreEqual(result.Content.Name, item.Name);
            ////return View(db.Categories.ToList());
            //Arrange
            CategoriesController controller = new CategoriesController();
            //act
            ViewResult result = controller.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            // Assert.AreEqual("Index", result.ViewName);


        }

        //[TestMethod]
        //public void EditCategorie()
        //{

        //    CategoriesController controller = new CategoriesController();
        //    //act
        //    ViewResult result = controller.Edit() as ViewResult;

        //    //Assert
        //    Assert.IsNotNull(result);
        //    // Assert.AreEqual("Index", result.ViewName);


        //}

        //[TestMethod]
        //public void DeleteCategorie()
        //{

        //    CategoriesController controller = new CategoriesController();
        //    //act
        //    ViewResult result = controller.Delete() as ViewResult;

        //    //Assert
        //    Assert.IsNotNull(result);
        //    // Assert.AreEqual("Index", result.ViewName);


        //}

        [TestMethod]
        public void CreateCategorie()
        {

            CategoriesController controller = new CategoriesController();
            //act
            ViewResult result = controller.Create() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            //Assert.AreEqual("Create", result.ViewName);

           
        }



    }
}

