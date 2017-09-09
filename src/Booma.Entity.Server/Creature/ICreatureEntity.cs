using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma
{
	/// <summary>
	/// Interfaces that contracts implementers as creature entities.
	/// </summary>
	public interface ICreatureEntity : IActiveStatsQueryable, IStatsQueryable
	{

	}
}
