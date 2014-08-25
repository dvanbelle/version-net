using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Data.Objects;
using CMWA.Packager;
using TopMachine.Topping.Dto;
using TopMachine.Desktop.Overall;
using System.Data.Common;
using System.Configuration;

namespace Topping.Core.Data
{
    public class ToppingAccessor: IDisposable,IToppingAccessor 
    {
        ToppingEntities entity;
            

        public void Save()
        {
            entity.SaveChanges();
            entity.AcceptAllChanges();
        }

        #region Configuration
        public List<GameConfig> GetConfigurationsPrimary()
        {
            List<GameConfig> list = new List<GameConfig>();
            foreach (GameConfig cfg in entity.GameConfigs)
            {
                list.Add(cfg);
            }

            return list;
        }


        public List<ConfigGameDto> GetConfigurations()
        {
            List<ConfigGameDto> list = new List<ConfigGameDto>();
            foreach (GameConfig cfg in entity.GameConfigs)
            {
                ConfigGameDto dto = new ObjectSerializer<ConfigGameDto>().Deserialize(cfg.XmlConfig);
                list.Add(dto);
            }

            return list;
        }

        public bool UpdateConfiguration(ConfigGameDto cfg)
        {
            try
            {
                GameConfig row = null;

                if (cfg.Id == Guid.Empty)
                {
                    row = new GameConfig();
                    row.Id = Guid.NewGuid();
                    entity.AddToGameConfigs(row);
                    cfg.Id = row.Id;
                }
                else
                {
                    row = entity.GameConfigs.Where(p => p.Id == cfg.Id).FirstOrDefault();
                }

                row.XmlConfig = new ObjectSerializer<ConfigGameDto>().Serialize(cfg);

                entity.SaveChanges();
            }
            catch (Exception)
            {

                return false;
            }

            return true;
        }

        public bool DeleteConfiguration(ConfigGameDto cfg)
        {
            try
            {
                    GameConfig row = null;
                    row = entity.GameConfigs.Where(p => p.Id == cfg.Id).FirstOrDefault();
                    if (row != null)
                    {
                        entity.GameConfigs.DeleteObject(row);
                        entity.SaveChanges(); 
                    }

            }
            catch (Exception)
            {
                return false;
            }

            return true;

        }

        public List<RankingDto> GetRankings(string player, int year, int mont, Guid configId)
        {

            IQueryable<ViewGetRanking> query = null;

            if (player.Length > 0 && configId == Guid.Empty)
            {
                query = entity.ViewGetRankings.Where(p => p.Username == player && p.Year == year && p.Month == mont);
            }

            if (player.Length > 0 && configId != Guid.Empty)
            {
                query = entity.ViewGetRankings.Where(p => p.Username == player && p.ConfigGameId == configId);
            }

            List<ViewGetRanking> rank = query.ToList();
            List<RankingDto> dto = new List<RankingDto>();
            Console.Write(rank.Count); 

            foreach(ViewGetRanking p in rank)
            {
                    RankingDto d =new RankingDto()
                                    { BestScore = p.BestScore*100,
                                      BestTime = p.BestTime / 60 + ":" + p.BestTime % 60, 
                                     Config = p.ConfigGameId, 
                                     NbGames = p.NbParties, 
                                     NbLostTops = (float) p.LostTops / p.NbParties, 
                                     NbTops = p.TotalTops,
                                     Percentage = (float) p.PlayerTop / p.GameTop*100,
                                     Period = p.Month +  "/" + p.Year,
                                     Player = p.Username }; 

                        int Time = p.Time / p.NbParties;
                        d.Time = Time / 60 + ":" + Time % 60;
                    dto.Add(d);
                                     
            }

            return dto; 

        }

        #endregion

        #region Game
        public List<GamesDetailDto> GetGamesDetail(string player, int year, int mont, Guid configId)
        {

            IQueryable<ViewGetGameDetail> query = null;

            DateTime start = new DateTime(year, mont, 1);
            DateTime end = start.AddMonths(1);

            query = entity.ViewGetGameDetails.Where(p => (p.Date >= start && p.Date < end) && p.Username == player && p.GameConfigId == configId);

            List<ViewGetGameDetail> rank = query.ToList();
            List<GamesDetailDto> dto = new List<GamesDetailDto>();
            Console.Write(rank.Count);

            foreach (ViewGetGameDetail p in rank)
            {
                GamesDetailDto d = new GamesDetailDto()
                {
                    Date = p.Date,
                    Percentage = (float)p.Percentage.GetValueOrDefault(0),
                    UserName = p.Username,
                    Negative = (int)p.Negative.GetValueOrDefault(0),
                    Total = (int)p.Total.GetValueOrDefault(0),
                    Top = (int)p.Top.GetValueOrDefault(0),
                    GameId = p.GameId,
                    TopLost = (int)p.TopLost.GetValueOrDefault(0),
                    ConfigId = p.GameConfigId
                };

                d.Time = p.Time / 60 + ":" + p.Time % 60;

                dto.Add(d);

            }

            return dto;

        }

        public void AddGame(GeneratedGameDto gameDto)
        {
            if (gameDto.Config.HistoryGame)
            {
                Game game = new Game()
                {
                    Date = gameDto.DateTime,
                    GameConfigId = gameDto.Config.Id,
                    GameId = gameDto.Id,
                    Status = (int)gameDto.Status,
                    Name = gameDto.Name,
                };

                game.GameXml = new ObjectSerializer<GeneratedGameDto>().Serialize(gameDto);

                entity.AddToGames(game);
                entity.SaveChanges();
            }

        }

        public void FinishGame(string player, GeneratedGameDto game, PlayerSummaryDto sum, bool doDetail, bool doHistory, bool doStats)
        {
            if (sum.Turn == 0) return; 

            ViewUserIdAndName vid = entity.ViewUserIdAndNames.Where(p => p.Username == player).FirstOrDefault();
            if (vid != null)
            {
                DateTime n = DateTime.Now;


                GamesDetail detail = new GamesDetail();

                if (doDetail)
                {
                    detail.DetailXml = new ObjectSerializer<List<PlayedGameRoundDto>>().Serialize(sum.Rounds);
                }

                int top = game.Rounds.Sum(p => p.Points);
                detail.Date = n;
                detail.GameId = game.Id;
                detail.Negative = sum.PointsLost;
                detail.Percentage = (double)sum.Total / top;
                detail.Time = (long)sum.Time;
                detail.Top = sum.Total;
                detail.TopLost = game.Rounds.Count - sum.TotalTop;
                detail.UserId = vid.PKID;

                sum.Percentage = (float) detail.Percentage; 

                if (doHistory)
                {
                    entity.AddToGamesDetails(detail);
                }

                if (doStats)
                {
                    GameRanking rnk =
                        entity.GameRankings.Where(
                        p => p.Year == n.Year && p.Month == n.Month && p.ConfigGameId == game.Config.Id && p.Playerid == vid.PKID).FirstOrDefault();

                    if (rnk == null)
                    {
                        rnk = new GameRanking()
                        {
                            Year = n.Year,
                            Month = n.Month,
                            ConfigGameId = game.Config.Id,
                            Playerid = vid.PKID
                        };

                        entity.AddToGameRankings(rnk);
                    }


                    rnk.NbParties++;
                    rnk.LostTops += detail.TopLost.GetValueOrDefault(0);
                    rnk.Time += (int)detail.Time.GetValueOrDefault(0);
                    if (detail.Percentage == 1) rnk.TotalTops++;
                    rnk.PlayerTop += (int)detail.Top;
                    rnk.GameTop += top;
                    rnk.Percentage = (float)rnk.PlayerTop / rnk.GameTop;


                    if (rnk.BestScore <= detail.Percentage)
                    {
                        rnk.BestScore = detail.Percentage.GetValueOrDefault();
                        if (rnk.BestTime == 0 || rnk.BestTime > detail.Time.GetValueOrDefault())
                        {
                            rnk.BestTime = (int)detail.Time.GetValueOrDefault();
                        }
                    }
                }

                entity.SaveChanges();
                entity.AcceptAllChanges();
            }

        }
        #endregion

        #region User

        public List<user> GetUsers()
        {
           return entity.users.ToList() ;
        }

        public bool UpdateUsers(user us)
        {
            try
            {
                entity.users.AddObject(us); 
                entity.SaveChanges();
            }
            catch (Exception)
            {

                return false;
            }

            return true;
        }

        public bool DeleteUsers(user us)
        {
            try
            {
                user row = null;
                row = entity.users.Where(p => p.PKID == us.PKID).FirstOrDefault();
                if (row != null)
                {
                    entity.users.DeleteObject(row);
                    entity.SaveChanges();
                }

            }
            catch (Exception)
            {
                return false;
            }

            return true;

        }

        public bool Login(string userName, string passWord)
        {
            ViewUserIdAndName vid = entity.ViewUserIdAndNames.Where(p => p.Username == userName).FirstOrDefault();
            if (vid == null)
            {
                user u = new user();
                u.PKID = Guids.SequentialGuid();
                u.Username = userName;
                entity.AddTousers(u);
                entity.SaveChanges();
            }
            return true;
        }
        #endregion


        #region IDisposable Members

        public void Dispose()
        {
            entity.Dispose(); 
        }

        #endregion

        void IToppingAccessor.ToppingAccessor(string path)
        {
            string conn = "metadata=res://*/ToppingModel.csdl|res://*/ToppingModel.ssdl|res://*/ToppingModel.msl;provider=System.Data.SQLite;provider connection string=\"data source={0}\";";
            string str = "";

            if (System.Configuration.ConfigurationManager.ConnectionStrings["TopMachineDB"] != null)
            {
                str = ConfigurationManager.ConnectionStrings["TopMachineDB"].ConnectionString;
            }
            else
            {
                if (System.Configuration.ConfigurationManager.AppSettings["PackageOnServer"] == "1")
                {
                    string p = AppDomain.CurrentDomain.BaseDirectory + "\\App_Data\\Topping.db";
                    str = string.Format(conn, p);
                }
                else
                {
                    string db = PackageManager.Instance.Project.GetFileName("TopMachineData\\Databases\\Topping");
                    str = string.Format(conn, db);
                }
            }

            entity = new ToppingEntities(str);
        }
    }
}
