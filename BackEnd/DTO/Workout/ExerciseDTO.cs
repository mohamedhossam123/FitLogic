public class ExerciseDTO
{
    public int? ExerciseId { get; set; }
    public string Name { get; set; }
    public string Sets { get; set; }
    public string Reps { get; set; }
    public string Notes { get; set; }
    public string VideoLink { get; set; }
    public string SkillLevelName { get; set; }
    public string ExerciseTypeName { get; set; }
    public List<string> MainMusclesTargeted { get; set; } = new List<string>();
}
