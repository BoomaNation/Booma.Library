using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GaiaOnline
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
			services.AddLogging();
			services.Configure<DatabaseConfigModel>(options => Configuration.GetSection(nameof(DatabaseConfigModel)).Bind(options)); //there a better way?

			IOptions<DatabaseConfigModel> dbConfig = services.BuildServiceProvider().GetService<IOptions<DatabaseConfigModel>>();

			if(dbConfig == null || String.IsNullOrWhiteSpace(dbConfig.Value.ConnectionString))
				throw new InvalidOperationException("No connection string is found in the configuration.");

			services.AddDbContext<GameServerListDbContext>(options => options.UseMySql(dbConfig.Value.ConnectionString));
			services.AddTransient<IGameServerListRepository, DatabaseGameServerListRepository>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			app.UseMvcWithDefaultRoute();
		}
	}
}
