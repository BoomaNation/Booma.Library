using Booma.Payloads.Common;
using GladNet.Payload;
using GladNet.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Booma.Payload.Common.Tests
{
	public abstract class GeneralPayloadTests
	{
		protected ITestOutputHelper logger { get; }

		public GeneralPayloadTests(ITestOutputHelper output)
		{
			this.logger = output;
		}

		[Fact]
		public void Test_All_Payloads_Marked_With_BoomaAttribute()
		{
			List<Type> failedTypes = new List<Type>();

			foreach (Type t in GetPacketPayloadTypes())
			{
				if (t.GetCustomAttribute<BoomaPayloadAttribute>() == null)
				{
					failedTypes.Add(t);
					logger.WriteLine($"***ERROR***: {t?.FullName} doesn't implement the {nameof(BoomaPayloadAttribute)}");
				}
			}

			if (failedTypes.Count > 0)
				throw new Exception("Invalid Payload declarations.");
		}

		[Fact]
		public void Test_All_Payloads_Marked_With_Contract()
		{
			List<Type> failedTypes = new List<Type>();

			foreach (Type t in GetPacketPayloadTypes())
			{
				if (t.GetCustomAttribute<GladNetSerializationContractAttribute>() == null)
				{
					failedTypes.Add(t);
					logger.WriteLine($"***ERROR***: {t?.FullName} doesn't implement the {nameof(GladNetSerializationContractAttribute)}");
				}
			}

			if (failedTypes.Count > 0)
				throw new Exception("Invalid Payload declarations.");
		}

		[Fact]
		public void Test_All_ProtoContractMarkedTypes_Contains_Parameterless_Ctor()
		{
			List<Type> failedTypes = new List<Type>();

			foreach (Type t in TypeToUseToFindAssembly.Assembly.GetTypes().Where(x => x.IsClass && x.GetCustomAttribute<GladNetSerializationContractAttribute>() != null))
			{
				if (t.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(ctor => ctor.GetParameters().Count() == 0).Count() == 0)
				{
					failedTypes.Add(t);
					logger.WriteLine($"***ERROR***: {t?.FullName} doesn't implement a parameterless constructor All GladNet serializable types must implement atleast a protected parameterless ctor.");
				}

				if (t.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(ctor => ctor.IsPublic).Count() == 0)
				{
					failedTypes.Add(t);
					logger.WriteLine($"***ERROR***: {t?.FullName} doesn't implement any public constructor All GladNet serializable types must implement atleast a 1 public ctor.");
				}
			}

			if (failedTypes.Count > 0)
				throw new Exception("Invalid Payload declarations.");
		}

		[Fact]
		public void Test_All_Payloads_Marked_With_BoomaAttribute_Have_Unique_Includes()
		{
			List<Type> failedTypes = new List<Type>();

			IEnumerable<Tuple<Type, BoomaPayloadAttribute>> tuples = GetPacketPayloadTypes()
				.Select(x => new Tuple<Type, BoomaPayloadAttribute>(x, x.GetCustomAttribute<BoomaPayloadAttribute>()))
				.Where(x => x != null);

			foreach (var tup in tuples)
			{
				foreach (var tup2 in tuples)
				{
					if (tup2.Item1 == tup.Item1)
						continue;
					else
						if (tup2.Item2 == tup.Item2)
					{
						failedTypes.Add(tup.Item1);
						logger.WriteLine($"***ERROR***: {tup.Item1?.FullName} and {tup2.Item1?.FullName} have the same {nameof(BoomaPayloadMessageType)}");
					}
				}
			}


			if (failedTypes.Count > 0)
				throw new Exception("Invalid Payload declarations.");
		}

		/// <summary>
		/// Inheritors can override this Type and be able to type their payloads without reimplementing the tests.
		/// </summary>
		protected abstract Type TypeToUseToFindAssembly { get; }

		protected IEnumerable<Type> GetPacketPayloadTypes()
		{
			return TypeToUseToFindAssembly.Assembly.GetTypes().Where(t => typeof(PacketPayload).IsAssignableFrom(t));
		}
	}
}