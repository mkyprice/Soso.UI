#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Soso.UI.ColorPalette.Editor
{
    [CustomPropertyDrawer(typeof(PaletteColor))]
    public class PaletteColorDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var palette = (PaletteColor)property.boxedValue;
            
            var container = new Box();
            
            var nameProp = property.FindPropertyRelative(nameof(PaletteColor.Name));
            var colorProp = property.FindPropertyRelative(nameof(PaletteColor.Color));
            var guidProp = property.FindPropertyRelative(nameof(PaletteColor.Guid));
            
            var nameField = new PropertyField(nameProp);
            var colorField = new PropertyField(colorProp);
            var guidField = new PropertyField(guidProp);
            
            container.Add(nameField);
            container.Add(colorField);
            // container.Add(guidField);
            
            return container;
        }
    }
}

#endif