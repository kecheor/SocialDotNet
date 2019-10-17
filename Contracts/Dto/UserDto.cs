using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
namespace Domain.Dto
{
	[Serializable]
	public class UserDto
	{
		protected Guid UserId { set; get; }
		[Required]
		public string PublicId { set; get; }
		[Required]
		public string Login { set; get; }
		[Required]
		public string Password { set; get; }
		[Required]
		public string Name { set; get; }
		public string? LastName { set; get; }
		public string? Gender { set; get; }
		public string? Location { set; get; }
		public string[] Interests { set; get; }
	}
}
