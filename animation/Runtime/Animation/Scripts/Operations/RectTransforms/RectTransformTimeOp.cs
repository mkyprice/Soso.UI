using Soso.UI.Core.Operations;
using Soso.UI.Core.Types;
using UnityEngine;

namespace Soso.UI.Animation.Operations.RectTransforms
{
	public abstract class RectTransformTimeOp : TimeOperation
	{
		protected readonly RectTransform Transform;

		public RectTransformTimeOp(RectTransform transform, float durationSec, EASING easing) : base(durationSec, easing)
		{
			Transform = transform;
		}

		public override void Update()
		{
			if (Transform == false)
			{
				SetCanceled();
				return;
			}
			base.Update();
		}
	}
}
