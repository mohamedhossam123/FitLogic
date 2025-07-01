using System;
using System.Collections.Generic;

namespace BackEnd.Models;

public partial class ExerciseType
{
    public int ExerciseTypeId { get; set; }

    public string ExerciseTypeName { get; set; }

    public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
}
