using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LVR_API.Controllers;
using LVR_API;
using Microsoft.AspNetCore.Mvc;
using Xunit;


namespace LVR_API.Tests
{
    public class LVRControllerTests
    {
        private readonly LVRController _controller;

        public LVRControllerTests()
        {
            _controller = new LVRController();
        }

        // Test: Successful LVR calculation
        [Fact]
        public void CalculateLvr_ValidInputs_ReturnsCorrectLvrPercentage()
        {
            // Arrange
            var request = new LvrRequest
            {
                PropertyValue = 100000,
                LoanAmount = 80000
            };
            // Act
            var result = _controller.CalculateLvr(request);
            // Assert
            var okResult = Assert.IsType<ActionResult<decimal>>(result);
            var lvrPercentage = Assert.IsType<OkObjectResult>(result.Result).Value;
            decimal expectedresult= 80.00M;
            Assert.Equal(expectedresult, lvrPercentage); // 80000 / 100000 = 80%
        }

        // Test: Invalid input (zero or less than zero)
        [Fact]
        public void CalculateLvr_InvalidLoanAmount_ReturnsBadRequest()
        {
            // Arrange
            var request = new LvrRequest
            {
                PropertyValue = 100000,
                LoanAmount = -60000  // Invalid loan amount
            };
            // Act
            var result = _controller.CalculateLvr(request);
            // Assert
            var badRequestResult = Assert.IsType<ActionResult<decimal>>(result);
            Assert.IsType<BadRequestObjectResult>(result.Result); // Expecting a BadRequest for invalid input
        }

        // Test: Invalid input (zero property value)
        [Fact]
        public void CalculateLvr_InvalidPropertyValue_ReturnsBadRequest()
        {
            // Arrange
            var request = new LvrRequest
            {
                PropertyValue = 0,  // Invalid property value
                LoanAmount = 100000
            };
            // Act
            var result = _controller.CalculateLvr(request);
            // Assert
            var badRequestResult = Assert.IsType<ActionResult<decimal>>(result);
            Assert.IsType<BadRequestObjectResult>(result.Result); // Expecting a BadRequest for invalid property value
        }

        // Test: Invalid input (both zero values)
        [Fact]
        public void CalculateLvr_InvalidZeroValues_ReturnsBadRequest()
        {
            // Arrange
            var request = new LvrRequest
            {
                PropertyValue = 0,
                LoanAmount = 0
            };
            // Act
            var result = _controller.CalculateLvr(request);
            // Assert
            var badRequestResult = Assert.IsType<ActionResult<decimal>>(result);
            Assert.IsType<BadRequestObjectResult>(result.Result); // Expecting a BadRequest for zero values
        }
    }

}
