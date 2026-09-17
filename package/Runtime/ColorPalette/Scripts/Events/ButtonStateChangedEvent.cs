using Soso.UI.Core.Types;
using System;
using UnityEngine.Events;

namespace Soso.UI.ColorPalette.Events
{
	[Serializable]
	public class ButtonStateChangedEvent : UnityEvent<SELECTION_STATE>
	{
	}
}
