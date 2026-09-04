#if UNITY_EDITOR

using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Soso.UI.ColorPalette.Editor
{
    [CustomEditor(typeof(SosoColorPalette))]
    public class SosoColorPaletteEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var palette = target as SosoColorPalette;
            
            VisualElement root = new VisualElement();
            
            // Serialize colors
            SerializedProperty animationsProp = serializedObject.FindProperty(nameof(SosoColorPalette.Colors));
            PropertyField animationsField = new PropertyField(animationsProp);
            root.Add(animationsField);

            // New color button
            var createButton = new Button(() =>
            {
                palette?.Colors.Add(new PaletteColor()
                {
                    Color = Color.white,
                    Name = "New Color"
                });
            });
            createButton.text = "New Color";
            root.Add(createButton);
            
            return root;
        }
    }
}

#endif