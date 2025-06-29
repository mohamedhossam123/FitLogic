public interface ICaloriesService
{
    public Task<float> CalculateCalories(TotalCaloriesRequestDTO request);
    public Task<float> CalculateTDEE(TDEERequestDTO request);
    public Task<float> CalculateBMR(BMIRequestDTO request);
    public Task<CalculateMicroNutriontsResponseDTO> CalculateMicroNutrionts(CalculateMicroNutriontsRequestDTO requset);
}