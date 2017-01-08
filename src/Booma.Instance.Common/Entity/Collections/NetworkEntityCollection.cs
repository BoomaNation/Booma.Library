using Booma.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Common
{
	/// <summary>
	/// Dictionary/map of <see cref="NetworkEntityGuid"/>s and their corressponding <see cref="GameObject"/>.
	/// </summary>
	public class NetworkEntityCollection : EntityGuidDictionary<NetworkEntityGuid, GameObject>
	{

	}
}
