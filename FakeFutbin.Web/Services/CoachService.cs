using FakeFutbin.Models.Dto;
using FakeFutbin.Web.Services.Contracts;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace FakeFutbin.Web.Services;

public class CoachService : ICoachService
{
    private readonly HttpClient _httpClient;

    public CoachService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public event Action<int> OnCoachChanged;

    public async Task<CoachPlayerDto> AddPlayer(CoachPlayerToAddDto coachPlayerToAddDto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync<CoachPlayerToAddDto>("api/Coach", coachPlayerToAddDto);
            if (response.IsSuccessStatusCode)
            {
                if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default(CoachPlayerDto);
                }
                return await response.Content.ReadFromJsonAsync<CoachPlayerDto>();
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

    public async Task<CoachPlayerDto> DeletePlayer(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/Coach/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CoachPlayerDto>();
            }
            return default(CoachPlayerDto);
        }
        catch (Exception)
        {
            //log exception
            throw;
        }
    }

    public async Task<List<CoachPlayerDto>> GetPlayers(int coachId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Coach/{coachId}/GetPlayers");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return Enumerable.Empty<CoachPlayerDto>().ToList();
                }
                return await response.Content.ReadFromJsonAsync<List<CoachPlayerDto>>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status code: {response.StatusCode} Message: {message}");
            }
        }
        catch (Exception)
        {
            //log exception
            throw;
        }
    }

    public void RaiseEventOnCoachChanged(int totalQty)
    {
        if (OnCoachChanged != null)
        {
            OnCoachChanged.Invoke(totalQty);
        }
    }

    public async Task<CoachPlayerDto> UpdateQty(CoachPlayerQtyUpdateDto coachPlayerQtyUpdateDto)
    {
        try
        {
            var jsonRequest = JsonConvert.SerializeObject(coachPlayerQtyUpdateDto);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");

            var response = await _httpClient.PatchAsync($"api/Coach/{coachPlayerQtyUpdateDto.CoachPlayerId}",content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CoachPlayerDto>();
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
