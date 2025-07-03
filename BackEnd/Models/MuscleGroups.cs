
namespace BackEnd.Models;

public partial class MuscleGroups
{
    public int MuscleGroupId { get; set; }

    public string MuscleGroupName { get; set; }

    public virtual ICollection<ExerciseMuscleTarget> ExerciseMuscleTargets { get; set; } = new List<ExerciseMuscleTarget>();

    public virtual ICollection<SubMuscles> SubMuscles { get; set; } = new List<SubMuscles>();
}
