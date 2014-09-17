using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopMachine.Topping.Dto;

namespace Topping.Core.Data
{
   public interface IToppingAccessor
    {
        void ToppingAccessor(string path);
        void Save();

       #region Configuration

         List<GameConfig> GetConfigurationsPrimary();
         List<ConfigGameDto> GetConfigurations();
         bool UpdateConfiguration(ConfigGameDto cfg);
         bool DeleteConfiguration(ConfigGameDto cfg);
         List<RankingDto> GetRankings(string player, int year, int mont, Guid configId);

      #endregion

       #region Game
         List<GamesDetailDto> GetGamesDetail(string player, int year, int mont, Guid configId);
         void AddGame(GeneratedGameDto gameDto);
         void FinishGame(string player, GeneratedGameDto game, PlayerSummaryDto sum, bool doDetail, bool doHistory, bool doStats);


      #endregion

       #region User

         List<user> GetUsers();
         bool UpdateUsers(user us);
         bool DeleteUsers(user us);
         bool Login(string userName, string passWord);

      #endregion

            
    }
}
