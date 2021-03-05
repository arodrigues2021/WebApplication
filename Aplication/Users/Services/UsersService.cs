using Aplication.Users.DTO;
using cache;
using Dapper;
using Domain;
using Domain.DTO;
using Domain.Users.Entities;
using Infrastructure.Users.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Aplication.Users.Services
{
    public class UsersService : IUsersService
    {

        private readonly IUsersRepository usersRepository;

        private readonly IDbConnection _db;

        private readonly Config _config;

        private readonly ICacheService<object> _cacheService;

        static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(UsersService));

        public UsersService(Config config, IUsersRepository usersRepository, ICacheService<object> cacheService)
        {
            _config = config;

            _db = new SqlConnection(_config.connectionStrings.Database);

            this.usersRepository = usersRepository;

            _cacheService = cacheService;
        }

        public int AddUsuario(UsuarioDTO usuario)
        {
            LoginDTO user = usersRepository.GetUsersByEmail(usuario.Email);
            if (user!=null)
            {
                return 0;
            }

            var parameter = new
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Password = usuario.Password,
                Movil = usuario.Movil,
                Balance = usuario.Balance
            };

            string SqlString = $@"INSERT INTO [Usuario]
                                     ([Nombre],
                                      [Apellido],
                                      [Email],
                                      [Password],
                                      [Movil],
                                      [Balance])
                                VALUES
                                   (@Nombre, 
                                    @Apellido, 
                                    @Email, 
                                    @Password, 
                                    @Movil, 
                                    @Balance)";

            int result = _db.Execute(SqlString, parameter);

            var mykey = "listUsers";

            if (_cacheService.IsExists(mykey))
            {
                _cacheService.IsExistsDelete(mykey);

                log.Info("Delete Cache Redis - AddUsuario");
            }

            return result;
        }


        public int BorrarUsuario(BorradoDTO usuario)
        {

            LoginDTO user = usersRepository.GetUsersByEmail(usuario.Email);

            var result = 0;

            if (user != null)
            {
                var id = user.id;

                string SqlString = $@" DELETE FROM [Usuario] WHERE [id] =  @id ";

                result = _db.Execute(SqlString, new { id = id });
            }

            var mykey = "listUsers";

            if (_cacheService.IsExists(mykey))
            {
                _cacheService.IsExistsDelete(mykey);

                log.Info("Delete Cache Redis - BorrarUsuario");
            }

            return result;
        }

        public int UpdateUsuario(UsuarioDTO usuario)
        {
            LoginDTO user = usersRepository.GetUsersByEmail(usuario.Email);
            if (user == null)
            {
                return 0;
            }

            var parameter = new
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Password = usuario.Password,
                Movil = usuario.Movil,
                Balance = usuario.Balance,
                id = user.id
            };

            string SqlString = $@"UPDATE Usuario SET
                                       [Nombre]   = @Nombre
                                      ,[Apellido] = @Apellido
                                      ,[Password] = @Password
                                      ,[Movil]    = @Movil
                                      ,[Balance]  = @Balance  WHERE id = @id";

            int result = _db.Execute(SqlString, parameter);

            return result;

        }

        public List<Usuario> GetListUsers()
        {
            try
            {


                List<Usuario> pRet = (from a in usersRepository.GetListUsers()

                                      select new Usuario()
                                      {
                                          Nombre = a.Nombre,
                                          Apellido = a.Apellido,
                                          Email = a.Email,
                                          Movil = a.Movil,
                                          Balance = a.Balance

                                      }).ToList();

                return pRet;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public LoginDTO GetUsersByEmail(string email, string password)
        {
            try
            {

                LoginDTO pRet = usersRepository.GetUsersByEmail(email);

                return pRet;
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}
