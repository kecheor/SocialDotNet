using Domain.Dto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
	public interface IUsersService
	{
		Task<UserDto> GetUserAsync(string publicId);

		Task<bool> CreateUserAsync(UserDto newUser);

		Task<UserToken> AuthenticateAsync(LoginDto loginDto);
	}
}
