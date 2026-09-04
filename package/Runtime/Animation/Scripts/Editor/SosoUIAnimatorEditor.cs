#if UNITY_EDITOR

using Soso.UI.Animation.Behaviors;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Soso.UI.Animation.Editor
{
    [CustomEditor(typeof(SosoRectTransformAnimator)), CanEditMultipleObjects]
    public class SosoUIAnimatorEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();
            
            SerializedProperty animationsProp = serializedObject.FindProperty("Animations");
            PropertyField animationsField = new PropertyField(animationsProp);
            root.Add(animationsField);
            
            return root;
        }
    }
}

#endif