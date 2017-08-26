using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Instance.NetworkObject
{
	/// <summary>
	/// Simple container that contains a <typeparamref name="TTagType"/>.
	/// </summary>
	/// <typeparam name="TTagType">Tag type.</typeparam>
	public interface ITagProvider<TTagType>
	{
		/// <summary>
		/// The tag value.
		/// </summary>
		TTagType Tag { get; }
	}
}
