using Soso.UI.Animation.Operations;
using Soso.UI.Animation.Operations.CanvasGroups;
using Soso.UI.Core.Operations;
using Soso.UI.Core.Types;
using UnityEngine;

namespace Soso.UI.Animation
{
	public static class CanvasGroupAnimations
	{
		public static SosoOperation AnimateFade(this CanvasGroup cg, float target, float duration, EASING easing)
		{
			return new CanvasFadeOp(cg, target, duration, easing);
		}
	}
}
