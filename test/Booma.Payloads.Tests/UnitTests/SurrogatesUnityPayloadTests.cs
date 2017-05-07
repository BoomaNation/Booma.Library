using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booma.Payload.Common.Tests;
using NUnit.Framework;

namespace Booma.Payloads.Tests.UnitTests
{
	[TestFixture]
	public class SurrogatesUnityPayloadTests : GeneralPayloadTests
	{
		/// <inheritdoc />
		public SurrogatesUnityPayloadTests()
			: base(typeof(Booma.Payloads.Surrogates.Unity.QuaternionSurrogate).Assembly)
		{

		}
	}
}
