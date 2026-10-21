using Soso.UI.Core.Behaviors;
using System.Reflection;
using UnityEditor;
using UnityEditor.UI;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Soso.UI.Core.Editor
{
	[CustomEditor(typeof(SosoButton)), CanEditMultipleObjects]
	public class SosoButtonEditor : ButtonEditor
	{
		public override VisualElement CreateInspectorGUI()
		{
			VisualElement root = new VisualElement();
			// Add button props
			root.Add(new PropertyField(serializedObject.FindProperty("m_Interactable")));
			root.Add(new PropertyField(serializedObject.FindProperty("m_TargetGraphic")));
			root.Add(new PropertyField(serializedObject.FindProperty("m_Navigation")));

			// Visualizer
			var visualizeContainer = new IMGUIContainer(DrawNativeVisualizeButton);
			visualizeContainer.style.marginTop = 4;
			visualizeContainer.style.marginBottom = 12;
			root.Add(visualizeContainer);
        
			// Add the click event
			root.Add(new PropertyField(serializedObject.FindProperty("m_OnClick")));
        
			// State changed
			root.Add(new PropertyField(serializedObject.FindProperty(nameof(SosoButton.OnStateChanged))));
        
			return root;
		}
		
		private void DrawNativeVisualizeButton()
		{
			var selectableEditorType = typeof(SelectableEditor);
			var flags = BindingFlags.Static | BindingFlags.NonPublic;
			var showNavField = selectableEditorType.GetField("s_ShowNavigation", flags);

			if (showNavField != null)
			{
				bool showNav = (bool)showNavField.GetValue(null);
				Rect toggleRect = EditorGUILayout.GetControlRect();
				toggleRect.xMin += EditorGUIUtility.labelWidth;

				EditorGUI.BeginChangeCheck();
            
				showNav = GUI.Toggle(toggleRect, showNav, "Visualize", EditorStyles.miniButton);

				if (EditorGUI.EndChangeCheck())
				{
					showNavField.SetValue(null, showNav);
					EditorPrefs.SetBool("SelectableEditor.ShowNavigation", showNav);
					SceneView.RepaintAll();
				}
			}
		}
	}
}
