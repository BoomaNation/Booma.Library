using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma
{
	/// <summary>
	/// Provides clean and simple way to create and initialize the immutable stats container.
	/// Constructing them directly is a bulky and cumbersome process.
	/// </summary>
	/// <typeparam name="TStatType">Container stats type. Not all types are supported.</typeparam>
	public class ImmutableStatsContainerBuilder<TStatType>
		where TStatType : struct, IConvertible
	{
		public interface IBuilder
		{
			//We need a setter like this to build the API
			int this[TStatType stat] { set; }

			Dictionary<TStatType, int> BuilderDictionary { get; }
		}

		/// <summary>
		/// Simple factory interface that provides .NET style API access to a builder object that
		/// does the construction.
		/// </summary>
		public interface IFactory
		{
			IBuilder Create();
		}

		/// <summary>
		/// Hidden implementation of the factory object.
		/// Simply returns new builders.
		/// </summary>
		protected class ImmutableStatsContainerBuilderFactory : IFactory
		{
			/// <summary>
			/// Creates a new builder object to aid in construction of stats container.
			/// </summary>
			/// <returns>A new <see cref="IBuilder"/> object for construction.</returns>
			public IBuilder Create()
			{
				return new ImmutableStatsContainerBuilderCarryObject();
			}
		}

		/// <summary>
		/// Hidden implementation of the <see cref="IBuilder"/> interface.
		/// </summary>
		protected class ImmutableStatsContainerBuilderCarryObject : IBuilder
		{
			/// <summary>
			/// Setter for the internal stats container builder.
			/// It is mutable and allows for changes pre-build.
			/// </summary>
			/// <param name="stat">Stat type.</param>
			/// <returns>The value that was set.</returns>
			public int this[TStatType stat]
			{
				//Don't use expression body set due to appveyor failure
				set { BuilderDictionary[stat] = value; }
			}

			/// <summary>
			/// Exposed dictionary that manages the incoming build requests.
			/// </summary>
			public Dictionary<TStatType, int> BuilderDictionary { get; }

			/// <summary>
			/// Creates a new <see cref="IBuilder"/>.
			/// </summary>
			public ImmutableStatsContainerBuilderCarryObject()
			{
				//TODO: Better way to get initial size
				BuilderDictionary = new Dictionary<TStatType, int>(10);
			}
		}

		/// <summary>
		/// Factory of <see cref="IBuilder"/>s that can construct <see cref="ImmutableStatsContainer{TStatType}"/> of
		/// type <see cref="TStatType"/>.
		/// </summary>
		public static IFactory Factory { get; } = new ImmutableStatsContainerBuilderFactory();
	}
}
