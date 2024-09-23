using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SeaBattleWeb.Data.Entities;
using SeaBattleWeb.Data.Repository.Interfaces;
using SeaBattleWeb.GameLogic.Components;
using SeaBattleWeb.GameLogic.Models;

namespace SeaBattleWeb.Server.Controllers
{
    [ApiController()]
    [Route("api/board")]
    [EnableCors("AllowSpecificOrigin")]
    public class BoardController : Controller
    {
        private readonly IBoardRepositroy _boardRepository;
        public BoardController(IBoardRepositroy boardRepository)
        {
            _boardRepository = boardRepository;
        }

        public async Task<IActionResult> CreateBoard()
        {
            var board = new Board();
            var shipPlacer = new ShipPlacer(board);

            var coords = shipPlacer.GetShipCoordinates(new Cruiser().Size);
            shipPlacer.FillEmptyBoard(board);
            shipPlacer.AddShipsToBoard(coords, new Cruiser());

            await _boardRepository.Add(board);
            return Ok(board.Id);
        }

        [HttpGet]
        public async Task<IActionResult> GetBoard()
        {
            var board = new Board();

            var shipPlacer = new ShipPlacer(board);
            var coords = shipPlacer.GetShipCoordinates(new Cruiser().Size);
            shipPlacer.FillEmptyBoard(board);
            shipPlacer.AddShipsToBoard(coords, new Cruiser());
            
            return Ok(board.board);
        }


        public async Task<IActionResult> ShootToBoard()
        {


            return Ok();
        }
    }
}
