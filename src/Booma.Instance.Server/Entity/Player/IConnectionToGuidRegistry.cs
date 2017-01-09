using Booma.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Instance.Server
{
	public interface IConnectionToGuidRegistry
	{
		void Register(int connectionId, NetworkEntityGuid guid);
	}
}
