using Business.Services;
using Domain.Services;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Extesions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddBusinessServices(this IServiceCollection services)
		{
			services.AddScoped<IUsersService, UsersService>();
			return services;
		}
	}
}
