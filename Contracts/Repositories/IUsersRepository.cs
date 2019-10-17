using System;
using Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
	public interface IUsersRepository
	{
		Task<UserDto> GetUserAsync(string publicId);

		Task<bool> CreateUserAsync(UserDto newUser);

		Task<UserDto> Validate(LoginDto loginDto);

	}
}
