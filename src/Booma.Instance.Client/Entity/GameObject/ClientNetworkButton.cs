﻿using Booma.Instance.Anim;
using Booma.Instance.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booma.Instance.NetworkObject;
using UnityEngine;

namespace Booma.Instance.Client
{
	[RequireComponent(typeof(ButtonEntityStateTag))]
	public class ClientNetworkButton : NetworkButton
	{
		[SerializeField]
		private Animator animationController;

		protected virtual void HandleAnimation(ButtonState state)
		{
			switch (state)
			{
				case ButtonState.Activated:
					animationController.SetTrigger(DefaultButtonAnimationTrigger.Pressed.ToString());
					break;
				case ButtonState.Deactivated:
					break;
			}
		}

		protected override void OnEntityStateChanged(ButtonState newState)
		{
			base.OnEntityStateChanged(newState);

			//Also animate
			HandleAnimation(newState);
		}

		/// <inheritdoc />
		protected override void OnStart(ButtonState initialState)
		{
			HandleAnimation(StateContainer.State);
		}
	}
}
