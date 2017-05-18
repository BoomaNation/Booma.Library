using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Booma.GameServerList.Lib;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MigrationsGenerator
{
	public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddEntityFramework()
				.AddDbContext<GameServerListDbContext>(options =>
				{
					options.UseMySql($"Server=localhost;Database=boomashiplist;Uid=root;Pwd=test;",
						mysqloptions =>
						{
							//We need to override the migrations assembly so that it adds the migrations to this project
							mysqloptions.MigrationsAssembly(this.GetType().Assembly.FullName);
						});

				});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{

			app.Run(async (context) =>
			{
				await context.Response.WriteAsync("Hello World!");
			});
		}
	}
}
