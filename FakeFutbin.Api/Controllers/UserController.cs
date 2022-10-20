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
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _coachRepository;
        private readonly IPlayerRepository _playerRepository;

        public UserController(IUserRepository coachRepository,
                              IPlayerRepository playerRepository)
        {
            _coachRepository = coachRepository;
            _playerRepository = playerRepository;
        }
        [HttpGet]
        [Route("{coachId}/GetPlayers")]
        public async Task<ActionResult<IEnumerable<UserPlayerDto>>> GetPlayers(int coachId)
        {
            try
            {
                var coachPlayers = await _coachRepository.GetPlayers(coachId);
                if (coachPlayers == null)
                {
                    return NoContent();
                }
                var players = await _playerRepository.GetPlayers();
                if (players == null)
                {
                    throw new Exception("No players exist in the system");
                }
                var coachPlayersDto = coachPlayers.ConvertToDto(players);
                return Ok(coachPlayersDto);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserPlayerDto>> GetPlayer(int id)
        {
            try
            {
                var coachPlayer = await _coachRepository.GetPlayer(id);
                if (coachPlayer == null)
                {
                    return NoContent();
                }
                var player = await _playerRepository.GetPlayer(coachPlayer.PlayerId);
                if (player == null)
                {
                    return NotFound();
                }
                var scoutPlayerDto = coachPlayer.ConvertToDto(player);
                return Ok(coachPlayer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserPlayerDto>> PostPlayer([FromBody] UserPlayerToAddDto coachPlayerToAddDto)
        {
            try
            {
                var newCoachPlayer = await _coachRepository.AddPlayer(coachPlayerToAddDto);
                if (newCoachPlayer == null)
                {
                    return NoContent();
                }
                var player = await _playerRepository.GetPlayer(newCoachPlayer.PlayerId);
                if (player == null)
                {
                    throw new Exception($"Something went wrong when attempting to retrieve product (productId:({coachPlayerToAddDto.PlayerId})");
                }
                var newCoachPlayerDto = newCoachPlayer.ConvertToDto(player);
                return CreatedAtAction(nameof(GetPlayer), new { id = newCoachPlayerDto.Id }, newCoachPlayerDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<UserPlayerDto>> DeletePlayer(int id)
        {
            try
            {
                var coachPlayer = await _coachRepository.DeletePlayer(id);
                if (coachPlayer == null)
                {
                    return NotFound();
                }
                var player = await _playerRepository.GetPlayer(coachPlayer.PlayerId);
                if (player == null)
                {
                    return NotFound();
                }
                var coachPlayerDto = coachPlayer.ConvertToDto(player);

                return Ok(coachPlayerDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<UserPlayerDto>> UpdateQty(int id, UserPlayerQtyUpdateDto coachPlayerQtyUpdateDto)
        {
            try
            {
                var coachPlayer = await _coachRepository.UpddateQty(id, coachPlayerQtyUpdateDto);
                if (coachPlayer == null)
                {
                    return NotFound();
                }
                var player = await _playerRepository.GetPlayer(coachPlayer.PlayerId);
                var scoutPlayerDto = coachPlayer.ConvertToDto(player);

                return Ok(scoutPlayerDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
