using Soso.UI.Animation.Operations.LayoutElements;
using Soso.UI.Core.Operations;
using Soso.UI.Core.Types;
using UnityEngine.UI;

namespace Soso.UI.Animation
{
	public static class LayoutElementAnimation
	{
		public static SosoOperation AnimateMinHeight(this LayoutElement layout, float target, float duration, EASING anim = EASING.Lerp)
		{
			return new LayoutMinHeightOp(duration, anim, layout, target);
		}
		public static SosoOperation AnimateMinWidth(this LayoutElement layout, float target, float duration, EASING anim = EASING.Lerp)
		{
			return new LayoutMinWidthOp(duration, anim, layout, target);
		}
	}
}
