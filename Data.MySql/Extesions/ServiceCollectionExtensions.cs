using Data.MySql.Repositories;
using Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Data.MySql.Extesions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddDataAccess(this IServiceCollection services)
		{
			services.TryAddScoped<IUsersRepository, UsersRepository>();
			return services;
		}
	}
}
