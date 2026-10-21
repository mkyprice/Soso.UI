using System;

namespace Soso.UI.Core.Types
{
	[Serializable]
	public enum EASING
	{
		Lerp,
		
		Cubic,
		
		EaseInCubic,
		EaseOutCubic,
		EaseInOutCubic,
		
		EaseInQuad,
		EaseOutQuad,
		EaseInOutQuad,
		
		EaseInQuart,
		EaseOutQuart,
		EaseInOutQuart,
		
		EaseInBack,
		EaseOutBack,
		EaseInOutBack,
		
		EaseInElastic,
		EaseOutElastic,
		EaseInOutElastic,
		
		EaseInBounce,
		EaseOutBounce,
		EaseInOutBounce,
	}
}
