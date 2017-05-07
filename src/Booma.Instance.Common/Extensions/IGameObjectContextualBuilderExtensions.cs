using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Instance.Common
{
	public static class IGameObjectContextualBuilderExtensions
	{
		/// <summary>
		/// Adds the <see cref="ISpawnContext"/> to the <see cref="IGameObjectContextualBuilder"/>.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static IGameObjectContextualBuilder With(this IGameObjectContextualBuilder builder, ISpawnContext context)
		{
			//Add the context and return the builder.
			return context.ProvideContext(builder);
		}
	}
}
