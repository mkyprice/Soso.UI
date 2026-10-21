using Soso.UI.Core.Operations;
using Soso.UI.Core.Types;
using UnityEngine;

namespace Soso.UI.Animation.Operations.RectTransforms
{
    public class AnchorPositionOp : RectTransformTimeOp
    {
        private readonly Vector2 _target;
        private Vector2 _start;

        public AnchorPositionOp(RectTransform transform, Vector2 target, float durationSec, EASING easing) : base(transform, durationSec, easing)
        {
            _target = target;
            _start = transform.anchoredPosition;
        }

        public override void Start()
        {
            _start = Transform.anchoredPosition;
        }
        public override void Update()
        {
            base.Update();
            
            Transform.anchoredPosition = Vector2.Lerp(_start, _target, Value);
            
        }

        public override void Finish()
        {
            base.Finish();

            Transform.anchoredPosition = _target;
        }
    }
}