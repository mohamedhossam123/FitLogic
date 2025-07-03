

namespace BackEnd.Models;

public partial class ExerciseMuscleTarget
{
    public int ExerciseId { get; set; }

    public int MuscleGroupId { get; set; }

    public int SubMuscleId { get; set; }

    public decimal Percentage { get; set; }

    public bool IsMainMuscle { get; set; }

    public virtual Exercises Exercise { get; set; }

    public virtual MuscleGroups MuscleGroup { get; set; }

    public virtual SubMuscles SubMuscle { get; set; }
}
