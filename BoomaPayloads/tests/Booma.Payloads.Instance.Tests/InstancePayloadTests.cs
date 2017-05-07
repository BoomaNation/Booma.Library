using Booma.Payload.Common.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Booma.Payloads.Instance.Tests
{
	public class InstancePayloadTests : GeneralPayloadTests
	{
		public InstancePayloadTests(ITestOutputHelper output) 
			: base(output)
		{

		}

		protected override Type TypeToUseToFindAssembly
		{
			get
			{
				return typeof(EntitySpawnEventPayload);
			}
		}
	}
}
