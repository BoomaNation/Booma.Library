using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Entity.Identity.Tests.UnitTests
{
	[TestFixture]
	public class NetworkEntityGuidBuilderTests
	{
		[Test]
		public static void Test_Ctor_Doesnt_Throw()
		{
			Assert.DoesNotThrow(() => new NetworkEntityGuidBuilder());
		}

		[Test]
		public static void Test_Can_Build_Empty_Guid()
		{
			//arrange
			NetworkEntityGuidBuilder builder = new NetworkEntityGuidBuilder();

			//act
			NetworkEntityGuid guid = builder.Build();

			//assert
			Assert.NotNull(guid);
			Assert.IsTrue(guid.isEmpty);
		}

		[Test]
		public static void Test_Can_Build_Empty_Guid_With_0_Id()
		{
			//arrange
			NetworkEntityGuidBuilder builder = new NetworkEntityGuidBuilder();

			//act
			NetworkEntityGuid guid = builder.WithId(0).WithId(0).Build();

			//assert
			Assert.NotNull(guid);
			Assert.IsTrue(guid.isEmpty);
		}

		[Test]
		[TestCase(1)]
		[TestCase(int.MaxValue)]
		[TestCase(5004)]
		[TestCase(55)]
		[TestCase(1)]
		public static void Test_Can_Build_Guid_With_Provided_Id(int id)
		{
			//arrange
			NetworkEntityGuidBuilder builder = new NetworkEntityGuidBuilder();

			//act
			NetworkEntityGuid guid = builder.WithId(Math.Max(65, id - 5)).WithId(id).Build();

			//assert
			Assert.NotNull(guid);
			Assert.IsFalse(guid.isEmpty);

			Assert.AreEqual(id, guid.EntityId);
		}

		[Test]
		[TestCase(EntityType.GameObject)]
		[TestCase(EntityType.Player)]
		public static void Test_Can_Build_Guid_With_Provided_EntityType(EntityType type)
		{
			//arrange
			NetworkEntityGuidBuilder builder = new NetworkEntityGuidBuilder();

			//act
			NetworkEntityGuid guid = builder.WithType(type).WithType(type).Build();

			//assert
			Assert.NotNull(guid);
			//won't be empty with an entity type.

			Assert.AreEqual(type, guid.EntityType);
		}
	}
}
