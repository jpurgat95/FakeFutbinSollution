using FakeFutbin.Api.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FakeFutbin.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PositionController : ControllerBase
{
	private readonly IPositionRepository _positionRepository;

	public PositionController(IPositionRepository positionRepository)
	{
		_positionRepository = positionRepository;
	}
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PositionDto>>> GetPositions()
    {
        try
        {
            var playerPositions = await _positionRepository.GetPositions();
            var playerPositionsDto = playerPositions.ConvertToDto();
            return Ok(playerPositionsDto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }
}
