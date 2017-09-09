using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma
{
	/// <summary>
	/// 
	/// </summary>
	public interface ISpawnContext
	{
		/// <summary>
		/// Visit method that provides the context content to the builder.
		/// (Returns object for fluent building purposes)
		/// </summary>
		/// <param name="builder">GameObject contextual builder.</param>
		IGameObjectContextualBuilder ProvideContext(IGameObjectContextualBuilder builder);
	}
}
