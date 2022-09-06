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
			var playerNationalities = await _playerRepository.GetNationalities();
			if (players == null || playerNationalities == null)
			{
                return NotFound();
            }
			else
			{
				var playerDto = players.ConvertToDto(playerNationalities);
				return Ok(playerDto);
			}	
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError,
				"Error retrivieng data from database");
		}
	}

	[HttpGet("{id:int}")]
	public async Task<ActionResult<PlayerDto>> GetPlayer(int id)
	{
		try
		{
			var player = await _playerRepository.GetPlayer(id);
			if (player == null)
			{
				return BadRequest();
			}
			else
			{
				var playerNationality = await _playerRepository.GetNationality(player.NationalityId);
				if (playerNationality == null)
				{
					return NotFound();
				}
				var playerDto = player.ConvertToDto(playerNationality);
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
