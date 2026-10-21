#if UNITY_EDITOR

using UnityEngine;
using UnityEngine.UIElements;

namespace Soso.UI.Core.Extensions
{
    public static class EditorExtensions
    {
        public static void SetMargins(this IStyle style, float value)
            => SetMargins(style, value, value, value, value);
        public static void SetMargins(this IStyle style, float top, float left, float right, float bottom)
        {
            style.marginTop = top;
            style.marginLeft = left;
            style.marginRight = right;
            style.marginBottom = bottom;
        }

        public static void SetPadding(this IStyle style, float value)
            => SetPadding(style, value, value, value, value);
        public static void SetPadding(this IStyle style, float top, float left, float right, float bottom)
        {
            style.paddingTop = top;
            style.paddingLeft = left;
            style.paddingRight = right;
            style.paddingBottom = bottom;
        }

        public static void SetBorderWidth(this IStyle style, float value)
            => SetBorderWidth(style, value, value, value, value);
        public static void SetBorderWidth(this IStyle style, float top, float left, float right, float bottom)
        {
            style.borderTopWidth = top;
            style.borderLeftWidth = left;
            style.borderRightWidth = right;
            style.borderBottomWidth = bottom;
        }

        public static void SetBorderColor(this IStyle style, Color value)
            => SetBorderColor(style, value, value, value, value);
        public static void SetBorderColor(this IStyle style, Color top, Color left, Color right, Color bottom)
        {
            style.borderTopColor = top;
            style.borderLeftColor = left;
            style.borderRightColor = right;
            style.borderBottomColor = bottom;
        }
    }
}

#endif