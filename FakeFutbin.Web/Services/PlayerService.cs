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
    public async Task<IEnumerable<PlayerDto>> GetPlayers()
    {
        try
        {
            var players = await _httpClient.GetFromJsonAsync<IEnumerable<PlayerDto>>("api/Player");
            return players;
        }
        catch (Exception)
        {
            //log exception
            throw;
        }  
    }
}
