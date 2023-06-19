using eVoucher_DAL.Repositories;
using eVoucher_DTO.Models;
using eVoucher_Utility.Enums;
using eVoucher_ViewModel.Requests.GameRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_BUS.Services
{
    public interface IGameService
    {
        Task<List<Game>> GetAllGamesAsync();
        Task<Game?> GetGameById(int id);
        Task<Game> AddGame(GameCreateRequest request);
        Task<Game?> UpdateGame(Game game);
        Task<Game> DeleteGame(int id);
        Task<Game> DeleteGamme(Game game);

    }
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<Game> AddGame(GameCreateRequest request)
        {
            var game = new Game()
            {
                Name = request.Name,
                CreatedTime = DateTime.Now,
                CreatedBy = request.CreatedBy,
                Status = ActiveStatus.Active,
                IsDeleted = false
            };
            var _game = await _gameRepository.Add(game);
            return _game;
        }
        public async Task<Game?> UpdateGame(Game game)
        {
            var _game = await _gameRepository.Update(game);
            return _game;
        }
        public async Task<Game> DeleteGame(int id)
        {
            var _game = await _gameRepository.Delete(id);
            return _game;
        }

        public async Task<Game> DeleteGamme(Game game)
        {
            var _game = await _gameRepository.Delete(game);
            return _game;
        }

        public async Task<List<Game>> GetAllGamesAsync()
        {
            return await _gameRepository.GetAllAsync();
            
        }
        public async Task<Game?> GetGameById(int id)
        {
            var game = await _gameRepository.GetSingleById(id);
            return game;
        }


    }
}
