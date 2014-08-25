
using CMWA.Packager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using TopMachine.Desktop.Overall;
using TopMachine.Topping.Dto;
using Topping.Core.Data.SQL;
using System.Web.Security;

 

namespace Topping.Core.Data.SQL
{


    public class ToppingAccessor : IDisposable
    {
        private string _ApplicationName;
        private string _roleName;
        
        
        public TopMachineDBEntities _context;

        public ToppingAccessor()
        {
            string connectionString = "";
            this._context = null;
            this._ApplicationName = "TOPMACHINE";
            this._roleName = "Admin";
            string key = "TopMachineDBEntities";
            if (ConfigurationManager.ConnectionStrings[key] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings[key].ConnectionString;
            }
            else 
            {
                throw new Exception("ConnectionString Not Exist");
            }
          
            this._context = new TopMachineDBEntities(connectionString);
        }

        //public ToppingAccessor(string path)
        //{            
        //    this._context = null;
        //    this._ApplicationName = "TOPMACHINE";
        //    this._roleName = "Admin";
        //    this._context = new SafeDB(path);
        //}

        
        public bool AddConfiguration(Topping.Core.Data.SQL.GameConfigs cfg)
        {
            try
            {
                this._context.AddToGameConfigs(cfg);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void AddGame(GeneratedGameDto gameDto)
        {
            if (gameDto.Config.HistoryGame)
            {
                Games game = new Games {
                    Date = gameDto.DateTime,
                    GameConfigId = gameDto.Config.Id,
                    GameId = gameDto.Id,
                    Status = (int) gameDto.Status,
                    Name = gameDto.Name
                };
                game.GameXml = new ObjectSerializer<GeneratedGameDto>().Serialize(gameDto);
                this._context.AddToGames(game);
                _context.SaveChanges();
            }
        }

        public bool ChangedPwdUsers(MembershipUser us, string newPWD)
        {
            try
            {


               string oldpwd = us.ResetPassword();

               bool result = us.ChangePassword(oldpwd, newPWD);
                
               return result;

            }
            catch (Exception)
            {
                return false;
            }
           
        }

        public bool DeleteConfiguration(ConfigGameDto cfg)
        {
            try
            {
                GameConfigs config = null;
                config = this._context.GameConfigs.Where<GameConfigs>(p => (p.Id == cfg.Id)).FirstOrDefault<GameConfigs>();
                if (config != null)
                {
                    this._context.GameConfigs.DeleteObject(config);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool DeleteUsers(MembershipUser us)
        {
            try
            {
                if (us != null)
                {
                    Membership.DeleteUser(us.UserName);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public void Dispose()
        {
            if (this._context != null)
            {
                this._context.Dispose();
                this._context = null;
            }
        }

        public void FinishGame(string player, GeneratedGameDto game, PlayerSummaryDto sum, bool doDetail, bool doHistory, bool doStats)
        {
            MembershipUser vid;
            if (sum.Turn != 0)
            {
                vid = GetUser(player);
                if (vid != null)
                {
                    DateTime n = DateTime.Now;
                    GamesDetail detail = new GamesDetail();
                    if (doDetail)
                    {
                        detail.DetailXml = new ObjectSerializer<List<PlayedGameRoundDto>>().Serialize(sum.Rounds);
                    }
                    int num = game.Rounds.Sum<GameRoundDto>((Func<GameRoundDto, int>) (p => p.Points));
                    detail.Date = n;
                    detail.GameId = game.Id;
                    detail.Negative = new int?((int) sum.PointsLost);
                    detail.Percentage = new double?(((double) sum.Total) / ((double) num));
                    detail.Time = new int?((int) sum.Time);
                    detail.Top = new int?((int) sum.Total);
                    detail.TopLost = new int?((int) (game.Rounds.Count - sum.TotalTop));
                    detail.UserId = (Guid)vid.ProviderUserKey;
                    detail.GameConfigId = game.Config.Id;
                    sum.Percentage = (float) detail.Percentage.Value;
                    if (doHistory)
                    {
                        this._context.AddToGamesDetail(detail);
                    }
                    if (doStats)
                    {
                        GameRankings ranking = this._context.GameRankings.Where<GameRankings>(p => ((((p.Year == n.Year) && (p.Month == n.Month)) && (p.ConfigGameId == game.Config.Id)) && (p.Playerid == ((Guid) vid.ProviderUserKey)))).FirstOrDefault();
                        if (ranking == null)
                        {
                            ranking = new GameRankings {
                                Year = n.Year,
                                Month = n.Month,
                                ConfigGameId = game.Config.Id,
                                Playerid = (Guid)vid.ProviderUserKey
                            };
                        }
                        ranking.NbParties++;
                        ranking.LostTops += (int) detail.TopLost.GetValueOrDefault(0);
                        ranking.Time += (int) detail.Time.GetValueOrDefault(0);
                        if (detail.Percentage == 1.0)
                        {
                            ranking.TotalTops++;
                        }
                        ranking.PlayerTop += (int) detail.Top.Value;
                        ranking.GameTop += num;
                        ranking.Percentage = ((float) ranking.PlayerTop) / ((float) ranking.GameTop);
                        if (ranking.BestScore <= detail.Percentage)
                        {
                            ranking.BestScore = detail.Percentage.GetValueOrDefault();
                            if ((ranking.BestTime == 0) || (ranking.BestTime > detail.Time.GetValueOrDefault()))
                            {
                                ranking.BestTime = (int) detail.Time.GetValueOrDefault();
                            }
                        }
                        this._context.AddToGameRankings(ranking);
                       
                    }
                }
                _context.SaveChanges();
            }
        }

        public List<ConfigGameDto> GetConfigurations()
        {
            List<ConfigGameDto> list = new List<ConfigGameDto>();
            var query = this._context.GameConfigs.Select<GameConfigs, GameConfigs>(p => p);
            foreach (GameConfigs config in query)
            {
                ConfigGameDto item = new ObjectSerializer<ConfigGameDto>().Deserialize(config.XmlConfig);
                list.Add(item);
            }
            return list;
        }

        public string[] getEnrolledUser(MembershipUser us)
        {            
            return Roles.GetRolesForUser(us.UserName);
        }

        public List<GamesDetailDto> GetGamesDetail(string player, int year, int mont, Guid configId)
        {
            Guid pid = (Guid)GetUser(player).ProviderUserKey;
            List<GamesDetail> list = null;
            DateTime start = new DateTime(year, mont, 1);
            DateTime end = start.AddMonths(1);
            list = this._context.GamesDetail.Where<GamesDetail>(p => ((((p.Date > start) && (p.Date < end)) && (p.UserId == pid)) && (p.GameConfigId == configId))).ToList<GamesDetail>();
            List<GamesDetailDto> list2 = new List<GamesDetailDto>();
            Console.Write(list.Count);
            foreach (GamesDetail detail in list)
            {
                long? nullable3;
                GamesDetailDto item = new GamesDetailDto {
                    Date = detail.Date,
                    Percentage = (float) detail.Percentage.GetValueOrDefault(0.0),
                    UserName = player,
                    Negative = (int) detail.Negative.GetValueOrDefault(0),
                    Total = (int) detail.Total.GetValueOrDefault(0),
                    Top = (int) detail.Top.GetValueOrDefault(0),
                    GameId = detail.GameId,
                    TopLost = (int) detail.TopLost.GetValueOrDefault(0),
                    ConfigId = (Guid)detail.GameConfigId
                };
                long? time = detail.Time;
                time = detail.Time;
                item.Time = (time.HasValue ? new long?(time.GetValueOrDefault() / 60L) : (nullable3 = null)) + ":" + (time.HasValue ? new long?(time.GetValueOrDefault() % 60L) : (nullable3 = null));
                list2.Add(item);
            }
            return list2;
        }

        //public IObjectContainer GetObjectContainer()
        //{
        //    if (this._context == null)
        //    {
        //        this._context = Db4oEmbedded.OpenFile(this.dbFile);
        //    }
        //    return this._context;
        //}

        public List<RankingDto> GetRankings(string player, int year, int mont, Guid configId)
        {
            Guid pid = (Guid)GetUser(player).ProviderUserKey;
            List<GameRankings> list = null;
            if ((player.Length > 0) && (configId == Guid.Empty))
            {
                list = this._context.GameRankings.Where<GameRankings>(p => (((p.Playerid == pid) && (p.Year == year)) && (p.Month == mont))).ToList<GameRankings>();
            }
            if ((player.Length > 0) && (configId != Guid.Empty))
            {
                list = this._context.GameRankings.Where<GameRankings>(p => ((p.Playerid == pid) && (p.ConfigGameId == configId))).ToList<GameRankings>();
            }
            List<RankingDto> list2 = new List<RankingDto>();
            Console.Write(list.Count);
            foreach (GameRankings ranking in list)
            {
                RankingDto item = new RankingDto {
                    BestScore = ranking.BestScore * 100.0,
                    BestTime = (ranking.BestTime / 60) + ":" + (ranking.BestTime % 60),
                    Config = ranking.ConfigGameId,
                    NbGames = ranking.NbParties,
                    NbLostTops = ((float) ranking.LostTops) / ((float) ranking.NbParties),
                    NbTops = ranking.TotalTops,
                    Percentage = (((float) ranking.PlayerTop) / ((float) ranking.GameTop)) * 100f,
                    Period = ranking.Month + "/" + ranking.Year,
                    Player = player
                };
                int num = ranking.Time / ranking.NbParties;
                item.Time = (num / 60) + ":" + (num % 60);
                list2.Add(item);
            }
            return list2;
        }

        public MembershipUser GetUser(Guid pkid)
        {
            return Membership.GetUser(pkid);
        }

        public aspnet_Users GetUserSql(string UserName)
        {
           var us = _context.aspnet_Users.Where(x=>x.UserName == UserName);

           if (us != null) 
           {
               return us.FirstOrDefault();
           }

           return null;
        }

        public MembershipUser GetUser(string UserName)
        {
            return Membership.GetUser(UserName);
        }

        public List<MembershipUser> GetUsers()
        {
            return Membership.GetAllUsers().Cast<MembershipUser>().ToList();
        }

        public bool IsAdminUser(MembershipUser us)
        {          
            
            return ((us != null) && Roles.GetRolesForUser(us.UserName).Contains(this._roleName));
        }

        public bool Login(string userName, string passWord)
        {
            return Membership.ValidateUser(userName, passWord);


            //if (this._context.Cast<MembershipUser>().Where<MembershipUser>(p => (p.Username == userName)).FirstOrDefault<MembershipUser>() == null)
            //{
            //    MembershipUser user2 = new MembershipUser {
            //        PKID = Guids.SequentialGuid(),
            //        Username = userName
            //    };
            //    this._context.Store(user2);
            //}
            //return true;
        }

        public bool UpdateConfiguration(ConfigGameDto cfg)
        {
            try
            {
                GameConfigs config = null;
                if (cfg.Id == Guid.Empty)
                {
                    config = new GameConfigs {
                        Id = Guid.NewGuid()
                    };
                    cfg.Id = config.Id;
                   this._context.AddToGameConfigs(config);
                }
                else
                {
                    config = this._context.GameConfigs.Where<GameConfigs>(p => (p.Id == cfg.Id)).FirstOrDefault<GameConfigs>();
                }
                config.XmlConfig = new ObjectSerializer<ConfigGameDto>().Serialize(cfg);
              
                _context.SaveChanges();

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private aspnet_Users ConvertUserClientServer(UserDto us)
        {
            aspnet_Users us_server;
            us_server = GetUserSql(us.Username);

            if (us_server == null) 
            {
                Membership.CreateUser(us.Username, us.Username,us.Username + "@topmachine.be");
                us_server = GetUserSql(us.Username);
                    
            }

            us_server.UserName = us.Username;
            us_server.FirstName = us.Firstname;
            us_server.LastName = us.Lastname;
            us_server.Email = us.Email;
            us_server.LoweredUserName = us.Username.ToLower();
    

            return us_server;
        }

        public bool UpdateUsers(UserDto us)
        {
          aspnet_Users user =  ConvertUserClientServer(us);
           
            
            
            _context.SaveChanges();
                        
            
            
            //try
            //{   Roles.GetRolesForUser(us.UserName).

            //    Roles.AddUserToRoles(us.UserName, roles.ToArray());
                         
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }



      
    }
}

