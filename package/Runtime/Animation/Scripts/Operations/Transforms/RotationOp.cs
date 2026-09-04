using Soso.UI.Core.Operations;
using Soso.UI.Core.Types;
using UnityEngine;

namespace Soso.UI.Animation.Operations.Transforms
{
    public class RotationOp : TransformTimeOp
    {
        private readonly Quaternion _target;
        private Quaternion _start;

        public RotationOp(Transform transform, Vector3 target, float durationSec, EASING easing) : base(transform, durationSec, easing)
        {
            _target = Quaternion.Euler(target.x, target.y, target.z);
        }

        public override void Start()
        {
            _start = Transform.rotation;
        }
        
        public override void Update()
        {
            base.Update();
            
            Transform.rotation = Quaternion.Lerp(_start, _target, Value);
        }

        public override void Finish()
        {
            base.Finish();
            
            Transform.rotation = _target;
        }
    }
}