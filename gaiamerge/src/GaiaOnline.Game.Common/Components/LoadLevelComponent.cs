using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GaiaOnline
{
	public sealed class LoadLevelComponent : MonoBehaviour
	{
		public void LoadLevel(int level)
		{
			SceneManager.LoadSceneAsync(level)
				.allowSceneActivation = true;
		}

		public void LoadLevel([NotNull] string level)
		{
			if(string.IsNullOrEmpty(level)) throw new ArgumentException("Value cannot be null or empty.", nameof(level));

			SceneManager.LoadSceneAsync(level)
				.allowSceneActivation = true;
		}
	}
}
