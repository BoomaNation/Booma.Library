using GladNet.Payload;
using GladNet.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Booma.Payload.Common.Tests
{
	[TestFixture]
	public abstract class GeneralPayloadTests
	{
		/// <summary>
		/// The assembly that should be tested.
		/// </summary>
		protected Assembly AssemblyToSearch { get; }

		protected GeneralPayloadTests(Assembly assemblyToSearch)
		{
			if (assemblyToSearch == null) throw new ArgumentNullException(nameof(assemblyToSearch));

			AssemblyToSearch = assemblyToSearch;
		}

		[Test]
		public void Test_All_Payloads_Marked_With_BoomaAttribute()
		{
			List<Type> failedTypes = new List<Type>();

			foreach (Type t in GetPacketPayloadTypes())
			{
				if (t.GetCustomAttribute<BoomaPayloadAttribute>(false) == null)
				{
					Assert.Fail($"***ERROR***: {t?.FullName} doesn't implement the {nameof(BoomaPayloadAttribute)}");
				}
			}
		}

		[Test]
		public void Test_All_Payloads_Marked_With_Contract()
		{
			List<Type> failedTypes = new List<Type>();

			foreach (Type t in GetPacketPayloadTypes())
			{
				if (t.GetCustomAttribute<GladNetSerializationContractAttribute>(true) == null)
				{
					Assert.Fail($"***ERROR***: {t?.FullName} doesn't implement the {nameof(GladNetSerializationContractAttribute)}");
				}
			}
		}

		[Test]
		public void Test_All_ProtoContractMarkedTypes_Contains_Parameterless_Ctor()
		{
			List<Type> failedTypes = new List<Type>();

			//TODO: Check if we can use GetTypes
			foreach (Type t in AssemblyToSearch.GetTypes().Where(x => x.IsClass && x.GetCustomAttribute<GladNetSerializationContractAttribute>(true) != null))
			{
				if (t.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).All(ctor => ctor.GetParameters().Any()))
				{
					Assert.Fail($"***ERROR***: {t?.FullName} doesn't implement a parameterless constructor All GladNet serializable types must implement atleast a protected parameterless ctor.");
				}

				if (!t.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Any(ctor => ctor.IsPublic))
				{
					Assert.Fail($"***ERROR***: {t?.FullName} doesn't implement any public constructor All GladNet serializable types must implement atleast a 1 public ctor.");
				}
			}
		}

		[Test]
		public void Test_All_Payloads_Marked_With_BoomaAttribute_Have_Unique_Includes()
		{
			List<Type> failedTypes = new List<Type>();

			IEnumerable<Tuple<Type, BoomaPayloadAttribute>> tuples = GetPacketPayloadTypes()
				.Select(x => new Tuple<Type, BoomaPayloadAttribute>(x, x.GetCustomAttribute<BoomaPayloadAttribute>(false)))
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
						Assert.Fail($"***ERROR***: {tup.Item1?.FullName} and {tup2.Item1?.FullName} have the same {nameof(BoomaPayloadMessageType)}");
					}
				}
			}
		}

		protected IEnumerable<Type> GetPacketPayloadTypes()
		{
			return AssemblyToSearch.GetTypes().Where(t => typeof(PacketPayload).IsAssignableFrom(t) && t.BaseType == typeof(PacketPayload));
		}
	}
}