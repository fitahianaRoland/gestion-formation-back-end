using Microsoft.AspNetCore.Mvc;
using GestionFormation.Models.repository;
using Microsoft.AspNetCore.Authorization;
using GestionFormation.Models.classes;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StateController : ControllerBase
{
    private readonly ILogger<StateController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly StateRepository _state;


    public StateController(
        ApplicationDbContext context,
        ILogger<StateController> logger,
        StateRepository state
     )
    {
        _context = context;
        _logger = logger;
        _state = state; 
    }

    [HttpPost("addstate")] 
    public async Task<IActionResult> CreateState([FromBody] State request)
    {
        var state = new State()
        {
            Value = request.Value,
            Description = request.Description,
        };

        await _state.Add(state);

        return Ok(state);
    }

    [HttpGet("allstate")]
    public async Task<IActionResult> GetAllState()
    {
        var state = await _state.FindAll();
        return Ok(state);
    }
 

    [HttpDelete("deleteState")]
    public async Task<IActionResult> DeleteState(int id)
    {
        var deleted = await _state.Delete(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }


    [HttpPut("updateState")]
    public async Task<IActionResult> UpdateState(int id, State state)
    {
        var updatedState = await _state.Update(id, state);
        if (updatedState == null)
        {
            return NotFound();
        }
        return Ok(updatedState);
    }
}

