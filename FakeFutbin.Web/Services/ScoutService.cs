using FakeFutbin.Models.Dto;
using FakeFutbin.Web.Services.Contracts;
using System.Net.Http.Json;

namespace FakeFutbin.Web.Services;

public class ScoutService : IScoutService
{
    private readonly HttpClient _httpClient;

    public ScoutService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<ScoutPlayerDto> AddPlayer(ScoutPlayerToAddDto scoutPlayerToAddDto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync<ScoutPlayerToAddDto>("api/Scout",scoutPlayerToAddDto);
            if (response.IsSuccessStatusCode)
            {
                if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default(ScoutPlayerDto);
                }
                return await response.Content.ReadFromJsonAsync<ScoutPlayerDto>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status:{response.StatusCode} Message -{message}");
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<ScoutPlayerDto> DeletePlayer(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/Scout/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ScoutPlayerDto>();
            }
            return default(ScoutPlayerDto);
        }
        catch (Exception)
        {
            //log exception
            throw;
        }
    }

    public async Task<List<ScoutPlayerDto>> GetPlayers(int coachId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Scout/{coachId}/GetPlayers");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return Enumerable.Empty<ScoutPlayerDto>().ToList();
                }
                return await response.Content.ReadFromJsonAsync<List<ScoutPlayerDto>>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status code: {response.StatusCode} Message: {message}");
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
}
