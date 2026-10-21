using Soso.UI.Core.Operations;
using Soso.UI.Core.Types;
using UnityEngine;

namespace Soso.UI.Animation.Operations.Transforms
{
	public abstract class TransformTimeOp : TimeOperation
	{
		protected readonly Transform Transform;

		public TransformTimeOp(Transform transform, float durationSec, EASING easing) : base(durationSec, easing)
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
