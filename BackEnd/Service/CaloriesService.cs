namespace MyApiProject.Services
{
    public class CaloriesService : ICaloriesService
    {
        public async Task<float> CalculateBMR(BMIRequestDTO request)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("Error calculating BMI: " + ex.Message);
            }
        }
        
        public async Task<float> CalculateTDEE(TDEERequestDTO request)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("Error calculating TDEE: " + ex.Message, ex);
            }
        }


        public async Task<float> CalculateCalories(TotalCaloriesRequestDTO request) 
        {
            float tdee = await CalculateTDEE(new TDEERequestDTO
            {
                ActivityLevel = request.ActivityLevel
            });
            float finalCalorieTarget = tdee; 
            
            switch (request.Goal.ToLower())
            {
                case "loseweight":
                    finalCalorieTarget -= 500; 
                    if (finalCalorieTarget < 1200) finalCalorieTarget = 1200;
                    break;
                case "gainweight":
                    finalCalorieTarget += 500;
                    break;
                case "maintainweight":
                    break;
                default:
                    throw new ArgumentException("Invalid goal specified. Goal must be 'LoseWeight', 'GainWeight', or 'MaintainWeight'.");
            }

            return finalCalorieTarget;
        }
        public async Task<CalculateMicroNutriontsResponseDTO> CalculateMicroNutrionts(CalculateMicroNutriontsRequestDTO request)
        {
            
        }
    }
}