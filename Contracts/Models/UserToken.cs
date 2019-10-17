using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
	public class UserToken
	{
		public string PublicId { set; get; }
		public string Name { set; get; }
		public string Token { set; get; }
	}
}
