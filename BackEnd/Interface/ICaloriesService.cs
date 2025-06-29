public interface ICaloriesService
{
    public Task<CaloriesAndMacrosResponseDTO> CalculateCalories(TotalCaloriesRequestDTO request);
    public Task<float> CalculateTDEE(TDEERequestDTO request);
    public Task<float> CalculateBMR(BMIRequestDTO request);
}