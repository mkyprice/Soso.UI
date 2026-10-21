using Soso.UI.Core.Operations;
using Soso.UI.Core.Types;
using UnityEngine;

namespace Soso.UI.Animation.Operations.CanvasGroups
{
	public class CanvasFadeOp : TimeOperation
	{
		private readonly CanvasGroup _canvasGroup;
		private readonly float _end;
		private float _start;
		
		public CanvasFadeOp(CanvasGroup group, float target, float durationSec, EASING easing) : base(durationSec, easing)
		{
			_canvasGroup = group;
			_end = target;
		}

		public override void Start()
		{
			_start = _canvasGroup.alpha;
		}
		
		public override void Update()
		{
			if (_canvasGroup == false)
			{
				SetCanceled();
				return;
			}
			base.Update();
			
			_canvasGroup.alpha = Mathf.Lerp(_start, _end, Value);
		}

		public override void Finish()
		{
			base.Finish();
			
			_canvasGroup.alpha = _end;
		}
	}
}
