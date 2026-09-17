#if UNITY_EDITOR

using System;
using Soso.UI.Core.Extensions;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Soso.UI.ColorPalette.Editor
{
    [CustomEditor(typeof(SosoColorPalettes))]
    public class SosoColorPaletteEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var palette = target as SosoColorPalettes;
            
            VisualElement root = new VisualElement();
            
            // Serialize index
            SerializedProperty paletteIndexProp = serializedObject.FindProperty(nameof(SosoColorPalettes.ActivePaletteIndex));
            SerializedProperty palettesProp = serializedObject.FindProperty(nameof(SosoColorPalettes.Palettes));
            
            // Serialize colors
            var palettesContainer = new VisualElement();
            palettesContainer.style.marginTop = 8;
            palettesContainer.style.marginBottom = 8;
            root.Add(palettesContainer);
            RefreshPalettes(palettesContainer, paletteIndexProp, palettesProp);

            // New color button
            var createButton = new Button(() =>
            {
                palette?.Palettes.Add(new ColorPalette());
                RefreshPalettes(palettesContainer, paletteIndexProp, palettesProp);
            });
            createButton.text = "New Palette";
            root.Add(createButton);
            
            return root;
        }

        private void RefreshPalettes(VisualElement root, SerializedProperty paletteIndexProp, SerializedProperty palettesProp)
        {
            const int HEADER_HEIGHT = 20;
            const int COLOR_SIZE = 36;
            Color closeColor = new Color(0.35f, 0.12f, 0.12f, 0.8f);
            Color selectedColor = new Color(1, 1, 1, 0.05f);
            Color selectedBorderColor = new Color(1, 1, 1, 0.35f);
            Color inactiveColor = new Color(0, 0, 0, 0.1f);
            Color inactiveBorderColor = new Color(0, 0, 0, 0.15f);
            
            root.Clear();
            serializedObject.Update();

            int currentActiveIndex = paletteIndexProp.intValue;
            for (int i = 0; i < palettesProp.arraySize; i++)
            {
                int index = i;
                SerializedProperty currentPaletteProp = palettesProp.GetArrayElementAtIndex(i);

                SerializedProperty paletteNameProp = currentPaletteProp.FindPropertyRelative(nameof(ColorPalette.Name));
                SerializedProperty colorsListProp = currentPaletteProp.FindPropertyRelative(nameof(ColorPalette.Colors));

                var rowContainer = new VisualElement();
                rowContainer.style.flexDirection = FlexDirection.Column;
                rowContainer.style.SetPadding(4);
                rowContainer.style.SetMargins(0, 0, 0, 8);
                root.Add(rowContainer);

                Color borderColor, backgroundColor;
                if (index == currentActiveIndex)
                {
                    backgroundColor = selectedColor;
                    borderColor = selectedBorderColor;
                }
                else
                {
                    backgroundColor = inactiveColor;
                    borderColor = inactiveBorderColor;
                }
                rowContainer.style.backgroundColor = backgroundColor;
                rowContainer.style.SetBorderColor(borderColor);
                rowContainer.style.SetBorderWidth(1);

                // Set as active palette
                rowContainer.RegisterCallback<PointerDownEvent>(evt =>
                {
                    if (evt.propagationPhase == PropagationPhase.TrickleDown)
                    {
                        return;
                    }
                    paletteIndexProp.intValue = index;
                    serializedObject.ApplyModifiedProperties();
                    RefreshPalettes(root, paletteIndexProp, palettesProp);
                    evt.StopPropagation();
                });

                // Header controls
                var headerLine = new VisualElement();
                headerLine.style.flexDirection = FlexDirection.Row;
                headerLine.style.alignItems = Align.Center;
                headerLine.style.justifyContent = Justify.SpaceBetween;
                headerLine.style.SetMargins(0, 0, 0, 8);
                headerLine.style.SetPadding(0);
                rowContainer.Add(headerLine);
                
                // Keep some stuff left
                var leftControls = new VisualElement();
                leftControls.style.flexDirection = FlexDirection.Row;
                leftControls.style.alignItems = Align.Center;
                leftControls.style.SetMargins(0);
                leftControls.style.SetPadding(0);
                headerLine.Add(leftControls);

                // Visual Radio Indicator Button
                var selectRadioBtn = new Button(() =>
                {
                    paletteIndexProp.intValue = index;
                    serializedObject.ApplyModifiedProperties();
                    RefreshPalettes(root, paletteIndexProp, palettesProp);
                });
                selectRadioBtn.text = index == currentActiveIndex ? "●" : "○";
                selectRadioBtn.style.width = HEADER_HEIGHT;
                selectRadioBtn.style.height = HEADER_HEIGHT;
                selectRadioBtn.style.SetMargins(0);
                selectRadioBtn.style.SetPadding(0);
                leftControls.Add(selectRadioBtn);

                // Palette name
                var nameField = new TextField(string.Empty);
                nameField.style.width = 128;
                nameField.style.height = HEADER_HEIGHT;
                nameField.value = paletteNameProp.stringValue;
                nameField.RegisterValueChangedCallback(evt =>
                {
                    paletteNameProp.stringValue = evt.newValue;
                    serializedObject.ApplyModifiedProperties();
                });
                leftControls.Add(nameField);
                
                // Keep some stuff right
                var rightControls = new VisualElement();
                rightControls.style.flexDirection = FlexDirection.Row;
                headerLine.Add(rightControls);

                var addColorIconBtn = new Button(() =>
                {
                    // Default new colors to white
                    colorsListProp.InsertArrayElementAtIndex(colorsListProp.arraySize);
                    var newProp = colorsListProp.GetArrayElementAtIndex(colorsListProp.arraySize - 1);
                    newProp.colorValue = Color.white;
                    serializedObject.ApplyModifiedProperties();
                    RefreshPalettes(root, paletteIndexProp, palettesProp);
                });
                addColorIconBtn.text = "+ Color";
                addColorIconBtn.style.fontSize = 10;
                addColorIconBtn.style.height = HEADER_HEIGHT;
                rightControls.Add(addColorIconBtn);

                // Deleting palette
                var deletePaletteBtn = new Button(() =>
                {
                    palettesProp.DeleteArrayElementAtIndex(index);
                    if (paletteIndexProp.intValue >= palettesProp.arraySize)
                    {
                        paletteIndexProp.intValue = Mathf.Max(0, palettesProp.arraySize - 1);
                    }
                    serializedObject.ApplyModifiedProperties();
                    RefreshPalettes(root, paletteIndexProp, palettesProp);
                });
                deletePaletteBtn.text = "✕";
                deletePaletteBtn.style.fontSize = 10;
                deletePaletteBtn.style.width = HEADER_HEIGHT;
                deletePaletteBtn.style.height = HEADER_HEIGHT;
                deletePaletteBtn.style.backgroundColor = closeColor;
                rightControls.Add(deletePaletteBtn);

                // --- EDITABLE COLOR GRID ---
                var editableGrid = new VisualElement();
                editableGrid.style.flexDirection = FlexDirection.Row;
                editableGrid.style.flexWrap = Wrap.Wrap;
                rowContainer.Add(editableGrid);
                if (colorsListProp != null && colorsListProp.arraySize > 0)
                {
                    for (int c = 0; c < colorsListProp.arraySize; c++)
                    {
                        int colorIndex = c;
                        SerializedProperty colorProp = colorsListProp.GetArrayElementAtIndex(colorIndex);

                        // Visual Element Wrapper containing the ColorField and an integrated remove button
                        var swatchContainer = new VisualElement();
                        swatchContainer.style.flexDirection = FlexDirection.Row;
                        swatchContainer.style.marginRight = 4;
                        swatchContainer.style.marginBottom = 4;
                        editableGrid.Add(swatchContainer);

                        // Native Interactive Color Field
                        var colorField = new ColorField();
                        colorField.BindProperty(colorProp);
                        colorField.style.width = COLOR_SIZE;
                        colorField.style.height = COLOR_SIZE;
                        colorField.style.SetMargins(0);
                        colorField.style.SetPadding(0);
                        swatchContainer.Add(colorField);
                        
                        // Side buttons
                        var actionColumn = new VisualElement();
                        actionColumn.style.flexDirection = FlexDirection.Column;
                        actionColumn.style.justifyContent = Justify.SpaceBetween;
                        swatchContainer.Add(actionColumn);

                        // Delete color
                        var removeColorBtn = new Button(() =>
                        {
                            colorsListProp.DeleteArrayElementAtIndex(colorIndex);
                            serializedObject.ApplyModifiedProperties();
                            RefreshPalettes(root, paletteIndexProp, palettesProp);
                        });
                        removeColorBtn.text = "x";
                        removeColorBtn.style.width = COLOR_SIZE * 0.5f;
                        removeColorBtn.style.height = COLOR_SIZE * 0.5f;
                        removeColorBtn.style.fontSize = 8;
                        removeColorBtn.style.SetMargins(0);
                        removeColorBtn.style.SetPadding(0);
                        removeColorBtn.style.backgroundColor =  closeColor;
                        actionColumn.Add(removeColorBtn);
                        
                        // Resize the eyedropper
                        colorField.schedule.Execute(() =>
                        {
                            var eyedropper = colorField.Q(name: "unity-eyedropper");
                            if (eyedropper != null)
                            {
                                eyedropper.style.width = COLOR_SIZE * 0.5f;
                                eyedropper.style.height = COLOR_SIZE * 0.5f;
                                eyedropper.style.SetMargins(0);
                                eyedropper.style.SetPadding(0);
                                eyedropper.style.flexGrow = 0;
                                eyedropper.style.flexShrink = 0;
                                actionColumn.Add(eyedropper);
                            }
                        });
                    }
                }
                else
                {
                    // No colors, add some text
                    var fallbackTxt = new TextElement();
                    fallbackTxt.text = "Click '+ Color' to add a swatch.";
                    fallbackTxt.style.fontSize = 10;
                    fallbackTxt.style.color = Color.gray;
                    fallbackTxt.style.marginTop = 2;
                    editableGrid.Add(fallbackTxt);
                }
            }
        }
    }
}

#endif