using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace GaiaOnline.Authentication.Service
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Threading.Tasks;
	using HaloLive.Hosting;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;

	namespace GaiaOnline
	{
		public class Program
		{
			/*Optional Arguments
			--usehttps={certPath}: Enables SSL/HTTPS on the web host.

			--url={customUrl}: Starts the listener on the specified url and port. For example http://localhost:5000.*/
			public static void Main(string[] args)
			{
				var host = new WebHostBuilder()
					.ConfigureKestrelHostWithCommandlinArgs(args) //setups HaloLive specific hosting
					.UseContentRoot(Directory.GetCurrentDirectory())
					.UseStartup<Startup>()
					.UseApplicationInsights()
					.Build();

				host.Run();
			}
		}
	}

}
