using Booma.Stats.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Combat.Formula.Server
{
	/// <summary>
	/// Basic/default melee damage result strategy based on original damage formula.
	/// Provides a <see cref="CombatStatType.HitPoints"/> united damage <see cref="Value"/>.
	/// </summary>
	public class MeleeDamageResultStrategy : IStatProvider<CombatStatType>
	{
		//Melee damage generally affects hit points so we apply a hitpoints stat unit to the value provided.
		/// <summary>
		/// Provides the units for the <see cref="Value"/> integer.
		/// For Melee Damage calculations it is always <see cref="CombatStatType.HitPoints"/> as damage usually means HP damage.
		/// </summary>
		public CombatStatType StatType { get; } = CombatStatType.HitPoints;

		/// <summary>
		/// Hitpoints damage value.
		/// </summary>
		public int Value { get; }

		public MeleeDamageResultStrategy(IStatProvider<CombatStatType> finalATP, IStatProvider<CombatStatType> targetDFP, IMultiplierProvider attackMultiplier)
		{
			//CombatDamage = ((UserATP - TargetDEF) / 5) * AttackTypeMultiplier
			//http://www.freewebs.com/azurepso/psostatistics.htm
			Value = (int)(((finalATP.Value - targetDFP.Value) / 5.0f) * attackMultiplier.Multiplier);
		}
	}
}
