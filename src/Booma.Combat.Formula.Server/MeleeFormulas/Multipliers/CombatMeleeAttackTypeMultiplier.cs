using Booma.Stats.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Combat.Formula.Server
{
	//Values from http://www.freewebs.com/azurepso/psostatistics.htm
	/// <summary>
	/// Psuedo-float-enum class that provides constant float values for mulitpliers on damage.
	/// </summary>
	public static class CombatMeleeAttackTypeMultiplier
	{
		protected class CombatAttackTypeMultiplier : IMultiplierProvider
		{
			public float Multiplier { get; }

			public CombatAttackTypeMultiplier(float val)
			{
				Multiplier = val;
			}
		}

		/// <summary>
		/// Base/weak mutliplier for damage.
		/// </summary>
		public static IMultiplierProvider Base { get; } = new CombatAttackTypeMultiplier(0.9f);


		/// <summary>
		/// Heavy multiplier for damage.
		/// </summary>
		public static IMultiplierProvider Heavy { get; } = new CombatAttackTypeMultiplier(1.701f);
	}
}
