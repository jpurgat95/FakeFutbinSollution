using FakeFutbin.Api.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FakeFutbin.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayerController : ControllerBase
{
	private readonly IPlayerRepository _playerRepository;

	public PlayerController(IPlayerRepository playerRepository)
	{
		_playerRepository = playerRepository;
	} 
}
