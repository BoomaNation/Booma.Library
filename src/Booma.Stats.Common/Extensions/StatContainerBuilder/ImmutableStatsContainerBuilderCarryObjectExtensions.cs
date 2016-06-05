using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Stats.Common
{
	public static class ImmutableStatsContainerBuilderCarryObjectExtensions
	{
		//generic extensions
		public static ImmutableStatsContainerBuilder<TStatType>.IBuilder WithStat<TStatType>(this ImmutableStatsContainerBuilder<TStatType>.IBuilder builder, TStatType statType, int statValue)
			where TStatType : struct, IConvertible
		{
			//Pretty simple, just initialize the desired stat value.
			builder[statType] = statValue;

			//fluent return
			return builder;
		}

		public static ImmutableStatsContainerBuilder<TStatType>.IBuilder WithDefaults<TStatType>(this ImmutableStatsContainerBuilder<TStatType>.IBuilder builder)
			where TStatType : struct, IConvertible
		{
			return builder.WithAllValuesAs(0);
		}

		public static ImmutableStatsContainerBuilder<TStatType>.IBuilder WithAllValuesAs<TStatType>(this ImmutableStatsContainerBuilder<TStatType>.IBuilder builder, int statValue)
			where TStatType : struct, IConvertible
		{
			//This is ok in Unity. Don't worry about GC.
			foreach (TStatType stat in Enum.GetValues(typeof(TStatType)))
			{
				builder[stat] = statValue;
			}

			//fluent return
			return builder;
		}

		//ResistStatType Extensions
		public static ImmutableStatsContainerBuilder<ResistanceStatType>.IBuilder WithFireResist(this ImmutableStatsContainerBuilder<ResistanceStatType>.IBuilder builder, int resistValue)
		{
			builder[ResistanceStatType.ElementalFire] = resistValue;

			//fluent return
			return builder;
		}

		//Build methods
		public static ImmutableStatsContainer<ResistanceStatType> Build(this ImmutableStatsContainerBuilder<ResistanceStatType>.IBuilder builder)
		{
			//create a new resistance container
			return new ImmutableResistanceStatsContainer(builder.BuilderDictionary);
		}
	}
}
