namespace FakeFutbin.Api.Repositories.Contracts;

public interface IAuthRepository
{
    Task<User> RegisterUser(UserDto request);
    Task<AuthResponseDto> Login(UserDto request);
    Task<AuthResponseDto> RefreshToken();
}
