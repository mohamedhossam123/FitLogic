using System;
using System.Collections.Generic;

namespace BackEnd.Models;

public partial class SkillLevel
{
    public int SkillLevelId { get; set; }

    public string LevelName { get; set; }

    public int YearsOfTraining { get; set; }

    public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
}
