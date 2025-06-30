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

            string activity = request.ActivityLevel?.Trim().ToLower(); 
            float activityFactor = activity switch
            {
                "sedentary" => 1.2f,
                "lightly active" => 1.375f,
                "moderately active" => 1.55f,
                "very active" => 1.725f,
                "extra active" => 1.9f,
                _ => throw new ArgumentException("Invalid activity level")
            };

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
            string goal = request.Goal?.Trim().ToLower(); 
            switch (goal)
            {
                case "loseweight":
                    totalCalories = Math.Max(tdee - 500, 1200); 
                    break;
                case "gainweight":
                    totalCalories = tdee + 500;
                    break;
                case "gainmuscle":
                    totalCalories = tdee + 250;
                    break;
                case "maintainweight":
                    totalCalories = tdee;
                    break;
                default:
                    throw new ArgumentException("Invalid goal");
            }

            float proteinGrams, proteinCalories, fatCalories, carbCalories;

            switch (goal)
            {
                case "gainmuscle":
                    proteinGrams = request.Weight * 2.2f; 
                    proteinCalories = proteinGrams * 4;
                    fatCalories = totalCalories * 0.25f;
                    carbCalories = totalCalories - proteinCalories - fatCalories;
                    break;

                case "loseweight":
                    proteinCalories = totalCalories * 0.30f;
                    fatCalories = totalCalories * 0.25f;
                    carbCalories = totalCalories * 0.45f;
                    proteinGrams = proteinCalories / 4;
                    break;

                case "gainweight":
                    proteinCalories = totalCalories * 0.25f;
                    fatCalories = totalCalories * 0.30f;
                    carbCalories = totalCalories * 0.45f;
                    proteinGrams = proteinCalories / 4;
                    break;

                case "maintainweight":
                    proteinCalories = totalCalories * 0.25f;
                    fatCalories = totalCalories * 0.25f;
                    carbCalories = totalCalories * 0.50f;
                    proteinGrams = proteinCalories / 4;
                    break;

                default:
                    throw new ArgumentException("Invalid goal");
            }
            return new CaloriesAndMacrosResponseDTO
            {
                TotalCalories = totalCalories,
                Protein = proteinGrams,
                Fats = fatCalories / 9,
                Carbohydrates = carbCalories / 4
            };
        }
    }
}
