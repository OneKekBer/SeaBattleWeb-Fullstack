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

        public GameHub(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task Connect()
        {
            // return all games what Idle
            var games = await _gameRepository.GetIdleGames();
            await Clients.Client(Context.ConnectionId).GetAllGames(games);
        }

        public async Task CreateNewGame()
        {
            // create new game entity

            var games = await _gameRepository.GetIdleGames();
            await Clients.All.GetAllGames(games);
        }

        public async Task ConnectToExistingGame() 
        {
        
        }



    }
}
