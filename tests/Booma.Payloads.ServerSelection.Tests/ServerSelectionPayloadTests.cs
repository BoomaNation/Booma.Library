using Booma.Payload.Common.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Booma.Payloads.ServerSelection.Tests
{
	public class ServerSelectionPayloadTests : GeneralPayloadTests
	{
		public ServerSelectionPayloadTests(ITestOutputHelper output) 
			: base(output)
		{

		}

		protected override Type TypeToUseToFindAssembly
		{
			get
			{
				return typeof(GameServerListRequestPayload);
			}
		}
	}
}
