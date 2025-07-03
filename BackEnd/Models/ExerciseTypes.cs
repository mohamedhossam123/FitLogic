
namespace BackEnd.Models;

public partial class ExerciseTypes
{
    public int ExerciseTypeId { get; set; }

    public string ExerciseTypeName { get; set; }

    public virtual ICollection<Exercises> Exercises { get; set; } = new List<Exercises>();
}
