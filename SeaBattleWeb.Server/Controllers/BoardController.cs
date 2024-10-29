using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SeaBattleWeb.Data.Entities;
using SeaBattleWeb.Data.GameLogic.Models.Values;
using SeaBattleWeb.Data.Repository.Interfaces;
using SeaBattleWeb.GameLogic.Components;
using SeaBattleWeb.GameLogic.Models;

namespace SeaBattleWeb.Server.Controllers
{
    [ApiController()]
    [Route("api/board")]
    [EnableCors("AllowAllOrigins")]
    public class BoardController : ControllerBase
    {
        private readonly IBoardRepository _boardRepository;
        private readonly ILogger<BoardController> _logger;

        public BoardController(IBoardRepository boardRepository, ILogger<BoardController> logger)
        {
            _boardRepository = boardRepository;
            _logger = logger;
        }

        public record GenerateBoardDTO(Guid userId, Guid gameId);
        [HttpPost("/generate")]
        public async Task<IActionResult> GenerateBoard([FromBody] GenerateBoardDTO dto)
        {
            var board = new Board(dto.userId, dto.gameId);

            var shipPlacer = new ShipPlacer();
            var allCoords = shipPlacer.GenerateBoard(board);

            return Ok( new { Coordinates = allCoords, BoardId = board.Id }) ;
        }

        public record ShootToBoardDTO(Guid boardId, Coordinates coords);
        [HttpPost("shoot-board")]
        public async Task<IActionResult> ShootToBoard([FromBody] ShootToBoardDTO dto)
        {
            _logger.LogInformation($"shoot to board id: {dto.boardId} coords x: {dto.coords.X}, y: {dto.coords.Y}");

            var board = await _boardRepository.GetById(dto.boardId);

            return Ok(new { Status = board[new Coordinates(dto.coords.X, dto.coords.Y)].PanelState.ToString()});
        }
    }
}
