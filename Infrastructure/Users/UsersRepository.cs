using Domain.Users.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Data.SqlClient;
using Infrastructure.Users.Interface;
using System.Linq;
using Aplication.Users.DTO;
using Domain;

namespace Infrastructure.Users
{
    public class UsersRepository :  IUsersRepository
    {
        private readonly IDbConnection _db;

        private readonly Config _config;
        public UsersRepository(Config config)
        {
            _config = config;

            _db = new SqlConnection(_config.connectionStrings.Database);

        }

        public List<Usuario> GetListUsers()
        {
            string SqlString = $@"SELECT   [Id]
                                          ,[Nombre]
                                          ,[Apellido]
                                          ,[Email]
                                          ,[Movil]
                                      FROM [dbo].[Usuario]";

            return _db.Query<Usuario>(SqlString).ToList();
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
