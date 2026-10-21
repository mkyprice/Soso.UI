using Soso.UI.Animation.Types;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Soso.UI.Animation.Behaviors
{
	public abstract class BaseSosoAnimator : UIBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
	{
		private CancellationTokenSource _cancellationTokenSource;
        
		protected override void Awake()
		{
			base.Awake();
            
			_cancellationTokenSource = new CancellationTokenSource();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
            
			_cancellationTokenSource.Cancel();
		}
		
		
		protected abstract Awaitable RunAnimationsAsync(OPERATION_EVENT e, CancellationToken token);

		protected override void OnEnable()
		{
			base.OnEnable();
            
			RunAnimations(OPERATION_EVENT.OnEnable);
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			RunAnimations(OPERATION_EVENT.OnPointerEnter);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			RunAnimations(OPERATION_EVENT.OnPointerExit);
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			RunAnimations(OPERATION_EVENT.OnPointerDown);
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			RunAnimations(OPERATION_EVENT.OnPointerUp);
		}
		

		private async void RunAnimations(OPERATION_EVENT operationEvent)
		{
			try
			{
				await RunAnimationsAsync(operationEvent, _cancellationTokenSource.Token);
			}
			catch (OperationCanceledException)
			{
				// Do nothing
			}
			catch (Exception e)
			{
				Debug.LogException(e);
			}
		}
	}
}
