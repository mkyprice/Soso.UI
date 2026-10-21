using Soso.UI.Core.Types;
using UnityEngine;
using UnityEngine.UI;

namespace Soso.UI.Animation.Operations.LayoutElements
{
	public class LayoutMinHeightOp : LayoutElementOp
	{
		private readonly float _target;
		private float _start;
		
		public LayoutMinHeightOp(float durationSec, EASING easing, LayoutElement layout, float target) : base(durationSec, easing, layout)
		{
			_target = target;
		}

		public override void Start()
		{
			_start = Layout.minHeight;
		}
		public override void Update()
		{
			base.Update();

			Layout.minHeight = Mathf.Lerp(_start, _target, Value);
		}
	}
}
