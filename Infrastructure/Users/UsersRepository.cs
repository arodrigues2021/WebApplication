using Domain.Users.Entities;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Data.SqlClient;
using Infrastructure.Users.Interface;
using System.Linq;
using Aplication.Users.DTO;
using Domain;
using cache;

namespace Infrastructure.Users
{
    public class UsersRepository :  IUsersRepository
    {
        private readonly IDbConnection _db;

        private readonly Config _config;

        private readonly ICacheService<object> _cacheService;

        static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(UsersRepository));

        public UsersRepository(Config config, ICacheService<object> cacheService)
        {
            _config = config;

            _db = new SqlConnection(_config.connectionStrings.Database);

            _cacheService = cacheService;
        }

        public List<Usuario> GetListUsers()
        {   
            var mykey = "listUsers";

            List<Usuario> listaUsers = new List<Usuario>();

            listaUsers = (List<Usuario>)_cacheService.Get<List<Usuario>>(mykey);
            
            if (_cacheService.IsExists(mykey))
            {
                listaUsers = (List<Usuario>)_cacheService.Get<List<Usuario>>(mykey);

                log.Info("CacheRedis-GetListUsers");
            }
            else
            {
                string SqlString = $@"SELECT [Id]
                                          ,[Nombre]
                                          ,[Apellido]
                                          ,[Email]
                                          ,[Movil]
                                          ,[Balance]
                                      FROM [dbo].[Usuario]";

                listaUsers = _db.Query<Usuario>(SqlString).ToList();

                _cacheService.Set<List<Usuario>>(mykey, listaUsers);

                log.Info("No CacheRedis-GetListUsers");
            }

            return listaUsers;
        }

        public LoginDTO GetUsersByEmail(string email)
        {
            string Email = email;

            string SqlString = $@"SELECT   [Id]
                                          ,[Email]
                                          ,[Password]
                                      FROM [dbo].[Usuario] Where Email = @Email";

            return _db.Query<LoginDTO>(SqlString, new { Email = Email }).FirstOrDefault();
        }
       
    }
}
