using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booma.Payload.Common.Tests;
using NUnit.Framework;

namespace Booma.Payloads.Tests.UnitTests
{
	[TestFixture]
	public class ServerSelectionPayloadTests : GeneralPayloadTests
	{
		/// <inheritdoc />
		public ServerSelectionPayloadTests()
			: base(typeof(Booma.Payloads.ServerSelection.GameServerListRequestPayload).Assembly)
		{

		}
	}
}
