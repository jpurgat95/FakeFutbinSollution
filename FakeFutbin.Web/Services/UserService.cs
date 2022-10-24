using FakeFutbin.Models.Dto;
using FakeFutbin.Web.Services.Contracts;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace FakeFutbin.Web.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public event Action<int> OnUserChanged;

    public async Task<UserPlayerDto> AddPlayer(UserPlayerToAddDto userPlayerToAddDto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync<UserPlayerToAddDto>("api/User", userPlayerToAddDto);
            if (response.IsSuccessStatusCode)
            {
                if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default(UserPlayerDto);
                }
                return await response.Content.ReadFromJsonAsync<UserPlayerDto>();
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

    public async Task<UserPlayerDto> DeletePlayer(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/User/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserPlayerDto>();
            }
            return default(UserPlayerDto);
        }
        catch (Exception)
        {
            //log exception
            throw;
        }
    }

    public async Task<List<UserPlayerDto>> GetPlayers(int userId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/User/{userId}/GetPlayers");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return Enumerable.Empty<UserPlayerDto>().ToList();
                }
                return await response.Content.ReadFromJsonAsync<List<UserPlayerDto>>();
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

    public void RaiseEventOnUserChanged(int totalQty)
    {
        if (OnUserChanged != null)
        {
            OnUserChanged.Invoke(totalQty);
        }
    }

    public async Task<UserPlayerDto> UpdateQty(UserPlayerQtyUpdateDto userPlayerQtyUpdateDto)
    {
        try
        {
            var jsonRequest = JsonConvert.SerializeObject(userPlayerQtyUpdateDto);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");

            var response = await _httpClient.PatchAsync($"api/User/{userPlayerQtyUpdateDto.UserPlayerId}",content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserPlayerDto>();
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
