using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SeaBattleWeb.Data.Repository.Interfaces;

namespace SeaBattleWeb.Server.Controllers;

[ApiController()]
[Route("api/game")]
[EnableCors("AllowAllOrigins")]
public class GameController : ControllerBase
{
    private readonly IGameRepository _gameRepository;
    
    public GameController(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;   
        
    }

    public record GetStatusDto(Guid gameId);
    [HttpPost("get-status")]
    public async Task<IActionResult> GetStatus([FromBody] GetStatusDto getStatusDto)
    {
        var game = await _gameRepository.GetById(getStatusDto.gameId);
        
        return Ok(new { Status = game.Status.ToString() });
    }
}