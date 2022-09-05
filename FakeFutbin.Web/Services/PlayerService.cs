using FakeFutbin.Models.Dto;
using FakeFutbin.Web.Services.Contracts;
using System.Net.Http.Json;
using System.Security.Cryptography;

namespace FakeFutbin.Web.Services;

public class PlayerService : IPlayerService
{
    private readonly HttpClient _httpClient;

    public PlayerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public Task<PlayerDto> GetPlayer(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PlayerNationalityDto>> GetPlayerNationalities(int id)
    {
        throw new NotImplementedException();
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
                throw new Exception(message);
            }
        }
        catch (Exception)
        {
            //log exception
            throw;
        }  
    }

    public Task<IEnumerable<PlayerDto>> GetPlayersByNationality(int nationalityId)
    {
        throw new NotImplementedException();
    }
}
