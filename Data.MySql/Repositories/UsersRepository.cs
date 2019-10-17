using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Domain.Dto;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;

namespace Data.MySql.Repositories
{
	public class UsersRepository : BaseRepository<UserDto>, IUsersRepository
	{
		protected readonly string PasswordSecret;

		public UsersRepository(IConfiguration appSettings) : base(appSettings.GetConnectionString("MySQL"))
		{
			PasswordSecret = appSettings.GetSection("Secrets").GetSection("Password").Value;
		}

		private string HashedPassword(string password)
		{
			var salted = Encoding.ASCII.GetBytes(password + PasswordSecret);
			var md5 = new MD5CryptoServiceProvider();
			var hash = md5.ComputeHash(salted);
			var hashed = Encoding.ASCII.GetString(hash);
			return hashed;
		}

		public async Task<bool> CreateUserAsync(UserDto newUser)
		{
			var sql = @"
INSERT INTO Users
(
	UserId,
	Login,
	Password,
	PublicId, 
	Name, 
	Surname, 
	Gender, 
	Location, 
	Interests 
)
VALUES
(
	UUID(),
	@Login,
	@Password,
	@PublicId, 
	@Name, 
	@Surname, 
	@Gender, 
	@Location, 
	@Interests 
)";

			var pars = new Dictionary<string, object>
			{
				{"@Login", newUser.Login},
				{"@Password", HashedPassword(newUser.Password)},
				{"@PublicId", newUser.PublicId},
				{"@Name", newUser.Name},
				{"@Surname", newUser.LastName},
				{"@Gender", newUser.Gender},
				{"@Location", newUser.Location},
				{"@Interests", string.Join(";", newUser.Interests) }
			};
			var result = await ExecuteNonQuery(sql, pars);
			return result == 1;
		}


		public async Task<UserDto> Validate(LoginDto loginDto)
		{
			var sql = @"
SELECT 
	UserId, 
	PublicId, 
	Name, 
	Surname, 
	Gender, 
	Location, 
	Interests 
FROM Users 
WHERE Login=@login AND Password=@password";
			var pars = new Dictionary<string, object>
			{
				{"login", loginDto.Login },
				{"password", HashedPassword(loginDto.Password) }
			};
			var result = await this.ExecuteQuery(sql, pars);
			return result.FirstOrDefault();
		}

		public async Task<UserDto?> GetUserAsync(string publicId)
		{
			var sql = @"
SELECT 
	UserId, 
	PublicId, 
	Name, 
	Surname, 
	Gender, 
	Location, 
	Interests 
FROM Users 
WHERE PublicId=@UserID";
			var pars = new Dictionary<string, object>
			{
				{"UserId", publicId }
			};
			var result = await this.ExecuteQuery(sql, pars);
			return result.FirstOrDefault();
		}

		protected override async Task<UserDto> Read(DbDataReader rdr)
		{
			await rdr.ReadAsync();
			UserDto result = new UserDto
			{
				//UserId = rdr.GetGuid(0),
				PublicId = rdr.GetString(1),
				Name = rdr.GetString(2),
				LastName = rdr.IsDBNull(3) ? null : rdr.GetString(3),
				Gender = rdr.IsDBNull(4) ? null : rdr.GetString(4),
				Location = rdr.IsDBNull(5) ? null : rdr.GetString(5),
				Interests = rdr.IsDBNull(6) ? null : rdr.GetString(6).Split(';', StringSplitOptions.RemoveEmptyEntries)
			};
			return result;

		}
	}
}
