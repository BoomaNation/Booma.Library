using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Instance.Common
{
	public interface IEntityState
	{
		byte State { get; set; }
	}

	public interface IEntityState<TEntityStateType> : IEntityState
	{
		TEntityStateType State { get; set; }
	}
}
