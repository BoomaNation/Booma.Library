using Booma.Instance.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Booma.Instance.Server
{
	[RequireComponent(typeof(ButtonEntityStateTag))] //require the state tag
	public class ServerNetworkButton : NetworkButton
	{
		protected override void HandleInitialState(ButtonState state)
		{
			//If it's activated then just push that state to OnStateChanged
			if (state == ButtonState.Activated)
				this.OnEntityStateChanged(state);
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.S))
				this.State = ButtonState.Activated;
		}
	}
}
