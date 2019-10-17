using Domain.Dto;
using Domain.Models;
using Domain.Repositories;
using Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Business.Services
{
	public class UsersService : IUsersService
	{
		private readonly IUsersRepository _usersRepository;
		private readonly IConfiguration _appSettings;
		public UsersService(IConfiguration appSettings, IUsersRepository users)
		{
			_usersRepository = users;
			_appSettings = appSettings;
		}

		public async Task<UserToken> AuthenticateAsync(LoginDto loginDto)
		{
			var user = await _usersRepository.Validate(loginDto);
			if (user == null)
			{
				return null;
			}

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = _appSettings.GetSection("Secrets").GetSection("Token").Value;
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[] { 
					new Claim(ClaimTypes.NameIdentifier, user.PublicId),
					new Claim("PublicId", user.PublicId)
				}),
				Expires = DateTime.UtcNow.AddDays(1),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);

			return new UserToken
			{
				PublicId = user.PublicId,
				Name = user.Name,
				Token = tokenHandler.WriteToken(token)
			};

		}

		public async Task<bool> CreateUserAsync(UserDto newUser)
		{
			var result = await _usersRepository.CreateUserAsync(newUser);
			return result;
		}

		public async Task<UserDto> GetUserAsync(string publicId)
		{
			var user = await _usersRepository.GetUserAsync(publicId);
			if (user == null)
				return null;
			return user;
		}
	}
}
