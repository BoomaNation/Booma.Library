using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Instance.NetworkObject
{
	/// <summary>
	/// Service that indicates a default state.
	/// </summary>
	public interface IDefaultStateProvider
	{
		/// <summary>
		/// Indicates the default state.
		/// </summary>
		byte DefaultState { get; }
	}

	/// <summary>
	/// Generic service that indicates a default state.
	/// </summary>
	/// <typeparam name="TNetworkStateType">The state type.</typeparam>
	public interface IDefaultStateProvider<out TNetworkStateType> : IDefaultStateProvider
		where TNetworkStateType : struct, IConvertible
	{
		/// <summary>
		/// Indicates the default state.
		/// </summary>
		new TNetworkStateType DefaultState { get; }
	}
}
