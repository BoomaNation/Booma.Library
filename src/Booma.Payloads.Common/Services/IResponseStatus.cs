using System;
using System.Collections.Generic;
using System.Linq;

namespace Booma
{
	/// <summary>
	/// Contract for an object that can provide a <see cref="TResponseCodeType"/> code.
	/// (The generic tries to constaint to Enums. No language support.)
	/// </summary>
	/// <typeparam name="TResponseCodeType">Status type.</typeparam>
	public interface IResponseStatus<TResponseCodeType>
		where TResponseCodeType : struct, IConvertible, IComparable, IFormattable
	{
		/// <summary>
		/// Response status; the response code of the request.
		/// </summary>
		TResponseCodeType ResponseCode { get; }
	}
}
