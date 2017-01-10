using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma.Instance.Common
{
	/// <summary>
	/// Generic hack for Unity3D. We have to close the generic manually by inheriting before it can be used
	/// in the editor.
	/// </summary>
	public class ButtonEntityStateTag : NetworkEntityStateTag<NetworkButton.ButtonState> { }
}
