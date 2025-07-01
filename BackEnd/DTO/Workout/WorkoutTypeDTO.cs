public class WorkoutTypeDTO
    {
        public string WorkoutName { get; set; }
        public List<ExerciseDTO> Exercises { get; set; } = new List<ExerciseDTO>();
    }
