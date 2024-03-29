﻿namespace FakeFutbin.Api.Controllers;

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
				var playerDto = player.ConvertToDto();
				return Ok(playerDto);
			}
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError,
							"Error retrivieng data from database");
		}
	}
	[HttpGet]
	[Route(nameof(GetPlayerNationalities))]
	public async Task<ActionResult<IEnumerable<PlayerNationalityDto>>> GetPlayerNationalities()
	{
		try
		{
			var playerNationalities = await _playerRepository.GetNationalities();
			var playerNationalityDto = playerNationalities.ConvertToDto();
			return Ok(playerNationalityDto);
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError,
				"Error retrieving data from the database");
		}
	}

	[HttpGet]
	[Route("{nationalityId}/GetPlayersByNationality")]
	public async Task<ActionResult<IEnumerable<PlayerDto>>> GetPlayersByNationality(int nationalityId)
	{
		try
		{
			var players = await _playerRepository.GetPlayersByCategory(nationalityId);
			var playerDtos = players.ConvertToDto();
			return Ok(playerDtos);
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError,
							"Error retrieving data from the database");
		}
	}
}
