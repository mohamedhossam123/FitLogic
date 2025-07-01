public interface IWorkoutService
{
    public Task<List<WorkoutTypeDTO>> GetWorkoutSchedule(string goal, string level, int workoutDays);
    public Task<List<ExerciseDTO>> GetSmartExerciseReplacement(int exerciseToChangeId, string exerciseToChangeName, int userSkillLevelId, string userFitnessGoalString, List<ExerciseDTO> currentWorkoutExercises);
    public string GetRecommendedRepsDescription(string userFitnessGoalString); 
}