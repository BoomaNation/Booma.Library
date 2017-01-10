using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Instance.Common
{
	public interface IDefaultStateProvider
	{
		/// <summary>
		/// Indicates the default state.
		/// </summary>
		byte DefaultState { get; }
	}

	public interface IDefaultStateProvider<TNetworkStateType> : IDefaultStateProvider
		where TNetworkStateType : struct, IConvertible
	{
		/// <summary>
		/// Indicates the default state.
		/// </summary>
		TNetworkStateType DefaultState { get; }
	}
}
