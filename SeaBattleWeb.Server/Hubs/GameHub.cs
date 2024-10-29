using Microsoft.AspNetCore.SignalR;
using SeaBattleWeb.Data.Entities;
using SeaBattleWeb.Data.Repository;
using SeaBattleWeb.Data.Repository.Interfaces;
using System;
using Microsoft.AspNetCore.Mvc;

namespace SeaBattleWeb.Server.Hubs
{
    public record ConnectResult(string gameState, Guid gameId);

    public interface ILobbyClient
    {
        public Task Connect(ConnectResult connectResult);
        
        public Task UserJoined();

        public Task StartGame();
    }

    public class GameHub : Hub<ILobbyClient>
    {
        private readonly IGameRepository _gameRepository;
        private readonly ILogger<GameHub> _logger;

        public GameHub(IGameRepository gameRepository, ILogger<GameHub> logger)
        {
            _gameRepository = gameRepository;
            _logger = logger;
        }

        public record ConnectDTO(Guid userId, Guid gameId);
        public async Task Connect(ConnectDTO dto)
        {
            _logger.LogInformation($"connect to game gameid: {dto.userId} userid:{dto.gameId} ");
            var game = await _gameRepository.GetById(dto.gameId);

            await _gameRepository.AddNewUser(dto.gameId, dto.userId);

            await Clients.All.Connect(new ConnectResult(game.State.ToString(), dto.gameId));
        }
        
        public record StartGameDTO(Guid gameId);
        public async Task StartGame(StartGameDTO dto)
        {
            var game = await _gameRepository.GetById(dto.gameId);

            await _gameRepository.StartGame(dto.gameId);
            
            await Clients.All.StartGame();
        }
    }
}
