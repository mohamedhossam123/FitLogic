using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyApiProject.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CaloriesController : ControllerBase
    {
        private readonly ICaloriesService _caloriesService;

        public CaloriesController(ICaloriesService caloriesService)
        {
            _caloriesService = caloriesService;
        }

        [HttpPost("calculate")]
        public async Task<ActionResult<CaloriesAndMacrosResponseDTO>> CalculateCalories([FromBody] TotalCaloriesRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request data.");
            }

            try
            {
                var result = await _caloriesService.CalculateCalories(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

}