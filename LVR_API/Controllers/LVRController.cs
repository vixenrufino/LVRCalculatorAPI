using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LVR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LVRController : ControllerBase
    {
        // POST: api/LVR_API
        [HttpPost]
        public ActionResult<decimal> CalculateLvr([FromBody] LvrRequest request)
        {
            // Validate the inputs
            if (request.PropertyValue <= 0 || request.LoanAmount <= 0)
            {
                return BadRequest("Both property value and loan amount must be greater than zero.");
            }

            // Calculate LVR (Loan Valuation Ratio)
            decimal lvr = Math.Round((request.LoanAmount / request.PropertyValue) * 100, 2);

            // Return LVR as percentage
            return Ok(lvr);
        }
    }

    // Model for the request parameters
    public class LvrRequest
    {
        public decimal PropertyValue { get; set; }
        public decimal LoanAmount { get; set; }
    }
}
