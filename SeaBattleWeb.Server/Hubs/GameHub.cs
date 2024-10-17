using Microsoft.AspNetCore.SignalR;
using SeaBattleWeb.Data.Entities;
using SeaBattleWeb.Data.Repository;
using SeaBattleWeb.Data.Repository.Interfaces;
using System;

namespace SeaBattleWeb.Server.Hubs
{

    public interface IGameClient
    {
        public Task GetAllGames(IEnumerable<Game> games);
    }

    public class GameHub : Hub<IGameClient>
    {
        private readonly IGameRepository _gameRepository;
        private readonly ILogger<GameHub> _logger;

        public GameHub(IGameRepository gameRepository, ILogger<GameHub> logger)
        {
            _gameRepository = gameRepository;
            _logger = logger;
        }

        public async Task Connect()
        {

            // return all games what Idle
            var games = await _gameRepository.GetIdleGames();
            await Clients.Client(Context.ConnectionId).GetAllGames(games);
        }

        public async Task CreateNewGame()
        {
            try
            {
                _logger.LogInformation("Create new game is working");
                // Создаем новую игру
                var game = new Game();

                await _gameRepository.Add(game);

                // Получаем список игр в состоянии Idle
                var games = await _gameRepository.GetIdleGames();
                await Clients.All.GetAllGames(games);

                _logger.LogInformation("Game created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateNewGame: {ex.Message}");
                throw;  // Пробросьте ошибку для SignalR, чтобы фронтенд мог её обработать
            }
        }


        public async Task ConnectToExistingGame() 
        {
            
        }
    }
}
