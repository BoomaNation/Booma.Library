using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Stats.Common
{
	//The reason this is in Common and not Server is because we may want to show the effect of resistances on the client some day
	/// <summary>
	/// Linear resistance strategy that computed resistance multipliers
	/// this decreases linearlly as resistance increases and is the default implementation.
	/// </summary>
	public class LinearResistanceMultiplierStrategy : IResistanceMultiplierStrategy
	{
		/// <summary>
		/// Private <see cref="IMultiplierProvider"/> type that computes the resistance values
		/// and helps keep a common interface between all multiplier values.
		/// </summary>
		private class LinearResistanceMultiplierProvider : IMultiplierProvider
		{
			/// <inheritdoc />
			public float Multiplier { get; }

			/// <summary>
			/// Computes the resistance multiplier that scales linearlly.
			/// </summary>
			/// <param name="resistValue">Resistance value.</param>
			public LinearResistanceMultiplierProvider(int resistValue)
			{
				//If we have a value we should compute the multiplier from it described by the 1 - (RES / 100) formula
				Multiplier = Math.Max(DEFAULT_MULTIPLIER - (resistValue / 100.0f), 0); //make sure to clamp the value so that nobody is using a negative value to calculate damage
			}

			/// <summary>
			/// Default resistance multiplier.
			/// </summary>
			public LinearResistanceMultiplierProvider()
			{
				Multiplier = DEFAULT_MULTIPLIER;
			}
		}

		private IStatsContainer<ResistanceStatType> resistanceContainer { get; }

		//For the time being default will be strat specific but it may be moved into a seperate const class later
		private const float DEFAULT_MULTIPLIER = 1.0f;

		/// <summary>
		/// Creates a strategy that computes resistance multipliers based on suplied resistance values from
		/// the resistance container.
		/// </summary>
		/// <param name="resistContainer">Resistance container that has the resist values.</param>
		public LinearResistanceMultiplierStrategy(IStatsContainer<ResistanceStatType> resistContainer)
		{
			if (resistContainer == null) throw new ArgumentNullException(nameof(resistContainer));

			resistanceContainer = resistContainer;
		}

		/// <summary>
		/// Generates a resistance multiplier for <paramref name="resistType"/>.
		/// </summary>
		/// <param name="resistType">Type of resistance.</param>
		/// <returns>Float values (1.0 if no resist) that acts as a multiplier for resistance.</returns>
		public IMultiplierProvider ResistanceMultiplier(ResistanceStatType resistType)
		{
			//This is a simple linear multiplier increase based on resistance value.
			//As far as I know this is the default behaviour and can be seen described: http://azurepso.webs.com/psostatistics.htm
			//Where tech damage is: Damage x (1 - RES)

			//Check the resist container for resist values
			IStatProvider<ResistanceStatType> valueProvider = resistanceContainer[resistType];

			//If there is no provider then we need to provide a default resist provider
			if (valueProvider == null)
				return new LinearResistanceMultiplierProvider(); //return default provider

			//If we have a value we should compute the multiplier from it described by the 1 - (RES / 100) formula
			return new LinearResistanceMultiplierProvider(valueProvider.Value);
		}
	}
}
