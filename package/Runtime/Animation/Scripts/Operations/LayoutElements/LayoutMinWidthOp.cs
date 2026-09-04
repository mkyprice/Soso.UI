using Soso.UI.Core.Types;
using UnityEngine;
using UnityEngine.UI;

namespace Soso.UI.Animation.Operations.LayoutElements
{
	public class LayoutMinWidthOp : LayoutElementOp
	{
		private readonly float _target;
		private float _start;
		
		public LayoutMinWidthOp(float durationSec, EASING easing, LayoutElement layout, float target) : base(durationSec, easing, layout)
		{
			_target = target;
		}

		public override void Start()
		{
			_start = Layout.minWidth;
		}
		
		public override void Update()
		{
			base.Update();

			Layout.minWidth = Mathf.Lerp(_start, _target, Value);
		}
	}
}
