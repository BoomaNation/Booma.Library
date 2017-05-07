using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Booma.Payload.Common.Tests;
using NUnit.Framework;

namespace Booma.Payloads.Tests
{
	[TestFixture]
	public class InstancePayloadTests : GeneralPayloadTests
	{
		/// <inheritdoc />
		public InstancePayloadTests() 
			: base(typeof(Booma.Payloads.Instance.EntityInteractionRequestPayload).Assembly)
		{

		}
	}
}
