using Microsoft.AspNetCore.SignalR;
using SeaBattleWeb.Data.Entities;
using SeaBattleWeb.Data.Repository;
using SeaBattleWeb.Data.Repository.Interfaces;
using System;
using Microsoft.AspNetCore.Mvc;
using SeaBattleWeb.Data.GameLogic.Models.Values;
using SeaBattleWeb.GameLogic.Components;

namespace SeaBattleWeb.Server.Hubs
{
    public record ConnectResult(string gameStatus, Guid gameId, Guid firstPlayerId, Guid secondPlayerId);

    public interface ILobbyClient
    {
        public Task Connect(ConnectResult res);
        
        public Task UserJoined();

        public Task StartGame();

        public Task getShooted();

        public Task ShootResult();
    }

    public class GameHub : Hub<ILobbyClient>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IBoardRepository _boardRepository;
        private readonly ILogger<GameHub> _logger;
        private readonly Lazy<ShipPlacer> _shipPlacer = new Lazy<ShipPlacer>(() => new ShipPlacer()); // q:did i correctly use lazy??


        public GameHub(IGameRepository gameRepository, ILogger<GameHub> logger, IBoardRepository boardRepository)
        {
            _gameRepository = gameRepository;
            _logger = logger;
            _boardRepository = boardRepository;
        }
        
        public record ConnectDTO(Guid userId, Guid gameId);
        public async Task Connect(ConnectDTO dto)
        {
            _logger.LogInformation($"connect to game gameid: {dto.userId} userid:{dto.gameId} ");
            var game = await _gameRepository.GetById(dto.gameId);
            
            if (game.FirstPlayerId == dto.userId || game.SecondPlayerId == dto.userId)
                await Clients.Client(Context.ConnectionId).Connect(new ConnectResult(game.State.ToString(),game.Id, game.FirstPlayerId, game.SecondPlayerId ));
            
            await _gameRepository.AddNewUser(dto.gameId, dto.userId, Context.ConnectionId); // work:need to handle when room is full error
            var board = _shipPlacer.Value.InitBoard(dto.userId, dto.gameId);
            await _boardRepository.Add(board);
            
            await Clients.Client(Context.ConnectionId).Connect(new ConnectResult(game.State.ToString(),game.Id, game.FirstPlayerId, game.SecondPlayerId ));
            
            if(game.FirstPlayerId != Guid.Empty)
                await Clients.Client(game.FirstPlayerConnectionId).UserJoined();
        }
        
        public record StartGameDTO(Guid gameId);
        public async Task StartGame(StartGameDTO dto)
        {
            var game = await _gameRepository.GetById(dto.gameId);

            if (game.FirstPlayerId != Guid.Empty && game.SecondPlayerId != Guid.Empty)
            {
                await _gameRepository.StartGame(dto.gameId);
                
                await Clients.All.StartGame();
            }
            else 
                throw new HubException("Game cant be started");
        }

        public record ShootToBoardDTO(Guid currentPlayerId, Guid gameId, Guid enemyBoardId, Coordinates coordinates );
        public async Task ShootToBoard(ShootToBoardDTO dto)
        {   
            var board = _boardRepository.GetById(dto.enemyBoardId).Result;
            
            var game = _gameRepository.GetById(dto.gameId).Result;
            
            if (game.CurrentPlayerId != dto.currentPlayerId)
            {
                throw new HubException("its not yours turn");  // need custom exceptions for website ui
            }
            
            _shipPlacer.Value.ShootToPanel(board, dto.coordinates);

            //what am i done?!
            await Clients.Client(game.CurrentPlayerId == game.FirstPlayerId
                ? game.FirstPlayerConnectionId
                : game.SecondPlayerConnectionId).ShootResult();
            
            await Clients.Client(game.CurrentPlayerId == game.FirstPlayerId
                ? game.SecondPlayerConnectionId
                : game.FirstPlayerConnectionId).getShooted();
            
            await _gameRepository.ChangeCurrentPlayerId(dto.gameId);
            
        }

        public override async Task OnConnectedAsync()
        {
            
        }
    }
}
