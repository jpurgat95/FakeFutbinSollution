using FakeFutbin.Models.Dto;
using FakeFutbin.Web.Services.Contracts;
using System.Net.Http.Json;

namespace FakeFutbin.Web.Services;

public class PlayerService : IPlayerService
{
    private readonly HttpClient _httpClient;

    public PlayerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PlayerDto> GetPlayer(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Player/{id}");
            if (response.IsSuccessStatusCode)
            {
                if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default(PlayerDto);
                }
                return await response.Content.ReadFromJsonAsync<PlayerDto>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Httpstatus code: {response.StatusCode} message:{message}");
            }
        }
        catch (Exception)
        {
            //log exception
            throw;
        }
    }

    public async Task<IEnumerable<PlayerDto>> GetPlayers()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/Player");
            if (response.IsSuccessStatusCode)
            {
                if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return Enumerable.Empty<PlayerDto>();
                }
                return await response.Content.ReadFromJsonAsync<IEnumerable<PlayerDto>>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Httpstatus code: {response.StatusCode} message:{message}");
            }
        }
        catch (Exception)
        {
            //log exception
            throw;
        }  
    }
}
