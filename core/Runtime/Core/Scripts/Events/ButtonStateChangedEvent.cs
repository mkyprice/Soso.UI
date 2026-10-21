using Soso.UI.Core.Types;
using System;
using UnityEngine.Events;

namespace Soso.UI.Core.Events
{
	[Serializable]
	public class ButtonStateChangedEvent : UnityEvent<SELECTION_STATE>
	{
	}
}
