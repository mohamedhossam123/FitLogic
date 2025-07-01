using System;
using System.Collections.Generic;

namespace BackEnd.Models;

public partial class SubMuscles
{
    public int SubMuscleId { get; set; }

    public string SubMuscleName { get; set; }

    public int MuscleGroupId { get; set; }

    public virtual ICollection<ExerciseMuscleTarget> ExerciseMuscleTargets { get; set; } = new List<ExerciseMuscleTarget>();

    public virtual MuscleGroups MuscleGroup { get; set; }
}
