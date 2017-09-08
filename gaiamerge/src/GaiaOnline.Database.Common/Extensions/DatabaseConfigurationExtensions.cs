using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GaiaOnline
{
	public static class DatabaseConfigurationExtensions
	{
		/// <summary>
		/// Registers <see cref="IOptions{DatabseConfigModel}"/> in the service container.
		/// The config must be available in the provided <see cref="configuration"/>.
		/// </summary>
		/// <param name="services">Service container to register to.</param>
		/// <param name="configuration">The configuration object.</param>
		/// <returns>The service collection to fluently build on.</returns>
		public static IServiceCollection RegisterDatabaseConfigOptions(this IServiceCollection services, IConfigurationRoot configuration)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));

			services.Configure<DatabaseConfigModel>(options => configuration.GetSection(nameof(DatabaseConfigModel)).Bind(options)); //there a better way?

			//fluent return
			return services;
		}

		/// <summary>
		/// Gets the configuration object from the service container.
		/// </summary>
		/// <param name="services">Service container.</param>
		/// <returns>The options model if available. Throws otherwise.</returns>
		public static IOptions<DatabaseConfigModel> GetDatabaseConfig(this IServiceCollection services)
		{
			IOptions<DatabaseConfigModel> dbConfig = services.BuildServiceProvider().GetService<IOptions<DatabaseConfigModel>>();

			if (dbConfig == null || String.IsNullOrWhiteSpace(dbConfig.Value.ConnectionString))
				throw new InvalidOperationException("No connection string is found in the configuration.");

			return dbConfig;
		}
	}
}
