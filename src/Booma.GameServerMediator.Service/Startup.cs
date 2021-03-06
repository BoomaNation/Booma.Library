﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaloLive.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Booma
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Add framework services.
			services.AddMvc();

			services.RegisterDatabaseConfigOptions(Configuration);

			IOptions<DatabaseConfigModel> dbConfig = services.GetDatabaseConfig();

			services.AddHaloLiveAuthorization();
			services.AddDbContext<GameSessionDatabaseContext>(options => options.UseMySql(dbConfig.Value.ConnectionString));
			services.AddTransient<IGameSessionRepository, DatabaseGameSessionRepository>();

			services.AddDbContext<CharacterDatabaseContext>(options => options.UseMySql(dbConfig.Value.ConnectionString));
			services.AddTransient<ICharacterRepository, DatabaseCharacterRepository>();
			services.AddTransient<IReadonlyCharacterRepository, DatabaseCharacterRepository>();

			services.AddSingleton<ISectionIdCalculatorStrategy>(new PlayerSectionIdCalculatorStrategy());
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			app.UseMvc();
		}
	}
}
