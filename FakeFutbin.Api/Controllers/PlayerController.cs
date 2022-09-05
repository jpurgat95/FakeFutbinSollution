using FakeFutbin.Api.Extensions;
using FakeFutbin.Api.Repositories.Contracts;
using FakeFutbin.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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
	[HttpGet]
	public async Task<ActionResult<IEnumerable<PlayerDto>>> GetPlayers()
	{
		try
		{
			var players = await _playerRepository.GetPlayers();
			if (players == null)
			{
                return NotFound();
            }
			else
			{
				var playerDto = players.ConvertToDto();
				return Ok(playerDto);
			}	
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError,
				"Error retrivieng data from database");
		}
	}
}
