using FakeFutbin.Web.Services.Contracts;
using System.Security.Claims;
using System.Text.Json;

namespace FakeFutbin.Web.Services;

public class UserIdService : IUserIdService
{
    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _http;

    public UserIdService(ILocalStorageService localStorage, HttpClient http)
    {
        _localStorage = localStorage;
        _http = http;
    }
    public async Task <int> GetUserId()
    {
        string token = await _localStorage.GetItemAsStringAsync("token");
        var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");

        try
        {
            if (_http.DefaultRequestHeaders.Authorization != null)
            {
                var userClaims = identity.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier", StringComparison.InvariantCultureIgnoreCase));
                var userId = userClaims.Value;
                var userIdInt = Convert.ToInt32(userId);
                return userIdInt;
            }
            return 0;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
    public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}
