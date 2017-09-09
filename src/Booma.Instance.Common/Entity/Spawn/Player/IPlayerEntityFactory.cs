using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma
{
	public interface IPlayerEntityFactory : IEntityFactory<IPlayerSpawnContext>
	{
		//Methods were moved to extension methods so implementing types don't need to implemt them every time
	}
}
