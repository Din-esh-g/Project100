using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Web.CodeGeneration;
using Project100.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using Project100.Models;

namespace UnitTestProject5
{
    [TestClass]
    public class DepositTest
    {
        private readonly ILogger<HomeController> _logger;

        private readonly BankContext _context;

        [TestMethod]
 
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController(_logger);
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController(_logger);
            // Act
            ViewResult result = controller.About() as ViewResult;
            // Assert
            Assert.AreEqual("Your application description page.", result.ViewData["ErrorMessage"]);
        }
        

        [TestMethod]
        public void Contact_Home()
        {
            // Arrange
            HomeController controller = new HomeController(_logger);
            // Act
            ViewResult result = controller.Contact() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void Business_Deposit_ID()
         {
           BusinessesController controller = new BusinessesController(_context);

            int id = 1;
            ViewResult result = controller.Deposit(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void Withdraw_Business_ID()
        {
            BusinessesController controller = new BusinessesController(_context);

            int id = 1;
            ViewResult result = controller.Withdraw(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result);

        }


        [TestMethod]
        public void Transfer_Business_ID()
        {
            BusinessesController controller = new BusinessesController(_context);

            int id = 1;
            ViewResult result = controller.Transfer(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result);

        }


        [TestMethod]
        public void Checking_Deposit_ID()
        {
            CheckingsController controller = new CheckingsController(_context);

            int id = 1;
            ViewResult result = controller.Deposit(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void Checking_Withdraws_ID()
        {
            CheckingsController controller = new CheckingsController(_context);

            int id = 1;
            ViewResult result = controller.Withdraw(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result);

        }


        [TestMethod]
        public void Checking_Transfer_ID()
        {
            CheckingsController controller = new CheckingsController(_context);

            int id = 1;
            ViewResult result = controller.Transfer(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result);

        }







    }
}
