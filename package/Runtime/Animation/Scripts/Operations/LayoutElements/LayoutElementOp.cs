using Soso.UI.Core.Operations;
using Soso.UI.Core.Types;
using UnityEngine;
using UnityEngine.UI;

namespace Soso.UI.Animation.Operations.LayoutElements
{
	public abstract class LayoutElementOp : TimeOperation
	{
		protected readonly LayoutElement Layout;
		
		public LayoutElementOp(float durationSec, EASING easing, LayoutElement layout) : base(durationSec, easing)
		{
			Layout = layout;
		}

		public override void Update()
		{
			if (Layout == false)
			{
				SetCanceled();
				return;
			}
            
			base.Update();
		}
	}
}
