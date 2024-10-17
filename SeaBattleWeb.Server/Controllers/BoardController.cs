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

        [HttpGet("create-board")]
        public async Task<IActionResult> CreateBoard()
        {
            var board = new Board();
            var shipPlacer = new ShipPlacer();

            shipPlacer.FillEmptyBoard(board);
            var coords = shipPlacer.GetShipCoordinates(board, new Cruiser().Size);
            shipPlacer.AddShipsToBoard(board, coords, new Cruiser());

            await _boardRepository.Add(board);

            return Ok(new { BoardId = board.Id, Coords = coords });
        }

        [HttpGet]
        public async Task<IActionResult> GetBoard()
        {
            var board = new Board();

            var shipPlacer = new ShipPlacer();
            var coords = shipPlacer.GetShipCoordinates(board,new Cruiser().Size);

            shipPlacer.FillEmptyBoard(board);
            shipPlacer.AddShipsToBoard(board, coords, new Cruiser());
            
            return Ok(board.board);
        }


        [HttpPost("shoot-board")]

        public async Task<IActionResult> ShootToBoard([FromBody] ShootToBoardDTO dto)
        {
            _logger.LogInformation($"shoot to board id: {dto.boardId} coords x: {dto.coords.X}, y: {dto.coords.Y}");

            var board = await _boardRepository.GetById(dto.boardId);

            return Ok(new { Status = board[new Coordinates(dto.coords.X, dto.coords.Y)].PanelState.ToString()});
        }

        public record ShootToBoardDTO(Guid boardId, Coordinates coords);

        [HttpPost("panel-status")]
        public async Task<IActionResult> GetPanelStatus([FromBody] ShootToBoardDTO dto)
        {
            //_logger.LogInformation($"shoot to board id: {dto.boardId} coords x: {dto.coords.X}, y: {dto.coords.Y}");

            var board = await _boardRepository.GetById(dto.boardId);

            if (board == null || board.board == null)
            {
                _logger.LogError("Board or board matrix is null");
                return BadRequest("Board not found or not initialized");
            }

            _logger.LogInformation($"shoot to board id: {dto.boardId} coords x: {dto.coords.X}, y: {dto.coords.Y}");
            _logger.LogError($"Board ID: {board.Id}, Board Element at [4,3]: {board.board[dto.coords.X, dto.coords.Y].PanelState.ToString()}");


            return Ok(new {Status = board.board[dto.coords.X, dto.coords.Y].PanelState.ToString()});
        }

    }
}
