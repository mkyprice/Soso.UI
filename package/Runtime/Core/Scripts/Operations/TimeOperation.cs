using NUnit.Framework;
using Soso.UI.Core.Helpers;
using Soso.UI.Core.Types;
using UnityEngine;

namespace Soso.UI.Core.Operations
{
    public abstract class TimeOperation : SosoOperation
    {
        protected float Value => GetValue();
        
        private float _dt = 0f;
        private readonly float _invDurationSec;
        private readonly EASING _easing;

        protected TimeOperation(float durationSec, EASING easing)
        {
            _invDurationSec = 1f / durationSec;
            _easing = easing;
        }

        public override void Update()
        {
            _dt += Time.deltaTime * _invDurationSec;
            _dt = Mathf.Clamp01(_dt);

            if (_dt >= 1f)
            {
                SetFinished();
            }
        }

        private float GetValue()
        {
            switch (_easing)
            {
                case EASING.Lerp:
                    return _dt;
                case EASING.Cubic:
                    return SosoEasing.Cubic(_dt);
                
                case EASING.EaseInQuad:
                    return SosoEasing.EaseInQuad(_dt);
                case EASING.EaseOutQuad:
                    return SosoEasing.EaseOutQuad(_dt);
                case EASING.EaseInOutQuad:
                    return SosoEasing.EaseInOutQuad(_dt);
                
                case EASING.EaseInCubic:
                    return SosoEasing.EaseInCubic(_dt);
                case EASING.EaseOutCubic:
                    return SosoEasing.EaseOutCubic(_dt);
                case EASING.EaseInOutCubic:
                    return SosoEasing.EaseInOutCubic(_dt);
                
                case EASING.EaseInQuart:
                    return SosoEasing.EaseInQuart(_dt);
                case EASING.EaseOutQuart:
                    return SosoEasing.EaseOutQuart(_dt);
                case EASING.EaseInOutQuart:
                    return SosoEasing.EaseInOutQuart(_dt);
                
                case EASING.EaseInBack:
                    return SosoEasing.EaseInBack(_dt);
                case EASING.EaseOutBack:
                    return SosoEasing.EaseOutBack(_dt);
                case EASING.EaseInOutBack:
                    return SosoEasing.EaseInOutBack(_dt);
                
                case EASING.EaseInElastic:
                    return SosoEasing.EaseInElastic(_dt);
                case EASING.EaseOutElastic:
                    return SosoEasing.EaseOutElastic(_dt);
                case EASING.EaseInOutElastic:
                    return SosoEasing.EaseInOutElastic(_dt);
                
                case EASING.EaseInBounce:
                    return SosoEasing.EaseInBounce(_dt);
                case EASING.EaseOutBounce:
                    return SosoEasing.EaseOutBounce(_dt);
                case EASING.EaseInOutBounce:
                    return SosoEasing.EaseInOutBounce(_dt);
            }
            return _dt;
        }
    }
}