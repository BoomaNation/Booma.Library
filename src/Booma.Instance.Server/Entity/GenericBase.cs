using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Server
{
	public class GenericBase<TEnumType> : MonoBehaviour
	{
		public class NestedState : MonoBehaviour
		{
			public TEnumType value;
		}
	}
}
