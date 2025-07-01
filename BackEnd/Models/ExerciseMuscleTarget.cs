using System;
using System.Collections.Generic;

namespace BackEnd.Models;

public partial class ExerciseMuscleTarget
{
    public int ExerciseId { get; set; }

    public int MuscleGroupId { get; set; }

    public int SubMuscleId { get; set; }

    public decimal Percentage { get; set; }

    public bool IsMainMuscle { get; set; }

    public virtual Exercise Exercise { get; set; }

    public virtual MuscleGroup MuscleGroup { get; set; }

    public virtual SubMuscle SubMuscle { get; set; }
}
