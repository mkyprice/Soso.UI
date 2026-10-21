using Soso.UI.Core.Operations;
using Soso.UI.Core.Types;
using UnityEngine;

namespace Soso.UI.Animation.Operations.RectTransforms
{
	public class SizeDeltaOp : RectTransformTimeOp
	{
		private readonly Vector2 _target;
		private Vector2 _start;
		
		public SizeDeltaOp(float durationSec, EASING easing, RectTransform transform, Vector2 target) : base(transform, durationSec, easing)
		{
			_target = target;
		}

		public override void Start()
		{
			_start = Transform.sizeDelta;
		}
		
		public override void Update()
		{
			base.Update();
			
			Transform.sizeDelta = Vector2.Lerp(_start, _target, Value);
		}

		public override void Finish()
		{
			base.Finish();

			Transform.sizeDelta = _target;
		}
	}
}
