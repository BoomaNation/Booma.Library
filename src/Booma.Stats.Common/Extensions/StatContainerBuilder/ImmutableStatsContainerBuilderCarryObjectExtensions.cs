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
		public static ImmutableStatsContainerBuilder<ResistanceStatType>.IBuilder WithFireResist(this ImmutableStatsContainerBuilder<ResistanceStatType>.IBuilder builder, int statValue)
		{
			builder.WithStat(ResistanceStatType.ElementalFire, statValue);

			//fluent return
			return builder;
		}

		public static ImmutableStatsContainerBuilder<ResistanceStatType>.IBuilder WithDarkResist(this ImmutableStatsContainerBuilder<ResistanceStatType>.IBuilder builder, int statValue)
		{
			builder.WithStat(ResistanceStatType.ElementalDark, statValue);

			//fluent return
			return builder;
		}

		public static ImmutableStatsContainerBuilder<ResistanceStatType>.IBuilder WithIceResist(this ImmutableStatsContainerBuilder<ResistanceStatType>.IBuilder builder, int statValue)
		{
			builder.WithStat(ResistanceStatType.ElementalIce, statValue);

			//fluent return
			return builder;
		}

		public static ImmutableStatsContainerBuilder<ResistanceStatType>.IBuilder WithLightResist(this ImmutableStatsContainerBuilder<ResistanceStatType>.IBuilder builder, int statValue)
		{
			builder.WithStat(ResistanceStatType.ElementalLight, statValue);

			//fluent return
			return builder;
		}

		public static ImmutableStatsContainerBuilder<ResistanceStatType>.IBuilder WithThunderResist(this ImmutableStatsContainerBuilder<ResistanceStatType>.IBuilder builder, int statValue)
		{
			builder.WithStat(ResistanceStatType.ElementalThunder, statValue);

			//fluent return
			return builder;
		}

		//CombatStatType Extensions
		public static ImmutableStatsContainerBuilder<CombatStatType>.IBuilder WithAttackAccuracy(this ImmutableStatsContainerBuilder<CombatStatType>.IBuilder builder, int statValue)
		{
			builder.WithStat(CombatStatType.AttackAccuracy, statValue);

			//fluent return
			return builder;
		}

		public static ImmutableStatsContainerBuilder<CombatStatType>.IBuilder WithAttackPower(this ImmutableStatsContainerBuilder<CombatStatType>.IBuilder builder, int statValue)
		{
			builder.WithStat(CombatStatType.AttackPower, statValue);

			//fluent return
			return builder;
		}

		public static ImmutableStatsContainerBuilder<CombatStatType>.IBuilder WithDefensivePower(this ImmutableStatsContainerBuilder<CombatStatType>.IBuilder builder, int statValue)
		{
			builder.WithStat(CombatStatType.DefensivePower, statValue);

			//fluent return
			return builder;
		}

		public static ImmutableStatsContainerBuilder<CombatStatType>.IBuilder WithEvasivePower(this ImmutableStatsContainerBuilder<CombatStatType>.IBuilder builder, int statValue)
		{
			builder.WithStat(CombatStatType.EvasivePower, statValue);

			//fluent return
			return builder;
		}

		public static ImmutableStatsContainerBuilder<CombatStatType>.IBuilder WithHitPoints(this ImmutableStatsContainerBuilder<CombatStatType>.IBuilder builder, int statValue)
		{
			builder.WithStat(CombatStatType.HitPoints, statValue);

			//fluent return
			return builder;
		}

		public static ImmutableStatsContainerBuilder<CombatStatType>.IBuilder WithLuck(this ImmutableStatsContainerBuilder<CombatStatType>.IBuilder builder, int statValue)
		{
			builder.WithStat(CombatStatType.Luck, statValue);

			//fluent return
			return builder;
		}

		public static ImmutableStatsContainerBuilder<CombatStatType>.IBuilder WithMentalStrength(this ImmutableStatsContainerBuilder<CombatStatType>.IBuilder builder, int statValue)
		{
			builder.WithStat(CombatStatType.MentalStrength, statValue);

			//fluent return
			return builder;
		}

		public static ImmutableStatsContainerBuilder<CombatStatType>.IBuilder WithTechniquePoints(this ImmutableStatsContainerBuilder<CombatStatType>.IBuilder builder, int statValue)
		{
			builder.WithStat(CombatStatType.TechniquePoints, statValue);

			//fluent return
			return builder;
		}

		//TransientStatType Extensions
		public static ImmutableStatsContainerBuilder<TransientStatType>.IBuilder WithHitPoints(this ImmutableStatsContainerBuilder<TransientStatType>.IBuilder builder, int statValue)
		{
			builder.WithStat(TransientStatType.HitPoints, statValue);

			//fluent return
			return builder;
		}

		public static ImmutableStatsContainerBuilder<TransientStatType>.IBuilder WithPhotonBlastCharge(this ImmutableStatsContainerBuilder<TransientStatType>.IBuilder builder, int statValue)
		{
			builder.WithStat(TransientStatType.PhotonBlastCharge, statValue);

			//fluent return
			return builder;
		}

		public static ImmutableStatsContainerBuilder<TransientStatType>.IBuilder WithTechniquePoints(this ImmutableStatsContainerBuilder<TransientStatType>.IBuilder builder, int statValue)
		{
			builder.WithStat(TransientStatType.TechniquePoints, statValue);

			//fluent return
			return builder;
		}

		//Build methods
		public static ImmutableStatsContainer<ResistanceStatType> Build(this ImmutableStatsContainerBuilder<ResistanceStatType>.IBuilder builder)
		{
			//create a new resistance container
			return new ImmutableResistanceStatsContainer(builder.BuilderDictionary);
		}

		public static ImmutableStatsContainer<CombatStatType> Build(this ImmutableStatsContainerBuilder<CombatStatType>.IBuilder builder)
		{
			//create a new combat container
			return new ImmutableCombatStatsContainer(builder.BuilderDictionary);
		}

		public static ImmutableStatsContainer<TransientStatType> Build(this ImmutableStatsContainerBuilder<TransientStatType>.IBuilder builder)
		{
			//create a new combat container
			return new ImmutableTransientStatsContainer(builder.BuilderDictionary);
		}
	}
}
