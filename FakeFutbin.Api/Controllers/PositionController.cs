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
    [HttpPatch]
    [Route("UpdatePosition/{id}")]
    public async Task<ActionResult<UserPlayerPositionDto>> UpdatePosition(int id, UserPlayerPositionUpdateDto userPlayerPositionUpdate)
    {
        try
        {
            var updatedUserPlayer = await _positionRepository.UpdatePosition(id, userPlayerPositionUpdate);
            if (updatedUserPlayer == null)
            {
                return NotFound();
            }
            var userPlayerDto = updatedUserPlayer.ConvertToDto();
            return Ok(userPlayerDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}