using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booma;
using UnityEngine;

namespace Booma
{
	[RequireComponent(typeof(ButtonEntityStateTag))] //require the state tag
	public class ServerNetworkButton : NetworkButton
	{
		void Update()
		{
			if (Input.GetKeyDown(KeyCode.S))
				StateContainer.State = ButtonState.Activated;
		}

		/// <inheritdoc />
		protected override void OnStart(ButtonState initialState)
		{
			//If it's activated then just push that state to OnStateChanged
			if (StateContainer.State == ButtonState.Activated)
				OnEntityStateChanged(StateContainer.State);
		}
	}
}
