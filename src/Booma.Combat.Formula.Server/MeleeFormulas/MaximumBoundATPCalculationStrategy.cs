using Booma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma
{
	/// <summary>
	/// A <see cref="IStatProvider{TStatType}"/> that computes and provides a value for the minimum bound ATP
	/// for combat calculation.
	/// </summary>
	public class MaximumBoundATPCalculationStrategy : IStatProvider<CombatStatType>
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

		public MaximumBoundATPCalculationStrategy(IStatProvider<CombatStatType> baseATP, IStatProvider<CombatStatType> maximumWeaponBoundATP, IStatProvider<CombatStatType> weaponRangeBonus)
		{
			if (baseATP.StatType != CombatStatType.AttackPower)
				throw new ArgumentException($"Base ATP must have ATP units in {nameof(CombatStatType.AttackPower)} but had {baseATP.StatType} instead",
					nameof(baseATP));

			if (maximumWeaponBoundATP.StatType != CombatStatType.AttackPower)
				throw new ArgumentException($"Maximum weapon bound ATP must have ATP units in {nameof(CombatStatType.AttackPower)} but had {maximumWeaponBoundATP.StatType} instead",
					nameof(maximumWeaponBoundATP));

			if (weaponRangeBonus.StatType != CombatStatType.AttackPower)
				throw new ArgumentException($"Weapons range bonus must have ATP units in {nameof(CombatStatType.AttackPower)} but had {weaponRangeBonus.StatType} instead",
					nameof(weaponRangeBonus));

			//CombatATP = BaseATP + MaximumWeaponATP
			//Can be seen here: http://www.freewebs.com/azurepso/psostatistics.htm
			Value = baseATP.Value + maximumWeaponBoundATP.Value + weaponRangeBonus.Value;
		}
	}
}
