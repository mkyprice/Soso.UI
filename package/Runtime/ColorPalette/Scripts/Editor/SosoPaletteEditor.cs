#if UNITY_EDITOR

using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Soso.UI.ColorPalette.Editor
{
    [CustomEditor(typeof(SosoPalette), true), CanEditMultipleObjects]
    public class SosoPaletteEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();
            
            // Props
            SerializedProperty animationsProp = serializedObject.FindProperty(nameof(SosoPalette.Palette));
            SerializedProperty colorGuidProp = serializedObject.FindProperty("_colorGuid");
            
            // Serialize colors
            PropertyField animationsField = new PropertyField(animationsProp);
            root.Add(animationsField);

            // Colors dropdown
            var primaryPalette = target as SosoPalette;
            if (primaryPalette != null && primaryPalette.Palette != null && primaryPalette.Palette.Colors != null)
            {
                // TODO: Don't use Linq
                var colorNames = primaryPalette.Palette.Colors.Select(color => color.Name).ToList();
                var colorGuids = primaryPalette.Palette.Colors.Select(color => color.Guid).ToList();
                EditorStyles.popup.richText = true;

                int defaultIndex = colorGuids.FindIndex(color => color == colorGuidProp.stringValue);
                if (defaultIndex < 0) defaultIndex = 0;

                var colorDropdown = new DropdownField(colorNames, defaultIndex, selection =>
                {
                    int index = colorNames.IndexOf(selection);
                    var color = primaryPalette.Palette.Colors[index];

                    colorGuidProp.stringValue = color.Guid;
                    serializedObject.ApplyModifiedProperties();

                    return $"<color=#{ColorUtility.ToHtmlStringRGB(color.Color)}>{selection}</color>";
                });

                colorDropdown.TrackPropertyValue(colorGuidProp, prop => 
                {
                    var index = colorGuids.IndexOf(prop.stringValue);
                    var colorValue = primaryPalette.Palette.Colors[index];
                    colorDropdown.value = colorValue.Name;
                });

                root.Add(colorDropdown);
            }
            
            return root;
        }
    }
}

#endif