using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SeaBattleWeb.Data.Repository.Interfaces;

namespace SeaBattleWeb.Server.Controllers;

[ApiController()]
[Route("api/lobby")]
[EnableCors("AllowAllOrigins")]
public class LobbyController : ControllerBase
{
    private readonly IGameRepository _gameRepository;
    
    public LobbyController(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }
    
    public record GetLobbiesDTO();
    [HttpPost]
    public async Task<IActionResult> GetLobbies([FromBody] GetLobbiesDTO dto)
    {
        var games = await _gameRepository.GetIdleGames();
        return Ok(games);
    }
}