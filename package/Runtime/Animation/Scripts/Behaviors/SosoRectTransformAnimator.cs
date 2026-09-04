using System.Collections.Generic;
using System.Threading;
using Soso.UI.Animation.Types;
using UnityEngine;

namespace Soso.UI.Animation.Behaviors
{
    public class SosoRectTransformAnimator : BaseSosoAnimator
    {
        [SerializeField] public List<SosoRectTransformAnimation> Animations;

        private RectTransform _transform;
        
        protected override void Awake()
        {
            base.Awake();
            
            _transform = GetComponent<RectTransform>();
        }
        
        protected override async Awaitable RunAnimationsAsync(OPERATION_EVENT e, CancellationToken token)
        {
            List<Awaitable> awaitables = new List<Awaitable>();
            foreach (var anim in Animations)
            {
                Awaitable awaitable = null;
                if (anim.Event == e)
                {
                    switch (anim.OpType)
                    {
                        case RECT_OPERATION_TYPE.AnchorPosition:
                            if (anim.HasStartingValue) _transform.anchoredPosition = anim.StartingValue;
                            awaitable = _transform.AnimateAnchorPosition(anim.Value, anim.Duration, anim.Anim).GetAwaiter(token);
                            break;
                        case RECT_OPERATION_TYPE.Scale:
                            if (anim.HasStartingValue) _transform.localScale = anim.StartingValue;
                            awaitable = _transform.AnimateLocalScale(anim.Value, anim.Duration, anim.Anim).GetAwaiter(token);
                            break;
                        case RECT_OPERATION_TYPE.Rotate:
                            if (anim.HasStartingValue) _transform.rotation = Quaternion.Euler(anim.StartingValue);
                            awaitable = _transform.AnimateRotation(anim.Value, anim.Duration, anim.Anim).GetAwaiter(token);
                            break;
                    }
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