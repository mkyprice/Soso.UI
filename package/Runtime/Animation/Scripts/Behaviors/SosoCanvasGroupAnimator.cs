using Soso.UI.Animation.Types;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Soso.UI.Animation.Behaviors
{
	[RequireComponent(typeof(CanvasGroup))]
	public class SosoCanvasGroupAnimator : BaseSosoAnimator
	{
		public List<SosoCanvasGroupAnimation> Animations;
		
		private CanvasGroup _canvasGroup;
        
		protected override void Awake()
		{
			base.Awake();
            
			_canvasGroup = GetComponent<CanvasGroup>();
		}
		
		protected override async Awaitable RunAnimationsAsync(OPERATION_EVENT e, CancellationToken token)
		{
			List<Awaitable> awaitables = new List<Awaitable>();
			foreach (var anim in Animations)
			{
				Awaitable awaitable = null;
				if (anim.Event == e)
				{
					if (anim.HasStartingValue) _canvasGroup.alpha = anim.StartingValue;
					awaitable = _canvasGroup.AnimateFade(anim.Value, anim.Duration, anim.Anim).GetAwaiter(token);
				}

				if (awaitable != null)
				{
					awaitables.Add(awaitable);
				}
			}

			foreach (var awaitable in awaitables)
			{
				await awaitable;
			}
		}
	}
}
