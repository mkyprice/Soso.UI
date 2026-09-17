#if UNITY_EDITOR

using System.Collections.Generic;
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
            SerializedProperty animationsProp = serializedObject.FindProperty(nameof(SosoPalette.palettes));
            SerializedProperty colorIndexProp = serializedObject.FindProperty("_colorIndex");

            // Serialize colors
            PropertyField animationsField = new PropertyField(animationsProp);
            root.Add(animationsField);

            // Colors dropdown
            var primaryPalette = target as SosoPalette;
            if (primaryPalette != null && primaryPalette.palettes?.Palettes?.Count > 0)
            {
                var palette = primaryPalette.palettes.GetActivePalette();
                if (palette != null)
                {
                    // TODO: Don't use Linq
                    var colorHex = palette.Colors.Select(ColorUtility.ToHtmlStringRGBA).ToList();
                    EditorStyles.popup.richText = true;

                    int selectedIndex = colorIndexProp.intValue;
                    if (selectedIndex < 0) selectedIndex = 0;

                    var colorContainer = new VisualElement();
                    colorContainer.style.flexDirection = FlexDirection.Column;
                    colorContainer.style.marginTop = 4;
                    colorContainer.style.marginBottom = 4;
                    root.Add(colorContainer);
                    RefreshColorButtons(colorContainer, colorHex, selectedIndex, colorIndexProp);
                }
            }

            // Refresh
            var refreshButton = new Button(() =>
            {
                foreach (var t in targets)
                {
                    if (t is SosoPalette palette)
                    {
                        palette.Refresh();
                    }
                }
            })
            {
                text = "Refresh"
            };
            root.Add(refreshButton);

            return root;
        }

        private void RefreshColorButtons(VisualElement colorContainer, List<string> hexColors, int selectedIndex, SerializedProperty colorIndexProp)
        {
            colorContainer.Clear();

            const int BUTTON_SIZE = 64;
            const int BUTTON_MARGIN = 0;

            var currRow = new VisualElement();
            currRow.style.flexDirection = FlexDirection.Row;
            currRow.style.flexWrap = Wrap.Wrap;
            colorContainer.Add(currRow);
            
            for (int i = 0; i < hexColors.Count; i++)
            {
                int index = i;
                string hex = hexColors[i];

                if (ColorUtility.TryParseHtmlString("#" + hex, out Color btnColor))
                {
                    // Wrapper for button and text
                    var btnWrapper = new VisualElement();
                    btnWrapper.style.flexDirection = FlexDirection.Column;
                    btnWrapper.style.alignItems = Align.Center;
                    btnWrapper.style.marginRight = 6;
                    btnWrapper.style.marginBottom = 8;
                    currRow.Add(btnWrapper);
                    
                    // Create color btn
                    var colorButton = new Button();
                    colorButton.style.backgroundColor = btnColor;
                    colorButton.style.width = BUTTON_SIZE;
                    colorButton.style.height = BUTTON_SIZE;
                    colorButton.style.marginLeft = BUTTON_MARGIN;
                    colorButton.style.marginRight = BUTTON_MARGIN;
                    colorButton.style.marginTop = BUTTON_MARGIN;
                    colorButton.style.marginBottom = BUTTON_MARGIN;
                    btnWrapper.Add(colorButton);

                    Color borderColor;
                    int borderWidth;
                    if (index == selectedIndex)
                    {
                        borderColor = GetOpposingColor(btnColor);
                        borderWidth = 4;
                    }
                    else
                    {
                        borderColor = new Color(0.1f, 0.1f, 0.1f, 0.5f);
                        borderWidth = 2;
                    }
                    colorButton.style.borderTopColor = borderColor;
                    colorButton.style.borderBottomColor = borderColor;
                    colorButton.style.borderLeftColor = borderColor;
                    colorButton.style.borderRightColor = borderColor;
                    colorButton.style.borderTopWidth = borderWidth;
                    colorButton.style.borderBottomWidth = borderWidth;
                    colorButton.style.borderLeftWidth = borderWidth;
                    colorButton.style.borderRightWidth = borderWidth;
                    
                    // Label
                    var colorLabel = new TextElement();
                    string hexNoAlpha = hex.Substring(0, Mathf.Min(6, hex.Length));
                    colorLabel.text = $"{index + 1}) #{hexNoAlpha}";
                    colorLabel.style.fontSize = 10;
                    colorLabel.style.color = Color.antiqueWhite;
                    colorLabel.style.maxWidth = BUTTON_SIZE;
                    colorLabel.style.marginTop = 2;
                    colorLabel.style.unityTextAlign = TextAnchor.MiddleCenter;
                    colorLabel.style.overflow = Overflow.Hidden;
                    colorLabel.style.textOverflow = TextOverflow.Ellipsis;
                    btnWrapper.Add(colorLabel);


                    // Selection
                    colorButton.clicked += () =>
                    {
                        colorIndexProp.intValue = index;
                        serializedObject.ApplyModifiedProperties();

                        RefreshColorButtons(colorContainer, hexColors, index, colorIndexProp);
                    };
                }
            }
        }

        private static Color GetOpposingColor(Color color)
        {
            return (color.r + color.g + color.b) >= 1.5f ? Color.black : Color.white;
        }
    }
}

#endif