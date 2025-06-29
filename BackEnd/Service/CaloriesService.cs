namespace MyApiProject.Services
{
    public class CaloriesService : ICaloriesService
    {
        public async Task<float> CalculateBMR(BMIRequestDTO request)
        {
            await Task.Delay(0);
            if (request.Gender == "Male")
            {
                return 10 * request.Weight + 6.25f * request.Height - 5 * request.Age + 5;
            }
            else
            {
                return 10 * request.Weight + 6.25f * request.Height - 5 * request.Age - 161;
            }
        }

        public async Task<float> CalculateTDEE(TDEERequestDTO request)
        {
            float bmr = await CalculateBMR(new BMIRequestDTO
            {
                Age = request.Age,
                Height = request.Height,
                Weight = request.Weight,
                Gender = request.Gender
            });
            float activityFactor;
            switch (request.ActivityLevel.ToLower())
            {
                case "sedentary":
                    activityFactor = 1.2f;
                    break;
                case "lightly active":
                    activityFactor = 1.375f;
                    break;
                case "moderately active":
                    activityFactor = 1.55f;
                    break;
                case "very active":
                    activityFactor = 1.725f;
                    break;
                case "extra active":
                    activityFactor = 1.9f;
                    break;
                default:
                    throw new ArgumentException("Invalid activity level");
            }

            return bmr * activityFactor;
        }

        public async Task<CaloriesAndMacrosResponseDTO> CalculateCalories(TotalCaloriesRequestDTO request)
        {
            float tdee = await CalculateTDEE(new TDEERequestDTO
            {
                Age = request.Age,
                Height = request.Height,
                Weight = request.Weight,
                Gender = request.Gender,
                ActivityLevel = request.ActivityLevel
            });
            float totalCalories;
            switch (request.Goal.ToLower())
            {
                case "loseweight":
                    totalCalories = tdee - 500;
                    if (totalCalories < 1200) totalCalories = 1200;
                    break;
                case "gainweight":
                    totalCalories = tdee + 500;
                    break;
                case "maintainweight":
                    totalCalories = tdee;
                    break;
                default:
                    throw new ArgumentException("Invalid goal");
            }
            float proteinCalories, fatCalories, carbCalories;
            switch (request.Goal.ToLower())
            {
                case "loseweight":
                    proteinCalories = totalCalories * 0.30f;
                    fatCalories = totalCalories * 0.25f;
                    carbCalories = totalCalories * 0.45f;
                    break;
                case "gainweight":
                    proteinCalories = totalCalories * 0.25f;
                    fatCalories = totalCalories * 0.30f;
                    carbCalories = totalCalories * 0.45f;
                    break;
                case "maintainweight":
                    proteinCalories = totalCalories * 0.25f;
                    fatCalories = totalCalories * 0.25f;
                    carbCalories = totalCalories * 0.50f;
                    break;
                default:
                    throw new ArgumentException("Invalid goal");
            }

            return new CaloriesAndMacrosResponseDTO
            {
                TotalCalories = totalCalories,
                Protein = proteinCalories / 4,
                Fats = fatCalories / 9,
                Carbohydrates = carbCalories / 4
            };
        }
    }
}
