namespace FakeFutbin.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
	private readonly IAuthRepository _authRepository;
	private readonly FakeFutbinDbContext _context;

	public AuthController(IAuthRepository authRepository, FakeFutbinDbContext context)
	{
		_authRepository = authRepository;
		_context = context;
	}

    [HttpPost]
    public async Task<ActionResult<User>> RegisterUser(UserDto request)
    {
        var addingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
        if (addingUser == null)
        {
            var response = await _authRepository.RegisterUser(request);
            return Ok(response);
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<User>> Login(UserDto request)
    {
        var response = await _authRepository.Login(request);
        if (response.Success)
            return Ok(response);

        return BadRequest(response.Message);
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<string>> RefreshToken()
    {
        var response = await _authRepository.RefreshToken();
        if (response.Success)
            return Ok(response);

        return BadRequest(response.Message);
    }

    [HttpGet, Authorize(Roles = "User,Admin")]
    public ActionResult<string> Aloha()
    {
        return Ok("Aloha! You're authorized!");
    }
}
