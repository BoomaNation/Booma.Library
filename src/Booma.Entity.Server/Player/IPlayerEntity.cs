using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Entity.Server.Player
{
	/// <summary>
	/// Interfaces that contracts implementers as player entities.
	/// </summary>
	public interface IPlayerEntity : IActiveStatsQueryable, IStatsQueryable
	{

	}
}
