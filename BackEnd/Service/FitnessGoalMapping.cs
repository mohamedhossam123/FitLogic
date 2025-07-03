
namespace MyApiProject.Services 
{
    public static class FitnessGoalMapping
    {
        public const string StrengthTypeName = "Strength"; 
        public const string CardioTypeName = "Cardio";     
        public const string HypertrophyTypeName = "Hypertrophy"; 
        public const string EnduranceTypeName = "Endurance"; 
        public const string CoreTypeName = "Core"; 

        public enum FitnessGoal
        {
            MaintainWeight,
            BuildMuscle,
            BuildStrength,
            LossWeight
        }

        public class RecommendedReps
        {
            public int MinReps { get; set; }
            public int MaxReps { get; set; }
            public string Description { get; set; } 
        }

        public static List<string> GetExerciseTypeNamesForGoal(FitnessGoal goal)
        {
            return goal switch
            {
                FitnessGoal.BuildStrength => new List<string> { StrengthTypeName }, 
                FitnessGoal.BuildMuscle => new List<string> { StrengthTypeName, HypertrophyTypeName }, 
                FitnessGoal.LossWeight => new List<string> { StrengthTypeName, CardioTypeName, EnduranceTypeName }, 
                FitnessGoal.MaintainWeight => new List<string> { StrengthTypeName, CardioTypeName, EnduranceTypeName }, 
                _ => new List<string>() 
            };
        }

        public static RecommendedReps GetRecommendedRepsForGoal(FitnessGoal goal)
        {
            return goal switch
            {
                FitnessGoal.BuildStrength => new RecommendedReps { MinReps = 1, MaxReps = 5, Description = "1-5 reps (heavy weight)" },
                FitnessGoal.BuildMuscle => new RecommendedReps { MinReps = 8, MaxReps = 12, Description = "8-12 reps (moderate weight, focus on contraction)" },
                FitnessGoal.LossWeight => new RecommendedReps { MinReps = 12, MaxReps = 15, Description = "12-15+ reps (higher reps, could be circuit-style or lighter weights)" },
                FitnessGoal.MaintainWeight => new RecommendedReps { MinReps = 10, MaxReps = 15, Description = "10-15 reps (balanced approach)" },
                _ => new RecommendedReps { MinReps = 8, MaxReps = 12, Description = "General fitness (8-12 reps)" }
            };
        }
        public static int GetRecommendedSetsForGoal(FitnessGoal goal, string userLevel = null)
        {
            // If building muscle and intermediate or above, use 2 sets
            if (goal == FitnessGoal.BuildMuscle && !string.IsNullOrEmpty(userLevel) &&
                (userLevel.Equals("Intermediate", StringComparison.OrdinalIgnoreCase) || userLevel.Equals("Advanced", StringComparison.OrdinalIgnoreCase)))
            {
                return 2;
            }
            return goal switch
            {
                FitnessGoal.BuildStrength => 5,
                FitnessGoal.BuildMuscle => 4,
                FitnessGoal.LossWeight => 3,
                FitnessGoal.MaintainWeight => 3,
                _ => 3
            };
        }

        public static string GetRecommendedRepsForGoalString(FitnessGoal goal, string userLevel = null)
        {
            if (goal == FitnessGoal.BuildMuscle && !string.IsNullOrEmpty(userLevel) &&
                (userLevel.Equals("Intermediate", StringComparison.OrdinalIgnoreCase) || userLevel.Equals("Advanced", StringComparison.OrdinalIgnoreCase)))
            {
                return "to failure (max 10)";
            }
            return GetRecommendedRepsForGoal(goal).Description;
        }
        public static FitnessGoal ParseFitnessGoal(string goalString)
        {
            var normalized = goalString.Replace(" ", string.Empty).ToLowerInvariant();
            return normalized switch
            {
                "maintainweight" => FitnessGoal.MaintainWeight,
                "buildmuscle" => FitnessGoal.BuildMuscle,
                "buildstrength" => FitnessGoal.BuildStrength,
                "lossweight" => FitnessGoal.LossWeight,
                _ => throw new ArgumentException($"Invalid fitness goal string provided: {goalString}. Expected 'maintain weight', 'build muscle', 'build strength', or 'loss weight'.")
            };
        }
    }
}