using Microsoft.AspNetCore.SignalR;
using SeaBattleWeb.Data.Entities;
using SeaBattleWeb.Data.Repository.Interfaces;

namespace SeaBattleWeb.Server.Hubs
{
    public interface IGameClient
    {
        public Task GetAllGames(IEnumerable<Game> games);
    }

    public class LobbyHub : Hub<IGameClient>
    {
        private readonly IGameRepository _gameRepository;
        private readonly ILogger<LobbyHub> _logger;

        public LobbyHub(IGameRepository gameRepository, ILogger<LobbyHub> logger)
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
        
        public async Task GetAllGames()
        {
            var games = await _gameRepository.GetIdleGames();
            await Clients.Client(Context.ConnectionId).GetAllGames(games);
        }

        public record CreateNewGameDTO(string name);
        public async Task CreateNewGame(CreateNewGameDTO dto)
        {
            if(string.IsNullOrEmpty(dto.name))
                throw new Exception();
            
            var game = new Game(dto.name);

            await _gameRepository.Add(game);

            // Получаем список игр в состоянии Idle
            var games = await _gameRepository.GetIdleGames();
            await Clients.All.GetAllGames(games);

            _logger.LogInformation("Game created successfully");
        
        }
    }
}
