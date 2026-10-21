using Soso.UI.Animation.Operations;
using Soso.UI.Animation.Operations.Transforms;
using Soso.UI.Core.Operations;
using Soso.UI.Core.Types;
using UnityEngine;

namespace Soso.UI.Animation
{
    public static class TransformAnimations
    {
        public static SosoOperation AnimateLocalScale(this Transform transform, Vector2 scale, float duration, EASING anim = EASING.Lerp)
        {
            return new ScaleOp(transform, scale, duration, anim);
        }

        public static SosoOperation AnimateRotation(this Transform transform, float rotation, float duration, EASING anim = EASING.Lerp)
            => transform.AnimateRotation(new Vector3(0, 0, rotation), duration, anim);

        public static SosoOperation AnimateRotation(this Transform transform, Vector3 rotation, float duration, EASING anim = EASING.Lerp)
        {
            return new RotationOp(transform, rotation, duration, anim);
        }

        public static SosoOperation AnimateLocalRotation(this Transform transform, float rotation, float duration, EASING anim = EASING.Lerp)
            => transform.AnimateLocalRotation(new Vector3(0, 0, rotation), duration, anim);

        public static SosoOperation AnimateLocalRotation(this Transform transform, Vector3 rotation, float duration, EASING anim = EASING.Lerp)
        {
            return new LocalRotationOp(transform, rotation, duration, anim);
        }
    }
}