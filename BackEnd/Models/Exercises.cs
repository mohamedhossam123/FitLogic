
namespace BackEnd.Models;

public partial class Exercises
{
    public int ExerciseId { get; set; }

    public string ExerciseName { get; set; }

    public string VideoLinks { get; set; }

    public int SkillLevelId { get; set; }

    public int ExerciseTypeId { get; set; }

    public virtual ICollection<ExerciseMuscleTarget> ExerciseMuscleTargets { get; set; } = new List<ExerciseMuscleTarget>();

    public virtual ExerciseTypes ExerciseType { get; set; }

    public virtual SkillLevels SkillLevel { get; set; }
}
