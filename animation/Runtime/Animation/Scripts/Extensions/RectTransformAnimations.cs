using Soso.UI.Animation.Operations;
using Soso.UI.Animation.Operations.RectTransforms;
using Soso.UI.Core.Operations;
using Soso.UI.Core.Types;
using UnityEngine;

namespace Soso.UI.Animation
{
    public static class RectTransformAnimations
    {
        public static SosoOperation AnimateAnchorPosition(this RectTransform transform, Vector2 position, float duration, EASING anim = EASING.Lerp)
        {
            return new AnchorPositionOp(transform, position, duration, anim);
        }
        
        public static SosoOperation AnimateSizeDelta(this RectTransform transform, Vector2 size, float duration, EASING anim = EASING.Lerp)
        {
            return new SizeDeltaOp(duration, anim, transform, size);
        }

    }
}