

namespace BackEnd.Models;

public partial class SkillLevels
{
    public int SkillLevelId { get; set; }

    public string LevelName { get; set; }

    public int YearsOfTraining { get; set; }

    public virtual ICollection<Exercises> Exercises { get; set; } = new List<Exercises>();
}
