using Soso.UI.Core.Types;
using System;
using UnityEngine;

namespace Soso.UI.Animation.Types
{
    [Serializable]
    public class SosoRectTransformAnimation
    {
        public OPERATION_EVENT Event;
        public RECT_OPERATION_TYPE OpType;
        public EASING Anim;

        public float Duration = 1;
        
        public Vector3 Value;
        
        public bool HasStartingValue = false;
        public Vector3 StartingValue;
    }
}