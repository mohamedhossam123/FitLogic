using System;
using System.Collections.Generic;

namespace BackEnd.Models;

public partial class MuscleGroup
{
    public int MuscleGroupId { get; set; }

    public string MuscleGroupName { get; set; }

    public virtual ICollection<ExerciseMuscleTarget> ExerciseMuscleTargets { get; set; } = new List<ExerciseMuscleTarget>();

    public virtual ICollection<SubMuscle> SubMuscles { get; set; } = new List<SubMuscle>();
}
