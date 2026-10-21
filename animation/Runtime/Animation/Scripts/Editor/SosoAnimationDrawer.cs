#if UNITY_EDITOR

using Soso.UI.Animation.Types;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Soso.UI.Animation.Editor
{
    [CustomPropertyDrawer(typeof(SosoRectTransformAnimation))]
    public class SosoAnimationDrawer : PropertyDrawer
    {
        private static SosoRectTransformAnimation _copiedData = null;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new Foldout();
            var animation = (SosoRectTransformAnimation)property.boxedValue;
            
            // Title
            container.text = $"{StringUtils.InsertSpacesAroundCaps(animation.OpType)} " +
                             $"{StringUtils.InsertSpacesAroundCaps(animation.Event).ToLower()} " +
                             $"to {animation.Value} for {animation.Duration}s";

            // Find the serialized properties
            var eventProp = property.FindPropertyRelative(nameof(SosoRectTransformAnimation.Event));
            var opTypeProp = property.FindPropertyRelative(nameof(SosoRectTransformAnimation.OpType));
            var animTypeProp = property.FindPropertyRelative(nameof(SosoRectTransformAnimation.Anim));
            var valueProp = property.FindPropertyRelative(nameof(SosoRectTransformAnimation.Value));
            var durationProp = property.FindPropertyRelative(nameof(SosoRectTransformAnimation.Duration));
            var hasStartingValueProp = property.FindPropertyRelative(nameof(SosoRectTransformAnimation.HasStartingValue));
            var startingValueProp = property.FindPropertyRelative(nameof(SosoRectTransformAnimation.StartingValue));

            // Create the UI fields
            var eventField = new PropertyField(eventProp);
            var opTypeField = new PropertyField(opTypeProp);
            var animTypeField = new PropertyField(animTypeProp);
            var vectorValueField = new PropertyField(valueProp);
            var durationField = new PropertyField(durationProp);
            var hasStartingValueField = new PropertyField(hasStartingValueProp);
            var startingValueField = new PropertyField(startingValueProp);

            // Callbacks
            opTypeField.RegisterValueChangeCallback(_ => 
            {
                UpdateLabel((RECT_OPERATION_TYPE)opTypeProp.intValue, property, vectorValueField, true);
            });
            UpdateLabel((RECT_OPERATION_TYPE)opTypeProp.intValue, property, vectorValueField, true);
            hasStartingValueField.RegisterValueChangeCallback(_ =>
            {
                UpdateLabel((RECT_OPERATION_TYPE)opTypeProp.intValue, property, startingValueField, hasStartingValueProp.boolValue);
            });
            UpdateLabel((RECT_OPERATION_TYPE)opTypeProp.intValue, property, startingValueField, hasStartingValueProp.boolValue);

            // Add everything to the container
            container.Add(eventField);
            container.Add(opTypeField);
            container.Add(animTypeField);
            container.Add(vectorValueField);
            container.Add(durationField);
            container.Add(hasStartingValueField);
            container.Add(startingValueField);

            ApplyIndexBackgroundColor(container, property);
            container.TrackPropertyValue(property, prop =>
            {
                ApplyIndexBackgroundColor(container, prop);
            });

            CreateContextualMenu(container, property);
            
            return container;
        }

        private void CreateContextualMenu(VisualElement container, SerializedProperty current)
        {
            var menuProp = current.Copy();
            
            container.AddManipulator(new ContextualMenuManipulator(evt =>
            {
                // Copy
                evt.menu.AppendAction("Copy", action =>
                {
                    _copiedData = (SosoRectTransformAnimation)menuProp.boxedValue;
                });
                
                // Paste
                var pastStatus = _copiedData != null ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled;
                evt.menu.AppendAction("Paste", action =>
                {
                    menuProp.FindPropertyRelative(nameof(SosoRectTransformAnimation.Event)).intValue = (int)_copiedData.Event;
                    menuProp.FindPropertyRelative(nameof(SosoRectTransformAnimation.OpType)).intValue = (int)_copiedData.OpType;
                    menuProp.FindPropertyRelative(nameof(SosoRectTransformAnimation.Anim)).intValue = (int)_copiedData.Anim;
                    menuProp.FindPropertyRelative(nameof(SosoRectTransformAnimation.Value)).vector3Value = _copiedData.Value;
                    menuProp.FindPropertyRelative(nameof(SosoRectTransformAnimation.Duration)).floatValue = _copiedData.Duration;
                    menuProp.FindPropertyRelative(nameof(SosoRectTransformAnimation.HasStartingValue)).boolValue = _copiedData.HasStartingValue;
                    menuProp.FindPropertyRelative(nameof(SosoRectTransformAnimation.StartingValue)).vector3Value = _copiedData.StartingValue;
                    
                    menuProp.serializedObject.ApplyModifiedProperties();
                    menuProp.serializedObject.Update();
                }, pastStatus);
            }));
        }

        private void UpdateLabel(RECT_OPERATION_TYPE type, SerializedProperty property, PropertyField vectorValueField, bool display)
        {
            vectorValueField.style.display = DisplayStyle.None;
            if (display == false)
            {
                return;
            }
            if (property.hasMultipleDifferentValues)
            {
                vectorValueField.style.display = DisplayStyle.Flex;
                vectorValueField.label = "Value (Mixed)";
                return;
            }
                
            switch (type)
            {
                case RECT_OPERATION_TYPE.AnchorPosition:
                    vectorValueField.style.display = DisplayStyle.Flex;
                    vectorValueField.label = "Target Position";
                    break;
                case RECT_OPERATION_TYPE.Scale:
                    vectorValueField.style.display = DisplayStyle.Flex;
                    vectorValueField.label = "Target Scale";
                    break;
                case RECT_OPERATION_TYPE.Rotate:
                    vectorValueField.style.display = DisplayStyle.Flex;
                    vectorValueField.label = "Euler Angles";
                    break;
            }
        }

        private void ApplyIndexBackgroundColor(VisualElement container, SerializedProperty property)
        {
            string path = property.propertyPath;
    
            int startIndex = path.LastIndexOf('[') + 1;
            int endIndex = path.LastIndexOf(']');
    
            if (startIndex > 0 && endIndex > startIndex)
            {
                string indexString = path.Substring(startIndex, endIndex - startIndex);
                if (int.TryParse(indexString, out int index))
                {
                    if (index % 2 == 0)
                    {
                        container.style.backgroundColor = new StyleColor(new Color(0.2f, 0.2f, 0.2f, 1f)); // Dark gray
                    }
                    else
                    {
                        container.style.backgroundColor = new StyleColor(new Color(0.15f, 0.15f, 0.15f, 1f)); // Slightly darker
                    }
                }
            }
        }
    }
}

#endif