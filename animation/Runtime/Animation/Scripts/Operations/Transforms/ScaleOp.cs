using Soso.UI.Core.Operations;
using Soso.UI.Core.Types;
using UnityEngine;

namespace Soso.UI.Animation.Operations.Transforms
{
    public class ScaleOp : TransformTimeOp
    {
        private readonly Vector3 _target;
        private Vector3 _start;

        public ScaleOp(Transform transform, Vector2 target, float durationSec, EASING easing) : base(transform, durationSec, easing)
        {   
            _target = target;
            _target.z = transform.localScale.z;
        }

        public override void Start()
        {
            _start = Transform.localScale;
        }
        
        public override void Update()
        {
            base.Update();
            
            Transform.localScale = Vector3.Lerp(_start, _target, Value);
            
        }

        public override void Finish()
        {
            base.Finish();
            
            Transform.localScale = _target;
        }
    }
}