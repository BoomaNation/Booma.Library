using GladLive.Module.System.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Booma.GameServerList.Module
{
	/// <summary>
	/// Register the controllers in the MVC builder.
	/// </summary>
	public class GameServerListMvcModule : MvcBuilderServiceRegistrationModule
	{
		public GameServerListMvcModule(IMvcBuilder mvcBuilder) 
			: base(mvcBuilder)
		{

		}

		public override void Register()
		{
			//Register the modules controllers.
			mvcBuilderService.ConfigureApplicationPartManager(apm => apm.FeatureProviders.Add(new ControllerRegistry()));
		}
	}
}
