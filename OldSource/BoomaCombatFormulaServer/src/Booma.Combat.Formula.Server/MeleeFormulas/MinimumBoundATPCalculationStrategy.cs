using Booma.Stats.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Combat.Formula.Server
{
	/// <summary>
	/// A <see cref="IStatProvider{TStatType}"/> that computes and provides a value for the minimum bound ATP
	/// for combat calculation.
	/// </summary>
	public class MinimumBoundATPCalculationStrategy : IStatProvider<CombatStatType>
	{
		/// <summary>
		/// Provides the units for the <see cref="Value"/> integer.
		/// For ATP calculations it is always <see cref="CombatStatType.AttackPower"/>.
		/// </summary>
		public CombatStatType StatType { get; } = CombatStatType.AttackPower;

		/// <summary>
		/// Provides the minimumbound ATP value for combat calculation.
		/// </summary>
		public int Value { get; }

		public MinimumBoundATPCalculationStrategy(IStatProvider<CombatStatType> baseATP, IStatProvider<CombatStatType> minimumWeaponBoundATP)
		{
			if (baseATP.StatType != CombatStatType.AttackPower)
				throw new ArgumentException($"Base ATP must have ATP units in {nameof(CombatStatType.AttackPower)} but had {baseATP.StatType} instead",
					nameof(baseATP));

			if (minimumWeaponBoundATP.StatType != CombatStatType.AttackPower)
				throw new ArgumentException($"Minimum weapon bound ATP must have ATP units in {nameof(CombatStatType.AttackPower)} but had {minimumWeaponBoundATP.StatType} instead",
					nameof(baseATP));

			//CombatATP = BaseATP + MimWeaponATP
			//Can be seen here: http://www.freewebs.com/azurepso/psostatistics.htm
			Value = baseATP.Value + minimumWeaponBoundATP.Value;
		}
	}
}
