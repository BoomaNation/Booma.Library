using Booma.GameServerList.Lib;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Booma.GameServerList.Module
{
	public class ControllerRegistry : IApplicationFeatureProvider<ControllerFeature>
	{
		public void PopulateFeature([NotNull] IEnumerable<ApplicationPart> parts, [NotNull] ControllerFeature feature)
		{
			if (parts == null) throw new ArgumentNullException(nameof(parts));
			if (feature == null) throw new ArgumentNullException(nameof(feature));

			feature.Controllers.Add(typeof(GameListRequestController).GetTypeInfo());
		}
	}
}
