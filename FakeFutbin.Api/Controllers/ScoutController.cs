using FakeFutbin.Api.Entities;
using FakeFutbin.Api.Extensions;
using FakeFutbin.Api.Repositories.Contracts;
using FakeFutbin.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FakeFutbin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoutController : ControllerBase
    {
        private readonly IScoutRepository _scoutRepository;
        private readonly IPlayerRepository _playerRepository;

        public ScoutController(IScoutRepository scoutRepository,
                              IPlayerRepository playerRepository)
        {
            _scoutRepository = scoutRepository;
            _playerRepository = playerRepository;
        }
        [HttpGet]
        [Route("{coachId}/GetPlayers")]
        public async Task<ActionResult<IEnumerable<ScoutPlayerDto>>> GetPlayers(int coachId)
        {
            try
            {
                var scoutPlayers = await _scoutRepository.GetPlayers(coachId);
                if (scoutPlayers == null)
                {
                    return NoContent();
                }
                var players = await _playerRepository.GetPlayers();
                if (players == null)
                {
                    throw new Exception("No players exist in the system");
                }
                var scoutPlayersDto = scoutPlayers.ConvertToDto(players);
                return Ok(scoutPlayersDto);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ScoutPlayerDto>> GetPlayer(int id)
        {
            try
            {
                var scoutPlayer = await _scoutRepository.GetPlayer(id);
                if(scoutPlayer == null)
                {
                    return NoContent();
                }
                var player = await _playerRepository.GetPlayer(scoutPlayer.PlayerId);
                if(player == null)
                {
                    return NotFound();
                }
                var scoutPlayerDto = scoutPlayer.ConvertToDto(player);
                return Ok(scoutPlayer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ScoutPlayerDto>> PostPlayer([FromBody] ScoutPlayerToAddDto scoutPlayerToAddDto)
        {
            try
            {
                var newScoutPlayer = await _scoutRepository.AddPlayer(scoutPlayerToAddDto);
                if(newScoutPlayer == null)
                {
                    return NoContent();
                }
                var player = await _playerRepository.GetPlayer(newScoutPlayer.PlayerId);
                if(player == null)
                {
                    throw new Exception($"Something went wrong when attempting to retrieve product (productId:({scoutPlayerToAddDto.PlayerId})");
                }
                var newScoutPlayerDto = newScoutPlayer.ConvertToDto(player);
                return CreatedAtAction(nameof(GetPlayer), new { id = newScoutPlayerDto.Id, newScoutPlayerDto});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
