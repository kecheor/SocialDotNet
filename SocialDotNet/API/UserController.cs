using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dto;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace SocialDotNew.API
{
	[Authorize]
	[EnableCors("allowall")]
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private IUsersService _usersService;

		public UserController(IUsersService users)
		{
			_usersService = users;
		}

		[AllowAnonymous]
		[HttpPost("authenticate")]
		public async Task<IActionResult> Authenticate([FromBody]LoginDto loginDto)
		{
			var token = await _usersService.AuthenticateAsync(loginDto);
			if (token == null)
				return BadRequest(new { message = "Username or password is incorrect" });
			return Ok(token);
		}


		[HttpGet("{id?}")]
		public async Task<IActionResult> Get(string id)
		{
			var model = await _usersService.GetUserAsync(id ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
			if (model == null)
				return NotFound();
			return Ok(model);
		}

		[AllowAnonymous]
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody]UserDto newUser)
		{
			var result = await _usersService.CreateUserAsync(newUser);
			if (result)
			{
				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}

		
	}
}