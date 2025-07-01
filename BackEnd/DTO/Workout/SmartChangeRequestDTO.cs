
    public class SmartChangeRequestDto
    {
        public int ExerciseToChangeId { get; set; } 
        public string ExerciseToChangeName { get; set; } 
        public int UserSkillLevelId { get; set; } 
        public string UserFitnessGoal { get; set; } 
        public List<ExerciseDTO> CurrentWorkoutExercises { get; set; } = new List<ExerciseDTO>(); 
    }
