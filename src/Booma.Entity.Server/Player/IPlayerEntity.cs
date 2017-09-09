using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma
{
	/// <summary>
	/// Interfaces that contracts implementers as player entities.
	/// </summary>
	public interface IPlayerEntity : IActiveStatsQueryable, IStatsQueryable
	{

	}
}
