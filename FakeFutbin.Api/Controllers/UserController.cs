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
        private readonly IUserRepository _userRepository;
        private readonly IPlayerRepository _playerRepository;

        public UserController(IUserRepository userRepository,
                              IPlayerRepository playerRepository)
        {
            _userRepository = userRepository;
            _playerRepository = playerRepository;
        }
        [HttpGet]
        [Route("{userId}/GetPlayers")]
        public async Task<ActionResult<IEnumerable<UserPlayerDto>>> GetPlayers(int userId)
        {
            try
            {
                var userPlayers = await _userRepository.GetPlayers(userId);
                if (userPlayers == null)
                {
                    return NoContent();
                }
                var players = await _playerRepository.GetPlayers();
                if (players == null)
                {
                    throw new Exception("No players exist in the system");
                }
                var userPlayersDto = userPlayers.ConvertToDto(players);
                return Ok(userPlayersDto);
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
                var userPlayer = await _userRepository.GetPlayer(id);
                if (userPlayer == null)
                {
                    return NoContent();
                }
                var player = await _playerRepository.GetPlayer(userPlayer.PlayerId);
                if (player == null)
                {
                    return NotFound();
                }
                var scoutPlayerDto = userPlayer.ConvertToDto(player);
                return Ok(userPlayer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserPlayerDto>> PostPlayer([FromBody] UserPlayerToAddDto userPlayerToAddDto)
        {
            try
            {
                var newUserPlayer = await _userRepository.AddPlayer(userPlayerToAddDto);
                if (newUserPlayer == null)
                {
                    return NoContent();
                }
                var player = await _playerRepository.GetPlayer(newUserPlayer.PlayerId);
                if (player == null)
                {
                    throw new Exception($"Something went wrong when attempting to retrieve product (productId:({userPlayerToAddDto.PlayerId})");
                }
                var newUserPlayerDto = newUserPlayer.ConvertToDto(player);
                return CreatedAtAction(nameof(GetPlayer), new { id = newUserPlayerDto.Id }, newUserPlayerDto);
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
                var userPlayer = await _userRepository.DeletePlayer(id);
                if (userPlayer == null)
                {
                    return NotFound();
                }
                var player = await _playerRepository.GetPlayer(userPlayer.PlayerId);
                if (player == null)
                {
                    return NotFound();
                }
                var userPlayerDto = userPlayer.ConvertToDto(player);

                return Ok(userPlayerDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<UserPlayerDto>> UpdateQty(int id, UserPlayerQtyUpdateDto userPlayerQtyUpdateDto)
        {
            try
            {
                var userPlayer = await _userRepository.UpddateQty(id, userPlayerQtyUpdateDto);
                if (userPlayer == null)
                {
                    return NotFound();
                }
                var player = await _playerRepository.GetPlayer(userPlayer.PlayerId);
                var userPlayerDto = userPlayer.ConvertToDto(player);

                return Ok(userPlayerDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
