using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

namespace MyApiProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;

        public WorkoutController(IWorkoutService workoutService)
        {
            _workoutService = workoutService;
        }

        [HttpPost("generate-plan")]
        [ProducesResponseType(typeof(WorkoutResponseDTO), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GenerateWorkoutPlan([FromBody] WorkoutPlanRequestDto request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Goal) || 
                string.IsNullOrWhiteSpace(request.Level) || request.WorkoutDays <= 0)
            {
                return BadRequest("Invalid request. Goal, Level, and WorkoutDays are required.");
            }

            var workoutPlan = await _workoutService.GetWorkoutSchedule(request.Goal, request.Level, request.WorkoutDays);

            if (workoutPlan == null || !workoutPlan.Any())
            {
                return NotFound("No workout plan could be generated for the given criteria.");
            }

            return Ok(new WorkoutResponseDTO { WorkoutDays = workoutPlan });
        }

        [HttpPost("smart-change")]
        [ProducesResponseType(typeof(SmartChangeResponseDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetSmartChangeSuggestion([FromBody] SmartChangeRequestDto request)
        {
            if (request == null || (request.ExerciseToChangeId <= 0 && string.IsNullOrWhiteSpace(request.ExerciseToChangeName)) || 
                string.IsNullOrWhiteSpace(request.UserFitnessGoal) || request.UserSkillLevelId <= 0)
            {
                return BadRequest("Invalid request. Exercise ID or Name, Skill Level, and Fitness Goal are required.");
            }

            if (request.CurrentWorkoutExercises == null)
            {
                request.CurrentWorkoutExercises = new List<ExerciseDTO>();
            }

            var suggestedExercises = await _workoutService.GetSmartExerciseReplacement(
                request.ExerciseToChangeId,
                request.ExerciseToChangeName,
                request.UserSkillLevelId,
                request.UserFitnessGoal,
                request.CurrentWorkoutExercises
            );

            if (suggestedExercises == null || !suggestedExercises.Any())
            {
                return NotFound("No suitable replacement exercises found based on your criteria. This could be due to no matching exercises, or all suitable exercises targeting already-hit muscle groups.");
            }

            var recommendedRepsDescription = _workoutService.GetRecommendedRepsDescription(request.UserFitnessGoal);

            return Ok(new SmartChangeResponseDto
            {
                Suggestions = suggestedExercises,
                RecommendedRepsDescription = recommendedRepsDescription
            });
        }
    }
}