using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Soso.UI.ColorPalette.Editor
{
    [CustomPropertyDrawer(typeof(ColorPalette))]
    public class ColorPaletteDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement container = new VisualElement();

            // Serialize colors
            SerializedProperty colorsProp = property.FindPropertyRelative(nameof(ColorPalette.Colors));
            PropertyField colorsField = new PropertyField(colorsProp, property.displayName);
            container.Add(colorsField);
            
            colorsField.BindProperty(colorsProp);

            // New color button
            var createButton = new Button(() =>
            {
                colorsProp.serializedObject.Update();

                int newIndex = colorsProp.arraySize;
                colorsProp.InsertArrayElementAtIndex(newIndex);
                
                SerializedProperty newColorProp = colorsProp.GetArrayElementAtIndex(newIndex);
                
                newColorProp.colorValue = Color.white;
                
                colorsProp.serializedObject.ApplyModifiedProperties();
            });
            createButton.text = "New Color";
            container.Add(createButton);
            
            return container;
        }
    }
}