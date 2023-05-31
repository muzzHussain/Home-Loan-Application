using BusinessLogic;
using BusinessLogic.DTO;
using HomeLoanAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using Utility;

namespace TestProject2
{
    public class Tests
    {
        UserController _userController = null;
        [SetUp]
        public void Setup()
        {
            Mock<IBusinessLogic> mockBusinessLogic = new Mock<IBusinessLogic>();
            Mock<ILogger> mockLogger = new Mock<ILogger>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            _userController = new UserController(mockBusinessLogic.Object, mockLogger.Object, mockConfiguration.Object);
        }

        [Test]
        public async void Test1()
        {
            //Arrange
            AdvisorRegisterDTO inputLoginDTO = new AdvisorRegisterDTO();
            inputLoginDTO.EmailId = "test1@gmail.com";
            inputLoginDTO.Password = "Test12345*";
            //Act
            //IActionResult ans = await _userController.UserLoginActionTask(inputLoginDTO);
            //Assert
            //Assert.That(ans != null);
        }
    }
}