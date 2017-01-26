using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Server.Entity
{
	[RequireComponent(typeof(TestGenericComponent.NestedState))]
	public class TestGenericComponent : GenericBase<TestEnum>
	{

	}

	public enum TestEnum
	{
		One,
		Two,
		Three
	}
}
