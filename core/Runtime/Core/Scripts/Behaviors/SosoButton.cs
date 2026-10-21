using Soso.UI.Core.Events;
using Soso.UI.Core.Types;
using UnityEngine;
using UnityEngine.UI;

namespace Soso.UI.Core.Behaviors
{
	public class SosoButton : Button
	{
		[SerializeField] public ButtonStateChangedEvent OnStateChanged = new ButtonStateChangedEvent();
		
		public SELECTION_STATE State => (SELECTION_STATE)base.currentSelectionState;

		protected override void DoStateTransition(SelectionState state, bool instant)
		{
			base.DoStateTransition(state, instant);
			
			var newState = (SELECTION_STATE)state;
			StateChanged(newState, instant);
			OnStateChanged.Invoke(newState);
		}

		protected virtual void StateChanged(SELECTION_STATE state, bool instant)
		{
		}
	}
}
