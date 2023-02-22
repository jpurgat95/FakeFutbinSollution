namespace FakeFutbin.Web.Services;

public class PositionService : IPositionService
{
    private readonly HttpClient _httpClient;

    public PositionService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<IEnumerable<PositionDto>> GetPositions()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/Position");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return Enumerable.Empty<PositionDto>();
                }
                return await response.Content.ReadFromJsonAsync<IEnumerable<PositionDto>>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http Status Code - {response.StatusCode} Message - {message}");
            }
        }
        catch (Exception)
        {
            //log exception
            throw;
        }
    }
}
