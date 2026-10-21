using Soso.UI.Core.Types;
using System;

namespace Soso.UI.Animation.Types
{
	[Serializable]
	public class SosoCanvasGroupAnimation
	{
		public OPERATION_EVENT Event;
		public EASING Anim;

		public float Duration = 1;
        
		public float Value;
        
		public bool HasStartingValue = false;
		public float StartingValue;
	}
}
