﻿using eVoucherDatabaseWebService_DAL.Repositories;
using eVoucherDatabaseWebService_DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucherDatabaseWebService_BUS.Services
{
    public interface IGameService
    {
        List<Game> GetAllGames();
        Task<Game?> GetGameById(int id);
        Task<Game> AddGame(Game game);
        Task<Game?> UpdateGame(Game game);
        Task<Game> DeleteGame(int id);
        Task<Game> DeleteGamme(Game game);

    }
    public class GameService: IGameService
    {
        private GameRepository _gameRepository;
        public GameService(GameRepository gameRepository) 
        {
            _gameRepository = gameRepository;
        }

        public async Task<Game> AddGame(Game game)
        {
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

        public List<Game> GetAllGames()
        {
            return _gameRepository.GetAll().ToList();
        }
        public async Task<Game?> GetGameById(int id)
        {
            var game =await  _gameRepository.GetSingleById(id);
            return game;
        }

        
    }
}
