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
    public class BoardController : Controller
    {
        private readonly IBoardRepository _boardRepository;
        private readonly ILogger<BoardController> _logger;

        public BoardController(IBoardRepository boardRepository, ILogger<BoardController> logger)
        {
            _boardRepository = boardRepository;
            _logger = logger;
        }

        [HttpGet("create-board")]
        public async Task<IActionResult> CreateBoard()
        {
            var board = new Board();
            var shipPlacer = new ShipPlacer();

            var coords = shipPlacer.GetShipCoordinates(new Cruiser().Size);
            shipPlacer.FillEmptyBoard(board);
            _logger.LogError($"create board {(board[new Coordinates(4, 3)] == null ? "null" : "not null")}");
            _logger.LogError($"create board {(board[new Coordinates(4, 3)].PanelState.ToString())}");

            shipPlacer.AddShipsToBoard(board, coords, new Cruiser());

            await _boardRepository.Add(board);
            return Ok(new { BoardId = board.Id });
        }

        [HttpGet]
        public async Task<IActionResult> GetBoard()
        {
            var board = new Board();

            var shipPlacer = new ShipPlacer();
            var coords = shipPlacer.GetShipCoordinates(new Cruiser().Size);

            shipPlacer.FillEmptyBoard(board);
            shipPlacer.AddShipsToBoard(board, coords, new Cruiser());
            
            return Ok(board.board);
        }


        [HttpPost("shoot-board")]

        public async Task<IActionResult> ShootToBoard([FromBody] ShootToBoardDTO dto)
        {
            _logger.LogInformation($"shoot to board id: {dto.boardId} coords x: {dto.coords.X}, y: {dto.coords.Y}");

            var board = await _boardRepository.GetById(dto.boardId);

            if (board == null || board.board == null)
            {
                _logger.LogError("Board or board matrix is null");
                return BadRequest("Board not found or not initialized");
            }

            // Safe logging after null check
            _logger.LogInformation($"Board ID: {board.Id}, Board Length: {board.board.Count}");

            await Console.Out.WriteLineAsync();

            //var panelState = board.board[dto.coords.Y, dto.coords.X].PanelState;

            return Ok(new { status = board[new Coordinates(1, 1)] });
        }

        public record ShootToBoardDTO(Guid boardId, Coordinates coords);
        public record GetPanelStatusDTO(int x, int y, Guid id);

        [HttpPost("panel-status")]
        public async Task<IActionResult> GetPanelStatus([FromBody] GetPanelStatusDTO dto)
        {
            //_logger.LogInformation($"shoot to board id: {dto.boardId} coords x: {dto.coords.X}, y: {dto.coords.Y}");

            var board = await _boardRepository.GetById(dto.id);

            if (board == null || board.board == null)
            {
                _logger.LogError("Board or board matrix is null");
                return BadRequest("Board not found or not initialized");
            }

            // Safe logging after null check
            _logger.LogInformation($"Board ID: {board.Id}, Board Length: {board.board.Count}");
            _logger.LogError($"Board ID: {board.Id}, Board Element at [4,3]: {(board[new Coordinates(4, 3)] == null ? "null" : "not null")}");


            return Ok();
        }

    }
}
