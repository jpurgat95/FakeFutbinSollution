﻿using Newtonsoft.Json;
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
    public event Action<int> OnWalletChanged;

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
    public async Task<UserWalletDto> GetUser(int userId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/User/GetUser/{userId}");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null;
                }
                return await response.Content.ReadFromJsonAsync<UserWalletDto>();
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

    public async Task<List<UserWalletDto>> GetUsers()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/User/GetUsers");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return Enumerable.Empty<UserWalletDto>().ToList();
                }
                return await response.Content.ReadFromJsonAsync<List<UserWalletDto>>();
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

    public void RaiseEventOnWalletChanged(int wallet)
    {
        if (OnWalletChanged != null)
        {
            OnWalletChanged.Invoke(wallet);
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

    public async Task<UserWalletDto> UpdateWallet(int userId,UserWalletUpdateDto userWalletUpdateDto)
    {
        try
        {
            var jsonRequest = JsonConvert.SerializeObject(userWalletUpdateDto);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");

            var response = await _httpClient.PatchAsync($"api/User/UpdateWallet/{userId}", content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserWalletDto>();
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