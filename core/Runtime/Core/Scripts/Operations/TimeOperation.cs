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
            return SosoEasing.Ease(_dt, _easing);
        }
    }
}