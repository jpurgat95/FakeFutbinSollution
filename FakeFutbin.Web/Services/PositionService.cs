using Newtonsoft.Json;
using System.Text;

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
    public async Task<UserPlayerPositionDto> UpdatePosition(int id, UserPlayerPositionUpdateDto userPlayerPositionUpdateDto)
    {
        try
        {
            var jsonRequest = JsonConvert.SerializeObject(userPlayerPositionUpdateDto);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");

            var response = await _httpClient.PatchAsync($"api/Position/UpdatePosition/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserPlayerPositionDto>();
            }
            return null;
        }
        catch (Exception)
        {
            //log exception
            throw;
        }
    }
}
