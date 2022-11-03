namespace FakeFutbin.Web.Services.Contracts;

public interface IUserIdService
{
    Task <int> GetUserId();
}
